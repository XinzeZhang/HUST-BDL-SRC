#define  FZYH

using System;
//using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ModuleTech;
using PowerControl;

namespace PDADemo_CF2._0
{
    public partial class LoadForm : Form
    {
        public LoadForm()
        {
             
            InitializeComponent();
        }
        public log dlog;
        Setting Setfile;
        public Setting SetFile
        {
            get { return Setfile; }
        }
        Setting.Params sparmas;
        public Setting.Params SParams
        {
            get { return sparmas; }
        }
        public MainForm mf;
        
        //public Reader ReaderO
        //{
        //    get { return Rd; }
            
        //}


        Thread RunThread;
        bool Runningl;
        delegate void Fundelegate(int curpercent,string mess);
        Fundelegate Funpercent;
        IAsyncResult Iar;

        bool IsExit;
        PowerC Pc;

        public PowerC Power
        {
            get { return Pc; }
        }
        public bool IsExitPrg
        {
            get { return IsExit; }
        }
        private void Canclebutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void SetControl(int curpercent,string mess)
        {
            if (curpercent >= 0)
            {
                progressBar.Value = curpercent;
                Loadlabel.Text = mess;
            }
            else
                this.Close();
        }
        private void HandlePercent(int curpercent,string mess)
        {
            Iar=this.BeginInvoke(new Fundelegate(SetControl), new object[] {curpercent,mess});
        }

