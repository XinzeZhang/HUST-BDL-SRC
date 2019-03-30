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
using ModuleTech.Gen2;
using PowerControl;
using ModuleTech.CustomCmd;
using ModuleLibrary;

namespace PDADemo_CF2._0
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //线程
        Thread ReadThread,RevThread;
        bool ReadRunning,Reving;
        IAsyncResult Iar;
        delegate void Fundgate(PGMessage pg, List<object> lobj);
        //module
        public Reader Rd;

        //ClassLibray
        Setting.Params Sp;
        Setting SetFile;
        Sound sound;
        SerialClass Sc;

        //Data
        Dictionary<string, int> Dictags;
        Object lockobj;
        TagFilter Tf=null;
        TagProtocol Tp = TagProtocol.GEN2;

        PowerC Pc;

        Mutex mtex = new Mutex();
         
        PowerManager PM;
        bool Checkbattery=false;
        log dlog, staticlog;

        ReaderType[] types = new ReaderType[] { ReaderType.MT_TWOANTS, ReaderType.MT_FOURANTS, ReaderType.MT_THREEANTS, ReaderType.MT_ONEANT, ReaderType.PR_ONEANT, ReaderType.MT_A7_FOURANTS, ReaderType.MT_A7_TWOANTS, ReaderType.SL_FOURANTS, ReaderType.M6_A7_FOURANTS, ReaderType.MT100, ReaderType.MT200 };
           
        // DateTime Dtreconnect;
        public enum PGMessage
        {
            ShowTick,
            ShowMessage,
            ShowSatatus,
            RECONNECT,
            StopReadThreadAction,
        }
        //alter by dkg 2011 10 19
        DateTime StartInv;
        //***
        public void HandlePGMessage(PGMessage pg, List<object> lobj)
        {
            switch (pg)
            {
                case PGMessage.ShowMessage:
                    {
                        //TagslistBox.Items.Clear();
                        lock (lockobj)
                        {
                            
                            foreach (KeyValuePair<string, int> kvp in Dictags)
                            {
                                bool isadd = true;
                                //TagslistBox.Items.Add(kvp.Key + ":" + kvp.Value);
                                foreach (ListViewItem viewitem in TagslistView.Items)
                                {
                                    if (viewitem.SubItems[0].Text == kvp.Key)
                                    {
                                        viewitem.SubItems[1].Text = (int.Parse(viewitem.SubItems[1].Text) + kvp.Value).ToString();
                                        isadd = false;
                                    }
                                }

                                if (isadd)
                                {
                                    ListViewItem lvi = new ListViewItem(kvp.Key);
                                    lvi.SubItems.Add(kvp.Value.ToString());
                                    TagslistView.Items.Add(lvi);
                                }
                              
                            }

                        }
                        
                        //if (Btfrm.BlueToothConnected)
                        ReadStatuslabel.Text = "本次读到:" + Dictags.Count.ToString() + "个标签  共读到:" + TagslistView.Items.Count.ToString() + "个标签";// +" cost:" + (Environment.TickCount - (int)lobj[0]).ToString();
                        //else
                        //    ReadStatuslabel.Text = "读到:" + Dictags.Count.ToString() + "个标签" + " 目标蓝牙断开,重新连接";
                    }
                    break;
                case PGMessage.RECONNECT:

                    //TimeSpan ts = DateTime.Now - Dtreconnect;
                    //if (ts.TotalSeconds > 25)
                    //{
                    //    Dtreconnect = DateTime.Now;
                    //    Btfrm.BlueTooth_Conected();
                    //}
                    break;
                case PGMessage.ShowSatatus:
                    ReadStatuslabel.Text = (string)lobj[0];
                    break;
                case PGMessage.StopReadThreadAction:
                    {
                        ReadStatuslabel.Text = (string)lobj[0];
                        Stopbutton_Click(this, null);
                    }
                    break;
                case PGMessage.ShowTick:
                    {
                        Costlabel.Text = lobj[0].ToString();
                        ReadStatuslabel.Text = (Environment.TickCount-(int)lobj[1]).ToString();
                    }
                    break;
            }
        }
        int ticcountbefore;
        public void Running()
        {
            while (ReadRunning)
            {

                try
                {
                    //ticcountbefore = Environment.TickCount;
                    List<object> lobs = new List<object>();
                    if (Checkbattery)
                    {
                        if (BatteryReportV())
                        {
                            lobs.Clear();
                            lobs.Add("电量过低，请及时充电");
                            Iar = this.BeginInvoke(new Fundgate(HandlePGMessage), new object[] { PGMessage.StopReadThreadAction, lobs });
                        }
                    }

                    if (Sp.IsAutoStop)
                    {
                        TimeSpan Ts = DateTime.Now - StartInv;
                        if (Ts.TotalSeconds > Sp.Seconds)
                        {
                            lobs.Clear();
                            lobs.Add("自动停止");
                            Iar = this.BeginInvoke(new Fundgate(HandlePGMessage), new object[] { PGMessage.StopReadThreadAction, lobs });
                        }
                    }
                     
                    lock (lockobj)
                    {
                        Dictags.Clear();
                    }
                    mtex.WaitOne();
                    //int readbefore = Environment.TickCount;
                    //System.Console.WriteLine("read before:"+(readbefore - ticcountbefore).ToString());
                    TagReadData[] trds=null;
                    
                    try
                    {
                        trds = Rd.Read(int.Parse(Sp.Timeout));
                    }
                    catch (ModuleLibrary.OpFaidedException ofex1)
                    {
                        throw ofex1;
                    }
                    catch (System.Exception ex1)
                    {
                        throw ex1;
                    }
                    finally
                    {
                        mtex.ReleaseMutex();
                    }
                  
                   
                    //int afterread = Environment.TickCount;
                    //System.Console.WriteLine("after read:" + (afterread - readbefore).ToString());
                    //lobs.Clear();
                    //lobs.Add(Environment.TickCount - ticcountbefore);
                    //lobs.Add(Environment.TickCount);
                    //Iar = this.BeginInvoke(new Fundgate(HandlePGMessage), new object[] { PGMessage.ShowTick, lobs });

                    if (trds.Length > 0)
                    {
                        sound.Play();
                        string strdata = string.Empty;

                        lock (lockobj)
                        {

                            foreach (TagReadData trd in trds)
                            {
                                strdata += trd.EPCString + ":" + trd.ReadCount + "\r\n";


                                if (!Dictags.ContainsKey(trd.EPCString))
                                    Dictags.Add(trd.EPCString, trd.ReadCount);
                                else
                                    Dictags[trd.EPCString] = Dictags[trd.EPCString] + trd.ReadCount;
                            }

                        }
                    }
                    //if (Btfrm.BlueToothConnected)
                    //    Btfrm.BlueTooth_Send(strdata);
                    //else
                    //    Iar = this.BeginInvoke(new Fundgate(HandlePGMessage), new object[] { PGMessage.RECONNECT, new List<object>() });

                    //int beforeinvoke = Environment.TickCount;
                    //System.Console.WriteLine("after read:" + (beforeinvoke - afterread).ToString());
                    Iar = this.BeginInvoke(new Fundgate(HandlePGMessage), new object[] { PGMessage.ShowMessage, new List<object>{} });
                }
                catch (ModuleLibrary.OpFaidedException ofex)
                {
                    List<object> lobs = new List<object>();
                    lobs.Add(ofex.Message + " :" + ofex.ErrCode);
                    Iar = this.BeginInvoke(new Fundgate(HandlePGMessage), new object[] { PGMessage.ShowSatatus, lobs });
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
                    List<object> lobs = new List<object>();
                    lobs.Add(ex.Message + " :" + msg);
                    Iar = this.BeginInvoke(new Fundgate(HandlePGMessage), new object[] { PGMessage.StopReadThreadAction, lobs });
                }

                Thread.Sleep(int.Parse(Sp.Intenval));
            }
        }

        private bool BatteryReportV()
        {
            if (Battery.GetBatteryLifePercent() == -1)
                return false;

            if (Sp.IsReport)
            {
                if (Battery.GetBatteryLifePercent() < Sp.BatteryReport)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        private void Startbutton_Click(object sender, EventArgs e)
        {
            //清除状态数据
            Dictags.Clear();
            //TagslistBox.Items.Clear();
            TagslistView.Items.Clear();
            ReadStatuslabel.Text = "";
            if (Checkbattery)
            {
                if (BatteryReportV())
                {
                    MessageBox.Show("电量过低，请及时充电");
                    return;
                }
            }
            StartInv = DateTime.Now;
            ReadRunning = true;
            ReadThread = new Thread(new ThreadStart(Running));
            ReadThread.Start();
            //if (dlog != null)
            //    dlog.WirteLog("Read name:" +ReadThread.Name+" "+ ReadThread.ManagedThreadId.ToString()+"\n");

            Stopbutton.Enabled = true;

            Startbutton.Enabled = false;

            ReadMembutton.Enabled = false;
            WriteMembutton.Enabled = false;
            LockMembutton.Enabled = false;
            WriteTagbutton.Enabled = false;

            Setfilterbutton.Enabled = false;
            SetFrenquencybutton.Enabled = false;
            SetRegionbutton.Enabled = false;
            SaveParambutton.Enabled = false;
            Setusuallybutton.Enabled = false;
            AntParamSetbutton.Enabled = false;
            GetFrequencybutton.Enabled = false;

            button_save.Enabled = false;

        }

        private void EndThread()
        {
            try
            {
                ReadRunning = false;
                if (ReadThread != null)
                {
                    if (Iar != null)
                        this.EndInvoke(Iar);

                    ReadThread.Join();
                }
            }
            catch //(System.Exception ex)
            {
                if (ReadThread != null)
                    ReadThread.Abort();
            }
           
        }

        private void Stopbutton_Click(object sender, EventArgs e)
        {
            EndThread();
            setcontrls(0);
        }

        delegate void SetFun(int type);
        private void setcontrls(int type)
        {
            if (type == 0)
            {
                Stopbutton.Enabled = false;
                Startbutton.Enabled = true;

                ReadMembutton.Enabled = true;
                WriteMembutton.Enabled = true;
                LockMembutton.Enabled = true;
                WriteTagbutton.Enabled = true;

                Setfilterbutton.Enabled = true;
                SetFrenquencybutton.Enabled = true;
                SetRegionbutton.Enabled = true;
                SaveParambutton.Enabled = true;
                Setusuallybutton.Enabled = true;
                AntParamSetbutton.Enabled = true;
                GetFrequencybutton.Enabled = true;

                button_save.Enabled = true;
            }
            else
            {
                Lf.ShowDialog();
            }
          
        }
        bool isInvoke = false;
        private void sleephandle(SleepEventArgs sea)
        {
          
            EndThread();
            this.Invoke(new SetFun(setcontrls), new object[] { 0 });
            Rd.Disconnect();
            
            isInvoke = true;
        }
        
        private void resumehandle(ResumeEventArgs rea)
        {
            if (isInvoke)
            {
                this.Invoke(new SetFun(setcontrls), new object[] { 1 });
                isInvoke = false;
            }
        }
        LoadForm Lf;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                dlog = new log();
                dlog.CreatLogFile("log.txt");
                staticlog = new log();
                staticlog.CreatLogFile("盘点结果.csv");
            }
            catch
            {

            }
            Lf = new LoadForm();
            Lf.dlog = dlog;
            Lf.mf = this;

            Rectangle ScreenArea = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            Lf.Location = new System.Drawing.Point((ScreenArea.Width - Lf.Width) / 2,
                (ScreenArea.Height - Lf.Height) / 2);  

            Lf.ShowDialog();
            Sp = Lf.SParams;
            SetFile = Lf.SetFile;
            if (Lf.SParams.RunType == 1)
            {
                if (Lf.IsExitPrg)
                {
                    this.Close();
                    return;
                }


                //Rd = Lf.ReaderO;
               

                ReadRunning = false;
                Dictags = new Dictionary<string, int>();
                lockobj = new Object();

                Stopbutton.Enabled = false;


                Pc=Lf.Power;

                try
                {
                    int[] connectedants = (int[])Rd.ParamGet("ConnectedAntennas");
                    if (connectedants.Length > 0)
                    {
                        for (int i = 0; i < connectedants.Length; i++)
                        {
                            if (connectedants[i] == 1)
                                Ant1ConnetedcheckBox.Checked = true;
                            else if (connectedants[i] == 2)
                                Ant2ConnetedcheckBox.Checked = true;
                            else if (connectedants[i] == 3)
                                Ant3ConnetedcheckBox.Checked = true;
                            else
                                Ant4ConnetedcheckBox.Checked = true;
                        }
                    }
                }
                catch// (System.Exception ex)
                {

                }

                //if (Sp.ReaderType == 9&&Sp.PdaType==8)//ModuleTech.ReaderType.MT100)
                //{
                //    sound = new Sound(@"\windows\Barcodebeep.wav");
                //    checkBox_mt100sig.Checked = true;
                //    Rd.ParamSet("InventoryMode", 1);
                //}
                //else
                //    sound = new Sound(@"\platform\WavAlias\BEEP.wav");
                sound = new Sound(@"beep.wav");
                //配置所有参赛值
                if (Sp.Opant == 1)
                    Ant1radioButton.Checked = true;
                else if (Sp.Opant == 2)
                    Ant2radioButton.Checked = true;
                else if (Sp.Opant == 3)
                    Ant3radioButton.Checked = true;
                else
                    Ant4radioButton.Checked = true;

                if (Sp.RunAuto)
                    AutoRuncheckBox.Checked = true;
                else
                    AutoRuncheckBox.Checked = false;

                if (Sp.Protocol == 0)
                    Gen2checkBox.Checked = true;
                else if (Sp.Protocol == 1)
                    ISO6BcheckBox.Checked = true;
                else
                {
                    Gen2checkBox.Checked = true;
                    ISO6BcheckBox.Checked = true;
                }

                TimeouttextBox.Text = Sp.Timeout;
                IntervaltextBox.Text = Sp.Intenval;

                SessioncomboBox.SelectedIndex = Sp.Session;

                Gen2QcomboBox.SelectedIndex = Sp.Gen2q;

                if (Sp.CheckAnt)
                    AntConnectedcheckBox.Checked = true;
                else
                    AntConnectedcheckBox.Checked = false;

                Ant1ReadPowertextBox.Text = Sp.ReadPw[0];
                Ant2ReadPowertextBox.Text = Sp.ReadPw[1];
                Ant3ReadPowertextBox.Text = Sp.ReadPw[2];
                Ant4ReadPowertextBox.Text = Sp.ReadPw[3];

                Ant1WritePowertextBox.Text = Sp.WritePw[0];
                Ant2WritePowertextBox.Text = Sp.WritePw[1];
                Ant3WritePowertextBox.Text = Sp.WritePw[2];
                Ant4WritePowertextBox.Text = Sp.WritePw[3];

                if (Sp.BlockWrite)
                {
                    BlockwriteradioButton.Checked = true;
                }
                else
                    WordwriteradioButton.Checked = true;

                RegioncomboBox.SelectedIndex = Sp.Region;

                if (Sp.CustomFrequency)
                {
                    for (int i = 0; i < Sp.Frequencys.Length; i++)
                    {
                        ListViewItem lvi = new ListViewItem(Sp.Frequencys[i].ToString());
                        lvi.Checked = true;
                        FrequencylistView.Items.Add(lvi);
                    }
                }

                if (Sp.IsReport)
                    BatterycheckBox.Checked = true;
                else
                    BatterycheckBox.Checked = false;

                switch (Sp.Sleep)
                {
                    case 0:
                        SleeptimeoutcomboBox.SelectedIndex = 0;
                        break;
                    case 60:
                        SleeptimeoutcomboBox.SelectedIndex = 1;
                        break;
                    case 120:
                        SleeptimeoutcomboBox.SelectedIndex = 2;
                        break;
                    case 180:
                        SleeptimeoutcomboBox.SelectedIndex = 3;
                        break;
                    case 240:
                        SleeptimeoutcomboBox.SelectedIndex = 4;
                        break;
                    case 300:
                        SleeptimeoutcomboBox.SelectedIndex = 5;
                        break;
                    case 600:
                        SleeptimeoutcomboBox.SelectedIndex = 6;
                        break;
                    case 1800:
                        SleeptimeoutcomboBox.SelectedIndex = 7;
                        break;
                }
            }
            else if (Lf.SParams.RunType == 0)
            {
                Pc = new PowerC((PDA_Type)Sp.PdaType);

                tabControl2.Enabled = false;

                Startbutton.Enabled = false;

                Stopbutton.Enabled = false;

                ReadMembutton.Enabled = false;

                WriteMembutton.Enabled = false;

                LockMembutton.Enabled = false;

                WriteTagbutton.Enabled = false;

                Closebutton.Enabled = false;

                
            }
            else
            {
                UpdateForm ufrm = new UpdateForm(Sp.Comv);
                ufrm.ShowDialog();

                
                this.Close();
            }
            //Lf.Close();
            //Lf.Dispose();//释放资源

            //dkg
            //hEvent[0] = Win32.CreateEvent(IntPtr.Zero, false, false, null);
            //isPD = true;
            //PowerDown = new Thread(new ThreadStart(CheckPower));
            //PowerDown.Start();
            //PM = new PowerManager();
            //PM.ResumeNotify += new ResumeEventHandler(resumehandle);
            //PM.SleepNotify+=new SleepEventHandler(sleephandle);
            //PM.Dlog = dlog;
            //PM.Start();

        }

        private bool CheckDatarule(string data)
        {
            bool returnvalue = true;
            if (data == string.Empty || data == null)
            {
                return false;
            }

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

            return returnvalue;
        }
        private void ReadMembutton_Click(object sender, EventArgs e)
        {
            try
            {
                OpStatuslabel.Text = "";
                Application.DoEvents();
                if (BatteryReportV())
                {
                    MessageBox.Show("电量过低，请及时充电");
                    return;
                }

                SetOpAnt();

                if (Tp == TagProtocol.GEN2)
                {
                    if (BankcomboBox.SelectedIndex == -1)
                        throw new Exception("未选择Bank");

                    int strblock = int.Parse(StartAddrtextBox.Text);
                    int blockcnt = int.Parse(BlockCounttextBox.Text);

                    ushort[] readdata = Rd.ReadTagMemWords(Tf, (ModuleTech.Gen2.MemBank)BankcomboBox.SelectedIndex, strblock, blockcnt);
                    string readdatastr = "";
                    for (int i = 0; i < readdata.Length; ++i)
                        readdatastr += readdata[i].ToString("X4");
                    DatatextBox.Text = readdatastr;

                    OpStatuslabel.Text = "读取成功";
                }
                else
                {
                    int strblock = int.Parse(StartAddrtextBox.Text);
                    int blockcnt = int.Parse(BlockCounttextBox.Text);


                    SimpleReadPlan srp = new SimpleReadPlan(TagProtocol.ISO180006B,new int[] { Sp.Opant });
                    Rd.ParamSet("ReadPlan", srp);

                    TagReadData[] trds = Rd.Read(500);
                    if (trds.Length > 0)
                    {
                        TagFilter tf = new ModuleTech.ISO180006b.ISO180006bTagData(trds[0].EPC);
                        Rd.ParamSet("TagopProtocol", TagProtocol.ISO180006B);
                        byte[] data = Rd.ReadTagMemBytes(tf, 0, strblock, blockcnt);
                        DatatextBox.Text = ByteFormat.ToHex(data);
                        OpStatuslabel.Text = "读取成功";
                    }
                    else
                    OpStatuslabel.Text = "没发现6B标签";
                }
               
            }
            catch (System.Exception ex)
            {
                string msg=string.Empty;
                if (ex is ModuleLibrary.FatalInternalException)
                    msg = Convert.ToString(((ModuleLibrary.FatalInternalException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.HardwareAlertException)
                    msg = Convert.ToString(((ModuleLibrary.HardwareAlertException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.ModuleException)
                    msg = Convert.ToString(((ModuleLibrary.ModuleException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.OpFaidedException)
                    msg = Convert.ToString(((ModuleLibrary.OpFaidedException)ex).ErrCode, 16);
                OpStatuslabel.Text = ex.Message+" :"+msg;
                return;
            }
        }

        private void WriteMembutton_Click(object sender, EventArgs e)
        {
            try
            {
                OpStatuslabel.Text = "";
                Application.DoEvents();
                if (BatteryReportV())
                {
                    MessageBox.Show("电量过低，请及时充电");
                    return;
                }


                SetOpAnt();

                if (IsValidHexstr(this.DatatextBox.Text.Trim(), 600) != 0)
                {
                    throw new Exception("将要写入的数据是16进制的字符,且长度为4字符的整数倍");
                    
                }

                if(Tp==TagProtocol.GEN2)
                {
                    if (BankcomboBox.SelectedIndex == -1)
                    {
                        throw new Exception("请选择区");
                    }
                    Rd.ParamSet("TagopProtocol", TagProtocol.GEN2);

                    int strblock = int.Parse(StartAddrtextBox.Text);
                    if (!CheckDatarule(DatatextBox.Text) || DatatextBox.Text.Length % 4 != 0)
                    {
                        throw new Exception("输入十六进制且长度为4倍数");
                    }
                    if (BankcomboBox.SelectedIndex == 1 & strblock < 2)
                        throw new Exception("EPC区从第二块开始写");


                    ushort[] writedata = new ushort[this.DatatextBox.Text.Trim().Length / 4];

                    for (int a = 0; a < writedata.Length; ++a)
                        writedata[a] = ushort.Parse(this.DatatextBox.Text.Trim().Substring(a * 4, 4), System.Globalization.NumberStyles.AllowHexSpecifier);
                    Rd.WriteTagMemWords(Tf, (ModuleTech.Gen2.MemBank)BankcomboBox.SelectedIndex, strblock, writedata);
                }
                else
                {
                    int strblock = int.Parse(StartAddrtextBox.Text);
                    //int blocknum = int.Parse(Block_textBox.Text);

                    if (strblock < 8)
                    {
                        throw new Exception("从第8块开始");
                    }

                    SimpleReadPlan srp = new SimpleReadPlan(TagProtocol.ISO180006B, new int[] { Sp.Opant });
                    Rd.ParamSet("ReadPlan", srp);

                    TagReadData[] trds = Rd.Read(500);
                    if (trds.Length > 0)
                    {


                        TagFilter tf =new ModuleTech.ISO180006b.ISO180006bTagData(trds[0].EPC);
                        Rd.ParamSet("TagopProtocol", TagProtocol.ISO180006B);

                        byte[] data = ByteFormat.FromHex(DatatextBox.Text);

                        Rd.WriteTagMemBytes(tf, 0, strblock, data);
                    }
 
                }
                OpStatuslabel.Text = "写入成功";
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
                OpStatuslabel.Text = ex.Message + " :" + msg;
                return;
            }
        }

        private void LockMembutton_Click(object sender, EventArgs e)
        {
            try
            {
                OpStatuslabel.Text = "";
                Application.DoEvents();
                if (BatteryReportV())
                {
                    MessageBox.Show("电量过低，请及时充电");
                    return;
                }

                    
                SetOpAnt();

                if (Tp == TagProtocol.GEN2)
                {

                    if (LockBankcomboBox.SelectedIndex == -1)
                    {
                        throw new Exception("请选择区");
                    }
                    if (LockTypecomboBox.SelectedIndex == -1)
                        throw new System.Exception("请选锁类型");           

                    Gen2LockAct[] act = new Gen2LockAct[1];
                    Gen2LockAct[] act2 = new Gen2LockAct[2];
                    switch (this.LockBankcomboBox.SelectedIndex)
                    {
                        case 0:
                            {
                                if (this.LockTypecomboBox.SelectedIndex == 0)
                                    act[0] = Gen2LockAct.ACCESS_UNLOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 1)
                                    act[0] = Gen2LockAct.ACCESS_LOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 2)
                                {
                                    act2[0] = Gen2LockAct.ACCESS_PERMALOCK;
                                    act2[1] = Gen2LockAct.ACCESS_LOCK;

                                }
                                break;
                            }
                        case 1:
                            {
                                if (this.LockTypecomboBox.SelectedIndex == 0)
                                    act[0] = Gen2LockAct.KILL_UNLOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 1)
                                    act[0] = Gen2LockAct.KILL_LOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 2)
                                {
                                    act2[0] = Gen2LockAct.KILL_PERMALOCK;
                                    act2[1] = Gen2LockAct.KILL_LOCK;
                                }
                                break;
                            }
                        case 2:
                            {
                                if (this.LockTypecomboBox.SelectedIndex == 0)
                                    act[0] = Gen2LockAct.EPC_UNLOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 1)
                                    act[0] = Gen2LockAct.EPC_LOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 2)
                                {
                                    act2[0] = Gen2LockAct.EPC_PERMALOCK;
                                    act2[1] = Gen2LockAct.EPC_LOCK;
                                }
                                break;
                            }
                        case 3:
                            {
                                if (this.LockTypecomboBox.SelectedIndex == 0)
                                    act[0] = Gen2LockAct.TID_UNLOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 1)
                                    act[0] = Gen2LockAct.TID_LOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 2)
                                {
                                    act2[0] = Gen2LockAct.TID_PERMALOCK;
                                    act2[1] = Gen2LockAct.TID_LOCK;
                                }
                                break;
                            }
                        case 4:
                            {
                                if (this.LockTypecomboBox.SelectedIndex == 0)
                                    act[0] = Gen2LockAct.USER_UNLOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 1)
                                    act[0] = Gen2LockAct.USER_LOCK;
                                else if (this.LockTypecomboBox.SelectedIndex == 2)
                                {
                                    act2[0] = Gen2LockAct.USER_PERMALOCK;
                                    act2[1] = Gen2LockAct.USER_LOCK;
                                }
                                break;
                            }
                    }
                    Gen2LockAction action = null;
                    if (this.LockTypecomboBox.SelectedIndex == 2)
                        action = new Gen2LockAction(act2);
                    else
                        action = new Gen2LockAction(act);

                    Rd.LockTag(Tf, action);
                    OpStatuslabel.Text = "锁定成功";
                }
                else
                {
                    SimpleReadPlan srp = new SimpleReadPlan(TagProtocol.ISO180006B, new int[] { Sp.Opant });
                    Rd.ParamSet("ReadPlan", srp);

                    TagReadData[] trds = Rd.Read(500);
                    if (trds.Length > 0)
                    {
                        Rd.LockTag(new ModuleTech.ISO180006b.ISO180006bTagData(ByteFormat.FromHex(trds[0].EPCString)), new ModuleTech.ISO180006b.ISO180006bLockAction(int.Parse(StartAddrtextBox.Text)));
                        OpStatuslabel.Text = "锁定成功";
                    }
                }

                    
            }
            catch (System.Exception ex)
            {
                string msg = string.Empty;
                if (ex is ModuleLibrary.FatalInternalException)
                    msg = Convert.ToString(((ModuleLibrary.FatalInternalException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.HardwareAlertException)
                    msg = Convert.ToString(((ModuleLibrary.HardwareAlertException)ex).ErrCode,16);
                if (ex is ModuleLibrary.ModuleException)
                    msg = Convert.ToString(((ModuleLibrary.ModuleException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.OpFaidedException)
                    msg = Convert.ToString(((ModuleLibrary.OpFaidedException)ex).ErrCode, 16);
                OpStatuslabel.Text = ex.Message + " :" + msg;

                return;
            }
        }

        private void WriteTagbutton_Click(object sender, EventArgs e)
        {
            try
            {
                OpStatuslabel.Text = "";
                Application.DoEvents();
                if (BatteryReportV())
                {
                    MessageBox.Show("电量过低，请及时充电");
                    return;
                }


                SetOpAnt();
                if (IsValidHexstr(this.DatatextBox.Text.Trim(), 600) != 0)
                {
                    throw new Exception("将要写入的数据是16进制的字符,且长度为4字符的整数倍");

                }

                if (Tp == TagProtocol.ISO180006B)
                    throw new Exception("请选择Gen2协议");

                Rd.WriteTag(Tf, new TagData(this.DatatextBox.Text.Trim()));

                OpStatuslabel.Text = "初始化成功";
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
                OpStatuslabel.Text = ex.Message + " :" + msg;

                return;
            }
        }

        private void SetOpAnt()
        {
            try
            {
                 
                if (Ant1radioButton.Checked)
                    Sp.Opant = 1;
                else if (Ant2radioButton.Checked)
                    Sp.Opant = 2;
                else if (Ant3radioButton.Checked)
                    Sp.Opant = 3;
                else
                    Sp.Opant = 4;

                Rd.ParamSet("TagopAntenna", Sp.Opant);
                
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private void RandomcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RandomcomboBox.SelectedIndex == -1)
            {
                MessageBox.Show("请选择随机数长度");
                return;
            }
            DatatextBox.Text = ReturnRandom(false, int.Parse(RandomcomboBox.Text.Substring(2,RandomcomboBox.Text.Length-2)), 0, 16);
        }
        private string ReturnRandom(bool vlength, int maxlength, int min, int max)
        {
            Random r = new Random(unchecked((int)DateTime.Now.Ticks));
            int temp = maxlength;
            if (vlength)
                temp = r.Next(1, maxlength + 1);

            return ReturnRandom(temp, min, max);
        }
        private string ReturnRandom(int length, int min, int max)
        {
            if (min < 0 || min > 16 || max < 1 || max > 16)
            {
                throw new Exception("min:0-16,max:1-16");
            }
            Random r = new Random(unchecked((int)DateTime.Now.Ticks));
            string back = string.Empty;
            int temp;
            for (int i = 0; i < length; i++)
            {
                temp = r.Next(min, max);
                if (temp >= 10)
                {
                    switch (temp)
                    {
                        case 10: back += "A"; break;
                        case 11: back += "B"; break;
                        case 12: back += "C"; break;
                        case 13: back += "D"; break;
                        case 14: back += "E"; break;
                        case 15: back += "F"; break;

                    }
                }
                else
                    back += temp.ToString();
            }
            return back;
        }
        public static int IsValidHexstr(string str, int len)
        {
            if (str == "")
                return -3;
            if (str.Length % 4 != 0)
                return -2;
            if (str.Length > len)
                return -4;
            string lowstr = str.ToLower();
            byte[] hexchars = Encoding.ASCII.GetBytes(lowstr);

            foreach (byte a in hexchars)
            {
                if (!((a >= 48 && a <= 57) || (a >= 97 && a <= 102)))
                    return -1;
            }
            return 0;
        }

        private void HighSignbutton_Click(object sender, EventArgs e)
        {
            if (Sp.RunType != 0)
            {
                MessageBox.Show("非调试模式");
                return;
            }
            Pc.PowerUP();
        }

        private void LowSignbutton_Click(object sender, EventArgs e)
        {
            if (Sp.RunType != 0)
            {
                MessageBox.Show("非调试模式");
                return;
            }
            Pc.PowerLow();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {  
            if(tabControl1.SelectedIndex==5)
            {
                try
                {
                    mtex.WaitOne();
                    int tempt=(Byte)Rd.ParamGet("Temperature");
                   
                    Templabel1.Text = tempt.ToString();
                   
                }
                catch (System.Exception ex)
                {
                	
                }
                finally
                {
                    mtex.ReleaseMutex(); 
                }

                try
                {
                   Batterylabel.Text=Battery.GetBatteryLifePercent().ToString();
                }
                catch// (System.Exception ex)
                {
                	
                }
            }
        }

        private void Setusuallybutton_Click(object sender, EventArgs e)
        {
            MultiReadPlan testMultiReadPlan = null;
            List<SimpleReadPlan> readPlans = new List<SimpleReadPlan>();
            SimpleReadPlan plan1 = null;
            SimpleReadPlan plan2 = null;
            SimpleReadPlan plan3 = null;
            SimpleReadPlan plan4 = null;
            try
            {
                if (Gen2checkBox.Checked)
                {
                    Tp = TagProtocol.GEN2;
                    Rd.ParamSet("TagopProtocol", TagProtocol.GEN2);
                    if (ISO6BcheckBox.Checked)
                    {
                      
                        if (Sp.Connectants == null)
                        {
                            plan1 = new SimpleReadPlan(TagProtocol.GEN2, new int[] { Sp.Opant },30);
                            plan2 = new SimpleReadPlan(TagProtocol.ISO180006B, new int[] { Sp.Opant },30);
                        }
                        else
                        {
                            plan1 = new SimpleReadPlan(TagProtocol.GEN2, Sp.Connectants,30);
                            plan2 = new SimpleReadPlan(TagProtocol.ISO180006B, Sp.Connectants,30);
                        }
                        readPlans.Add(plan1);
                        readPlans.Add(plan2);
                        //testMultiReadPlan = new MultiReadPlan(readPlans.ToArray());
                        //Rd.ParamSet("ReadPlan", testMultiReadPlan);

                        Sp.Protocol = 2;
                    }
                    else
                    {
                        if (Sp.Connectants == null)
                        {
                            plan1 = new SimpleReadPlan(TagProtocol.GEN2, new int[] { Sp.Opant },30);
                        }
                        else
                            plan1 = new SimpleReadPlan(TagProtocol.GEN2, Sp.Connectants,30);
                        //Rd.ParamSet("ReadPlan", plan1);
                        readPlans.Add(plan1);
                        Sp.Protocol = 0;

                    }
                    Protocollabel1.Text = "协议:Gen2";
                }
                else
                {
                    if (ISO6BcheckBox.Checked)
                    {
                        Tp = TagProtocol.ISO180006B;
                        Rd.ParamSet("TagopProtocol", TagProtocol.ISO180006B);
                        
                        if (Sp.Connectants == null)
                        {
                            plan1 = new SimpleReadPlan(TagProtocol.ISO180006B, new int[] { Sp.Opant },30);
                        }
                        else
                            plan1 = new SimpleReadPlan(TagProtocol.ISO180006B, Sp.Connectants,30);
                        readPlans.Add(plan1);
                        //Rd.ParamSet("ReadPlan", plan1);
                        Protocollabel1.Text = "协议:6B";

                        Sp.Protocol = 1;
                    }
                    else
                    {
                        if (!ipx64checkBox.Checked && !ipx256checkBox.Checked)
                        {
                            MessageBox.Show("选择协议");
                            return;
                        }
                    }
                }

                if(ipx64checkBox.Checked)
                {
                    if (Sp.Connectants == null)
                    {
                        plan3 = new SimpleReadPlan(TagProtocol.IPX64, new int[] { Sp.Opant }, 30);
                       
                    }
                    else
                    {
                        plan3 = new SimpleReadPlan(TagProtocol.IPX64, Sp.Connectants, 30);
                         
                    }
                    readPlans.Add(plan3);
                }

                if (ipx256checkBox.Checked)
                {
                    if (Sp.Connectants == null)
                    {
                        plan4 = new SimpleReadPlan(TagProtocol.IPX256, new int[] { Sp.Opant }, 30);

                    }
                    else
                    {
                        plan4 = new SimpleReadPlan(TagProtocol.IPX256, Sp.Connectants, 30);

                    }
                    readPlans.Add(plan4);
                }

                if(readPlans.Count>1)
                {
                    testMultiReadPlan = new MultiReadPlan(readPlans.ToArray());
                    Rd.ParamSet("ReadPlan", testMultiReadPlan);
                }
                else
                {
                    SimpleReadPlan srps = readPlans[0];
                    Rd.ParamSet("ReadPlan", srps);
                }



                int temp = int.Parse(TimeouttextBox.Text);
                Sp.Timeout = TimeouttextBox.Text;
                temp = int.Parse(IntervaltextBox.Text);
                Sp.Intenval = IntervaltextBox.Text;

                if (SessioncomboBox.SelectedIndex == -1)
                    throw new Exception("没有选择会话模式");
                if (Gen2QcomboBox.SelectedIndex == -1)
                    throw new Exception("没有选择Q值");
                
                    Sp.Session = SessioncomboBox.SelectedIndex;
                    Rd.ParamSet("Gen2Session", (ModuleTech.Gen2.Session)Sp.Session);
                
                    Sp.Gen2q = Gen2QcomboBox.SelectedIndex;

                   //if ((ReaderType)Sp.ReaderType == ReaderType.M1S||(ReaderType)Sp.ReaderType==ReaderType.M2S)
                    //if ((ReaderType)Sp.ReaderType == ReaderType.MT100)
                    //{
                        if (BlockwriteradioButton.Checked)
                        {
                            Rd.ParamSet("Gen2WriteMode", ModuleTech.Gen2.WriteMode.BLOCK_ONLY);
                        }
                        else
                        {
                            Rd.ParamSet("Gen2WriteMode", ModuleTech.Gen2.WriteMode.WORD_ONLY);
                        }
                    //}

                    if (AutoStopcomboBox.SelectedIndex == -1)
                    {
                        Sp.IsAutoStop = false;
                    }
                    else
                    {
                        Sp.IsAutoStop = true;
                        Sp.Seconds = AutoStopcomboBox.SelectedIndex * 60;
                    }

                    int epclen = 96;

                    if (bits496checkBox.Checked)
                        epclen = 496;

                    try
                    {
                        //if(Rd.HwDetails.module!=Reader.Module_Type.MODOULE_M6E_MICRO)
                        Rd.ParamSet("MaxEPCLength", epclen);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("设置最大EPC长度失败，请确认是否支持 ");
                    }

                    try
                    {
                       
                        if ( Sp.ReaderType == 9)//ModuleTech.ReaderType.MT100)
                        {
                            if (checkBox_mt100sig.Checked)
                                Rd.ParamSet("InventoryMode", 1);
                            else
                                Rd.ParamSet("InventoryMode", 0);
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }

                    MessageBox.Show("设置完成");
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
                MessageBox.Show(ex.Message+" :"+msg);
                TimeouttextBox.Text = Sp.Timeout;
                IntervaltextBox.Text = Sp.Intenval;
            }
            
        }

        private void AntParamSetbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(types[Sp.ReaderType] == ReaderType.M6_A7_FOURANTS && Sp.AntType == 2))
                {
                    if (AntConnectedcheckBox.Checked)
                    {
                        Rd.ParamSet("CheckAntConnection", true);
                        Sp.CheckAnt = true;
                    }
                    else
                    {
                        Rd.ParamSet("CheckAntConnection", false);
                        Sp.CheckAnt = false;
                    }
                }

                int[] powread=new int[Sp.AntType];
                int[] powwrite=new int[Sp.AntType];
                AntPower[] apwrs = new AntPower[Sp.AntType];

                List<int> lrpow = new List<int>();
                List<int> lwpow = new List<int>();

                lrpow.Add(int.Parse(Ant1ReadPowertextBox.Text));
                lrpow.Add(int.Parse(Ant2ReadPowertextBox.Text));
                lrpow.Add(int.Parse(Ant3ReadPowertextBox.Text));
                lrpow.Add(int.Parse(Ant4ReadPowertextBox.Text));

                lwpow.Add(int.Parse(Ant1WritePowertextBox.Text));
                lwpow.Add(int.Parse(Ant2WritePowertextBox.Text));
                lwpow.Add(int.Parse(Ant3WritePowertextBox.Text));
                lwpow.Add(int.Parse(Ant4WritePowertextBox.Text));

                for (int i = 0; i < powread.Length;i++ )
                {
                    powread[i] = lrpow[i];
                    powwrite[i] = lwpow[i];
                    apwrs[i].AntId = (byte)(i + 1);
                    apwrs[i].ReadPower = (ushort)powread[i];
                    apwrs[i].WritePower=(ushort)powwrite[i];
                }

                Rd.ParamSet("AntPowerConf", apwrs);

                for (int i = 0; i < powread.Length;i++ )
                {
                    Sp.ReadPw[i] = powread[i].ToString();
                    Sp.WritePw[i] = powwrite[i].ToString();

                }
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
 
                MessageBox.Show(ex.Message+" :"+msg);
                return;
            }
            MessageBox.Show("设置完成");
        }

        private void SetRegionbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (RegioncomboBox.SelectedIndex == -1)
                    throw new System.Exception("未选区域");

                ModuleTech.Region rg = ModuleTech.Region.UNSPEC;
                bool is840_845 = false;

//未定
//加拿大
//欧洲1
//欧洲2
//欧洲3
//印度
//韩国
//日本
//北美
//中国
//全频段
//840-845
                switch (this.RegioncomboBox.SelectedIndex)
                {
                    case 0:
                        rg = ModuleTech.Region.UNSPEC;
                        break;
                    case 1:
                        rg = ModuleTech.Region.CN;
                        break;
                    case 2:
                        rg = ModuleTech.Region.EU;
                        break;
                    case 3:
                        rg = ModuleTech.Region.EU2;
                        break;
                    case 4:
                        rg = ModuleTech.Region.EU3;
                        break;
                    case 5:
                        rg = ModuleTech.Region.IN;
                        break;
                    case 6:
                        rg = ModuleTech.Region.KR;
                        break;
                    case 7:
                        rg = ModuleTech.Region.JP;
                        break;
                    case 8:
                        rg = ModuleTech.Region.NA;
                        break;
                    case 9:
                        rg = ModuleTech.Region.PRC;
                        break;
                    case 10:
                        rg = ModuleTech.Region.OPEN;
                        break;
                    case 11:
                        rg = ModuleTech.Region.OPEN;
                        is840_845 = true;
                        break;
                }


                if ((types[Sp.ReaderType] != ReaderType.MT100) || types[Sp.ReaderType] == ReaderType.MT100 && rg != ModuleTech.Region.OPEN)
                { Rd.ParamSet("Region", rg);}
                else
                {
                    if (!is840_845)
                    {
                        MessageBox.Show("mt100 不支持全频段");
                        return;
                    }
                }

                if (is840_845)
                {
                    List<uint> htab = new List<uint>();
                    htab.Add(840625);
                    htab.Add(840875);
                    htab.Add(841125);
                    htab.Add(841375);
                    htab.Add(841625);
                    htab.Add(841875);
                    htab.Add(842125);
                    htab.Add(842375);
                    htab.Add(842625);
                    htab.Add(842875);
                    htab.Add(843125);
                    htab.Add(843375);
                    htab.Add(843625);
                    htab.Add(843875);
                    htab.Add(844125);
                    htab.Add(844375);
                    Rd.ParamSet("FrequencyHopTable", htab.ToArray());
                    Sp.Frequencys = new string[htab.Count];
                    Sp.CustomFrequency = true;
                    for (int i = 0; i < htab.Count; i++)
                    {
                        Sp.Frequencys[i] = htab[i].ToString();
                    }
                }
                else
                {
                    Sp.CustomFrequency = false;
                }
                
                Sp.Region = RegioncomboBox.SelectedIndex;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("设置完成");
        }

        private void SelectAllbutton_Click(object sender, EventArgs e)
        {
            
                for (int i = 0; i < FrequencylistView.Items.Count;i++ )
                {
                    FrequencylistView.Items[i].Checked=true;
                }
            
        }

        private void SelectNonebutton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < FrequencylistView.Items.Count; i++)
            {
                FrequencylistView.Items[i].Checked = false;
            }
        }

        private void SetFrenquencybutton_Click(object sender, EventArgs e)
        {
            try
            {
                List<uint> htb = new List<uint>();
                foreach (ListViewItem item in FrequencylistView.Items)
                {
                    if (item.Checked)
                        htb.Add(uint.Parse(item.SubItems[0].Text));
                }


                Rd.ParamSet("FrequencyHopTable", htb.ToArray());
                Sp.Frequencys = new string[htb.Count];
                Sp.CustomFrequency = true;
                for (int i = 0; i < htb.Count;i++ )
                {
                    Sp.Frequencys[i] = htb[i].ToString();
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("设置完成");
        }

        private void Setfilterbutton_Click(object sender, EventArgs e)
        {
            try
            {
                if(FiltercheckBox.Checked)
                {
                     
                    int ret = IsValidBinaryStr(this.FilterDatatextBox.Text.Trim());
                    switch (ret)
                    {
                        case -3:
                            MessageBox.Show("匹配数据不能为空");
                            break;
                        case -1:
                            MessageBox.Show("匹配数据只能是二进制字符串");
                            break;

                    }

                    if (ret != 0)
                        return;
                    if (this.FilterBankcomboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择过滤bank");
                        return;
                    }

                    if (this.MatchcomboBox.SelectedIndex == -1)
                    {
                        MessageBox.Show("请输入过滤规则");
                        return;
                    }

                    int bitaddr = 0;
                    if (this.FilterStartAddrtextBox.Text.Trim() == "")
                    {
                        MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                        return;
                    }
                    else
                    {
                        try
                        {
                            bitaddr = int.Parse(this.FilterStartAddrtextBox.Text.Trim());
                        }
                        catch// (Exception exc)
                        {
                            MessageBox.Show("起始地址请输入数字");
                            return;
                        }
                        if (bitaddr < 0)
                        {
                            MessageBox.Show("地址必须大于零");
                            return;
                        }
                    }

                    byte[] filterbytes = new byte[(this.FilterDatatextBox.Text.Trim().Length - 1) / 8 + 1];
                    for (int c = 0; c < filterbytes.Length; ++c)
                        filterbytes[c] = 0;

                    int bitcnt = 0;
                    foreach (Char ch in this.FilterDatatextBox.Text.Trim())
                    {
                        if (ch == '1')
                            filterbytes[bitcnt / 8] |= (byte)(0x01 << (7 - bitcnt % 8));
                        bitcnt++;

                    }

                     Tf= new Gen2TagFilter(this.FilterDatatextBox.Text.Trim().Length, filterbytes,
                        (MemBank)this.FilterBankcomboBox.SelectedIndex + 1, bitaddr,
                        this.MatchcomboBox.SelectedIndex == 0 ? false : true);
                }
                else
                {
                    Tf = null;
                    
                }
                Rd.ParamSet("Singulation", Tf);
                if(AccessPwdcheckBox.Checked)
                {
                     
                    int ret = IsValidPasswd(this.PasswordtextBox.Text.Trim());
                    {
                        switch (ret)
                        {
                            case -3:
                                MessageBox.Show("访问密码不能为空");
                                break;
                            case -2:
                            case -4:
                                MessageBox.Show("访问密码必须是8个16进制数");
                                break;
                            case -1:
                                MessageBox.Show("访问密码只能是16进制数字");
                                break;

                        }
                    }
                    if (ret != 0)
                        return;
                    else
                    {
                        uint passwd = uint.Parse(this.PasswordtextBox.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        Rd.ParamSet("AccessPassword", passwd);

                        //设置 
                        EmbededCmdData ecd = new EmbededCmdData(MemBank.RESERVED, (UInt32)2,4);
                        Rd.ParamSet("EmbededCmdOfInventory", ecd);
                    }
                }
                else
                    Rd.ParamSet("AccessPassword", (uint)0);

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
 
                MessageBox.Show(ex.Message+" :"+msg);
                return;
            }
            MessageBox.Show("设置完成");
        }

        private void SaveParambutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (AutoRuncheckBox.Checked)
                    Sp.RunAuto = true;
                else
                    Sp.RunAuto = false;

                if (BatterycheckBox.Checked)
                {
                    Sp.IsReport = true;
                    if (Sp.BatteryReport < 5)
                        Sp.BatteryReport = 5;
                }
                else
                { Sp.IsReport = false; Sp.BatteryReport = 0; }

                switch (SleeptimeoutcomboBox.SelectedIndex)
                {
                    case 0:
                        Sp.Sleep = 0;
                        break;
                    case 1:
                        Sp.Sleep = 60;
                        break;
                    case 2:
                        Sp.Sleep = 120;
                        break;
                    case 3:
                        Sp.Sleep = 180;
                        break;
                    case 4:
                        Sp.Sleep = 240;
                        break;
                    case 5:
                        Sp.Sleep = 300;
                        break;
                    case 6:
                        Sp.Sleep = 600;
                        break;
                    case 7:
                        Sp.Sleep = 0;
                        break;
                }
                 
                RegEdit.WTRegedit(Sp.Sleep.ToString());
                SetFile.MendParams(Sp);
                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("保存失败:" + ex.Message);
                return;
            }
            MessageBox.Show("保存成功");

        }
        public static int IsValidBinaryStr(string str)
        {
            if (str == "")
                return -3;

            foreach (Char a in str)
            {
                if (!((a == '1') || (a == '0')))
                    return -1;
            }
            return 0;

        }

        public static int IsValidPasswd(string passwd)
        {
            int ret = IsValidHexstr(passwd, 8);
            if (ret == 0)
            {
                if (passwd.Length != 8)
                    return -4;
            }

            return ret;
        }

       
        private void GetFrequencybutton_Click(object sender, EventArgs e)
        {
            try
            {
                FrequencylistView.Items.Clear();
               uint[] fres=(uint[])Rd.ParamGet("FrequencyHopTable");
               Sort(ref fres);

               for (int i = 0; i < fres.Length;i++ )
               {
                   ListViewItem lvi = new ListViewItem(fres[i].ToString());
                   lvi.Checked = true;
                   FrequencylistView.Items.Add(lvi);
               }
               FrequencylistView.Refresh();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
             
        }
        public void Sort(ref uint[] array)
        {
            uint tmpIntValue = 0;
            for (int xIndex = 0; xIndex < array.Length; xIndex++)
            {
                for (int yIndex = 0; yIndex < array.Length; yIndex++)
                {
                    if (array[xIndex] < array[yIndex])
                    {
                        tmpIntValue = array[xIndex];
                        array[xIndex] = array[yIndex];
                        array[yIndex] = tmpIntValue;
                    }
                }
            }
        }

        private void OpenCombutton_Click(object sender, EventArgs e)
        {
            if (Sp.RunType != 0)
            {
                MessageBox.Show("非调试模式");
                return;
            }
            
            try
            {
                Sc = new SerialClass(this.ComtextBox.Text, int.Parse(this.BaudtextBox.Text));
                Sc.OpenCom();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Closebutton.Enabled = true;
            OpenCombutton.Enabled = false;
        }

        private void Closebutton_Click(object sender, EventArgs e)
        {
            if (Sp.RunType != 0)
            {
                MessageBox.Show("非调试模式");
                return;
            }
            try
            {
               
                Sc.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Closebutton.Enabled = false;
            OpenCombutton.Enabled = true;
        }

        private void Sendbutton_Click(object sender, EventArgs e)
        {
            if (Sp.RunType != 0)
            {
                MessageBox.Show("非调试模式");
                return;
            }

            if(!Sc.IsOpen)
            {
                MessageBox.Show("未打开COM口");
                return;
            }
            try
            {
                SendFun();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
           
        }
        private void SendFun()
        {
            try
            {
                byte[] hexsenddata = Sc.FromHex(SendDatatextBox.Text);
                Sc.SendData(hexsenddata);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
           
        }
        private void Handlefun(string msg)
        {
            RevdDatatextBox.Text += msg;

            label_rev.Text = RevdDatatextBox.Text.Length.ToString();
        }
        delegate void deleFun(string msg);

        int readcount=1;
        private void RevFun()
        {
            while (Reving)
            {
                try
                {
                    //byte[] databyte = Sc.ReadDataAvaiblie();
                    byte[] databyte = Sc.ReadDataByte(readcount);
                    if (databyte.Length > 0)
                    {
                        string hexstr = Sc.ToHex(databyte);
                        this.Invoke(new deleFun(Handlefun), new object[] { hexstr });
                    }

                }
                catch(System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("RevFun:"+ex.Message);
                }

                Thread.Sleep(100);
            }

        }

        private void Revbutton_Click(object sender, EventArgs e)
        {
            if (Sp.RunType != 0)
            {
                MessageBox.Show("非调试模式");
                return;
            }
            try
            {
                readcount = int.Parse(textBox_readcount.Text);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("读个数异常");
                return;
            }
            if (Revbutton.Text =="接收(hex)")
            {
                Reving = true;
                RevThread = new Thread(new ThreadStart(RevFun));
                RevThread.Start();

                label_rev.Text = "";
                Revbutton.Text = "停止";
            }
            else
            {
                Reving = false;
                Thread.Sleep(50);
                if (RevThread != null)
                    RevThread.Join();

                Revbutton.Text = "接收(hex)";
            }


        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int icode = (int)e.KeyCode;
            switch (Sp.PdaType)
            {
                case 0:
                    {
                        if (icode == 239)
                        {
                            if (Startbutton.Enabled)
                            {
                                Startbutton_Click(sender, e);
                            }
                            else
                                Stopbutton_Click(sender, e);
                        }
                        break;
                    }
                case 2:
                    if (icode == 17)
                        if (Startbutton.Enabled)
                        {
                            Startbutton_Click(sender, e);
                        }
                        else
                            Stopbutton_Click(sender, e);
                    break;
                case 1:
                    if (icode == 135)
                        if (Startbutton.Enabled)
                        {
                            Startbutton_Click(sender, e);
                        }
                        else
                            Stopbutton_Click(sender, e);
                    break;

                case 4:
                    if (icode == 135)
                        if (Startbutton.Enabled)
                        {
                            Startbutton_Click(sender, e);
                        }
                        else
                            Stopbutton_Click(sender, e);
                    break;
                case 7:
                    if (icode == 121)
                        if (Startbutton.Enabled)
                        {
                            Startbutton_Click(sender, e);
                        }
                        else
                            Stopbutton_Click(sender, e);
                    break;
                case 8:
                    if (icode == 238)
                        if (Startbutton.Enabled)
                        {
                            Startbutton_Click(sender, e);
                        }
                        else
                            Stopbutton_Click(sender, e);
                    break;
            }
           
        }

        //private void MainForm_KeyUp(object sender, KeyEventArgs e)
        //{
        //    int icode = (int)e.KeyCode;
        //    switch (Sp.PdaType)
        //    {
        //        case 0:

        //        case 1:
        //            if (icode == 135)
        //                if (Stopbutton.Enabled)
        //                {
        //                    Stopbutton_Click(sender, e);
        //                }
        //            break;
        //        case 4:
        //            if (icode == 135)
        //                if (Stopbutton.Enabled)
        //                {
        //                    Stopbutton_Click(sender, e);
        //                }
        //            break;
                    
        //    }
            
        //}

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            EndThread();
            if(Rd!=null)
            Rd.Disconnect();

            if(Pc!=null)
            Pc.PowerLow();
            //if(Btfrm!=null)
            //Btfrm.Dispose_BlueTooth();

            //dkg
            //isPD = false;
            //if (PowerDown != null)
            //    PowerDown.Join();
            //Win32.CloseHandle(hEvent[0]);
            if(PM!=null)
            PM.Stop();
        }

        private void MtradioButton_CheckedChanged(object sender, EventArgs e)
        {
            SendDatatextBox.Text = "FF00031D0C"; 
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SendDatatextBox.Text = "AA00451D4A"; 
        }

        private void Clearbutton_Click(object sender, EventArgs e)
        {
            RevdDatatextBox.Text = "";
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //EndThread();
            //this.Invoke(new SetFun(setcontrls));
            //Rd.Disconnect();
            Device.SuspendHold(true);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //LoadForm Lf = new LoadForm();
            //Lf.dlog = dlog;
            //Lf.mf = this;
            //Lf.ShowDialog();
            ////Rd=Lf.ReaderO;
            //Lf.Close();
            //Lf.Dispose();
            Device.SuspendHold(false);
        }

        private void btnChangeEAS_Click(object sender, EventArgs e)
        {
           
            if ((!this.rbEASset.Checked) && (!this.rbEASreset.Checked))
            {
                MessageBox.Show("请选择EAS状态");
                return;
            }
            SetOpAnt();
            bool isSet = false;

            if (this.rbEASset.Checked)
                isSet = true;
            else
                isSet = false;


           
            byte[] pwd = null;

            int ret = IsValidPasswd(this.tbaccesspasswd.Text.Trim());
            {
                switch (ret)
                {
                    case -3:
                        MessageBox.Show("访问密码不能为空");
                        break;
                    case -2:
                    case -4:
                        MessageBox.Show("访问密码必须是8个16进制数");
                        break;
                    case -1:
                        MessageBox.Show("访问密码只能是16进制数字");
                        break;

                }
            }
            if (ret != 0)
                return;
            else
            {
                pwd = ByteFormat.FromHex(this.tbaccesspasswd.Text.Trim());
            }
          
            NXP_ChangeEASPara ChEasPara = new NXP_ChangeEASPara(pwd, isSet);

            try
            {
                Rd.CustomCmd(Tf, CustomCmdType.NXP_ChangeEAS, ChEasPara);
            }
            catch (OpFaidedException notagexp)
            {
                if (notagexp.ErrCode == 0x400)
                    MessageBox.Show("没法发现标签");
                else
                    MessageBox.Show("操作失败:" + notagexp.ToString());

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ChangeEAS失败:" + ex.ToString());
                return;
            }

            if (this.rbEASset.Checked)
                MessageBox.Show("ChangeEAS set成功");
            else
            MessageBox.Show("ChangeEAS reset成功");
           
        }

        private void btnEASAlarm_Click(object sender, EventArgs e)
        {
            SetOpAnt();
            
            try
            {
                NXP_EASAlarmPara EasAlarmPara = new NXP_EASAlarmPara(0x01, 0x02, 0x01);
                NXP_EASAlarmResult result = (NXP_EASAlarmResult)Rd.CustomCmd(CustomCmdType.NXP_EASAlarm, EasAlarmPara);
                tbEASAlarmData.Text = ByteFormat.ToHex(result.EASAlarmData);
            }
            catch (OpFaidedException notagexp)
            {
                if (notagexp.ErrCode == 0x400)
                    MessageBox.Show("没法发现标签");
                else
                    MessageBox.Show("操作失败:" + notagexp.ToString());

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("EASAlarm失败:" + ex.ToString());
                return;
            }
        }
        bool isEASDetect = false;
        private void btnEASDetect_Click(object sender, EventArgs e)
        {
            if (!isEASDetect)
            {
                SetOpAnt();

                this.btnEASDetect.Text = "停止探测";
                Rd.ParamSet("OpTimeout", (ushort)250);
                this.timer1.Enabled = true;
                isEASDetect = true;
            }
            else
            {
                this.btnEASDetect.Text = "EAS探测";
                this.timer1.Enabled = false;
                isEASDetect = false;
                Rd.ParamSet("OpTimeout", (ushort)1000);
                this.labEASAlert.BackColor = Color.Gray;
            }
        }

        private void btnSetReadProtect_Click(object sender, EventArgs e)
        {
            SetOpAnt();

            byte[] pwd = null;

            int ret = IsValidPasswd(this.tbaccesspasswd.Text.Trim());
            {
                switch (ret)
                {
                    case -3:
                        MessageBox.Show("访问密码不能为空");
                        break;
                    case -2:
                    case -4:
                        MessageBox.Show("访问密码必须是8个16进制数");
                        break;
                    case -1:
                        MessageBox.Show("访问密码只能是16进制数字");
                        break;

                }
            }
            if (ret != 0)
                return;
            else
            {
                pwd = ByteFormat.FromHex(this.tbaccesspasswd.Text.Trim());
            }

            
            try
            {
                NXP_SetReadProtectPara para = new NXP_SetReadProtectPara(pwd);
                Rd.CustomCmd(Tf, CustomCmdType.NXP_SetReadProtect, para);
            }
            catch (OpFaidedException notagexp)
            {
                if (notagexp.ErrCode == 0x400)
                    MessageBox.Show("没法发现标签");
                else
                    MessageBox.Show("操作失败:" + notagexp.ToString());

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
                return;
            }
        }

        private void btnResetReadProtect_Click(object sender, EventArgs e)
        {
            SetOpAnt();

            byte[] pwd = null;

            int ret = IsValidPasswd(this.tbaccesspasswd.Text.Trim());
            {
                switch (ret)
                {
                    case -3:
                        MessageBox.Show("访问密码不能为空");
                        break;
                    case -2:
                    case -4:
                        MessageBox.Show("访问密码必须是8个16进制数");
                        break;
                    case -1:
                        MessageBox.Show("访问密码只能是16进制数字");
                        break;

                }
            }
            if (ret != 0)
                return;
            else
            {
                pwd = ByteFormat.FromHex(this.tbaccesspasswd.Text.Trim());
            }
 
            try
            {
                NXP_ResetReadProtectPara para = new NXP_ResetReadProtectPara(pwd);
                Rd.CustomCmd(Tf, CustomCmdType.NXP_ResetReadProtect, para);
            }
            catch (OpFaidedException notagexp)
            {
                if (notagexp.ErrCode == 0x400)
                    MessageBox.Show("没法发现标签");
                else
                    MessageBox.Show("操作失败:" + notagexp.ToString());

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.labEASAlert.BackColor = Color.Gray;

                NXP_EASAlarmPara EasAlarmPara = new NXP_EASAlarmPara(0x01, 0x02, 0x01);
                NXP_EASAlarmResult result = (NXP_EASAlarmResult)Rd.CustomCmd(null,CustomCmdType.NXP_EASAlarm, EasAlarmPara);
                
                tbEASAlarmData.Text = ByteFormat.ToHex(result.EASAlarmData);
                sound.Play();
                this.labEASAlert.BackColor = Color.Red;

            }
            catch (Exception ex)
            {
                 string msg=string.Empty;
                if (ex is ModuleLibrary.FatalInternalException)
                    msg = Convert.ToString(((ModuleLibrary.FatalInternalException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.HardwareAlertException)
                    msg = Convert.ToString(((ModuleLibrary.HardwareAlertException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.ModuleException)
                    msg = Convert.ToString(((ModuleLibrary.ModuleException)ex).ErrCode, 16);
                if (ex is ModuleLibrary.OpFaidedException)
                    msg = Convert.ToString(((ModuleLibrary.OpFaidedException)ex).ErrCode, 16);
                statelabel.Text = ex.Message+" :"+msg;
                
            }
            statelabel.Text = "OK";
        }

        private void btngen2qget_Click(object sender, EventArgs e)
        {
            try
            {
                int gen2qval = (int)Rd.ParamGet("Gen2Qvalue");
                this.cbbGen2Q.SelectedIndex = gen2qval + 1;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取失败:" + ex.ToString());
            }
        }

        private void btngen2qset_Click(object sender, EventArgs e)
        {
            if (this.cbbGen2Q.SelectedIndex == -1)
            {
                MessageBox.Show("请选择Q值");
                return;
            }
            try
            {
                Rd.ParamSet("Gen2Qvalue", this.cbbGen2Q.SelectedIndex - 1);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.ToString());
                return;
            }

            MessageBox.Show("OK");
        }

        private void btngetgen2blf_Click(object sender, EventArgs e)
        {
            try
            {
                int blf = (int)Rd.ParamGet("gen2BLF");
                switch (blf)
                {
                    case 40:
                        this.cbbgen2blf.SelectedIndex = 0;
                        break;
                    case 250:
                        this.cbbgen2blf.SelectedIndex = 1;
                        break;
                    case 400:
                        this.cbbgen2blf.SelectedIndex = 2;
                        break;
                    case 640:
                        this.cbbgen2blf.SelectedIndex = 3;
                        break;
                    default:
                        MessageBox.Show("取值失败");
                        return;
                }
            }
            catch
            {
                MessageBox.Show("操作失败");
            }

        }

        private void btnsetgen2blf_Click(object sender, EventArgs e)
        {
            if (this.cbbgen2blf.SelectedIndex == -1)
            {
                MessageBox.Show("请选择编码方式");
                return;
            }
            int blf = 0;
            switch (this.cbbgen2blf.SelectedIndex)
            {
                case 0:
                    blf = 40;
                    break;
                case 1:
                    blf = 250;
                    break;
                case 2:
                    blf = 400;
                    break;
                case 3:
                    blf = 640;
                    break;
                default:
                    MessageBox.Show("参数错误");
                    return;
            }

            try
            {
                Rd.ParamSet("gen2BLF", blf);
            }
            catch
            {
                MessageBox.Show("操作失败");
                return;
            }
            MessageBox.Show("OK");
        }

        private void btngetgen2target_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleTech.Gen2.Target tt = (ModuleTech.Gen2.Target)Rd.ParamGet("Gen2Target");
                this.cbbgen2target.SelectedIndex = (int)tt;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取失败:" + ex.ToString());
            }
        }

        private void btnsetgen2target_Click(object sender, EventArgs e)
        {
            if (this.cbbgen2target.SelectedIndex == -1)
            {
                MessageBox.Show("请选择Target");
                return;
            }
            try
            {
                Rd.ParamSet("Gen2Target", (ModuleTech.Gen2.Target)this.cbbgen2target.SelectedIndex);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.ToString());
                return;
            }
            MessageBox.Show("OK");
        }

        private void btngetgen2encode_Click(object sender, EventArgs e)
        {
            try
            {
                int enc = (int)Rd.ParamGet("gen2tagEncoding");
                this.cbbgen2encode.SelectedIndex = enc;
            }
            catch
            {
                MessageBox.Show("操作失败");
            }
        }

        private void btnsetgen2encode_Click(object sender, EventArgs e)
        {
            if (this.cbbgen2encode.SelectedIndex == -1)
            {
                MessageBox.Show("请选择编码方式");
                return;
            }

            try
            {
                Rd.ParamSet("gen2tagEncoding", this.cbbgen2encode.SelectedIndex);
            }
            catch (Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
                return;
            }
            MessageBox.Show("OK");
        }

        private void btngetgen2tari_Click(object sender, EventArgs e)
        {
            try
            {
                Tari tari = (Tari)Rd.ParamGet("Gen2Tari");
                this.cbbgen2tari.SelectedIndex = (int)tari;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }

        }

        private void btnsetgen2tari_Click(object sender, EventArgs e)
        {
            if (this.cbbgen2tari.SelectedIndex == -1)
            {
                MessageBox.Show("请选择Gen2Tari");
                return;
            }
            Tari tari = (Tari)this.cbbgen2tari.SelectedIndex;
            try
            {
                Rd.ParamSet("Gen2Tari", tari);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
                return;
            }
            MessageBox.Show("OK");
        }

        private void BatterycheckBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (BatterycheckBox.Checked)
                Checkbattery = true;
            else
                Checkbattery = false;
        }

        private void checkBox_out_CheckStateChanged(object sender, EventArgs e)
        {
            if (SleeptimeoutcomboBox.SelectedIndex < 0)
            {
                MessageBox.Show("请选择时间");
                checkBox_out.Checked = false;
                return;
            }

            if (SleeptimeoutcomboBox.SelectedIndex == 0)
            {
                checkBox_out.Checked = false;
                return;
            }
            else
            {
                if(checkBox_out.Checked==false)
                {
                    timer2.Enabled = false;
                    return;
                }

                int inven;
                if (SleeptimeoutcomboBox.SelectedIndex <= 5)
                    inven = SleeptimeoutcomboBox.SelectedIndex * 60*1000;
                else if (SleeptimeoutcomboBox.SelectedIndex == 6)
                    inven = 10 * 60*1000;
                else
                    inven = 30 * 60*1000;

                timer2.Interval = inven;
                timer2.Enabled = true;

                int tempt = 0;
                string bgb=string.Empty;
                try
                {
                    mtex.WaitOne();
                    tempt = (Byte)Rd.ParamGet("Temperature");
                }
                catch (System.Exception ex)
                {

                }
                finally
                {
                    mtex.ReleaseMutex();
                }

                try
                {
                    bgb = Battery.GetBatteryLifePercent().ToString();
                }
                catch// (System.Exception ex)
                {

                }
                if(dlog!=null)
                    this.dlog.WirteLog("Time:" + DateTime.Now.ToString() + " Temp:" + tempt.ToString() + " Batlp:" + bgb + "\r\n");
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int tempt = 0;
            string bgb = string.Empty;
            try
            {
                mtex.WaitOne();
                tempt = (Byte)Rd.ParamGet("Temperature");
            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                mtex.ReleaseMutex();
            }

            try
            {
                bgb = Battery.GetBatteryLifePercent().ToString();
            }
            catch// (System.Exception ex)
            {

            }
            if (dlog != null)
            this.dlog.WirteLog("Time:" + DateTime.Now.ToString() + " Temp:" + tempt.ToString() + " Batlp:" + bgb+"\r\n");
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            this.staticlog.WirteLog("EPC");
            this.staticlog.WirteLog(",");
            this.staticlog.WirteLog("Count");
            this.staticlog.WirteLog(",");
            this.staticlog.WirteLog("\r\n");
            for (int i = 0; i < TagslistView.Items.Count; i++)
            {
                if (staticlog != null)
                {
                    this.staticlog.WirteLog(TagslistView.Items[i].SubItems[0].Text);
                    this.staticlog.WirteLog(",");
                    this.staticlog.WirteLog(TagslistView.Items[i].SubItems[1].Text);
                    this.staticlog.WirteLog(",");
                    this.staticlog.WirteLog("\r\n");
                }
            }

            if (TagslistView.Items.Count > 0)
            {
                MessageBox.Show("保存信息" + TagslistView.Items.Count.ToString() + "条");
            }
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            Form_cmds fcm = new Form_cmds();
            fcm.ShowDialog();
            SendDatatextBox.Text = fcm.cmds.Replace(" ", "").ToUpper();
        }

    }
}