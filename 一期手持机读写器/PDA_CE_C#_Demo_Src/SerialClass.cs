using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO.Ports;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Diagnostics;
using System.Threading;

namespace PDADemo_CF2._0
{
    public enum NUMKIND
    {
        TWO,
        TEN,
        SIXTN,
    }

    public enum READWAY
    {
        BYTE,
        CHAR,
    }

    public enum INCLUDENTLENGTH
    {
        INCLUDE,
        NOTINCLUDE,
    }
    public class SerialClass
    {
        public SerialClass()
        {
            BaudRate = 115200;
            string[] tempcoms = Find_WIN32_Com_2();
            if (tempcoms != null && tempcoms.Length > 0)
            {
                ComName = tempcoms[0];
            }
            WriteTimeout = 200;
            ReadTimeout = 1000;
            SBuffer = 255;
            ForeReadbyte = 1;
        }
        public SerialClass(string comnamep, int baudratep)
        {


            WriteTimeout = 200;
            ReadTimeout = 1000;
            SBuffer = 255;
            ForeReadbyte = 1;
            BaudRate = baudratep;
            ComName = comnamep;
        }
        public SerialClass(int comnump, int baudratep)
        {
            BaudRate = baudratep;
            ComName = "COM" + comnump;
            WriteTimeout = 200;
            ReadTimeout = 200;
            SBuffer = 255;
            ForeReadbyte = 1;
            // this(ComName, BaudRate);
        }
        private SerialPort serialp;
        private string comname;
        private int baudrate;
        private int defaultbaudrate;
        private int writetimeout;
        private int readtimeout;
        private bool isopen;
        private int buffer;//serialp 默认缓冲区4096个字节
        private int foreread;//预读多个


