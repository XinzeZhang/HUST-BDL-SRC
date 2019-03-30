using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;

namespace ModuleReaderManager
{
    public partial class FrmLongTaskExt : Form
    {
        public FrmLongTaskExt(Reader rdr_)
        {
            InitializeComponent();
            rdr = rdr_;
        }

        Reader rdr = null;

        private void FrmLongTaskExt_Load(object sender, EventArgs e)
        {
            cbis6btagopreadmem.Enabled = false;
            cbisgen2tagopreadbank.Enabled = false;
            gb6btagop.Enabled = false;
            gbgen2tagop.Enabled = false;
            gbpotlweight.Enabled = false;
            gbantjudge.Enabled = false;
            gbantjuddur.Enabled = false;
            gbantjudaft.Enabled = false;

            gbjaml1.Enabled = false;
        }

        LongTaskExtInfo ParseLongTask()
        {
            LongTaskExtInfo lteinfo = new LongTaskExtInfo();
            List<LongTaskExtInfo.LT_PotlConf> potls = new List<LongTaskExtInfo.LT_PotlConf>();

            if (cbistagopgen2.Checked)
            {
                LongTaskExtInfo.LT_PotlConf gen2potl = new LongTaskExtInfo.LT_PotlConf();
                gen2potl.Potl = TagProtocol.GEN2;

                if (cbistagop6b.Checked)
                {
                    if (tbgen2tagopweight.Text.Trim() == string.Empty || tb6btagopweight.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请协议权重");
                        return null;
                    }
                    gen2potl.Weight = byte.Parse(tbgen2tagopweight.Text.Trim());
                }

                if (cbisgen2tagopreadbank.Checked)
                {
                    if (cbbgen2tagopbank.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择gen2标签读哪个bank");
                        return null;
                    }
                    if (tbgen2tagopaddr.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入gen2标签bank起始地址");
                        return null;
                    }
                    if (tbgen2tagopblkcnt.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入gen2标签读块数");
                        return null;
                    }
                    if (cbbgen2tagopbankreadmode.SelectedIndex == -1)
                    {
                        MessageBox.Show("请输入gen2标签bank读取模式");
                        return null;
                    }
                    gen2potl.Address = UInt16.Parse(tbgen2tagopaddr.Text.Trim());
                    gen2potl.Bank = (byte)(cbbgen2tagopbank.SelectedIndex + 1);
                    gen2potl.Blkcnt = byte.Parse(tbgen2tagopblkcnt.Text.Trim());
                    gen2potl.Brmode = (LongTaskExtInfo.LT_PotlConf.BankReadMode)(cbbgen2tagopbankreadmode.SelectedIndex + 1);
                    gen2potl.Optype = LongTaskExtInfo.LT_PotlConf.LTTAGOPEXTType.LTTAGOPEXTType_InventoryAndReadBank;
                }
                else
                    gen2potl.Optype = LongTaskExtInfo.LT_PotlConf.LTTAGOPEXTType.LTTAGOPEXTType_Inventory;
                potls.Add(gen2potl);
            }
            if (cbistagop6b.Checked)
            {
                LongTaskExtInfo.LT_PotlConf iso183k6bpotl = new LongTaskExtInfo.LT_PotlConf();
                iso183k6bpotl.Potl = TagProtocol.ISO180006B;
                if (cbistagopgen2.Checked)
                    iso183k6bpotl.Weight = byte.Parse(tb6btagopweight.Text.Trim());

                if (cbis6btagopreadmem.Checked)
                {
                    if (tb6btagopaddr.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入6b标签bank起始地址");
                        return null;
                    }
                    if (tb6btagopblkcnt.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入6b标签读块数");
                        return null;
                    }
                    iso183k6bpotl.Address = UInt16.Parse(tb6btagopaddr.Text.Trim());
                    iso183k6bpotl.Blkcnt = byte.Parse(tb6btagopblkcnt.Text.Trim());
                    iso183k6bpotl.Optype = LongTaskExtInfo.LT_PotlConf.LTTAGOPEXTType.LTTAGOPEXTType_InventoryAndReadBank;
                }
                else
                    iso183k6bpotl.Optype = LongTaskExtInfo.LT_PotlConf.LTTAGOPEXTType.LTTAGOPEXTType_Inventory;
                potls.Add(iso183k6bpotl);
            }

            if (tbmaxepclen.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入最大epc长度");
                return null;
            }
            lteinfo.MaxEpcLen = int.Parse(tbmaxepclen.Text.Trim());
            lteinfo.PotlConf = potls.ToArray();
            if (!(cbisant1.Checked || cbisant2.Checked || cbisant3.Checked || cbisant4.Checked))
            {
                MessageBox.Show("请至少选择一个天线");
                return null;
            }
            List<int> ants = new List<int>();
            if (cbisant1.Checked)
                ants.Add(1);
            if (cbisant2.Checked)
                ants.Add(2);
            if (cbisant3.Checked)
                ants.Add(3);
            if (cbisant4.Checked)
                ants.Add(4);
            lteinfo.OpAnts = ants.ToArray();
            if (tbreaddur.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入盘存周期");
                return null;
            }
            lteinfo.InvDur = int.Parse(tbreaddur.Text.Trim());

            if (tbsleepdur.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入盘存间隔");
                return null;
            }
            lteinfo.InvInterval = int.Parse(tbsleepdur.Text.Trim());

            if (cbisrevertants.Checked)
                lteinfo.IsRevertAnts = true;
            else
                lteinfo.IsRevertAnts = false;

            if (cbisantjudge.Checked)
            {
                if (!(rbantjudalgafttaglv.Checked || rbantjudalgbydur.Checked))
                {
                    MessageBox.Show("请选择天线判决算法");
                    return null;
                }
                lteinfo.EnableAntJudgeAlgorithm = true;
                LongTaskExtInfo.AntJudgeConf ajc = new LongTaskExtInfo.AntJudgeConf();
                if (rbantjudalgafttaglv.Checked)
                {
                    ajc.AntJudgeAlg =
                        LongTaskExtInfo.AntJudgeConf.AntJudgeAlgorithm.AntJudgeAlgorithm_AfterTagLeave;
                    if (tbantdugaftwaitdur.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入等待时间");
                        return null;
                    }
                    if (tbantjudafttimeout.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入超时时间");
                        return null;
                    }
                    ajc.TimeDurAftTagLeaveJudge = int.Parse(tbantdugaftwaitdur.Text.Trim());
                    ajc.TimeoutAftTagLeaveJudge = int.Parse(tbantjudafttimeout.Text.Trim());
                }
                else
                {
                    ajc.AntJudgeAlg =
                        LongTaskExtInfo.AntJudgeConf.AntJudgeAlgorithm.AntJudgeAlgorithm_EachDuration;
                    if (tbantjudcycle.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入判决周期");
                        return null;
                    }
                    ajc.AntJudgeCycle = int.Parse(tbantjudcycle.Text.Trim());
                }
                lteinfo.AntJudgeAlgConf = ajc;
            }
            else
                lteinfo.EnableAntJudgeAlgorithm = false;
            lteinfo.IsTriggerByGpi = false;
            lteinfo.IsDriveGpo = false;
            if (cbisackbysid.Checked)
                lteinfo.IsAckBySerialNum = true;
            else
                lteinfo.IsAckBySerialNum = false;

            if (tbrtuploadip.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入实时数据上传地址");
                return null;
            }
            if (tbrtuploadport.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入实时数据上传端口");
                return null;
            }
            if (tboluploadip.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入离线数据上传地址");
                return null;
            }
            if (tboluploadport.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入离线数据上传端口");
                return null;
            }
            if (tbmaxtagbuffercnt.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入最大缓存数据量");
                return null;
            }
            if (cbbuploadmode.SelectedIndex == -1)
            {
                MessageBox.Show("请选择数据上传模式");
                return null;
            }
            if (cbbtcpmode.SelectedIndex == -1)
            {
                MessageBox.Show("请选择TCP连接模式");
                return null;
            }
            lteinfo.RealTimeUploadIp = tbrtuploadip.Text.Trim();
            lteinfo.RealTimeUploadPort = int.Parse(tbrtuploadport.Text.Trim());
            lteinfo.OfflineUploadIp = tboluploadip.Text.Trim();
            lteinfo.OfflineUploadPort = int.Parse(tboluploadport.Text.Trim());
            lteinfo.MaxOfflineTagBufferCnt = int.Parse(tbmaxtagbuffercnt.Text.Trim());
            lteinfo.UploadTagMode = (LongTaskExtInfo.UploadDataMode)(cbbuploadmode.SelectedIndex + 1);
            lteinfo.TcpCnnMode = (LongTaskExtInfo.TcpConnectionMode)(cbbtcpmode.SelectedIndex + 1);

            if (tbgen2tagoppwd.Text.Trim() == string.Empty)
            {
                lteinfo.AccessPassword = (uint)0;
            }
            else
            {
                byte[] tmp = ByteFormat.FromHex(tbgen2tagoppwd.Text.Trim());
                lteinfo.AccessPassword = (uint)((tmp[0] << 24) | (tmp[1] << 16) | (tmp[2] << 8) | tmp[3]);
            }

            if (cbisjaml0.Checked)
            {
                lteinfo.IsChanPowerWhenJamLevel0 = true;
                if (this.tbjaml0crtime.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入阻塞级别0连续读到时间");
                    return null;
                }
                lteinfo.TrafficJamLevel0ReadTime = UInt16.Parse(this.tbjaml0crtime.Text.Trim());
                if (this.tbl0gen2ant1pwr.Text.Trim() == string.Empty || this.tbl0gen2ant2pwr.Text.Trim() == string.Empty ||
                    this.tbl0gen2ant3pwr.Text.Trim() == string.Empty || this.tbl0gen2ant4pwr.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入阻塞级别0gen2功率");
                    return null;
                }
                if (this.tbl06bant1pwr.Text.Trim() == string.Empty || this.tbl06bant2pwr.Text.Trim() == string.Empty ||
                    this.tbl06bant3pwr.Text.Trim() == string.Empty || this.tbl06bant4pwr.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入阻塞级别06b功率");
                    return null;
                }

                lteinfo.TrafficJamLevel0Gen2Pwr = new UInt16[4];
                lteinfo.TrafficJamLevel0Gen2Pwr[0] = UInt16.Parse(this.tbl0gen2ant1pwr.Text.Trim());
                lteinfo.TrafficJamLevel0Gen2Pwr[1] = UInt16.Parse(this.tbl0gen2ant2pwr.Text.Trim());
                lteinfo.TrafficJamLevel0Gen2Pwr[2] = UInt16.Parse(this.tbl0gen2ant3pwr.Text.Trim());
                lteinfo.TrafficJamLevel0Gen2Pwr[3] = UInt16.Parse(this.tbl0gen2ant4pwr.Text.Trim());

                lteinfo.TrafficJamLevel06BPwr = new UInt16[4];
                lteinfo.TrafficJamLevel06BPwr[0] = UInt16.Parse(this.tbl06bant1pwr.Text.Trim());
                lteinfo.TrafficJamLevel06BPwr[1] = UInt16.Parse(this.tbl06bant2pwr.Text.Trim());
                lteinfo.TrafficJamLevel06BPwr[2] = UInt16.Parse(this.tbl06bant3pwr.Text.Trim());
                lteinfo.TrafficJamLevel06BPwr[3] = UInt16.Parse(this.tbl06bant4pwr.Text.Trim());
            }
            else
                lteinfo.IsChanPowerWhenJamLevel0 = false;

            if (cbisjaml1.Checked)
            {
                lteinfo.IsChanPowerWhenJamLevel1 = true;
                if (this.tbjaml1crtime.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入阻塞级别1连续读到时间");
                    return null;
                }
                lteinfo.TrafficJamLevel1ReadTime = UInt16.Parse(this.tbjaml1crtime.Text.Trim());
                if (this.tbl1gen2ant1pwr.Text.Trim() == string.Empty || this.tbl1gen2ant2pwr.Text.Trim() == string.Empty ||
                    this.tbl1gen2ant3pwr.Text.Trim() == string.Empty || this.tbl1gen2ant4pwr.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入阻塞级别1gen2功率");
                    return null;
                }
                if (this.tbl16bant1pwr.Text.Trim() == string.Empty || this.tbl16bant2pwr.Text.Trim() == string.Empty ||
                    this.tbl16bant3pwr.Text.Trim() == string.Empty || this.tbl16bant4pwr.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入阻塞级别16b功率");
                    return null;
                }
                if (lteinfo.TrafficJamLevel1ReadTime <= lteinfo.TrafficJamLevel0ReadTime)
                {
                    MessageBox.Show("阻塞级别1连续读取时间必须大于阻塞级别0连续读取时间");
                    return null;
                }

                lteinfo.TrafficJamLevel1Gen2Pwr = new UInt16[4];
                lteinfo.TrafficJamLevel1Gen2Pwr[0] = UInt16.Parse(this.tbl1gen2ant1pwr.Text.Trim());
                lteinfo.TrafficJamLevel1Gen2Pwr[1] = UInt16.Parse(this.tbl1gen2ant2pwr.Text.Trim());
                lteinfo.TrafficJamLevel1Gen2Pwr[2] = UInt16.Parse(this.tbl1gen2ant3pwr.Text.Trim());
                lteinfo.TrafficJamLevel1Gen2Pwr[3] = UInt16.Parse(this.tbl1gen2ant4pwr.Text.Trim());

                lteinfo.TrafficJamLevel16BPwr = new UInt16[4];
                lteinfo.TrafficJamLevel16BPwr[0] = UInt16.Parse(this.tbl16bant1pwr.Text.Trim());
                lteinfo.TrafficJamLevel16BPwr[1] = UInt16.Parse(this.tbl16bant2pwr.Text.Trim());
                lteinfo.TrafficJamLevel16BPwr[2] = UInt16.Parse(this.tbl16bant3pwr.Text.Trim());
                lteinfo.TrafficJamLevel16BPwr[3] = UInt16.Parse(this.tbl16bant4pwr.Text.Trim());
            }
            else
                lteinfo.IsChanPowerWhenJamLevel1 = false;

            return lteinfo;
        }

