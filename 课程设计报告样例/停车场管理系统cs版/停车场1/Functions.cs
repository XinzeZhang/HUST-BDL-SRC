using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace CarManager
{
    class Functions
    {
        DataAccess DataAccess1 = new DataAccess();

        public int getHtime(string Time1)
        {
            int Time_hh = Convert.ToInt32(Time1.Substring(0, 2));
            int Time_mm = Convert.ToInt32(Time1.Substring(3, 2));

            if (Time_mm > 29) 
            {
                Time_hh = Time_hh + 1;
            }
            return Time_hh;
        }

        public void Orderit(ref int time1, ref int time2, ref int time3, ref double rate1, ref double rate2, ref double rate3)
        {
            int temp;
            double temp2;
            if (time2  < time3 && time2  < time1 )
            {
                temp = time1;
                time1 = time2;
                time2 = time3;
                time3 = temp;

                temp2 = rate1;
                rate1 = rate2;
                rate2 = rate3;
                rate3 = temp2;
            }
            if (time3  < time1  && time3  < time2 )
            {
                temp = time1;
                time1 = time3;
                time3 = time2;                
                time2 = temp;

                temp2 = rate1;
                rate1 = rate3;
                rate3 = rate2;
                rate2 = temp2;
            }
        }

        public double getCharge( string CarNo)
        {
            string Carcla,sintime,portno;
            int time1, time2, time3;
            double Rate1, Rate2, Rate3, money;
            DataAccess1.getCardetail(CarNo, out Carcla, out sintime,out portno);
            int intime = getHtime(sintime);
            int outtime = getHtime(getOuttime());

            DataAccess1.getRate(Carcla, out time1, out time2, out time3, out Rate1, out Rate2, out Rate3);
            Orderit(ref time1, ref time2, ref time3, ref Rate1, ref Rate2, ref Rate3);



            if (intime > outtime)
            {
                if (outtime < time1 + 1)
                {
                    if (intime < time1 + 1)
                    {
                        money = (time2 - time1) * Rate1 + (time3 - time2) * Rate2 + (time1 - intime + 23 - time3 + outtime) * Rate3;
                    }
                    else if (intime < time2 + 1 && intime > time1)
                    {
                        money = (time2 - intime) * Rate1 + (time3 - time2) * Rate2 + (23 - time3 + outtime) * Rate3;
                    }
                    else if (intime > time2 && intime < time3 + 1)
                    {
                        money = (time3 - intime) * Rate2 + (23 - time3 + outtime) * Rate3;
                    }
                    else
                    {
                        money = (23 - intime + outtime) * Rate3;
                    }
                }
                else if (outtime > time1 && outtime < time2 + 1)
                {

                    if (intime < time2 + 1)
                    {
                        money = (time2 - intime + outtime - time1) * Rate1 + (time3 - time2) * Rate2 + (23 - time3 + time1) * Rate3;
                    }
                    else if (intime < time3 + 1 && intime > time2)
                    {
                        money = (time3 - intime) * Rate2 + (23 - time3 + time1) * Rate3 + (outtime - time1) * Rate1;
                    }
                    else
                    {
                        money = (23 - intime + time1) * Rate3 + (outtime - time1) * Rate1;
                    }
                }
                else
                {
                    money = (time3 - time2) * Rate2 + (outtime - time3 + 23 - intime + time1) * Rate3 + (time2 - time1) * Rate1;
                }
            }
            else
            {
                if (intime < time1 + 1)
                {
                    if (outtime < time1 + 1)
                    {
                        money = (outtime - intime) * Rate3;
                    }
                    else if (outtime > time1 && outtime < time2 + 1)
                    {
                        money = (time1 - intime) * Rate3 + (outtime - time1) * Rate1;
                    }
                    else if (outtime > time2 && outtime < time3 + 1)
                    {
                        money = (time1 - intime) * Rate3 + (time2 - time1) * Rate1 + (outtime - time2) * Rate2;
                    }
                    else
                    {
                        money = (time1 - intime + outtime - time3) * Rate3 + (time2 - time1) * Rate1 + (time3 - time2) * Rate2;
                    }
                }
                else if (intime > time1 && intime < time2 + 1)
                {
                    if (outtime < time2)
                    {
                        money = (outtime - intime) * Rate1;
                    }
                    else if (outtime > time2 && outtime < time3 + 1)
                    {
                        money = (time2 - intime) * Rate1 + (outtime - time2) * Rate2;
                    }
                    else
                    {
                        money = (time2 - intime) * Rate1 + (time3 - time2) * Rate2 + (outtime - time3) * Rate3;
                    }
                }
                else if (intime > time2 && intime < time3 + 1)
                {
                    if (outtime < time3 + 1)
                    {
                        money = (outtime - intime) * Rate2;
                    }
                    else
                    {
                        money = (time3 - intime) * Rate2 + (outtime - time3) * Rate3;
                    }
                }
                else
                {
                    money = (outtime - intime) * Rate3;
                }
            }
            return money;
        }

        public string getOuttime()
        {
            string OutTime;
            string OutTime_hh, OutTime_mm;
            int hh = Convert.ToInt32(DateTime.Now.TimeOfDay.Hours.ToString());
            int mm = Convert.ToInt32(DateTime.Now.TimeOfDay.Minutes.ToString());

            if (hh < 10)
            {
                OutTime_hh = "0" + hh.ToString();
            }
            else
            {
                OutTime_hh = DateTime.Now.TimeOfDay.Hours.ToString();
            }

            if (mm < 10)
            {
                OutTime_mm = "0" + mm.ToString();
            }
            else
            {
                OutTime_mm = DateTime.Now.TimeOfDay.Minutes.ToString();
            }

            OutTime = OutTime_hh +":"+ OutTime_mm+":00";
            return OutTime;       
        }
        
        
        
        



//===========================================================not mine====================================================================


       

        public string RandNum(int n)
        {
            char[] arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < n; i++)
            {
                num.Append(arrChar[rnd.Next(0, 9)].ToString());

            }

            return num.ToString();
        }

        //public string RandEng(int n)
        //{
        //    char[] arrChar = new char[]{'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'};

        //    StringBuilder num = new StringBuilder();

        //    Random rnd = new Random(DateTime.Now.Millisecond);
        //    for (int i = 0; i < n; i++)
        //    {
        //        num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());

        //    }
        //    return num.ToString();
        //}

        public string Randhanzi()
        {

            Encoding gb = Encoding.GetEncoding("gb2312");

            //调用函数产生4个随机中文汉字编码
            object[] bytes = CreateRegionCode(1);

            //根据汉字编码的字节数组解码出中文汉字

            string str1 = gb.GetString((byte[])Convert.ChangeType(bytes[0], typeof(byte[])));

            return str1;

        }

        public static object[] CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素
            string[] rBase=new String [16]{"0","1","2","3","4","5","6","7","8","9","a","b","c","d","e","f"};
            
            Random rnd=new Random();
        
            //定义一个object数组用来
            object[] bytes=new object[strlength];

            /**//*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中
             每个汉字有四个区位码组成
             区位码第1位和区位码第2位作为字节数组第一个元素
             区位码第3位和区位码第4位作为字节数组第二个元素
            */
            for(int i=0;i<strlength;i++)
            {
                //区位码第1位
                int r1=rnd.Next(11,14);
                string str_r1=rBase[r1].Trim();

                //区位码第2位
                rnd=new Random(r1*unchecked((int)DateTime.Now.Ticks)+i);//更换随机数发生器的种子避免产生重复值
                int r2;
                if (r1==13)
                {
                    r2=rnd.Next(0,7);
                }
                else
                {
                    r2=rnd.Next(0,16);
                }
                string str_r2=rBase[r2].Trim();

                //区位码第3位
                rnd=new Random(r2*unchecked((int)DateTime.Now.Ticks)+i);
                int r3=rnd.Next(10,16);
                string str_r3=rBase[r3].Trim();

                //区位码第4位
                rnd=new Random(r3*unchecked((int)DateTime.Now.Ticks)+i);
                int r4;
                if (r3==10)
                {
                    r4=rnd.Next(1,16);
                }
                else if (r3==15)
                {
                    r4=rnd.Next(0,15);
                }
                else
                {
                    r4=rnd.Next(0,16);
                }
                string str_r4=rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码
                byte byte1=Convert.ToByte(str_r1 + str_r2,16);
                byte byte2=Convert.ToByte(str_r3 + str_r4,16);
                //将两个字节变量存储在字节数组中
                byte[] str_r=new byte[]{byte1,byte2};

                //将产生的一个汉字的字节数组放入object数组中
                bytes.SetValue(str_r,i);
                
            }

            return bytes;

            }

        public void ToExcel(string filename, DataTable dt)
        { 
            string FileName = filename;
            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            Excel.Application xlApp = null;
            xlApp = new Excel.Application();
            Excel.Workbooks workbooks = xlApp.Workbooks;
            Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            Excel.Range range;

            worksheet.Cells[1, 1] = "编号";
            for (int i = 0; i < dt.Columns.Count ; i++)
            {
                worksheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;
                range = (Excel.Range)worksheet.Cells[1, i + 1];
            }
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                worksheet.Cells[r + 2, 1] = r + 1;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    //worksheet.Cells[r+2,i+1]=dt.Rows[r][i];
                    if (i != dt.Columns.Count )
                        worksheet.Cells[r + 2, i + 2] = dt.Rows[r][i];
                }
                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
                //this.FM.CaptionText.Text = "正在导出数据，已导出[" + percent.ToString("0.00") + "%]...";
                System.Windows.Forms.Application.DoEvents();
            }
            range = worksheet.get_Range(worksheet.Cells[2, 1], worksheet.Cells[dt.Rows.Count + 2, dt.Columns.Count]);
            workbook.Saved = true;
            workbook.SaveCopyAs(FileName);
            //this.FM.CaptionText.Text = "";
        
        }

        public void ProgramReset()
        {

            DataAccess1.Emptyit("CarIn");
            DataAccess1.Dropit("PortState");
            DataAccess1.Dropit("Rate");
            DataAccess1.ResetPortState();
            DataAccess1.ResetRate();
            DataAccess1.ResetCarIn();
            DataAccess1.Emptyit("Carlog");
            DataAccess1.CarlogInitial();

            Application.Restart();
        }

        public void ProgramDataClear()
        {
            DataAccess1.Emptyit("Carlog");
            DataAccess1.Emptyit("CarIn");
            DataAccess1.Dropit("PortState");
            DataAccess1.Dropit("Rate");
            DataAccess1.ResetPortState();
            DataAccess1.ResetRate();
            DataAccess1.CarlogInitial();
            Application.Restart();

        }

    }
}

