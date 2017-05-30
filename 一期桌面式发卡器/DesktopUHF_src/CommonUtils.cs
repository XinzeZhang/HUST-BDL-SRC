using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

namespace Hit.Common
{
    class CommonUtils
    {

        public static void ActiveAllCom()
        {
            try
            {
                string[] portlist = SerialPort.GetPortNames();
                foreach (string portName in portlist)
                {
                    SerialPort sp = new SerialPort(portName, 57600, Parity.None, 8, StopBits.One);

                    if (!sp.IsOpen)
                    {
                        sp.Open();
                        sp.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public static void ActiveSpecCom(string portName)
        {
            try
            {
                SerialPort sp = new SerialPort(portName, 57600, Parity.None, 8, StopBits.One);

                if (!sp.IsOpen)
                {
                    sp.Open();
                    sp.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static void ActiveAllCom(int lBaud)
        {
            try
            {
                string[] portlist = SerialPort.GetPortNames();
                foreach (string portName in portlist)
                {
                    SerialPort sp = new SerialPort(portName, lBaud, Parity.None, 8, StopBits.One);

                    if (!sp.IsOpen)
                    {
                        sp.Open();
                        sp.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public static void ActiveSpecCom(string portName, int lBaud)
        {
            try
            {
                SerialPort sp = new SerialPort(portName, lBaud, Parity.None, 8, StopBits.One);

                if (!sp.IsOpen)
                {
                    sp.Open();
                    sp.Close();
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 十六进制字符串转为二进制数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string s)
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

        public static byte[] StringToByteArray(String str)
        {
            //UnicodeEncoding converter = new UnicodeEncoding();
            //return converter.GetBytes(str);
            return Encoding.Default.GetBytes(str);
        }

        /// <summary>
        /// 合并数组
        /// </summary>
        /// <param name="First">第一个数组</param>
        /// <param name="Second">第二个数组</param>
        /// <returns>合并后的数组(第一个数组+第二个数组，长度为两个数组的长度)</returns>
        public string[] MergerArray(string[] First, string[] Second)
        {
            string[] result = new string[First.Length + Second.Length];
            First.CopyTo(result, 0);
            Second.CopyTo(result, First.Length);
            return result;
        }

        /// <summary>
        /// 数组追加
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="str">字符串</param>
        /// <returns>合并后的数组(数组+字符串)</returns>
        public string[] MergerArray(string[] Source, string str)
        {
            string[] result = new string[Source.Length + 1];
            Source.CopyTo(result, 0);
            result[Source.Length] = str;
            return result;
        }

        /// <summary>
        /// 从数组中截取一部分成新的数组
        /// </summary>
        /// <param name="Source">原数组</param>
        /// <param name="StartIndex">原数组的起始位置</param>
        /// <param name="EndIndex">原数组的截止位置</param>
        /// <returns></returns>
        public string[] SplitArray(string[] Source, int StartIndex, int EndIndex)
        {
            try
            {
                string[] result = new string[EndIndex - StartIndex + 1];
                for (int i = 0; i <= EndIndex - StartIndex; i++) result[i] = Source[i + StartIndex];
                return result;
            }
            catch (IndexOutOfRangeException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 不足长度的前面补空格,超出长度的前面部分去掉
        /// </summary>
        /// <param name="First">要处理的数组</param>
        /// <param name="byteLen">数组长度</param>
        /// <returns></returns>
        public string[] MergerArray(string[] First, int byteLen)
        {
            string[] result;
            if (First.Length > byteLen)
            {
                result = new string[byteLen];
                for (int i = 0; i < byteLen; i++) result[i] = First[i + First.Length - byteLen];
                return result;
            }
            else
            {
                result = new string[byteLen];
                for (int i = 0; i < byteLen; i++) result[i] = " ";
                First.CopyTo(result, byteLen - First.Length);
                return result;
            }
        }


        /// <summary>
        /// 数字转换成字母
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        public string NumToChar(string data)
        {
            if (data == null || "".Equals(data.Trim()))
            {
                return "";
            }

            byte[] array = new byte[1];
            array[0] = (byte)(Convert.ToInt32(data)); //ASCII码强制转换二进制
            return Convert.ToString(Encoding.ASCII.GetString(array));

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
        /// ASCII转字符
        /// </summary>
        /// <param name="asciiCode"></param>
        /// <returns></returns>
        public static string AscToChr(string asciiCodeStr)
        {
            int asciiCode = 0;
            try
            {
                asciiCode = Convert.ToInt32(asciiCodeStr);
            }
            catch (Exception ex)
            {
                asciiCode = 0;
            }
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


    }
}