        public int ForeReadbyte
        {
            get { return foreread; }
            set
            {
                if (value <= 255 || value > 0)
                {
                    foreread = value;
                }
                else
                    foreread = 1;

            }
        }
        public bool IsOpen
        {
            get { return isopen; }
        }
        public int SBuffer
        {
            get { return buffer; }
            set
            {
                if (value <= 4096 || value > 0)
                {
                    buffer = value;
                }
                else
                    buffer = 255;
            }
        }
        public string ComName
        {
            get { return comname; }
            set { comname = value; }
        }
        public int BaudRate
        {
            get { return baudrate; }
            set { baudrate = value; }
        }
        public int WriteTimeout
        {
            get { return writetimeout; }
            set { writetimeout = value; }
        }
        public int ReadTimeout
        {
            get { return readtimeout; }
            set { readtimeout = value; }
        }
        public void OpenCom()
        {
            try
            {
                serialp = new SerialPort(ComName, BaudRate, Parity.None, 8, StopBits.One);
                serialp.Open();
                isopen = true;
            }
            catch (System.Exception ex)
            {
                isopen = false;
                throw ex;
            }
        }
        public void SendData(string data)
        {
            if (!isopen)
                return;
            char[] chardata = data.ToCharArray();
            byte[] bytedata = Encoding.Default.GetBytes(data);
            SendData(bytedata);
        }
        public string AscIItoHex(string ascstr)
        {
            StringBuilder sb = new StringBuilder();
            byte[] cmdtemp = Encoding.Default.GetBytes(ascstr);
            foreach (byte cmdb in cmdtemp)
            {
                sb.Append(cmdb.ToString("X2"));
            }
            return sb.ToString();
        }
        public byte[] GetStringShowWithHex(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] cmdtemp = Encoding.Default.GetBytes(str);
            foreach (byte cmdb in cmdtemp)
            {
                sb.Append(cmdb.ToString("X2"));
            }
            string cmdstr = sb.ToString();
            byte[] cmdbytes = FromHex(cmdstr);
            return cmdbytes;
        }
        public void DebugOut(string head, byte[] data)
        {
            Debug.WriteLine(head + ":");
            for (int i = 0; i < data.Length; i++)
            {
                Debug.Write(data[i].ToString("X2") + " ");
            }
            Debug.WriteLine("");
        }
        public void SendData(byte[] data)
        {
            //  DebugOut("SED",data);
            if (!isopen)
                return;
            try
            {
                serialp.Write(data, 0, data.Length);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public void SendData(char[] data)
        {
            if (!isopen)
                return;

            try
            {
                serialp.Write(data, 0, data.Length);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public string ReadDataString(NUMKIND numkind, READWAY readway, INCLUDENTLENGTH isinclude, int startbyte, int endbyte, int addlenth, int length)
        {
            string recstring = string.Empty;
            if (!isopen)
                throw new Exception("is not open");
            try
            {

                byte[] forback;
                if (readway == READWAY.BYTE)
                {
                    if (isinclude == INCLUDENTLENGTH.INCLUDE)
                    {
                        forback = ReadDataByte(startbyte, endbyte, addlenth);
                    }
                    forback = ReadDataByte(length);
                }
                else
                    return null;

                foreach (byte onebyte in forback)
                {
                    if (numkind == NUMKIND.SIXTN)
                    {
                        recstring += onebyte.ToString("X2");
                    }
                    else
                        recstring += Convert.ToString(onebyte);
                }
                return recstring;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public byte[] FromHex(string hex)
        {
            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Byte.Parse(hex.Substring(i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            }
            return bytes;
        }

        public string ToHex(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(2 * bytes.Length);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

       
        public byte[] ReadDataAvaiblie()
        {
            byte[] recieve = new byte[255];
             int ret=1;
            int count = 0;
            int offset = 0;
            serialp.ReadTimeout = ReadTimeout;
            if (!isopen)
                throw new Exception("is not open");
            try
            {
                int nowtime = Environment.TickCount;
                int overtime = nowtime + 3000;
                while (ret > 0 && overtime - nowtime > 0)
                {

                    if (serialp.BytesToRead > 0)
                    {
                        ret = serialp.BytesToRead;
                    }
                    count = serialp.Read(recieve, offset, ret);
                    offset += count;
                    ret -= count;
                    Thread.Sleep(100);
                    nowtime = Environment.TickCount;
                }

            }
            catch (System.Exception ex)
            {

                throw ex;
            }

            if (offset == 0)
                return new byte[0];
            else
            {
                byte[] redata = new byte[offset];
                Array.Copy(recieve, 0, redata, 0, offset);
                return redata;
            }
        }

        public byte[] ReadDataByte(int length)//指定读多少字节
        {
            // byte[] recieve = new byte[SBuffer];
            byte[] recieve = new byte[length];
            int ret = length;
            //if (length < 1)
            //{
            //    ret = ForeReadbyte;
            //}
            //else
            //    ret = length;


            int count = 0;
            int offset = 0;
            serialp.ReadTimeout = ReadTimeout;
            try
            {

                if (!isopen)
                    throw new Exception("is not open");

                while (ret > 0)
                {
                    count = serialp.Read(recieve, offset, ret);
                    offset += count;
                    ret -= count;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return recieve;
        }
        public static string[] Find_WIN32_Com_2()
        {
            ArrayList comsmap = new ArrayList();
            RegistryKey keyCommap = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");
            if (keyCommap != null)
            {
                string[] sSubKeys = keyCommap.GetValueNames();
                for (int i = 0; i < sSubKeys.Length; i++)
                {
                    comsmap.Add((string)keyCommap.GetValue(sSubKeys[i]));
                }

            }
            if (comsmap.Count == 0)
            {
                return null;
            }

            string[] coms = null;
            ArrayList acoms = new ArrayList();
            RegistryKey keyFTDIBUS = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\FTDIBUS");
            if (keyFTDIBUS != null)
            {
                string[] FTDIBUS_SubKeys = keyFTDIBUS.GetSubKeyNames();


                for (int i = 0; i < FTDIBUS_SubKeys.Length; i++)
                {

                    RegistryKey keyVID = keyFTDIBUS.OpenSubKey(FTDIBUS_SubKeys[i]);
                    string[] Sub0000_Keys = keyVID.GetSubKeyNames();
                    for (int j = 0; j < Sub0000_Keys.Length; j++)
                    {
                        RegistryKey keyCom = keyVID.OpenSubKey(Sub0000_Keys[j]);

                        string[] keyComparms = keyCom.GetValueNames();
                        if (((string)keyCom.GetValue("FriendlyName")).IndexOf("Serial") != -1)
                        {
                            string[] sSubKeys = keyCom.GetSubKeyNames();
                            bool ishavecontrol = false;
                            if (sSubKeys.Length == 3)
                            {
                                ishavecontrol = true;
                            }

                            if (ishavecontrol)
                            {
                                RegistryKey keycomname = keyCom.OpenSubKey("Device Parameters");
                                string[] ParamKey = keycomname.GetValueNames();
                                int k; bool ishaveportname = false;
                                for (k = 0; k < ParamKey.Length; k++)
                                {
                                    if (ParamKey[k] == "PortName")
                                    {
                                        ishaveportname = true;
                                        break; ;
                                    }
                                }
                                if (ishaveportname)
                                {
                                    string portname = (string)keycomname.GetValue(ParamKey[k]);
                                    if (portname.IndexOf("COM") != -1)
                                        if (!acoms.Contains(portname) && comsmap.Contains(portname))
                                            acoms.Add(portname);
                                }
                            }
                        }

                    }

                }

            }
            RegistryKey keyUSB = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Enum\\USB");
            if (keyUSB != null)
            {
                string[] USB_SubKeys = keyUSB.GetSubKeyNames();
                for (int i = 0; i < USB_SubKeys.Length; i++)
                {
                    if (USB_SubKeys[i].IndexOf("Vid") != -1)
                    {
                        RegistryKey keyVid = keyUSB.OpenSubKey(USB_SubKeys[i]);
                        string[] Vid_SubKeys = keyVid.GetSubKeyNames();
                        for (int j = 0; j < Vid_SubKeys.Length; j++)
                        {
                            RegistryKey keyVid_sub = keyVid.OpenSubKey(Vid_SubKeys[j]);
                            string[] keyComparms = keyVid_sub.GetValueNames();
                            if (((string)keyVid_sub.GetValue("DeviceDesc")).IndexOf("Serial") != -1)
                            {
                                string[] sSubKeys = keyVid_sub.GetSubKeyNames();
                                bool ishavecontrol = false;
                                if (sSubKeys.Length == 3)
                                {
                                    ishavecontrol = true;
                                }
                                if (ishavecontrol)
                                {
                                    RegistryKey keycomname = keyVid_sub.OpenSubKey("Device Parameters");
                                    string[] ParamKey = keycomname.GetValueNames();
                                    int k; bool ishaveportname = false;
                                    for (k = 0; k < ParamKey.Length; k++)
                                    {
                                        if (ParamKey[k] == "PortName")
                                        {
                                            ishaveportname = true;
                                            break; ;
                                        }
                                    }
                                    if (ishaveportname)
                                    {
                                        string portname = (string)keycomname.GetValue(ParamKey[k]);
                                        if (portname.IndexOf("COM") != -1)
                                            if (!acoms.Contains(portname) && comsmap.Contains(portname))
                                                acoms.Add(portname);
                                    }
                                }
                            }
                        }

                    }
                }
            }

            coms = (string[])acoms.ToArray(typeof(string));
            return coms;
        }
        public byte[] ReadDataByte(int startbyte, int endbyte, int addlength)//字节信息流中某些字节本身包含长度
        {
            byte[] recieve = new byte[SBuffer], back;

            //byte[] recieve ={ 0xee, 0x01, 0xe0,0x99, 0x11, 0x11 },back;
            //return recieve;


            int ret = 0;

            byte[] lengthbytes = new byte[endbyte + 1 - startbyte];
            int count = 0;
            int offset = 0;
            serialp.ReadTimeout = ReadTimeout;
            try
            {
                if (lengthbytes.Length > 0)
                {
                    int lecount = 0;
                    int leoffset = 0;
                    int lelet = endbyte;

                    while (lelet > 0)
                    {
                        lecount = serialp.Read(recieve, leoffset, lelet);
                        leoffset += lecount;
                        lelet -= lecount;
                    }

                    for (int i = startbyte - 1; i < endbyte; i++)
                    {
                        lengthbytes[i - startbyte + 1] = recieve[i];
                    }
                    if (lengthbytes.Length == 1)
                    {
                        ret = Convert.ToInt32(lengthbytes[0]);
                    }
                    else if (lengthbytes.Length == 2)
                    {
                        ret = BitConverter.ToInt16(lengthbytes, 0);
                    }

                    ret += addlength;
                }
                else
                    return null;

                back = new byte[ret + endbyte];

                for (int i = 0; i < endbyte; i++)
                {
                    back[i] = recieve[i];
                }
                offset = endbyte;


                while (ret > 0)
                {
                    count = serialp.Read(back, offset, ret);
                    offset += count;
                    ret -= count;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return back;
        }
        public char[] ReadDataChars(int startbyte, int endbyte)//
        {
            char[] data = new char[SBuffer];
            return data;
        }
        public void Close()
        {
            if (isopen)
            {
                serialp.Close();
                isopen = false;
            }

        }

        public bool CheckDatarule(string data, int numk)
        {
            bool returnvalue = true;
            if (data == string.Empty || data == null)
            {
                return false;
            }
            if (numk == 2)
            {
                foreach (char single in data)
                {
                    if (single != '0' && single != '1')
                        return false;
                }
            }
            else if (numk == 10)
            {

                foreach (char single in data)
                {
                    switch (single)
                    {
                        case '0':
                        case '1':
                        case '2':
                        case '3':
                        case '4':
                        case '5':
                        case '6':
                        case '7':
                        case '8':
                        case '9': break;
                        default: { returnvalue = false; break; }

                    }
                    if (!returnvalue)
                    {
                        break;
                    }
                }
            }
            else
            {
                foreach (char single in data)
                {
                    switch (single.ToString().ToUpper())
                    {
                        case "0":
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                        case "A":
                        case "B":
                        case "C":
                        case "D":
                        case "E":
                        case "F": break;
                        default: { returnvalue = false; break; }


                    }
                    if (!returnvalue)
                    {
                        break;
                    }
                }
            }
            return returnvalue;
        }

    }
}
