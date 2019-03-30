using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace PDADemo_CF2._0
{
   public class Setting
    {
        public struct Params
        {
          public  bool RunAuto;
          public int RunType;
          public int PdaType;
          public int AntType;
          public string Comv;
          public int Protocol;
          public string Timeout;
          public string Intenval;
          public int Session;
          public int Gen2q;
          public bool CheckAnt;
          public string[] ReadPw;
          public string[] WritePw;
          public int Region;
          public bool CustomFrequency;
          public string[] Frequencys;
          public int Opant;
          public int ReaderType;
          public bool BlockWrite;
          public int[] Connectants;
          public int BatteryReport;
          public bool IsReport;
          //alter by dkg 2011 10 19
          public bool IsAutoStop;
          public int Seconds;
            //***
          public int PowerMode;

          public int Sleep;
        }
        private Params paramslist;
        public Params ParamList;
        private XmlDocument xmldoc;
        private XmlNode xmlnode;
        private XmlElement xmlelem;
        const string defautfilename = "Settings.xml";
        private bool isexist;
        public bool IsExist
        {
            get { return isexist; }
            set { isexist = value; }
        }
        public bool Creatxmlfile()
        {
            //string path = Directory.GetCurrentDirectory();
            //string path = Application.StartupPath;
            string path=System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            xmldoc = new XmlDocument();
            xmlnode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmldoc.AppendChild(xmlnode);
            xmlelem = xmldoc.CreateElement("", "ParamsList", "");
            xmldoc.AppendChild(xmlelem);
            try
            {
                xmldoc.Save(path + "\\" + defautfilename);
                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

        public Setting.Params SetDefault()
        {
            Setting.Params sptemp = new Setting.Params();
            sptemp.AntType = 1;
            sptemp.CheckAnt = false;
            sptemp.Comv = "COM1";
            sptemp.CustomFrequency = false;
            sptemp.Frequencys = new string[0];
            sptemp.Gen2q = 0;
            sptemp.Intenval = "50";
            sptemp.Opant = 1;
            sptemp.PdaType = 0;
            sptemp.Protocol = 0;
            sptemp.ReaderType = 3;
            sptemp.ReadPw = new string[4];
            sptemp.ReadPw[0] = "2300";
            sptemp.ReadPw[1] = "2300";
            sptemp.ReadPw[2] = "2300";
            sptemp.ReadPw[3] = "2300";

            sptemp.WritePw = new string[4];
            sptemp.WritePw[0] = "2300";
            sptemp.WritePw[1] = "2300";
            sptemp.WritePw[2] = "2300";
            sptemp.WritePw[3] = "2300";

            sptemp.Region = 8;
            sptemp.RunAuto = false;
            sptemp.RunType = 1;
            sptemp.Session = 0;
            sptemp.Timeout = "300";
            sptemp.BlockWrite = false;
            sptemp.IsReport = true;
            sptemp.BatteryReport = 5;
            sptemp.PowerMode = 0;
            sptemp.Sleep = 0;
            return sptemp;
        }
        public bool SaveParams(Params param_temp)
        {
            //string path = Directory.GetCurrentDirectory();
            //string path = Application.StartupPath;
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(path + "\\" + defautfilename);
            }
            catch (Exception ex)
            {
                return false;
            }
            XmlElement xmlelemroot = (XmlElement)xmldoc.ChildNodes[1];

            XmlElement xmlelem1 = xmldoc.CreateElement("", "RUNAUTO", "");
            xmlelem1.SetAttribute("Value", param_temp.RunAuto?"1":"0");
            xmlelemroot.AppendChild(xmlelem1);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem2 = xmldoc.CreateElement("", "RUNTYPE", "");
            xmlelem2.SetAttribute("Value", param_temp.RunType.ToString());
            xmlelem2.InnerText = "0 表示调试 1 表示正常运行 2表示升级";
            xmlelemroot.AppendChild(xmlelem2);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem3 = xmldoc.CreateElement("", "PDATYPE", "");
            xmlelem3.SetAttribute("Value", param_temp.PdaType.ToString());
            xmlelem3.InnerText = "0 表示DLJ 1 表示FZYH_BC_COM2 2 表示KM 3表示RDTM 4表示FZYH_BL_COM3";
            xmlelemroot.AppendChild(xmlelem3);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem4 = xmldoc.CreateElement("", "ANTTYPE", "");
            xmlelem4.SetAttribute("Value", param_temp.AntType.ToString());
            xmlelem4.InnerText = "1 表示单天线 2 表示双天线 4 表示四天线";
            xmlelemroot.AppendChild(xmlelem4);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem5 = xmldoc.CreateElement("", "COM", "");
            xmlelem5.SetAttribute("Value", param_temp.Comv.ToString());
            xmlelemroot.AppendChild(xmlelem5);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem6 = xmldoc.CreateElement("", "PROTOCOL", "");
            xmlelem6.SetAttribute("Value", param_temp.Protocol.ToString());
            xmlelem6.InnerText = "0 表示Gen2 1 表示6B 2 表示都有";
            xmlelemroot.AppendChild(xmlelem6);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem7 = xmldoc.CreateElement("", "TIMEOUT", "");
            xmlelem7.SetAttribute("Value", param_temp.Timeout);
            xmlelemroot.AppendChild(xmlelem7);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem8 = xmldoc.CreateElement("", "INTENVAL", "");
            xmlelem8.SetAttribute("Value", param_temp.Intenval);
            xmlelemroot.AppendChild(xmlelem8);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem9 = xmldoc.CreateElement("", "SESSION", "");
            xmlelem9.SetAttribute("Value", param_temp.Session.ToString());
            xmlelemroot.AppendChild(xmlelem9);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem10 = xmldoc.CreateElement("", "GEN2Q", "");
            xmlelem10.SetAttribute("Value", param_temp.Gen2q.ToString());
            xmlelemroot.AppendChild(xmlelem10);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem11 = xmldoc.CreateElement("", "CHECKANT", "");
            xmlelem11.SetAttribute("Value", param_temp.CheckAnt?"1":"0");
            xmlelem6.InnerText = "0 表示不检测 1 表示检测";
            xmlelemroot.AppendChild(xmlelem11);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem12 = xmldoc.CreateElement("", "ANT1R", "");
            xmlelem12.SetAttribute("Value", param_temp.ReadPw[0]);
            xmlelemroot.AppendChild(xmlelem12);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem13 = xmldoc.CreateElement("", "ANT2R", "");
            xmlelem13.SetAttribute("Value", param_temp.ReadPw[1]);
            xmlelemroot.AppendChild(xmlelem13);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem14 = xmldoc.CreateElement("", "ANT3R", "");
            xmlelem14.SetAttribute("Value", param_temp.ReadPw[2]);
            xmlelemroot.AppendChild(xmlelem14);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem15 = xmldoc.CreateElement("", "ANT4R", "");
            xmlelem15.SetAttribute("Value", param_temp.ReadPw[3]);
            xmlelemroot.AppendChild(xmlelem15);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem16 = xmldoc.CreateElement("", "ANT1W", "");
            xmlelem16.SetAttribute("Value", param_temp.WritePw[0]);
            xmlelemroot.AppendChild(xmlelem16);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem17 = xmldoc.CreateElement("", "ANT2W", "");
            xmlelem17.SetAttribute("Value", param_temp.WritePw[1]);
            xmlelemroot.AppendChild(xmlelem17);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem18 = xmldoc.CreateElement("", "ANT3W", "");
            xmlelem18.SetAttribute("Value", param_temp.WritePw[2]);
            xmlelemroot.AppendChild(xmlelem18);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem19 = xmldoc.CreateElement("", "ANT4W", "");
            xmlelem19.SetAttribute("Value", param_temp.WritePw[3]);
            xmlelemroot.AppendChild(xmlelem19);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem20 = xmldoc.CreateElement("", "REGION", "");
            xmlelem20.SetAttribute("Value", param_temp.Region.ToString());
            xmlelem20.InnerText = "未定，加拿大，欧洲1，欧洲2，欧洲3，印度，韩国，日本，北美,中国,全频段,840-845依次0,1,2...";
            xmlelemroot.AppendChild(xmlelem20);
            xmldoc.Save(path + "\\" + defautfilename);


            XmlElement xmlelem21 = xmldoc.CreateElement("", "FREQUENCY", "");
            xmlelem21.SetAttribute("Value", param_temp.CustomFrequency?"1":"0");
            for (int i = 0; i < param_temp.Frequencys.Length; i++)
                xmlelem21.InnerText += param_temp.Frequencys[i] + ",";
            if (xmlelem21.InnerText.Length > 0)
            xmlelem21.InnerText = xmlelem21.InnerText.Substring(0, xmlelem21.InnerText.Length - 1);
                xmlelemroot.AppendChild(xmlelem21);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem22 = xmldoc.CreateElement("", "OPANT", "");
            xmlelem22.SetAttribute("Value", param_temp.Opant.ToString());
            xmlelemroot.AppendChild(xmlelem22);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem23 = xmldoc.CreateElement("", "READERTYPE", "");
            xmlelem23.SetAttribute("Value", param_temp.ReaderType.ToString());
            xmlelem23.InnerText = "MT_TWOANTS,MT_FOURANTS,MT_THREEANTS,MT_ONEANT,PR_ONEANT,MT_A7_FOURANTS,MT_A7_TWOANTS,SL_FOURANTS,M6_A7_FOURANTS,M1S,M2S依次0,1,2,3";
            xmlelemroot.AppendChild(xmlelem23);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem24 = xmldoc.CreateElement("", "WRITEMODE", "");
            xmlelem24.SetAttribute("Value", param_temp.BlockWrite?"0":"1");
            xmlelem24.InnerText = "0 表示字写 1表示块写";
            xmlelemroot.AppendChild(xmlelem24);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem25 = xmldoc.CreateElement("", "BATTERY", "");
            xmlelem25.SetAttribute("Value", param_temp.IsReport?param_temp.BatteryReport.ToString():"0");
            xmlelem25.InnerText = "0 不报警 ";
            xmlelemroot.AppendChild(xmlelem25);
            xmldoc.Save(path + "\\" + defautfilename);

            XmlElement xmlelem26 = xmldoc.CreateElement("", "POWERMODE", "");
            xmlelem26.SetAttribute("Value", param_temp.PowerMode.ToString());
            xmlelem26.InnerText = "省电模式 ";
            xmlelemroot.AppendChild(xmlelem26);
            xmldoc.Save(path + "\\" + defautfilename);


            XmlElement xmlelem27 = xmldoc.CreateElement("", "SLEEP", "");
            xmlelem27.SetAttribute("Value", param_temp.Sleep.ToString());
            xmlelem27.InnerText = "睡眠时间 ";
            xmlelemroot.AppendChild(xmlelem27);
            xmldoc.Save(path + "\\" + defautfilename);
            return true;
        }
        public Params ReadParams()
        {
            Params param_re;
            param_re.ReadPw = new string[4];
            param_re.WritePw = new string[4];
            //string path = Directory.GetCurrentDirectory();
            //string path = Application.StartupPath;
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(path + "\\" + defautfilename);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            XmlNode xmlroot = xmldoc.ChildNodes[1];
            param_re.RunAuto = int.Parse(xmlroot.ChildNodes[0].Attributes["Value"].Value)==1?true:false;
            param_re.RunType = int.Parse(xmlroot.ChildNodes[1].Attributes["Value"].Value);
            param_re.PdaType = int.Parse(xmlroot.ChildNodes[2].Attributes["Value"].Value);
            param_re.AntType = int.Parse(xmlroot.ChildNodes[3].Attributes["Value"].Value);
            param_re.Comv = xmlroot.ChildNodes[4].Attributes["Value"].Value;
            
            param_re.Protocol= int.Parse(xmlroot.ChildNodes[5].Attributes["Value"].Value);

            param_re.Timeout= xmlroot.ChildNodes[6].Attributes["Value"].Value;

            param_re.Intenval = xmlroot.ChildNodes[7].Attributes["Value"].Value;

            param_re.Session = int.Parse(xmlroot.ChildNodes[8].Attributes["Value"].Value);

            param_re.Gen2q = int.Parse(xmlroot.ChildNodes[9].Attributes["Value"].Value);

            param_re.CheckAnt = int.Parse(xmlroot.ChildNodes[10].Attributes["Value"].Value) == 1 ? true : false;

            param_re.ReadPw[0] = xmlroot.ChildNodes[11].Attributes["Value"].Value;

            param_re.ReadPw[1] = xmlroot.ChildNodes[12].Attributes["Value"].Value;

            param_re.ReadPw[2] = xmlroot.ChildNodes[13].Attributes["Value"].Value;

            param_re.ReadPw[3] = xmlroot.ChildNodes[14].Attributes["Value"].Value;

            param_re.WritePw[0] = xmlroot.ChildNodes[15].Attributes["Value"].Value;

            param_re.WritePw[1] = xmlroot.ChildNodes[16].Attributes["Value"].Value;

            param_re.WritePw[2] = xmlroot.ChildNodes[17].Attributes["Value"].Value;

            param_re.WritePw[3] = xmlroot.ChildNodes[18].Attributes["Value"].Value;

            param_re.Region = int.Parse(xmlroot.ChildNodes[19].Attributes["Value"].Value);

            param_re.CustomFrequency = int.Parse(xmlroot.ChildNodes[20].Attributes["Value"].Value)==1?true:false;

            string[] fres = xmlroot.ChildNodes[20].InnerText.Split(',');
            param_re.Frequencys = new string[fres.Length];
            for (int i = 0; i < fres.Length; i++)
                param_re.Frequencys[i] = fres[i];

            param_re.Opant = int.Parse(xmlroot.ChildNodes[21].Attributes["Value"].Value);

            param_re.ReaderType = int.Parse(xmlroot.ChildNodes[22].Attributes["Value"].Value);

            param_re.BlockWrite = int.Parse(xmlroot.ChildNodes[23].Attributes["Value"].Value) == 0 ? true : false;

            param_re.BatteryReport = int.Parse(xmlroot.ChildNodes[24].Attributes["Value"].Value);
            param_re.IsReport = param_re.BatteryReport > 0 ? true : false;

            param_re.PowerMode = int.Parse(xmlroot.ChildNodes[25].Attributes["Value"].Value);

            param_re.Sleep = int.Parse(xmlroot.ChildNodes[26].Attributes["Value"].Value);

            param_re.Connectants = null;


            param_re.Seconds = 0;
            param_re.IsAutoStop = false;
            return param_re;

        }
        public bool MendParams(Params toparam)
        {
            //string path = Directory.GetCurrentDirectory();
            //string path = Application.StartupPath;
            string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.Load(path + "\\" + defautfilename);
            }
            catch (Exception ex)
            {

                return false;
            }

            XmlNode xmlroot = xmldoc.ChildNodes[1];
            xmlroot.ChildNodes[0].Attributes["Value"].Value = toparam.RunAuto?"1":"0";
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[1].Attributes["Value"].Value = toparam.RunType.ToString();
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[2].Attributes["Value"].Value = toparam.PdaType.ToString();
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[3].Attributes["Value"].Value = toparam.AntType.ToString();
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[4].Attributes["Value"].Value = toparam.Comv;
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[5].Attributes["Value"].Value = toparam.Protocol.ToString();
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[6].Attributes["Value"].Value = toparam.Timeout;
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[7].Attributes["Value"].Value = toparam.Intenval;
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[8].Attributes["Value"].Value = toparam.Session.ToString();
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[9].Attributes["Value"].Value = toparam.Gen2q.ToString();
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[10].Attributes["Value"].Value = toparam.CheckAnt?"1":"0";
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[11].Attributes["Value"].Value = toparam.ReadPw[0];
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[12].Attributes["Value"].Value = toparam.ReadPw[1];
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[13].Attributes["Value"].Value = toparam.ReadPw[2];
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[14].Attributes["Value"].Value = toparam.ReadPw[3];
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[15].Attributes["Value"].Value = toparam.WritePw[0];
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[16].Attributes["Value"].Value = toparam.WritePw[1];
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[17].Attributes["Value"].Value = toparam.WritePw[2];
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[18].Attributes["Value"].Value = toparam.WritePw[3];
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[19].Attributes["Value"].Value = toparam.Region.ToString();
            xmldoc.Save(path + "\\" + defautfilename);


            xmlroot.ChildNodes[20].Attributes["Value"].Value = toparam.CustomFrequency ? "1" : "0";
           
            xmlroot.ChildNodes[20].InnerText = "";
            for (int i = 0; i < toparam.Frequencys.Length; i++)
                xmlroot.ChildNodes[20].InnerText += toparam.Frequencys[i] + ",";
            if(xmlroot.ChildNodes[20].InnerText!="")
            xmlroot.ChildNodes[20].InnerText = xmlroot.ChildNodes[20].InnerText.Substring(0, xmlroot.ChildNodes[20].InnerText.Length - 1);
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[21].Attributes["Value"].Value = toparam.Opant.ToString();
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[22].Attributes["Value"].Value = toparam.ReaderType.ToString();
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[23].Attributes["Value"].Value = toparam.BlockWrite ? "0" : "1";
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[24].Attributes["Value"].Value = toparam.IsReport ? toparam.BatteryReport.ToString() : "0";
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[25].Attributes["Value"].Value = toparam.PowerMode.ToString();
            xmldoc.Save(path + "\\" + defautfilename);

            xmlroot.ChildNodes[26].Attributes["Value"].Value = toparam.Sleep.ToString();
            return true;
        }
    }
}
