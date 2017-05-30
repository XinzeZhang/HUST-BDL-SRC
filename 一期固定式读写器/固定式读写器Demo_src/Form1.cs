//#define TEMPERATRATURE

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ModuleTech;
using System.Diagnostics;
using ModuleTech.Gen2;
using ModuleLibrary;
using System.IO;
using System.Runtime.InteropServices;

namespace ModuleReaderManager
{
    
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        [DllImport("winmm")]
        public static extern bool PlaySound(string szSound, IntPtr hMod, PlaySoundFlags flags);

        [Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,    /* play synchronously (default) */ //同步 
            SND_ASYNC = 0x0001,    /* play asynchronously */ //异步 
            SND_NODEFAULT = 0x0002,    /* silence (!default) if sound not found */
            SND_MEMORY = 0x0004,    /* pszSound points to a memory file */
            SND_LOOP = 0x0008,    /* loop the sound until next sndPlaySound */
            SND_NOSTOP = 0x0010,    /* don't stop any currently playing sound */
            SND_NOWAIT = 0x00002000, /* don't wait if the driver is busy */
            SND_ALIAS = 0x00010000, /* name is a registry alias */
            SND_ALIAS_ID = 0x00110000, /* alias is a predefined ID */
            SND_FILENAME = 0x00020000, /* name is file name */
            SND_RESOURCE = 0x00040004    /* name is resource name or atom */
        } 

   //     ListView lvTags = null;
        Dictionary<string, TagInfo> m_Tags = new Dictionary<string, TagInfo>();
        Mutex tagmutex = new Mutex();
        public ReaderParams rParms = new ReaderParams(200, 0, 1);

        bool isInventory = false;
        bool isConnect = false;
 //       Exception InvExp = null;

        delegate void TagHandler(TagReadData tag);

#if(TEMPERATRATURE)
        //check tempurature
        int tempture = 0;
#endif

        void AddTagToDic(TagReadData tag)
        {
            TagInfo tmptag = null;

            if (rParms.isUniByEmd && (tag.Tag.Protocol == TagProtocol.GEN2))
            {
                if (tag.EMDDataString == string.Empty)
                    return;
            }
            //
            tagmutex.WaitOne();

            string keystr = tag.EPCString;

            if (rParms.isUniByEmd)
                keystr += tag.EMDDataString;

            if (rParms.isUniByAnt)
                keystr += tag.Antenna.ToString();

            if (m_Tags.ContainsKey(keystr))
            {
                tmptag = m_Tags[keystr];
                if (rParms.isOneReadOneTime)
                    tmptag.readcnt += 1;
                else
                    tmptag.readcnt += tag.ReadCount;

                tmptag.Frequency = tag.Frequency;
                tmptag.RssiRaw = tag.Rssi;
                tmptag.Phase = tag.Phase;
                tmptag.antid = tag.Antenna;

                if (!rParms.isUniByEmd)
                {
                    if (tmptag.emddatastr != tag.EMDDataString)
                    {
                        if (tag.EMDDataString != string.Empty)
                            tmptag.emddatastr = tag.EMDDataString;
                    }
                }

                //added on 3-26
                if (rParms.isIdtAnts)
                {
                    TimeSpan span = tag.Time- tmptag.timestamp;
  //                  Console.WriteLine("span.TotalMilliseconds:" + span.TotalMilliseconds.ToString()
  //                      + " tag.ReadCount:" + tag.ReadCount.ToString());
                    if (tag.Rssi <= 0)
                        tmptag.RssiSum += (tag.Rssi + 120) * tag.ReadCount * ((int)span.TotalMilliseconds / 80);
                    else
                        tmptag.RssiSum += tag.Rssi * tag.ReadCount * ((int)span.TotalMilliseconds / 80);
                }

                tmptag.timestamp = tag.Time;
                ///
            }
            else
            {
                TagInfo newtag = null;
                if (rParms.isOneReadOneTime)
                {
                    newtag = new TagInfo(tag.EPCString, 1, tag.Antenna, tag.Time,
                        tag.Tag.Protocol, tag.EMDDataString);
                    newtag.RssiRaw = tag.Rssi;
                    newtag.Phase = tag.Phase;
                    newtag.Frequency = tag.Frequency;
                }
                else
                {
                    newtag = new TagInfo(tag.EPCString, tag.ReadCount, tag.Antenna, tag.Time,
                        tag.Tag.Protocol, tag.EMDDataString);
                    newtag.RssiRaw = tag.Rssi;
                    newtag.Phase = tag.Phase;
                    newtag.Frequency = tag.Frequency;
                }
              
                m_Tags.Add(keystr, newtag);

                //added on 3-26
                if (rParms.isIdtAnts)
                {
                    if (tag.Rssi <= 0)
                        newtag.RssiSum += (tag.Rssi + 120) * tag.ReadCount;
                    else
                        newtag.RssiSum += tag.Rssi * tag.ReadCount;
                }
                ///
            }
            //if (!tagbuf.ContainsKey(tag.EPCString))
            //    tagbuf.Add(tag.EPCString, tag);
            tagmutex.ReleaseMutex();
        }
        public Reader modulerdr = null;

        delegate void ReconnectHandler(Exception ex);

        delegate void OpFailedHandler(Exception ex);

        delegate void AutoStopReaderHandler();

        void StopFixTimesRead()
        {
            btnstop_Click(null, null);
        }

        string opfailstr = "";
        void ShowOpFailedMsg(Exception ex)
        {
            opfailstr += ex.ToString();
            opfailstr += "\n";
            this.rtbopfailmsg.Text = opfailstr;
        }

        void Reconnect(Exception ex)
        {
            if (ex != null)
            {
                if (ex.ToString() == "No antennas detected or selected for read operations")
                {
                    MessageBox.Show("没有接天线");
                    return;
                }
                this.toolStripStatusLabel1.Text = "内部错误，请重连reader";
                MessageBox.Show("连接出错，请重新连接reader :" + ex.ToString());
     //           MessageBox.Show(ex.ToString());
            }
            this.btnconnect.Enabled = true;
            this.btnstart.Enabled = false;
            this.btnstop.Enabled = false;
            this.readparamenu.Enabled = false;
            this.Custommenu.Enabled = false;
            this.tagopmenu.Enabled = false;
            this.MsgDebugMenu.Enabled = false;
            this.timer1.Enabled = false;
            this.btnInvParas.Enabled = false;
            this.btndisconnect.Enabled = true;
            this.btn16portsSet.Enabled = false;

            for (int f = 1; f <= allAnts.Count; ++f)
            {
                allAnts[f].Checked = false;
                allAnts[f].Enabled = false;
                allAnts[f].ForeColor = antdefaulcolor;
            }

            this.timer1.Enabled = false;
            if (isConnect)
            {
                modulerdr.Disconnect();
                isConnect = false;
                modulerdr = null;
            }
            if (readThread != null)
            {
                isInventory = false;
                readThread.Join();
                readThread = null;
            }

            if (ex == null)
            {
                this.toolStripStatusLabel1.Text = "断开";
                return;
            }
            //this.labMoudevir.Text = "";
            //this.labfirmware.Text = "";
            //this.ant1.Text = "未连接";
            //this.ant2.Text = "未连接";


        }