        private void SetDefault()
        {
            try
            {

                Setting.Params sptemp = Setfile.SetDefault();
                Setfile.Creatxmlfile();
                Setfile.SaveParams(sptemp);

                sparmas = sptemp;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        private void LoadForm_Load(object sender, EventArgs e)
        {
            dlog.WirteLog("loadstart\r\n");
            //读取参数表
            try
            {
                //dlog = new log();
                //dlog.CreatLogFile("log.txt");
                Setfile = new Setting();
                sparmas=Setfile.ReadParams();
                 
            }
            catch //(System.Exception ex)
            {

                try
                {
                    SetDefault();
                }
                catch //(System.Exception ex2)
                {
                    IsExit = true;
                    this.Close();
                    return;
                }

            }
            dlog.WirteLog("read params\r\n");
            Pc = new PowerC((PowerControl.PDA_Type)sparmas.PdaType);
            //if (sparmas.PdaType != 1 && sparmas.PdaType != 4 && sparmas.PdaType != -1)
                Pc.PowerUP();
                dlog.WirteLog("power up\r\n");
            if (sparmas.RunType == 1)
            {
                Funpercent = HandlePercent;
                RunThread = new Thread(new ThreadStart(Running));
                RunThread.Start();
                //if (dlog != null)
                //    dlog.WirteLog("connect name:" + RunThread.Name + " " + RunThread.ManagedThreadId.ToString()+"\n");
                Runningl = true;

                dlog.WirteLog("connect to reader\r\n");
            }
            else
            {this.Close(); }
        }

        private void Running()
        {
            while (Runningl)
            {
                try
                {
                    ReaderType[] types = new ReaderType[] { ReaderType.MT_TWOANTS, ReaderType.MT_FOURANTS, ReaderType.MT_THREEANTS, ReaderType.MT_ONEANT, ReaderType.PR_ONEANT, ReaderType.MT_A7_FOURANTS, ReaderType.MT_A7_TWOANTS, ReaderType.SL_FOURANTS, ReaderType.M6_A7_FOURANTS, ReaderType.MT100, ReaderType.MT200 };
                    mf.Rd = Reader.Create(sparmas.Comv, ModuleTech.Region.PRC, types[sparmas.ReaderType]);
                    Funpercent(30, "创建读写器完毕");
                    dlog.WirteLog("创建读写器完毕\r\n");
                    //
                    if (!(types[sparmas.ReaderType] == ReaderType.M6_A7_FOURANTS && sparmas.AntType == 2))
                    {
                        mf.Rd.ParamSet("CheckAntConnection", sparmas.CheckAnt);
                        Funpercent(40, "检测天线");
                        dlog.WirteLog("检测天线\r\n");
                    }

                    AntPower[] apwrs = new AntPower[sparmas.AntType];
                    for (int i = 0; i < apwrs.Length; i++)
                    {
                        apwrs[i].AntId = (byte)(i + 1);
                        apwrs[i].ReadPower = ushort.Parse(sparmas.ReadPw[i]);
                        apwrs[i].WritePower = ushort.Parse(sparmas.WritePw[i]);
                    }
                    mf.Rd.ParamSet("AntPowerConf", apwrs);
                    Funpercent(50, "配置天线功率");
                    dlog.WirteLog("配置天线功率\r\n");
                    mf.Rd.ParamSet("TagopAntenna", sparmas.Opant);
                    Funpercent(60, "配置默认天线");
                    dlog.WirteLog("配置默认天线\r\n");
                    int[] connectedants = (int[])mf.Rd.ParamGet("ConnectedAntennas");

                    SimpleReadPlan gen2srp = null;
                    SimpleReadPlan iso6bsrp = null;
                    if (connectedants.Length > 0 && (sparmas.AntType != 2 && types[sparmas.ReaderType]!=ReaderType.M6_A7_FOURANTS))
                    {
                        sparmas.Connectants = connectedants;
                        gen2srp = new SimpleReadPlan(TagProtocol.GEN2, connectedants,30);
                        iso6bsrp = new SimpleReadPlan(TagProtocol.ISO180006B, connectedants,30);
                    }
                    else
                    {
                        sparmas.Connectants = null;
                        gen2srp = new SimpleReadPlan(TagProtocol.GEN2, new int[] { sparmas.Opant },30);
                        iso6bsrp = new SimpleReadPlan(TagProtocol.ISO180006B, new int[] { sparmas.Opant },30);
                    }

                    if (sparmas.Protocol == 0)
                    {
                        mf.Rd.ParamSet("ReadPlan", gen2srp);

                    }
                    else if (sparmas.Protocol == 1)
                    {
                        mf.Rd.ParamSet("ReadPlan", iso6bsrp);

                    }
                    else
                    {
                        List<SimpleReadPlan> Lrp = new List<SimpleReadPlan>();
                        //List<ReadPlan> Lrp = new List<ReadPlan>();
                        Lrp.Add(gen2srp);
                        Lrp.Add(iso6bsrp);
                        MultiReadPlan mrp = new MultiReadPlan(Lrp.ToArray());
                        mf.Rd.ParamSet("ReadPlan", mrp);

                    }
                    Funpercent(70, "配置盘点方式");
                    dlog.WirteLog("配置盘点方式\r\n");
                    mf.Rd.ParamSet("Gen2Session", (ModuleTech.Gen2.Session)sparmas.Session);
                    Funpercent(80, "配置会话模式");
                    dlog.WirteLog("配置会话模式\r\n");

                    ModuleTech.Region[] mregion = new ModuleTech.Region[] { ModuleTech.Region.UNSPEC, ModuleTech.Region.CN, ModuleTech.Region.EU, ModuleTech.Region.EU2, ModuleTech.Region.EU3, ModuleTech.Region.IN, ModuleTech.Region.KR, ModuleTech.Region.JP, ModuleTech.Region.NA, ModuleTech.Region.PRC, ModuleTech.Region.OPEN, ModuleTech.Region.OPEN };
                    mf.Rd.ParamSet("Region", mregion[sparmas.Region]);
                    Funpercent(90, "配置区域");
                    dlog.WirteLog("配置区域" + mregion[sparmas.Region].ToString() + "\r\n");

                    if (sparmas.CustomFrequency)
                    {
                        List<uint> htb = new List<uint>();
                        for (int i = 0; i < sparmas.Frequencys.Length; i++)
                        {
                            htb.Add(uint.Parse(sparmas.Frequencys[i]));
                            //dlog.WirteLog(sparmas.Frequencys[i].ToString()+"\r\n");
                        }
                        try
                        {
                            mf.Rd.ParamSet("FrequencyHopTable", htb.ToArray());
                            Funpercent(90, "配置频率表");
                            dlog.WirteLog("配置频率表\r\n");
                        }
                        catch (System.Exception ex)
                        {
                        	
                        }
                       
                    }
 
                    try
                    {
                        //case 0x00:
                        //    pm = SerialReader.PowerMode.FULL;
                        //    break;
                        //case 0x01:
                        //    pm = SerialReader.PowerMode.MINSAVE;
                        //    break;
                        //case 0x02:
                        //    pm = SerialReader.PowerMode.MEDSAVE;
                        //    break;
                        //case 0x03:
                        //    pm = SerialReader.PowerMode.MAXSAVE;
                        //    break;
                        //case 0x04:
                        //    pm = SerialReader.PowerMode.SLEEP;
                        mf.Rd.ParamSet("PowerMode", (byte)sparmas.PowerMode);//0x03
                        mf.Rd.ParamSet("IsTransmitPowerSave", true);
                    }
                    catch// (Exception omitex)
                    {

                    }
                    Funpercent(100, "配置完毕，读写器工作就绪");
                }
                catch (System.Exception ex)
                {
                    string msg = string.Empty;
                    if (ex is ModuleLibrary.FatalInternalException)
                        msg = Convert.ToString(((ModuleLibrary.FatalInternalException)ex).ErrCode, 16);
                    if (ex is ModuleLibrary.HardwareAlertException)
                        msg = Convert.ToString(((ModuleLibrary.HardwareAlertException)ex).ErrCode, 16);
                    if (ex is ModuleLibrary.ModuleException)
                        msg = Convert.ToString(((ModuleLibrary.ModuleException)ex).ErrCode, 16);
                    if (ex is ModuleLibrary.OpFaidedException)
                        msg = Convert.ToString(((ModuleLibrary.OpFaidedException)ex).ErrCode, 16);
                    if (mf.Rd != null)
                        mf.Rd.Disconnect();
                    Funpercent(0, "连接失败" + ex.Message + " :" + msg);
                    if (dlog != null)
                    {
                        dlog.WirteLog(ex.Message + ":" + msg + ex.StackTrace);
                    }
                }

                Thread.Sleep(1000);
                Runningl = false;
            }
            IsExit = false;

            Funpercent(-100, string.Empty);
        }

        private void LoadForm_Closing(object sender, CancelEventArgs e)
        {
            Runningl = false;
          

            if (RunThread != null)
            {
                if (Iar != null)
                    this.EndInvoke(Iar);
                RunThread.Join();
               
            }

            if (progressBar.Value < 100)
                IsExit = true;

            
        }
    }
}