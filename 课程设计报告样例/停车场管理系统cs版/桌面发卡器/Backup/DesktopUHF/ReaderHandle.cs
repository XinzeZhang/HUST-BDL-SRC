using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using Hit.RFID;
using System.IO.Ports;
using Hit.Common;
using ReaderB;

namespace Hit.RFID
{
    public class ReaderHandle
    {
        // 端口号
        int Port = -1;

        // 读写器地址
        byte ComAdr = 0xff;


        /*  波特率对应值
            0	9600bps
            1	19200 bps
            2	38400 bps
            5	57600 bps
            6	115200 bps
        */
        byte Baud = 5;

        //波特率 57600 bps
        int lBaud = 57600;

        // 句柄
        int FrmHandle = -1;

        bool isConnect = false;


        private int EraseMaxLen = 50;
        private int WriteMaxLen = 50;
        private int WriteTryMaxCount = 20;
        
        /// <summary>
        /// 打开串口
        /// </summary>
        /// <returns></returns>
        public bool Connect()
        {
            
            if (Port > 0)
            {
                // 关闭串口
                StaticClassReaderB.CloseSpecComPort(FrmHandle);
            }

            CommonUtils.ActiveAllCom();

            // 打开串口
            int iRet = StaticClassReaderB.AutoOpenComPort(ref Port, ref ComAdr, Baud, ref FrmHandle);

            // 串口打开成功
            if (iRet == 0)
            {
                return true;
            }
            else
            {
                // 串口打开失败
                return false;
            }
        }