        private void btnexecrn_Click(object sender, EventArgs e)
        {
            LongTaskExtInfo lteinfo = ParseLongTask();
            if (lteinfo == null)
                return;
            lteinfo.Action = LongTaskExtInfo.LongTaskAction.LTA_Start_Instant;
            try
            {
                rdr.ParamSet("LongTaskExtSetting", lteinfo);
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
        }

        private void cbistagop6b_CheckedChanged(object sender, EventArgs e)
        {
            if (cbistagop6b.Checked)
            {
                if (cbistagopgen2.Checked)
                    gbpotlweight.Enabled = true;
                cbis6btagopreadmem.Enabled = true;
                if (cbis6btagopreadmem.Checked)
                {
                    gb6btagop.Enabled = true;
                }
            }
            else
            {
                gbpotlweight.Enabled = false;
                cbis6btagopreadmem.Checked = false;
                cbis6btagopreadmem.Enabled = false;
                gb6btagop.Enabled = false;
            }
        }

        private void cbistagopgen2_CheckedChanged(object sender, EventArgs e)
        {
            if (cbistagopgen2.Checked)
            {
                if (cbistagop6b.Checked)
                    gbpotlweight.Enabled = true;
                cbisgen2tagopreadbank.Enabled = true;
                if (cbisgen2tagopreadbank.Checked)
                {
                    gbgen2tagop.Enabled = true;
                }
            }
            else
            {
                gbpotlweight.Enabled = false;
                cbisgen2tagopreadbank.Checked = false;
                cbisgen2tagopreadbank.Enabled = false;
                gbgen2tagop.Enabled = false;
            }
        }

        private void cbisgen2tagopreadbank_CheckedChanged(object sender, EventArgs e)
        {
            if (cbisgen2tagopreadbank.Checked)
                gbgen2tagop.Enabled = true;
            else
                gbgen2tagop.Enabled = false;
        }

        private void cbis6btagopreadmem_CheckedChanged(object sender, EventArgs e)
        {
            if (cbis6btagopreadmem.Checked)
                gb6btagop.Enabled = true;
            else
                gb6btagop.Enabled = false;
        }

        private void cbisantjudge_CheckedChanged(object sender, EventArgs e)
        {
            if (cbisantjudge.Checked)
            {
                gbantjudge.Enabled = true;
            }
            else
                gbantjudge.Enabled = false;
        }

        private void btnstoptask_Click(object sender, EventArgs e)
        {
            try
            {
                rdr.ParamSet("StopLongTaskExt", LongTaskExtInfo.StopLongTaskAction.SLA_Stop_LongTask);
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
            
        }

        private void btnDataRecv_Click(object sender, EventArgs e)
        {
            if (FrmLongTaskTagRecv.recvfrm != null)
            {
                FrmLongTaskTagRecv.recvfrm.Focus();
            }
            else
            {
                FrmLongTaskTagRecv frm = new FrmLongTaskTagRecv();
                frm.Show();
            }
        }

        private void rbantjudalgafttaglv_CheckedChanged(object sender, EventArgs e)
        {
            if (rbantjudalgafttaglv.Checked)
                gbantjudaft.Enabled = true;
            else
                gbantjudaft.Enabled = false;
        }

        private void rbantjudalgbydur_CheckedChanged(object sender, EventArgs e)
        {
            if (rbantjudalgbydur.Checked)
                gbantjuddur.Enabled = true;
            else
                gbantjuddur.Enabled = false;
        }

        private void btngetname_Click(object sender, EventArgs e)
        {
            this.tbreadername.Text = (string)rdr.ParamGet("ReaderName");
        }

        private void btnsetname_Click(object sender, EventArgs e)
        {
            if (this.tbreadername.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入读写器名字");
                return;
            }
            rdr.ParamSet("ReaderName", this.tbreadername.Text.Trim());
        }

        private void btnexecpn_Click(object sender, EventArgs e)
        {
            LongTaskExtInfo lteinfo = ParseLongTask();
            if (lteinfo == null)
                return;
            lteinfo.Action = LongTaskExtInfo.LongTaskAction.LTA_Start_PowerOn;
            try
            {
                rdr.ParamSet("LongTaskExtSetting", lteinfo);
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
        }

        private void btnstopdeltask_Click(object sender, EventArgs e)
        {
            try
            {
                rdr.ParamSet("StopLongTaskExt", LongTaskExtInfo.StopLongTaskAction.SLA_Stop_LongTask_And_Modify_Conf);
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
        }

        private void btngetlongtask_Click(object sender, EventArgs e)
        {
            try
            {
                LongTaskExtInfo lteinfo = (LongTaskExtInfo)rdr.ParamGet("LongTaskExtSetting");
                if (lteinfo == null)
                {
                    MessageBox.Show("没有长任务");
                    return;
                }
                this.cbistagopgen2.Checked = false;
                this.cbistagop6b.Checked = false;
                this.cbisgen2tagopreadbank.Checked = false;
                this.cbis6btagopreadmem.Checked = false;

                foreach (LongTaskExtInfo.LT_PotlConf potl in lteinfo.PotlConf)
                {
                    if (potl.Potl == TagProtocol.GEN2)
                    {
                        this.cbistagopgen2.Checked = true;
                        this.tbgen2tagopweight.Text = potl.Weight.ToString();
                        if (potl.Optype == LongTaskExtInfo.LT_PotlConf.LTTAGOPEXTType.LTTAGOPEXTType_InventoryAndReadBank)
                        {
                            this.cbisgen2tagopreadbank.Checked = true;
                            this.cbbgen2tagopbank.SelectedIndex = potl.Bank - 1;
                            this.tbgen2tagopaddr.Text = potl.Address.ToString();
                            this.tbgen2tagopblkcnt.Text = potl.Blkcnt.ToString();
                            this.tbgen2tagoppwd.Text = lteinfo.AccessPassword.ToString("X2");
                            this.cbbgen2tagopbankreadmode.SelectedIndex = (int)potl.Brmode - 1;
                        }
                    }
                    else if (potl.Potl == TagProtocol.ISO180006B)
                    {
                        this.cbistagop6b.Checked = true;
                        this.tb6btagopweight.Text = potl.Weight.ToString();
                        if (potl.Optype == LongTaskExtInfo.LT_PotlConf.LTTAGOPEXTType.LTTAGOPEXTType_InventoryAndReadBank)
                        {
                            this.cbis6btagopreadmem.Checked = true;
                            this.tb6btagopaddr.Text = potl.Address.ToString();
                            this.tb6btagopblkcnt.Text = potl.Blkcnt.ToString();
                        }
                    }
                }
                this.tbmaxepclen.Text = lteinfo.MaxEpcLen.ToString();

                this.cbisant1.Checked = false;
                this.cbisant2.Checked = false;
                this.cbisant3.Checked = false;
                this.cbisant4.Checked = false;

                foreach (int ant in lteinfo.OpAnts)
                {
                    if (ant == 1)
                        this.cbisant1.Checked = true;
                    else if (ant == 2)
                        this.cbisant2.Checked = true;
                    else if (ant == 3)
                        this.cbisant3.Checked = true;
                    else if (ant == 4)
                        this.cbisant4.Checked = true;
                }
                this.tbreaddur.Text = lteinfo.InvDur.ToString();
                this.tbsleepdur.Text = lteinfo.InvInterval.ToString();
                this.cbisrevertants.Checked = lteinfo.IsRevertAnts;

                this.cbisantjudge.Checked = false;
                this.checkBox1.Checked = lteinfo.IsTriggerByGpi;
                this.checkBox2.Checked = lteinfo.IsDriveGpo;
                if (lteinfo.AntJudgeAlgConf.AntJudgeAlg != LongTaskExtInfo.AntJudgeConf.AntJudgeAlgorithm.AntJudgeAlgorithm_None)
                {
                    this.cbisantjudge.Checked = true;
                    if (lteinfo.AntJudgeAlgConf.AntJudgeAlg == 
                        LongTaskExtInfo.AntJudgeConf.AntJudgeAlgorithm.AntJudgeAlgorithm_EachDuration)
                    {
                        this.rbantjudalgbydur.Checked = true;
                        this.tbantjudcycle.Text = lteinfo.AntJudgeAlgConf.AntJudgeCycle.ToString();
                    }
                    else if (lteinfo.AntJudgeAlgConf.AntJudgeAlg == 
                        LongTaskExtInfo.AntJudgeConf.AntJudgeAlgorithm.AntJudgeAlgorithm_AfterTagLeave)
                    {
                        this.rbantjudalgafttaglv.Checked = true;
                        this.tbantjudafttimeout.Text = lteinfo.AntJudgeAlgConf.TimeoutAftTagLeaveJudge.ToString();
                        this.tbantdugaftwaitdur.Text = lteinfo.AntJudgeAlgConf.TimeDurAftTagLeaveJudge.ToString();
                    }
                }

                this.tbrtuploadip.Text = lteinfo.RealTimeUploadIp;
                this.tbrtuploadport.Text = lteinfo.RealTimeUploadPort.ToString();
                this.tboluploadip.Text = lteinfo.OfflineUploadIp;
                this.tboluploadport.Text = lteinfo.OfflineUploadPort.ToString();
                this.tbmaxtagbuffercnt.Text = lteinfo.MaxOfflineTagBufferCnt.ToString();
                this.cbbuploadmode.SelectedIndex = (int)lteinfo.UploadTagMode - 1;
                this.cbbtcpmode.SelectedIndex = (int)lteinfo.TcpCnnMode - 1;
                this.cbisackbysid.Checked = lteinfo.IsAckBySerialNum;

                if (lteinfo.IsChanPowerWhenJamLevel0)
                {
                    this.gbjaml0.Enabled = true;
                    this.cbisjaml0.Checked = true;
                    this.tbjaml0crtime.Text = lteinfo.TrafficJamLevel0ReadTime.ToString();
                    this.tbl0gen2ant1pwr.Text = lteinfo.TrafficJamLevel0Gen2Pwr[0].ToString();
                    this.tbl0gen2ant2pwr.Text = lteinfo.TrafficJamLevel0Gen2Pwr[1].ToString();
                    this.tbl0gen2ant3pwr.Text = lteinfo.TrafficJamLevel0Gen2Pwr[2].ToString();
                    this.tbl0gen2ant4pwr.Text = lteinfo.TrafficJamLevel0Gen2Pwr[3].ToString();

                    this.tbl06bant1pwr.Text = lteinfo.TrafficJamLevel06BPwr[0].ToString();
                    this.tbl06bant2pwr.Text = lteinfo.TrafficJamLevel06BPwr[1].ToString();
                    this.tbl06bant3pwr.Text = lteinfo.TrafficJamLevel06BPwr[2].ToString();
                    this.tbl06bant4pwr.Text = lteinfo.TrafficJamLevel06BPwr[3].ToString();
                }
                else
                {
                    this.gbjaml0.Enabled = true;
                    this.cbisjaml0.Checked = false;
                }

                if (lteinfo.IsChanPowerWhenJamLevel1)
                {
                    this.gbjaml1.Enabled = true;
                    this.cbisjaml1.Checked = true;
                    this.tbjaml1crtime.Text = lteinfo.TrafficJamLevel1ReadTime.ToString();
                    this.tbl1gen2ant1pwr.Text = lteinfo.TrafficJamLevel1Gen2Pwr[0].ToString();
                    this.tbl1gen2ant2pwr.Text = lteinfo.TrafficJamLevel1Gen2Pwr[1].ToString();
                    this.tbl1gen2ant3pwr.Text = lteinfo.TrafficJamLevel1Gen2Pwr[2].ToString();
                    this.tbl1gen2ant4pwr.Text = lteinfo.TrafficJamLevel1Gen2Pwr[3].ToString();

                    this.tbl16bant1pwr.Text = lteinfo.TrafficJamLevel16BPwr[0].ToString();
                    this.tbl16bant2pwr.Text = lteinfo.TrafficJamLevel16BPwr[1].ToString();
                    this.tbl16bant3pwr.Text = lteinfo.TrafficJamLevel16BPwr[2].ToString();
                    this.tbl16bant4pwr.Text = lteinfo.TrafficJamLevel16BPwr[3].ToString();
                }
                else
                {
                    this.gbjaml1.Enabled = false;
                    this.cbisjaml1.Checked = false;
                }
            }

            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
        }

        private void cbisjaml1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbisjaml1.Checked)
            {
                this.gbl1gen2pwr.Enabled = true;
                this.gbl16bpwr.Enabled = true;
            }
            else
            {
                this.gbl1gen2pwr.Enabled = false;
                this.gbl16bpwr.Enabled = false;
            }
        }

        private void cbisjaml0_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbisjaml0.Checked)
            {
                this.gbl0gen2pwr.Enabled = true;
                this.gbl06bpwr.Enabled = true;
            }
            else
            {
                this.gbl0gen2pwr.Enabled = false;
                this.gbl06bpwr.Enabled = false;
            }
        }

        private void cbisjaml0_CheckedChanged_1(object sender, EventArgs e)
        {
            if (this.cbisjaml0.Checked)
            {
                this.gbjaml1.Enabled = true;
            }
            else
            {
                this.gbjaml1.Enabled = false;
            }
        }

    }
}