        string sourceip;
        Color antdefaulcolor;
        Thread readThread = null;
        int readtimedur = 0;

        public string serialcommunicationmsg = "";

  //      Dictionary<string, TagReadData> tagbuf = new Dictionary<string, TagReadData>();
        void ReadFunc()
        {
            int firsttagtime = 0;
            int hassetfirsttime = 0;
            int totalreadtimes = 0;

            int lastrettgtime = Environment.TickCount;
            int rettgcnt = 0;


            while (isInventory)
            {
                try
                {
                    if (hassetfirsttime == 0)
                    {
                        firsttagtime = Environment.TickCount;
                        hassetfirsttime = 1;
                    }
//                    int ee = Environment.TickCount;

                    TagReadData[] reads = modulerdr.Read(rParms.readdur);
#if(TEMPERATRATURE)
                    tempture = (Byte)modulerdr.ParamGet("Temperature");
#endif
 //                   Console.WriteLine("read dur: " + (Environment.TickCount - ee) + ":" + reads.Length.ToString());

                    
                    /*如果需要驱动gpo
                    //if (reads.Length != 0)
                    //{
                    //    if (rParms.setGPO1)
                    //    {
                    //        modulerdr.GPOSet(1, true);
                    //        Thread.Sleep(20);
                    //        modulerdr.GPOSet(1, false);
                    //    }
                    //}
                     * */
                    //if (reads.Length != 0)
                    //    PlaySound("ding.wav", IntPtr.Zero, PlaySoundFlags.SND_SYNC);

                    foreach (TagReadData read in reads)
                    {
 //                       Console.WriteLine("Rssi: " + read.Rssi);
                        AddTagToDic(read);
                    }

                    totalreadtimes++;
                    if (rParms.isReadFixCount)
                    {
                        if (totalreadtimes == rParms.FixReadCount)
                        {
                            this.BeginInvoke(new AutoStopReaderHandler(StopFixTimesRead));
                            readtimedur = Environment.TickCount - firsttagtime;
                            return;
                        }
                    }
                    //gen2多标签读取，如果需要改变天线搜索顺序
                    if (rParms.isRevertAnts)
                    {
                        SimpleReadPlan pl = modulerdr.ParamGet("ReadPlan") as SimpleReadPlan;
                        if (pl != null)
                        {
                            if (pl.Protocol == TagProtocol.GEN2)
                            {
                                int[] neworder = new int[pl.Antennas.Length];
                                for (int j = 0; j < pl.Antennas.Length; ++j)
                                    neworder[j] = pl.Antennas[pl.Antennas.Length - 1 - j];
                                modulerdr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, neworder));
                            }
                        }
                    }
                    //
                  
                    Thread.Sleep(rParms.sleepdur);
                }
                catch (OpFaidedException exxx)
                {
                    Debug.WriteLine("inventory failed --------------------------------------------------");
                    this.BeginInvoke(new OpFailedHandler(ShowOpFailedMsg), exxx);
                //    Debug.WriteLine(exxx.ToString());
                    Debug.WriteLine(DateTime.Now.ToString());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("fatal error --------------------------------------------------");
                    Debug.WriteLine(DateTime.Now.ToString());
    //                Debug.WriteLine("33333333333");
                    this.BeginInvoke(new ReconnectHandler(Reconnect), ex);
   //                 Debug.WriteLine("4444444444444");
                    return;
                }


            }
  //          Debug.WriteLine("5555555555555555");
            readtimedur = Environment.TickCount - firsttagtime;

            

        }
        private void btnconnect_Click(object sender, EventArgs e)
        {
            if (this.cbbreadertype.SelectedIndex == -1)
            {
                MessageBox.Show("请选择天线端口数");
                return;
            }


            if (modulerdr != null)
            {
                modulerdr.Disconnect();
                //this.labMoudevir.Text = "";
                //this.labfirmware.Text = "";
                //this.ant1.Text = "未连接";
                //this.ant2.Text = "未连接";
            }
            rParms.resetParams();
   //         sourceip = "tmr:///";
            sourceip = this.tbip.Text.Trim();
            if (!this.tbip.Text.Trim().ToLower().Contains("com"))
            {
      //          sourceip += ":8080";
                rParms.hasIP = true;
            }
            else
                rParms.hasIP = false;

                try
                {
                    int st = Environment.TickCount;
                    if (cbbreadertype.SelectedIndex == 4)
                        modulerdr = Reader.Create(sourceip, ModuleTech.Region.NA, 16);
                    else
                        modulerdr = Reader.Create(sourceip, ModuleTech.Region.NA, cbbreadertype.SelectedIndex + 1);

                    Debug.WriteLine("connect time:" + (Environment.TickCount - st).ToString());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("连接失败，请检查读写器地址是否正确" + ex.ToString());
                    this.toolStripStatusLabel1.Text = "连接失败";            
                    return;
                }
                

            rParms.AntsState.Clear();

            if (modulerdr.HwDetails.logictype != ReaderType.MT_A7_16ANTS)
            {
                rParms.antcnt = cbbreadertype.SelectedIndex + 1;
                for (int aa = 1; aa <= cbbreadertype.SelectedIndex + 1; ++aa)
                {
                    allAnts[aa].Enabled = true;
                    allAnts[aa].ForeColor = Color.Red;
                }
            }
            rParms.readertype = modulerdr.HwDetails.logictype;
            if (modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E ||
                modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC
                || modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_MICRO)
                rParms.isMultiPotl = true;
            
            if (modulerdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_WIFI)
                rParms.hasIP = false;

            if (modulerdr.HwDetails.logictype != ReaderType.SL_FOURANTS)
                this.menuitlongtask.Enabled = false;

            if (modulerdr.HwDetails.logictype != ReaderType.MT_A7_16ANTS)
            {
                if (!rParms.isMultiPotl)
                {
                    this.cbpotl6b.Enabled = false;
                    this.cbpotlipx256.Enabled = false;
                    this.cbpotlipx64.Enabled = false;
                    this.cbpotlgen2.Checked = true;
                    this.cbpotlgen2.Enabled = false;
                    this.iso183k6btagopToolStripMenuItem.Enabled = false;
                }
                else
                {
                    this.cbpotlgen2.Checked = false;
                    this.cbpotl6b.Enabled = true;
                    this.cbpotlipx256.Enabled = true;
                    this.cbpotlipx64.Enabled = true;
                    this.cbpotlgen2.Enabled = true;
                    this.iso183k6btagopToolStripMenuItem.Enabled = true;
                }

                if (rParms.readertype != ReaderType.PR_ONEANT)
                {
                    //      Debug.WriteLine("before ConnectedAntennas");
                    int[] connectedants = (int[])modulerdr.ParamGet("ConnectedAntennas");
                    for (int c = 0; c < connectedants.Length; ++c)
                        allAnts[connectedants[c]].ForeColor = Color.Green;
                }

                for (int ff = 1; ff <= allAnts.Count; ++ff)
                {
                    if (allAnts[ff].Enabled)
                    {
                        if (allAnts[ff].ForeColor == Color.Green)
                            rParms.AntsState.Add(new AntAndBoll(ff, true));
                        else
                            rParms.AntsState.Add(new AntAndBoll(ff, false));
                    }
                }
            }
            else
            {
                this.btn16portsSet.Enabled = true;
                this.rParms.SixteenDevConAnts = (int[])modulerdr.ParamGet("ConnectedAntennas");
                rParms.antcnt = 0;
            }
     

          

            if (rParms.readertype != ReaderType.PR_ONEANT)
            {
           //     Debug.WriteLine("before HardwareVersion");
                rParms.hardvir = (string)modulerdr.ParamGet("HardwareVersion");
                Debug.WriteLine("before SoftwareVersion");
                rParms.softvir = (string)modulerdr.ParamGet("SoftwareVersion");
            }
            else
            {
                rParms.hardvir = "pr9000";
                rParms.softvir = "pr9000";
                rParms.sleepdur = 50;
                rParms.readdur = 500;
            }
      //      Debug.WriteLine("before RfPowerMax");
            rParms.powermax = ((int)modulerdr.ParamGet("RfPowerMax")) / 100;
            rParms.powermin = ((int)modulerdr.ParamGet("RfPowerMin")) / 100;

            isConnect = true;

  //          modulerdr.TagRead += AddTagToDic;
  //          modulerdr.ReadException += ReadExpHandler;

            
            rParms.gen2session = (int)modulerdr.ParamGet("Gen2Session");
 //             modulerdr.ParamSet("ReadTxPower", 3150);
 /*
            if (!(modulerdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM9 ||
                modulerdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM9_WIFI))
            {
                try
                {
                    Environment.CurrentDirectory = cur_dir;
                    StreamReader confReader = new StreamReader("module.conf", System.Text.Encoding.Default);
                    char[] dep1 = new char[1];
                    dep1[0] = ':';
                    string sLine = "";
                    while (sLine != null)
                    {
                        sLine = confReader.ReadLine();
                        if (sLine != null && !sLine.Equals(""))
                        {
                            string[] para = sLine.Split(dep1, StringSplitOptions.None);
                            //     if (para[0] !)
                            if (para[0] == "defaultpower")
                            {
                                float dp = float.Parse(para[1]);
                                ushort power = (ushort)((float)rParms.powermax * 100 * dp);
                                if (modulerdr.HwDetails.logictype != ReaderType.MT_A7_16ANTS)
                                {
                                    AntPower[] apwrs = new AntPower[rParms.AntsState.Count];
                                    for (int c = 0; c < rParms.AntsState.Count; ++c)
                                    {
                                        apwrs[c].AntId = (byte)(c + 1);
                                        apwrs[c].WritePower = power;
                                        apwrs[c].ReadPower = power;
                                    }
                                    modulerdr.ParamSet("AntPowerConf", apwrs);
                                }
                                else
                                {
                                    AntPower[] apwrs = new AntPower[16];
                                    for (int v = 0; v < 16; ++v)
                                    {
                                        apwrs[v].AntId = (byte)(v + 1);
                                        apwrs[v].WritePower = power;
                                        apwrs[v].ReadPower = power;
                                    }
                                }

                            }
                            else if (para[0] == "checkantenna")
                            {
                                if (rParms.readertype != ReaderType.PR_ONEANT &&
                                    modulerdr.HwDetails.module != Reader.Module_Type.MODOULE_M6E_MICRO)
                                {
                                    if (para[1] == "true")
                                    {
                                        modulerdr.ParamSet("CheckAntConnection", true);
                                    }
                                    else if (para[1] == "false")
                                    {
                                        modulerdr.ParamSet("CheckAntConnection", false);
                                    }
                                }
                            }
                            else if (para[0] == "gen2session")
                            {
                                int sess = int.Parse(para[1]);
                                modulerdr.ParamSet("Gen2Session", (Session)sess);
                            }
                            else if (para[0] == "SetGPO1")
                            {
                                if (para[1] == "true")
                                {
                                    rParms.setGPO1 = true;
                                }
                                else if (para[1] == "false")
                                {
                                    rParms.setGPO1 = false;
                                }
                            }
                        }
                    }

                    confReader.Close();
                    confReader = null;
                }
                catch (Exception exxp)
                {
                    MessageBox.Show("读配置文件错误");
                    Reconnect(null);
                    return;
                }
            }
            //*/
            rParms.fisrtLoad = true;
            this.btnstart.Enabled = true;
            this.readparamenu.Enabled = true;
            this.Custommenu.Enabled = true;
            this.tagopmenu.Enabled = true;
            this.MsgDebugMenu.Enabled = true;
            this.menutest.Enabled = true;
            this.menuoutputtags.Enabled = true;
            this.btnconnect.Enabled = false;
            this.btnInvParas.Enabled = true;
            this.btndisconnect.Enabled = true;
            this.updatemenu.Enabled = false;
            this.toolStripStatusLabel1.Text = "连接成功";

          
            return;
            
        }



        private List<AntAndBoll> CheckAntsValid()
        {
            List<AntAndBoll> selants = new List<AntAndBoll>();

            for (int cc = 1; cc <= allAnts.Count; ++cc)
            {
                if (allAnts[cc].Checked)
                {
                    if (allAnts[cc].ForeColor == Color.Red)
                        selants.Add(new AntAndBoll(cc, false)); 
                    else
                        selants.Add(new AntAndBoll(cc, true));
                }
            }

            return selants;
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

        private void btnstart_Click(object sender, EventArgs e)
        {
            if (rParms.readertype != ReaderType.MT_A7_16ANTS)
            {
                if (readThread != null)
                {
                    isInventory = false;
                    readThread.Join();
                }

                List<AntAndBoll> selants = CheckAntsValid();
                if (selants.Count == 0)
                {
                    MessageBox.Show("请选择天线");
                    return;
                }
                
                //if(rParms.isCheckConnection)
                for (int i = 0; i < selants.Count; ++i)
                {
                    if (selants[i].isConn == false)
                    {
                        DialogResult stat = DialogResult.OK;
                        stat = MessageBox.Show("在未检测到天线的端口执行搜索，真的要执行吗?", "警告",
                                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                        MessageBoxDefaultButton.Button2);
                        if (stat == DialogResult.OK)
                            break;
                        else
                            return;
                    }
                }


                List<int> antsExe = new List<int>();
                for (int i = 0; i < selants.Count; ++i)
                {
                    antsExe.Add(selants[i].antid);
                }

                if ((!this.cbpotl6b.Checked) && (!this.cbpotlgen2.Checked) && (!this.cbpotlipx64.Checked)
                    && (!this.cbpotlipx256.Checked))
                {
                    MessageBox.Show("请选择协议");
                    return;
                }

                List<SimpleReadPlan> readplans = new List<SimpleReadPlan>();
                if (this.cbpotl6b.Checked)
                    readplans.Add(new SimpleReadPlan(TagProtocol.ISO180006B, antsExe.ToArray(), rParms.weight180006b));
                if (this.cbpotlgen2.Checked)
                    readplans.Add(new SimpleReadPlan(TagProtocol.GEN2, antsExe.ToArray(), rParms.weightgen2));
                if (this.cbpotlipx256.Checked)
                    readplans.Add(new SimpleReadPlan(TagProtocol.IPX256, antsExe.ToArray(), rParms.weightipx256));
                if (this.cbpotlipx64.Checked)
                    readplans.Add(new SimpleReadPlan(TagProtocol.IPX64, antsExe.ToArray(), rParms.weightipx64));
                if (readplans.Count > 1)
                    modulerdr.ParamSet("ReadPlan", new MultiReadPlan(readplans.ToArray()));
                else
                    modulerdr.ParamSet("ReadPlan", readplans[0]);

            }
            else
            {
                if (rParms.SixteenDevsrp == null)
                {
                    MessageBox.Show("请点击 ‘16天线设置’ 选择天线");
                    return;
                }

                modulerdr.ParamSet("ReadPlan", rParms.SixteenDevsrp);
            }

            m_Tags.Clear();
            //added on 3-26
            if (rParms.isIdtAnts && rParms.IdtAntsType == 1)
                this.timer1.Interval = rParms.DurIdtval;
            else
                this.timer1.Interval = rParms.readdur;
            ///
            this.timer1.Enabled = true;
            isInventory = true;
   //         modulerdr.StartReading();
            this.labreadtime.Text = "0";
            readThread = new Thread(ReadFunc);
            readThread.Start();

    //        TagReadData[] reads = modulerdr.Read(rParms.readdur);

            this.btndisconnect.Enabled = false;
            this.btnstop.Enabled = true;
            this.readparamenu.Enabled = false;
            this.Custommenu.Enabled = false;
            this.menutest.Enabled = false;
            this.tagopmenu.Enabled = false;
            this.MsgDebugMenu.Enabled = false;
            this.menuoutputtags.Enabled = false;
            this.btnstart.Enabled = false;
            this.btnInvParas.Enabled = false;
            this.btn16portsSet.Enabled = false;
            this.toolStripStatusLabel1.Text = "Inventory";
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            isInventory = false;
  //          Thread.Sleep(2000);
            readThread.Join();
   //         modulerdr.StopReading();

            timer1_Tick(null, null);
            this.btnstop.Enabled = false;
            this.readparamenu.Enabled = true;
            this.Custommenu.Enabled = true;
            this.menutest.Enabled = true;
            this.tagopmenu.Enabled = true;
            this.MsgDebugMenu.Enabled = true;
            this.btnstart.Enabled = true;
            this.menuoutputtags.Enabled = true;
            this.btndisconnect.Enabled = true;
            this.btnInvParas.Enabled = true;
            if (rParms.readertype == ReaderType.MT_A7_16ANTS)
                this.btn16portsSet.Enabled = true;
            this.toolStripStatusLabel1.Text = "";
            this.labreadtime.Text = readtimedur.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tagmutex.WaitOne();
            m_Tags.Clear();
            tagmutex.ReleaseMutex();
            this.lvTags.Items.Clear();
        }
        List<TagInfo> tmplist = new List<TagInfo>();
//        List<string> dellist = new List<string>();

        private void timer1_Tick(object sender, EventArgs e)
        {

            tmplist.Clear();

            tagmutex.WaitOne();
#if(TEMPERATRATURE)
            label6.Text = tempture.ToString();
#endif
            foreach (TagInfo tag in m_Tags.Values)
            {
                tmplist.Add(tag);
            }
            tagmutex.ReleaseMutex();

            //added on 3-26
            if (rParms.isIdtAnts)
            {
                Dictionary<string, TagInfo> tmp_Tags = new Dictionary<string, TagInfo>();
                if (rParms.IdtAntsType == 2)
                {
                    List<TagInfo> tags_dy = new List<TagInfo>();
                    List<TagInfo> tags_xy = new List<TagInfo>();
                    List<TagInfo> tags_del = new List<TagInfo>();

                    foreach (TagInfo tag in tmplist)
                    {
                        TimeSpan span = DateTime.Now - tag.timestamp;
                        if (span.Seconds > rParms.AfterIdtWaitval)
                            tags_dy.Add(tag);
                        else
                            tags_xy.Add(tag);
                    }

                    foreach (TagInfo tag_dy in tags_dy)
                    {
                        foreach (TagInfo tag_xy in tags_xy)
                        {
                            bool isfind = false;
                            if (rParms.isUniByEmd)
                            {
                                if ((tag_dy.epcid + tag_dy.emddatastr) == (tag_xy.epcid + tag_xy.emddatastr))
                                    isfind = true;
                            }
                            else
                            {
                                if (tag_dy.epcid == tag_xy.epcid)
                                    isfind = true;
                            }
                            if (isfind)
                            {
                                tags_del.Add(tag_dy);
                                break;
                            }
                        }
                    }

                    foreach (TagInfo tag in tags_del)
                    {
                        tags_dy.Remove(tag);
                    }
                    tmplist = tags_dy;
                }

                foreach (TagInfo tag in tmplist)
                {
                    string unistr = null;
                    if (rParms.isUniByEmd)
                        unistr = tag.epcid + tag.emddatastr;
                    else
                        unistr = tag.epcid;
                    if (tmp_Tags.ContainsKey(unistr))
                    {                       
                        if (tmp_Tags[unistr].RssiSum < tag.RssiSum)
                        {
                            
                            tmp_Tags.Remove(unistr);
                            tmp_Tags.Add(unistr, tag);
                        }
                    }
                    else
                            tmp_Tags.Add(unistr, tag);
                }

                tagmutex.WaitOne();
                foreach (TagInfo tag in tmplist)
                {
                    string keystr = tag.epcid;

                    if (rParms.isUniByEmd)
                        keystr += tag.emddatastr;

                    if (rParms.isUniByAnt)
                        keystr += tag.antid.ToString();
                    m_Tags.Remove(keystr);
                }

                tagmutex.ReleaseMutex();
                tmplist.Clear();
                foreach (TagInfo tag in tmp_Tags.Values)
                {
                    tmplist.Add(tag);
                }
        
            }

            int ant1cnt = 0;
            int ant2cnt = 0;
            int ant3cnt = 0;
            int ant4cnt = 0;

            foreach (TagInfo tag in tmplist)
            {
                //if (tag.antid == 1)
                //    ant1cnt++;
                //if (tag.antid == 2)
                //    ant2cnt++;
                //if (tag.antid == 3)
                //    ant3cnt++;
                //if (tag.antid == 4)
                //    ant4cnt++;

                int flag = 0;
                foreach (ListViewItem viewitem in lvTags.Items)
                {
                    bool isupdate = false;
                    if (rParms.isUniByEmd && rParms.isUniByAnt)
                        isupdate = (viewitem.SubItems[2].Text == tag.epcid && viewitem.SubItems[4].Text == tag.emddatastr
                            && viewitem.SubItems[3].Text == tag.antid.ToString());
                    else if (rParms.isUniByEmd)
                        isupdate = (viewitem.SubItems[2].Text == tag.epcid && viewitem.SubItems[4].Text == tag.emddatastr);
                    else if (rParms.isUniByAnt)
                        isupdate = (viewitem.SubItems[2].Text == tag.epcid && viewitem.SubItems[3].Text == tag.antid.ToString());
                    else
                        isupdate = (viewitem.SubItems[2].Text == tag.epcid);

                    if (isupdate)
                    {
                        int isupdatecolor = 0;
                        if (viewitem.SubItems[4].Text != tag.emddatastr)
                        {
                            if (tag.emddatastr != string.Empty)
                                viewitem.SubItems[4].Text = tag.emddatastr;
                        }

                        if (tag.readcnt != int.Parse(viewitem.SubItems[1].Text))
                        {
                            isupdatecolor = 1;
                            viewitem.SubItems[1].Text = tag.readcnt.ToString();
                        }

                        if (tag.antid != int.Parse(viewitem.SubItems[3].Text))
                        {
                            isupdatecolor = 1;
                            viewitem.SubItems[3].Text = tag.antid.ToString();
                        }

                        viewitem.SubItems[6].Text = tag.RssiRaw.ToString();
                        viewitem.SubItems[8].Text = tag.Phase.ToString();
                        viewitem.SubItems[7].Text = tag.Frequency.ToString();


                        if (rParms.isChangeColor)
                        {
                            if (isupdatecolor == 0)
                            {
                                TimeSpan span = DateTime.Now - tag.timestamp;
                                if (span.Seconds > 2 && span.Seconds < 4)
                                    viewitem.BackColor = Color.Silver;
                                else if (span.Seconds >= 4)
                                    viewitem.BackColor = Color.DimGray;
                                //            else
                                //                viewitem.BackColor = Color.White;
                            }
                            else
                                viewitem.BackColor = Color.White;
                        }
                        flag = 1;
         
                    }
                }
                if (flag == 0)
                {
                    ListViewItem item = new ListViewItem(lvTags.Items.Count.ToString());
                    item.SubItems.Add(tag.readcnt.ToString());
                    item.SubItems.Add(tag.epcid);
                    item.SubItems.Add(tag.antid.ToString());

                    item.SubItems.Add(tag.emddatastr);

                    if (tag.potl == TagProtocol.GEN2)
                        item.SubItems.Add("GEN2");
                    else if (tag.potl == TagProtocol.ISO180006B)
                        item.SubItems.Add("ISO180006B");
                    else if (tag.potl == TagProtocol.IPX256)
                        item.SubItems.Add("IPX256");
                    else if (tag.potl == TagProtocol.IPX64)
                        item.SubItems.Add("IPX64");
                    else
                        item.SubItems.Add("GEN2");

                    item.SubItems.Add(tag.RssiRaw.ToString());
                    item.SubItems.Add(tag.Frequency.ToString());
                    item.SubItems.Add(tag.Phase.ToString());
                    lvTags.Items.Add(item);
                }
            }

            foreach (ListViewItem viewitem in lvTags.Items)
            {
                if (viewitem.SubItems[3].Text == "1")
                    ant1cnt++;
                else if (viewitem.SubItems[3].Text == "2")
                    ant2cnt++;
                else if (viewitem.SubItems[3].Text == "3")
                    ant3cnt++;
                else if (viewitem.SubItems[3].Text == "4")
                    ant4cnt++;
            }
            this.labant1cnt.Text = ant1cnt.ToString();
            this.labant2cnt.Text = ant2cnt.ToString();
            this.labant3cnt.Text = ant3cnt.ToString();
            this.labant4cnt.Text = ant4cnt.ToString();
            //modify on 3-26
            this.labtotalcnt.Text = lvTags.Items.Count.ToString();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Enabled = false;
            if (isConnect)
                modulerdr.Disconnect();
            if (readThread != null)
            {
                isInventory = false;
                readThread.Join();
            }

        }

        Dictionary<int, CheckBox> allAnts = new Dictionary<int, CheckBox>();
        string cur_dir = null;
 //       ProgramLog taginfolog = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.cbpotl6b.Enabled = false;
            this.cbpotlipx64.Enabled = false;
            this.cbpotlipx256.Enabled = false;
            this.cbpotlgen2.Enabled = false;
            this.iso183k6btagopToolStripMenuItem.Enabled = false;
            this.Custommenu.Enabled = false;
            this.readparamenu.Enabled = false;
            this.tagopmenu.Enabled = false;
            this.MsgDebugMenu.Enabled = false;
            this.menutest.Enabled = false;
            this.menuoutputtags.Enabled = false;
            this.btnstart.Enabled = false;
            this.btnstop.Enabled = false;
            this.btnInvParas.Enabled = false;
            this.toolStripStatusLabel1.Text = "未连接";

            this.btn16portsSet.Enabled = false;

            allAnts.Add(1, cbant1);
            allAnts.Add(2, cbant2);
            allAnts.Add(3, cbant3);
            allAnts.Add(4, cbant4);
            antdefaulcolor = cbant1.ForeColor;

            for (int f = 1; f <= allAnts.Count; ++f)
                allAnts[f].Enabled = false;

            lvTags.Font = new Font("", 12, FontStyle.Bold);
            cur_dir = Environment.CurrentDirectory;
 //           this.taginfolog = new ProgramLog(3, "taginfo");
        }

        private void readparamenu_Click(object sender, EventArgs e)
        {
            rParms.isIpModify = false;
            rParms.isM5eModify = false;

            if (rParms.hasIP)
            {
                //if (rParms.readertype == ReaderType.MT_TWOANTS ||
                //rParms.readertype == ReaderType.MT_FOURANTS)
                //{
                if (!rParms.isGetIp)
                {
                    ReaderIPInfo ipinfo = null;
                    try
                    {
                        ipinfo = (ReaderIPInfo)modulerdr.ParamGet("IPAddress");
                        rParms.ip = ipinfo.IP;
                        rParms.subnet = ipinfo.SUBNET;
                        rParms.gateway = ipinfo.GATEWAY;
                        if (ipinfo.MACADDR != null)
                        {
                            rParms.macstr = ByteFormat.ToHex(ipinfo.MACADDR);
                        }
                        rParms.isGetIp = true;
                    }
                    catch
                    {
                        rParms.hasIP = false;
                    }

                }
            }

            readerParaform frm = new readerParaform(rParms, modulerdr);
            if (frm.ShowDialog() == DialogResult.Cancel)
                return;

        }



        //Gen2TagFilter filter = null;
        //EmbededCmdData embededdata = null;

        private void btnInvParas_Click(object sender, EventArgs e)
        {
            InventoryParasform frm = new InventoryParasform(this);
            frm.ShowDialog();
        }

        private void updatemenu_Click(object sender, EventArgs e)
        {
            if (rParms.readertype == ReaderType.PR_ONEANT)
            {
                MessageBox.Show("此类型读写器不支持升级操作");
                return;
            }
            updatefrm frm = new updatefrm();
            frm.ShowDialog();
        }

        private void Custommenu_Click(object sender, EventArgs e)
        {
            if (rParms.readertype != ReaderType.PR_ONEANT)
            {
                CustomCmdFrm frm = new CustomCmdFrm(modulerdr, rParms);
                frm.ShowDialog();
            }
            else
                MessageBox.Show("此类型读写器不支持标签特殊指令");
        }


        private void gen2tagopMenuItem_Click(object sender, EventArgs e)
        {
            gen2opForm frm = new gen2opForm(modulerdr, rParms);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void iso183k6btagopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Iso186bopForm frm = new Iso186bopForm(modulerdr, rParms);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btndisconnect_Click(object sender, EventArgs e)
        {
            Reconnect(null);
        }

        private void CountTagMenuItem_Click(object sender, EventArgs e)
        {
            CountTagsFrm frm = new CountTagsFrm(modulerdr, rParms);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void MulperlockMenuItem_Click(object sender, EventArgs e)
        {
            MulperlockFrm frm = new MulperlockFrm(modulerdr, rParms);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btn16portsSet_Click(object sender, EventArgs e)
        {
            Frm16portAnts frm = new Frm16portAnts(modulerdr, rParms);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void menuitlongtask_Click(object sender, EventArgs e)
        {
            longtaskfrm frm = new longtaskfrm(modulerdr);
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void menuabout_Click(object sender, EventArgs e)
        {
            AboutFrm frm = new AboutFrm();
            frm.ShowDialog();
        }

        private void menutest_Click(object sender, EventArgs e)
        {
            regulatoryFrm frm = new regulatoryFrm(modulerdr);
            frm.ShowDialog();
        }

        private void menuitemlog_Click(object sender, EventArgs e)
        {
            logFrm frm = new logFrm();
            frm.ShowDialog();
        }

        private void lvTags_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column >= 1 && e.Column <= 8)
            {
                tmplist.Clear();
                foreach (ListViewItem viewitem in lvTags.Items)
                {
                    TagProtocol potl = TagProtocol.NONE;
                    if (viewitem.SubItems[5].Text == "GEN2")
                        potl = TagProtocol.GEN2;
                    else if (viewitem.SubItems[5].Text == "ISO180006B")
                        potl = TagProtocol.ISO180006B;
                    else if (viewitem.SubItems[5].Text == "IPX256")
                        potl = TagProtocol.IPX256;
                    else if (viewitem.SubItems[5].Text == "IPX64")
                        potl = TagProtocol.IPX64;

                    TagInfo newtag = new TagInfo(viewitem.SubItems[2].Text, int.Parse(viewitem.SubItems[1].Text),
                        int.Parse(viewitem.SubItems[3].Text), DateTime.Now, potl, viewitem.SubItems[4].Text);
                    newtag.RssiRaw = int.Parse(viewitem.SubItems[6].Text);
                    newtag.Frequency = int.Parse(viewitem.SubItems[7].Text);
                    newtag.Phase = int.Parse(viewitem.SubItems[8].Text);
                    tmplist.Add(newtag);
                }
                this.lvTags.Items.Clear();
                IComparer<TagInfo> tagcmper = null;
                if (e.Column == 1)
                    tagcmper = new TagInfoCompReadCnt();
                else if (e.Column == 2)
                    tagcmper = new TagInfoCompEPCId();
                else if (e.Column == 3)
                    tagcmper = new TagInfoCompAntId();
                else if (e.Column == 4)
                    tagcmper = new TagInfoCompEmdData();
                else if (e.Column == 5)
                    tagcmper = new TagInfoCompPotl();
                else if (e.Column == 6)
                    tagcmper = new TagInfoCompRssi();
                else if (e.Column == 7)
                    tagcmper = new TagInfoCompFreq();
                else if (e.Column == 8)
                    tagcmper = new TagInfoCompPhase();

                tmplist.Sort(tagcmper);
                foreach (TagInfo tag in tmplist)
                {
                    ListViewItem item = new ListViewItem(lvTags.Items.Count.ToString());
                    item.SubItems.Add(tag.readcnt.ToString());
                    item.SubItems.Add(tag.epcid);
                    item.SubItems.Add(tag.antid.ToString());

                    item.SubItems.Add(tag.emddatastr);

                    if (tag.potl == TagProtocol.GEN2)
                        item.SubItems.Add("GEN2");
                    else if (tag.potl == TagProtocol.ISO180006B)
                        item.SubItems.Add("ISO180006B");
                    else if (tag.potl == TagProtocol.IPX256)
                        item.SubItems.Add("IPX256");
                    else if (tag.potl == TagProtocol.IPX64)
                        item.SubItems.Add("IPX64");
                    item.SubItems.Add(tag.RssiRaw.ToString());
                    item.SubItems.Add(tag.Frequency.ToString());
                    item.SubItems.Add(tag.Phase.ToString());
                    lvTags.Items.Add(item);
                }
            }
        }

        private void menuoutputtags_Click(object sender, EventArgs e)
        {
            string filename = null;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "txt(*.txt)| *.txt|csv(*.csv)|*.csv";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
                FileInfo fileInfo = new FileInfo(filename);
                fileInfo.Delete();
                StreamWriter streamWriter = fileInfo.CreateText();
            //    streamWriter.WriteLine("格式：epc，读次数，天线，附加数据，协议,RSSI");
                foreach (ListViewItem viewitem in lvTags.Items)
                {
                    string wline = viewitem.SubItems[2].Text + "," + viewitem.SubItems[1].Text + "," +
                        viewitem.SubItems[3].Text + "," + viewitem.SubItems[4].Text + "," +
                        viewitem.SubItems[5].Text + "," + viewitem.SubItems[6].Text + "," +
                        viewitem.SubItems[7].Text + "," + viewitem.SubItems[8].Text;
                    streamWriter.WriteLine(wline);
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
     
        }

        private void MsgDebugMenu_Click(object sender, EventArgs e)
        {
            FrmMsgDebug frm = new FrmMsgDebug(this);
            frm.ShowDialog();
        }


        private void menulongtaskext_Click(object sender, EventArgs e)
        {
            FrmLongTaskExt frm = new FrmLongTaskExt(modulerdr);
            frm.ShowDialog();
        }

        private void menuitemmultibankwrite_Click(object sender, EventArgs e)
        {
            MultiBankWriteFrm frm = new MultiBankWriteFrm(modulerdr);
            frm.ShowDialog();
        }

        private void pSAMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPsam frm = new FrmPsam(modulerdr);
            frm.ShowDialog();
        }

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    for (int i = 0; i < 4; ++i)
        //    {
        //        ListViewItem item = new ListViewItem(i.ToString());
        //        item.SubItems.Add("1");
        //        item.SubItems.Add(i.ToString()+i.ToString());
        //        item.SubItems.Add("2");

        //        item.SubItems.Add("");

        //        item.SubItems.Add("GEN2");
    
        //        lvTags.Items.Add(item);
        //    }
        //}
    }

    public class AntAndBoll
    {
        public AntAndBoll(int ant, bool conn)
        {
            antid = ant;
            isConn = conn;
        }

        public int antid;
        public bool isConn;
        public UInt16 rpower;
        public UInt16 wpower;
    }

    public class ReaderParams
    {
        public ReaderParams(int rdur, int sdur, int sess)
        {
            readdur = rdur;
            sleepdur = sdur;
            gen2session = sess;
            isIpModify = false;
            isM5eModify = false;
            fisrtLoad = true;
            
            ip = "";
            subnet = "";
            gateway = "";
            macstr = "";
            hasIP = false;
            isGetIp = false;
            Gen2Qval = -2;
            isCheckConnection = false;
            isMultiPotl = false;
            antcnt = -1;
            isRevertAnts = false;
            weightgen2 = 30;
            weight180006b = 30;
            weightipx64 = 30;
            weightipx256 = 30;

            isIdtAnts = false;
            IdtAntsType = 0;
            DurIdtval = 0;
            AfterIdtWaitval = 0;

            FixReadCount = 0;
            isReadFixCount = false;
            isOneReadOneTime = false;

            usecase_ishighspeedblf = false;
            usecase_tagcnt = -1;
            usecase_readperform = -1;
            usecase_antcnt = -1;
        }

        public void resetParams()
        {
            isIpModify = false;
            isM5eModify = false;
            fisrtLoad = true;

            ip = "";
            subnet = "";
            gateway = "";
            macstr = "";
            hasIP = false;
            isGetIp = false;
            Gen2Qval = -2;
            isCheckConnection = false;
            isMultiPotl = false;
            antcnt = -1;
            SixteenDevsrp = null;
            SixteenDevConAnts = null;
            isRevertAnts = false;
            weightgen2 = 30;
            weight180006b = 30;
            weightipx64 = 30;
            weightipx256 = 30;

            isChangeColor = true;
            isUniByEmd = false;
            isUniByAnt = false;

            isIdtAnts = false;
            IdtAntsType = 0;
            DurIdtval = 0;
            AfterIdtWaitval = 0;

            FixReadCount = 0;
            isReadFixCount = false;
            isOneReadOneTime = false;

            usecase_ishighspeedblf = false;
            usecase_tagcnt = -1;
            usecase_readperform = -1;
            usecase_antcnt = -1;

        }

        public bool setGPO1;
        public int gen2session;
        public int readdur;
        public int sleepdur;
        public int antcnt;
        public string hardvir;
        public string softvir;
        public ReaderType readertype;

        public List<AntAndBoll> AntsState = new List<AntAndBoll>();
        public int ModuleReadervir;
        public string ip;
        public string subnet;
        public string gateway;
        public string macstr;
        public bool isGetIp;
        public bool isIpModify;
        public bool isM5eModify;
        public bool fisrtLoad;
        public bool hasIP;
        public int powermin;
        public int powermax;
        public int Gen2Qval;
        public bool isCheckConnection;
        public bool isMultiPotl;
        public SimpleReadPlan SixteenDevsrp = null;
        public int[] SixteenDevConAnts = null;
        public bool isRevertAnts;

        public int weightgen2;
        public int weight180006b;
        public int weightipx64;
        public int weightipx256;

        public bool isChangeColor;
        public bool isUniByEmd;
        public bool isUniByAnt;

        public bool isIdtAnts;
        public int IdtAntsType;
        public int DurIdtval;
        public int AfterIdtWaitval;

        public int FixReadCount;
        public bool isReadFixCount;
        public bool isOneReadOneTime;

        public bool usecase_ishighspeedblf;
        public int usecase_tagcnt;
        public int usecase_readperform;
        public int usecase_antcnt;

    }

    public class TagInfoCompEPCId : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.epcid.CompareTo(y.epcid);
        }
    }

    public class TagInfoCompReadCnt : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.readcnt.CompareTo(y.readcnt);
        }
    }

    public class TagInfoCompPotl : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.potl.CompareTo(y.potl);
        }
    }

    public class TagInfoCompFreq : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.Frequency.CompareTo(y.Frequency);
        }
    }

    public class TagInfoCompPhase : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.Phase.CompareTo(y.Phase);
        }
    }

    public class TagInfoCompRssi : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.RssiRaw.CompareTo(y.RssiRaw);
        }
    }

    public class TagInfoCompEmdData : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.emddatastr.CompareTo(y.emddatastr);
        }
    }

    public class TagInfoCompAntId : IComparer<TagInfo>
    {
        public int Compare(TagInfo x, TagInfo y)
        {
            return x.antid.CompareTo(y.antid);
        }
    }

    public class TagInfo
    {
        public TagInfo(string epc, int rcnt, int ant, DateTime time, TagProtocol potl_, string emdstr)
        {
            epcid = epc;
            readcnt = rcnt;
            antid = ant;
            timestamp = time;
            potl = potl_;
            emddatastr = emdstr;
            RssiSum = 0;
        }
        public string epcid;
        public int readcnt;
        public int antid;
        public TagProtocol potl;
        public DateTime timestamp;
        public string emddatastr;
        public int RssiSum;
        public int RssiRaw;
        public int Frequency;
        public int Phase;
    }

    class DoubleBufferListView : ListView
    {
        public DoubleBufferListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
        }
    }

    //日志类
    public class ProgramLog
    {
        int logdayscnt;//日志文件保持数据的天数限制
        string preffixname = null;//日志文件的固定前缀名 

        DateTime lastupdate = DateTime.Now.Date; //上一次创建新日志文件的时间
        StreamWriter curlogfile = null;//当前的日志文件

        public ProgramLog(int daycnts, string prefname)
        {
            logdayscnt = daycnts;
            preffixname = prefname;
            DelLogFile();
            curlogfile = File.CreateText(preffixname + "_" +
                DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
        }
        //获取备份日志文件名字
        public string GetOldLog()
        {
            DirectoryInfo dir = null;
            dir = new DirectoryInfo(Environment.CurrentDirectory);
            FileInfo[] fls = dir.GetFiles();
            List<DateLogFile> logfiles = new List<DateLogFile>();
            foreach (FileInfo fl in fls)
            {
                if (fl.Name.StartsWith(preffixname))
                {
                    logfiles.Add(new DateLogFile(fl));
                }
            }
            return logfiles[0].logfi.Name;
        }
        //获取当前日志文件名 
        public string GetCurLog()
        {
            DirectoryInfo dir = null;
            dir = new DirectoryInfo(Environment.CurrentDirectory);
            FileInfo[] fls = dir.GetFiles();
            List<DateLogFile> logfiles = new List<DateLogFile>();
            foreach (FileInfo fl in fls)
            {
                if (fl.Name.StartsWith(preffixname))
                {
                    logfiles.Add(new DateLogFile(fl));
                }
            }
            return logfiles[logfiles.Count - 1].logfi.Name;
        }

        //删除多余日志，只保持日志文件不多于两个，且是最近创建的两个 
        private void DelLogFile()
        {
            DirectoryInfo dir = null;
            try
            {
                //根据文件名前缀，找出所有日志文件 
                dir = new DirectoryInfo(Environment.CurrentDirectory);
                FileInfo[] fls = dir.GetFiles();
                List<DateLogFile> logfiles = new List<DateLogFile>();
                foreach (FileInfo fl in fls)
                {
                    if (fl.Name.StartsWith(preffixname))
                    {
                        logfiles.Add(new DateLogFile(fl));
                    }
                }
                //根据日志文件创建的时间排序
                logfiles.Sort();
                //删除多余日子文件
                int delcnt = 0;
                if (logfiles.Count > 1)
                {
                    delcnt = logfiles.Count - 1;
                    for (int i = 0; i < delcnt; ++i)
                    {
                        logfiles[i].logfi.Delete();
                    }
                }
            }
            catch
            {
            }
            finally
            {
            }
        }
        //日志写入函数
        public void WriteLine(string line)
        {
            //首先判断是否需要建立新的日志文件，对某类日志文件都会规定一个期限，比如只存储三天的数据。一旦
            //当前日期大于文件创建日期三，则应该关闭当前日志文件，建立一个新的日志文件
            if (DateTime.Now.Date.Subtract(lastupdate).Days > logdayscnt)
            {
                //关闭当前日志文件
                curlogfile.Dispose();
                //删除多余日志文件
                DelLogFile();
                //创建新日志文件，命名规则为：固定前缀+'_'+日期字符串
                curlogfile = File.CreateText(preffixname + "_" +
                    DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss"));
                //更新前一次创建文件时间
                lastupdate = DateTime.Now.Date;
            }
            else //写入日志,一次写一行
            {
                curlogfile.WriteLine(DateTime.Now.ToString() + "--" + line);
                curlogfile.Flush();
            }

        }
        //内部类，用于日志文件按照创建日期进行排序
        class DateLogFile : IComparable<DateLogFile>
        {
            public DateLogFile(FileInfo fi)
            {
                logfi = fi;
            }
            public int CompareTo(DateLogFile other)
            {
                return logfi.CreationTime.CompareTo(other.logfi.CreationTime);
            }

            public FileInfo logfi;
        }
    }

}