        /// <summary>
        /// 关闭串口
        /// </summary>
        /// <returns></returns>
        public bool DisConnect()
        {
            // 关闭串口
            int iRet = StaticClassReaderB.CloseSpecComPort(Port);
            //
            if (iRet == 0)
            {
                Port = -1;
                FrmHandle = -1;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 设置桌面发卡器功率
        /// </summary>
        /// <param name="powerDb">功率对应db值，范围1-18，桌面发卡器一般采用默认值</param>
        /// <returns></returns>
        public bool SetPowerDbm(int powerDb)
        {
            int fCmdRet = -1;
            byte powerDbm = (byte)powerDb;

            // 设置桌面发卡器功率
            fCmdRet = StaticClassReaderB.SetPowerDbm(ref ComAdr, powerDbm, FrmHandle);

            if (fCmdRet == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询EPC值
        /// </summary>
        /// <returns>返回标签EPC值</returns>
        public string ReadEpc()
        {
            return Inventory_G2((byte)0, (byte)0, (byte)0);
        }

        /// <summary>
        /// 查询EPC值
        /// </summary>
        /// <returns>返回标签EPC值</returns>
        public string ReadTID()
        {
            return Inventory_G2((byte)1, (byte)0, (byte)8);


        }

        /// <summary>
        /// 查询标签EPC或TID信息
        /// </summary>
        /// <param name="TIDFlag">TID地址</param>
        /// <param name="AdrTID">以字为单位的TID长度</param>
        /// <param name="LenTID">标志位，0表示查询EPC,1表示查询TID</param>
        /// <returns></returns>
        public string Inventory_G2(byte TIDFlag, byte AdrTID, byte LenTID)
        {
            if (Port == 0)
            {
                return "";
            }
            // 一字节的EPC的长度+ EPC的字节数组表示 
            byte[] EPC = new byte[5000];

            // 返回的EPC的实际字节数
            int Totallen = 0;

            // 查询到的标签数量
            int CardNum = 0;
            int iCount = 0;
            // TID地址
            //byte AdrTID = 0;
            // 以字为单位的TID长度
            //byte LenTID = 8;
            // 标志位，0表示查询EPC,1表示查询TID
            //byte TIDFlag = 1;

            string hexEPC = "";

            while (iCount < 100)
            {
                int fCmdRet = StaticClassReaderB.Inventory_G2(ref ComAdr, AdrTID, LenTID, TIDFlag, EPC, ref Totallen, ref CardNum, FrmHandle);
                if ((fCmdRet == 1) | (fCmdRet == 2) | (fCmdRet == 3) | (fCmdRet == 4) | (fCmdRet == 0xFB))//代表已查找结束
                {
                    byte[] daw = new byte[Totallen];
                    Array.Copy(EPC, daw, Totallen);
                    string temps = ByteArrayToHexString(daw);
                    //
                    if (CardNum > 0)
                    {
                        int EPClen = daw[0];
                        hexEPC = temps.Substring(2, EPClen * 2);

                        break;
                    }

                }
                iCount++;
            }
            return hexEPC;
        }

        /// <summary>
        /// 将16进制EPC字符串写入EPC区
        /// </summary>
        /// <param name="HexEpc">写入EPC的字符串，长度必须为4的倍数，且其最大长度受标签芯片EPC区域存储空间。</param>
        /// <returns></returns>
        public bool WriteEpc(String HexEpc)
        {
            // 返回错误代码
            int ferrorcode = -1;
            
            int fCmdRet = 0;

            // 标签密码00000000
            byte[] Password = HexStringToByteArray("00000000");

            // 准备写入的EPC值的字节数组表示
            byte[] writeEpc = HexStringToByteArray(HexEpc);

            // EPC的字节数，必须为偶数
            byte WriteEPClen = (byte)writeEpc.Length;

            fCmdRet = StaticClassReaderB.WriteEPC_G2(ref ComAdr, Password, writeEpc, WriteEPClen, ref ferrorcode, FrmHandle);

            if (fCmdRet == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }


        /// <summary>
        /// 写用户区
        /// </summary>
        /// <param name="hexEPC">十六进制EPC</param>
        /// <param name="offset">十进制偏移字节数</param>
        /// <param name="writeDataStr">需写入字符串,字节数应为偶数</param>
        /// <returns></returns>
        public bool WriteUserData(string hexEPC, int offset, string writeDataStr)
        {

            byte[] Writedata = new byte[writeDataStr.Length];
            Writedata = StringToByteArray(writeDataStr);

            byte bwriteLen = (Byte)writeDataStr.Length;

            return WriteUserDataByHexStr(hexEPC, offset, Writedata);
        }


        /// <summary>
        /// 写用户区
        /// </summary>
        /// <param name="hexEPC">十六进制EPC</param>
        /// <param name="offset">十进制偏移字节数</param>
        /// <param name="writeDataStr">需写入字节数组，长度为偶数</param>
        /// <returns></returns>
        public bool WriteUserDataByHexStr(string hexEPC, int offset, byte[] writeData)
        {

            if (Port == 0)
            {
                return false;
            }

            if (offset < 0 || writeData.Length <= 0)
            {
                return false;
            }
            offset = offset / 2;
            //
            byte maskFlag = 0;
            byte maskadr = 0;
            byte maskLen = 0;
            byte[] EPC = new byte[hexEPC.Length / 2];
            EPC = HexStringToByteArray(hexEPC);
            byte Mem = 0x03;//用户区
            //
            byte[] fPassWord = HexStringToByteArray("00000000");

            Int32 WrittenDataNum = 0;

            byte EPClength = (Byte)EPC.Length;
            Int32 ferrorcode = -1;

            int fCmdRet = 0;

            byte WordPtr = Convert.ToByte(offset.ToString(), 10);

            byte bwriteLen = (Byte)writeData.Length;

            for (int tryCnt = 0; tryCnt < WriteTryMaxCount; tryCnt++)
            {
                fCmdRet = StaticClassReaderB.WriteCard_G2(ref ComAdr, EPC, Mem, WordPtr, bwriteLen, writeData, fPassWord, maskadr, maskLen, maskFlag, WrittenDataNum, EPClength, ref ferrorcode, FrmHandle);

                if (fCmdRet == 0)
                {
                    break;
                }
            }

            //
            if (fCmdRet == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 读用户区数据，返回ascii字符串
        /// </summary>
        /// <param name="hexEPC">十六进制epc字符串</param>
        /// <param name="offset">十进制偏移字数</param>
        /// <param name="readNum">十进制读取字数</param>
        /// <returns></returns>
        public string ReadUserData(string hexEPC, string offset, string readNum)
        {
            string userData = "";

            if (Port == 0)
            {
                return "";
            }

            //if (Port == 0 || hexEPC.Length <= 0)
            //{
            //    return "";
            //}

            if (offset.Length <= 0 || readNum.Length <= 0)
            {
                return "";
            }
            //
            byte maskFlag = 0;
            byte maskadr = 0;
            byte maskLen = 0;
            byte[] EPC = new byte[hexEPC.Length / 2];
            EPC = HexStringToByteArray(hexEPC);
            byte Mem = 0x03;//用户区
            byte WordPtr = Convert.ToByte(offset.Trim(), 10);
            byte Num = Convert.ToByte(readNum);
            byte[] fPassWord = HexStringToByteArray("00000000");
            byte[] Data = new byte[1320];
            byte EPClength = (Byte)EPC.Length;
            Int32 ferrorcode = -1;
            //
            Int32 fCmdRet = StaticClassReaderB.ReadCard_G2(ref ComAdr, EPC, Mem, WordPtr, Num, fPassWord, maskadr, maskLen, maskFlag, Data, EPClength, ref ferrorcode, FrmHandle);

            if (fCmdRet == 0)
            {
                byte[] daw = new byte[Num * 2];
                Array.Copy(Data, daw, Num * 2);
                userData = ByteArrayToString(daw);
            }
            return userData;
        }

        /// <summary>
        /// 读用户区数据，返回ascii字符串
        /// </summary>
        /// <param name="hexEPC">十六进制epc字符串</param>
        /// <param name="offset">十进制偏移字数</param>
        /// <param name="readNum">十进制读取字数</param>
        /// <returns></returns>
        public byte[] ReadUserByByte(string hexEPC, string offset, string readNum)
        {
            string userData = "";

            if (Port == 0)
            {
                return null;
            }

            if (offset.Length <= 0 || readNum.Length <= 0)
            {
                return null;
            }
            //
            byte maskFlag = 0;
            byte maskadr = 0;
            byte maskLen = 0;
            byte[] EPC = new byte[hexEPC.Length / 2];
            EPC = HexStringToByteArray(hexEPC);
            byte Mem = 0x03;//用户区

            byte[] fPassWord = HexStringToByteArray("00000000");

            byte EPClength = (Byte)EPC.Length;
            Int32 ferrorcode = -1;
            //

            int offsetWord = Convert.ToInt32(offset.Trim(), 10);
            int readWordLen = Convert.ToInt32(readNum, 10);

            int maxReadWordLen = 50;
            byte[] daw = new byte[readWordLen * 2];

            if (readWordLen <= maxReadWordLen)
            {
                byte[] Data = new byte[1320];

                byte WordPtr = Convert.ToByte(offsetWord);

                byte Num = Convert.ToByte(readWordLen);


                Int32 fCmdRet = StaticClassReaderB.ReadCard_G2(ref ComAdr, EPC, Mem, WordPtr, Num, fPassWord, maskadr, maskLen, maskFlag, Data, EPClength, ref ferrorcode, FrmHandle);

                if (fCmdRet == 0)
                {
                    Array.Copy(Data, daw, Num * 2);
                    userData = ByteArrayToString(daw);
                    return Data;
                }
            }
            else
            {
                int count = 0;
                if (readWordLen % maxReadWordLen == 0)
                {
                    count = readWordLen / maxReadWordLen;
                }
                else
                {
                    count = (readWordLen / maxReadWordLen) + 1;
                }

                for (int i = 0; i < count; i++)
                {
                    byte[] Data = new byte[1320];

                    byte WordPtr = Convert.ToByte(offsetWord + maxReadWordLen * i);

                    byte Num = 0;
                    if (readWordLen - maxReadWordLen * (i + 1) >= 0)
                    {
                        Num = Convert.ToByte(maxReadWordLen);
                    }
                    else
                    {
                        Num = Convert.ToByte(readWordLen - maxReadWordLen * i);
                    }

                    Int32 fCmdRet = StaticClassReaderB.ReadCard_G2(ref ComAdr, EPC, Mem, WordPtr, Num, fPassWord, maskadr, maskLen, maskFlag, Data, EPClength, ref ferrorcode, FrmHandle);

                    if (fCmdRet == 0)
                    {
                        Array.Copy(Data, 0, daw, maxReadWordLen * 2 * i, Num * 2);
                    }
                }

            }
            return daw;
        }


        /// <summary>
        /// 十六进制字符串转为二进制数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary>
        /// 二进制数组转为十六进制字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            return sb.ToString().ToUpper();

        }

        public static string ByteArrayToString(byte[] data)
        {
            //UnicodeEncoding converter = new UnicodeEncoding();

            //return converter.GetString(data);
            UnicodeEncoding encoding = new UnicodeEncoding();
            return Encoding.Default.GetString(data);
        }

        private byte[] StringToByteArray(String str)
        {
            //UnicodeEncoding converter = new UnicodeEncoding();
            //return converter.GetBytes(str);
            return Encoding.Default.GetBytes(str);
        }

        private byte[] GetWordPtr(string offset)
        {
            //UnicodeEncoding converter = new UnicodeEncoding();
            //return converter.GetBytes(str);
            int value = 0;
            try
            {
                value = Convert.ToInt32(offset);
            }
            catch (Exception ex)
            {

            }

            byte[] wordPtr = new byte[2];

            wordPtr[0] = (byte)(value >> 8);
            wordPtr[1] = (byte)(value & 255);

            return wordPtr;
        }


        /// <summary>
        /// 数字转换成acsii
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public string NumToChar(string data)
        {
            if (data == null || "".Equals(data.Trim()))
            {
                return " ";
            }

            byte[] array = new byte[1];
            array[0] = (byte)(Convert.ToInt32(data)); //ASCII码强制转换二进制
            //return Convert.ToString(Encoding.ASCII.GetString(array));
            return Convert.ToString(Encoding.ASCII.GetString(array, 0, array.Length));

        }


        /// <summary>
        /// 字符转ASCII
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        public static int ChrToAsc(string character)
        {
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }

        /// <summary>
        /// ASCII转字符
        /// </summary>
        /// <param name="asciiCode"></param>
        /// <returns></returns>
        public static string AscToChr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                byte[] byteArray = new byte[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray, 0, byteArray.Length);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }



        /// <summary>
        /// 字符转为十六进制字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StrToHex(string str)
        {
            StringBuilder sb = new StringBuilder();
            char[] values = str.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X}", value);
                sb.Append(hexOutput);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 十六进制字符串转换为字符串
        /// </summary>
        /// <param name="hexValues"></param>
        /// <returns></returns>
        public static string HexToStr(string hexValues)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hexValues.Length; )
            {
                if (i + 2 > hexValues.Length)
                {
                    break;
                }
                string subHex = hexValues.Substring(i, 2);
                if ("00".Equals(subHex))
                {
                    sb.Append(" ");
                }
                else
                {
                    int value = Convert.ToInt32(subHex, 16);
                    char charValue = (char)value;
                    sb.Append(charValue);
                }

                i = i + 2;
            }

            return sb.ToString();
        }


    }
}
