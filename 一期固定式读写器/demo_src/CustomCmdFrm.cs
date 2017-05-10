using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;
using ModuleTech.Gen2;
using ModuleTech.CustomCmd;
using ModuleLibrary;


namespace ModuleReaderManager
{
    public partial class CustomCmdFrm : Form
    {
        Reader mrdr = null;
        ReaderParams rparam = null;

        public CustomCmdFrm(Reader rdr, ReaderParams param)
        {
            InitializeComponent();
            mrdr = rdr;
            rparam = param;
        }

        Dictionary<int, RadioButton> allants = new Dictionary<int, RadioButton>();

        private int IsAntSet()
        {

            int ret = -1;
            for (int i = 1; i <= allants.Count; ++i)
            {
                if (allants[i].Checked)
                {
                    mrdr.ParamSet("TagopAntenna", i);
                    if (allants[i].ForeColor == Color.Red)
                        ret = 1;
                    else
                        ret = 0;
                }
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


        private void CustomCmdFrm_Load(object sender, EventArgs e)
        {
            allants.Add(1, rbant1);
            allants.Add(2, rbant2);
            allants.Add(3, rbant3);
            allants.Add(4, rbant4);

            for (int i = 1; i <= allants.Count; ++i)
                allants[i].Enabled = false;

            for (int j = 0; j < rparam.AntsState.Count; ++j)
            {
                allants[rparam.AntsState[j].antid].Enabled = true;
                if (rparam.AntsState[j].isConn)
                    allants[rparam.AntsState[j].antid].ForeColor = Color.Green;
                else
                    allants[rparam.AntsState[j].antid].ForeColor = Color.Red;
            }

            this.timer1.Enabled = false;
            mrdr.ParamSet("TagopProtocol", TagProtocol.GEN2);
        }

        private void btnChangeEAS_Click(object sender, EventArgs e)
        {
            int ret;
            TagChipType nxpchiptype = TagChipType.TagChipType_None;
            ret = IsAntSet();
            if (ret == -1)
            {
                MessageBox.Show("请选择操作天线");
                return;
            }
            else if (ret == 1)
            {
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;

            }

            if ((!this.rbEASset.Checked) && (!this.rbEASreset.Checked))
            {
                MessageBox.Show("请选择EAS状态");
                return;
            }

            if (this.cbbnxpchiptype.SelectedIndex == -1)
            {
                MessageBox.Show("请选择芯片类型");
                return;
            }
            else
            {
                if (this.cbbnxpchiptype.SelectedIndex == 0)
                    nxpchiptype = TagChipType.TagChipType_NXP_G2X;
                else if (this.cbbnxpchiptype.SelectedIndex == 1)
                    nxpchiptype = TagChipType.TagChipType_NXP_G2i;
            }

            bool isSet = false;

            if (this.rbEASset.Checked)
                isSet = true;
            else
                isSet = false;

            byte [] pwd = null;

            ret = Form1.IsValidPasswd(this.tbaccesspasswd.Text.Trim());
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
            Gen2TagFilter filter = null;
            if (checkfilter(ref filter) != 0)
                return;
            NXP_ChangeEASPara ChEasPara = new NXP_ChangeEASPara(pwd, isSet, nxpchiptype);

            try
            {
                mrdr.CustomCmd(filter, CustomCmdType.NXP_ChangeEAS, ChEasPara);
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
        }

        private void btnEASAlarm_Click(object sender, EventArgs e)
        {
            TagChipType nxpchiptype = TagChipType.TagChipType_None;
            int ret;
            ret = IsAntSet();
            if (ret == -1)
            {
                MessageBox.Show("请选择操作天线");
                return;
            }
            else if (ret == 1)
            {
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;

            }

            if (this.cbbnxpchiptype.SelectedIndex == -1)
            {
                MessageBox.Show("请选择芯片类型");
                return;
            }
            else
            {
                if (this.cbbnxpchiptype.SelectedIndex == 0)
                    nxpchiptype = TagChipType.TagChipType_NXP_G2X;
                else if (this.cbbnxpchiptype.SelectedIndex == 1)
                    nxpchiptype = TagChipType.TagChipType_NXP_G2i;
            }

            try
            {
                NXP_EASAlarmPara EasAlarmPara = new NXP_EASAlarmPara(0x01, 0x02, 0x01, nxpchiptype);
                NXP_EASAlarmResult result = (NXP_EASAlarmResult)mrdr.CustomCmd(null, CustomCmdType.NXP_EASAlarm, EasAlarmPara);
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

        int checkfilter(ref Gen2TagFilter flt)
        {
            if (this.cbisfilter.Checked)
            {

                int ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
                switch (ret)
                {
                    case -3:
                        MessageBox.Show("匹配数据不能为空");
                        return -1;
                    case -1:
                        MessageBox.Show("匹配数据只能是二进制字符串");
                        return -1;

                }

                if (ret != 0)
                    return -1;
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return -1;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return -1;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return -1;
                }
                else
                {
                    try
                    {
                        bitaddr = int.Parse(this.tbfilteraddr.Text.Trim());
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show("起始地址请输入数字");
                        return -1;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return -1;
                    }
                }

                byte[] filterbytes = new byte[(this.tbfldata.Text.Trim().Length - 1) / 8 + 1];
                for (int c = 0; c < filterbytes.Length; ++c)
                    filterbytes[c] = 0;

                int bitcnt = 0;
                foreach (Char ch in this.tbfldata.Text.Trim())
                {
                    if (ch == '1')
                        filterbytes[bitcnt / 8] |= (byte)(0x01 << (7 - bitcnt % 8));
                    bitcnt++;

                }

                flt = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
                return 0;
            }
            flt = null;
            return 0;
        }

        private void btnbrl_Click(object sender, EventArgs e)
        {
            int ret;
            ret = IsAntSet();
            if (ret == -1)
            {
                MessageBox.Show("请选择操作天线");
                return;
            }
            else if (ret == 1)
            {
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;

            }

            byte[] pwd = null;

            ret = Form1.IsValidPasswd(this.tbaccesspasswd.Text.Trim());
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

            byte bitmap = 0;
            if (cb1.Checked)
                bitmap |= 1 << 7;
            if (cb2.Checked)
                bitmap |= 1 << 6;
            if (cb3.Checked)
                bitmap |= 1 << 5;
            if (cb4.Checked)
                bitmap |= 1 << 4;
            if (cb5.Checked)
                bitmap |= 1 << 3;
            if (cb6.Checked)
                bitmap |= 1 << 2;
            if (cb7.Checked)
                bitmap |= 1 << 1;
            if (cb8.Checked)
                bitmap |= 1 << 0;

            Gen2TagFilter filter = null;
            if (checkfilter(ref filter) != 0)
                return;

            try
            {
                ALIEN_Higgs3_BlockReadLockPara para = new ALIEN_Higgs3_BlockReadLockPara(bitmap, pwd);
                mrdr.CustomCmd(filter, CustomCmdType.ALIEN_Higgs3_BlockReadLock, para);
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
                MessageBox.Show("操作失败" + ex.ToString());
                return;
            }

        }

        bool isEASDetect = false;

        private void btnEASDetect_Click(object sender, EventArgs e)
        {
            if (this.cbbnxpchiptype.SelectedIndex == -1)
            {
                MessageBox.Show("请选择芯片类型");
                return;
            }

            if (!isEASDetect)
            {
                int ret;
                ret = IsAntSet();
                if (ret == -1)
                {
                    MessageBox.Show("请选择操作天线");
                    return;
                }
                else if (ret == 1)
                {
                    DialogResult stat = DialogResult.OK;
                    stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                    MessageBoxDefaultButton.Button2);
                    if (stat != DialogResult.OK)
                        return;

                }

                this.btnEASDetect.Text = "停止探测";
                mrdr.ParamSet("OpTimeout", (ushort)250);
                this.timer1.Enabled = true;
                isEASDetect = true;
            }
            else
            {
                this.btnEASDetect.Text = "EAS探测";
                this.timer1.Enabled = false;
                isEASDetect = false;
                mrdr.ParamSet("OpTimeout", (ushort)1000);
                this.labEASAlert.BackColor = Color.Gray;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TagChipType nxpchiptype = TagChipType.TagChipType_None;
            try
            {
                this.labEASAlert.BackColor = Color.Gray;

                if (this.cbbnxpchiptype.SelectedIndex == 0)
                    nxpchiptype = TagChipType.TagChipType_NXP_G2X;
                else if (this.cbbnxpchiptype.SelectedIndex == 1)
                    nxpchiptype = TagChipType.TagChipType_NXP_G2i;

                NXP_EASAlarmPara EasAlarmPara = new NXP_EASAlarmPara(0x01, 0x02, 0x01, nxpchiptype);
                NXP_EASAlarmResult result = (NXP_EASAlarmResult)mrdr.CustomCmd(null, CustomCmdType.NXP_EASAlarm, EasAlarmPara);
                tbEASAlarmData.Text = ByteFormat.ToHex(result.EASAlarmData);
                System.Media.SystemSounds.Beep.Play();
                this.labEASAlert.BackColor = Color.Red;
            }
            catch (Exception ex)
            {
            }
        }

        private void btnSetReadProtect_Click(object sender, EventArgs e)
        {
            int ret;
            ret = IsAntSet();
            if (ret == -1)
            {
                MessageBox.Show("请选择操作天线");
                return;
            }
            else if (ret == 1)
            {
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;

            }

            byte[] pwd = null;

            ret = Form1.IsValidPasswd(this.tbaccesspasswd.Text.Trim());
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

            Gen2TagFilter filter = null;
            if (checkfilter(ref filter) != 0)
                return;
            try
            {
                NXP_SetReadProtectPara para = new NXP_SetReadProtectPara(pwd);
                mrdr.CustomCmd(filter, CustomCmdType.NXP_SetReadProtect, para);
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
            int ret;
            ret = IsAntSet();
            if (ret == -1)
            {
                MessageBox.Show("请选择操作天线");
                return;
            }
            else if (ret == 1)
            {
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;

            }

            byte[] pwd = null;

            ret = Form1.IsValidPasswd(this.tbaccesspasswd.Text.Trim());
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

            Gen2TagFilter filter = null;
            if (checkfilter(ref filter) != 0)
                return;

            try
            {
                NXP_ResetReadProtectPara para = new NXP_ResetReadProtectPara(pwd);
                mrdr.CustomCmd(filter, CustomCmdType.NXP_ResetReadProtect, para);
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

        private void CustomCmdFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timer1.Enabled = false;
        }

        private void btnsetimpinjqt_Click(object sender, EventArgs e)
        {
            int ret;
            ret = IsAntSet();
            if (ret == -1)
            {
                MessageBox.Show("请选择操作天线");
                return;
            }
            else if (ret == 1)
            {
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;

            }

            byte[] pwd = null;

            ret = Form1.IsValidPasswd(this.tbaccesspasswd.Text.Trim());
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

            Gen2TagFilter filter = null;
            if (checkfilter(ref filter) != 0)
                return;

            if ((!this.rbimpinjqtread.Checked) && (!this.rbimpinjwrite.Checked))
            {
                MessageBox.Show("请选择命令类型");
                return;
            }

            try
            {
                if (this.rbimpinjqtread.Checked)
                {
                    IMPINJ_M4_QtPara para = new IMPINJ_M4_QtPara(pwd);

                    IMPINJ_M4_QtResult qtret = (IMPINJ_M4_QtResult)mrdr.CustomCmd(filter, CustomCmdType.IMPINJ_M4_Qt, para);
                    if (qtret.MemType == IMPINJ_M4_QtPara.IMPINJ_Qt_Mem_Type.IMPINJ_Qt_Mem_Private)
                        this.rbimpinjqtmemprivate.Checked = true;
                    else
                        this.rbimpinjqtmempublic.Checked = true;
                    if (qtret.RangeType == IMPINJ_M4_QtPara.IMPINJ_Qt_Range_Type.IMPINJ_Qt_Range_FarField)
                        this.rbimpinjqtfarfield.Checked = true;
                    else
                        this.rbimpinjqtnearfiled.Checked = true;

                }
                else
                {
                    if ((!rbimpinjqtnearfiled.Checked) && (!rbimpinjqtfarfield.Checked))
                    {
                        MessageBox.Show("请选择识别距离");
                        return;
                    }
                    if ((!rbimpinjqtmemprivate.Checked) && (!rbimpinjqtmempublic.Checked))
                    {
                        MessageBox.Show("请选择内存视图");
                        return;
                    }
                    if ((!rbimpinjqtperm.Checked) && (!rbimpinjqttemp.Checked))
                    {
                        MessageBox.Show("请选择状态类型");
                        return;
                    }

                    IMPINJ_M4_QtPara.IMPINJ_Qt_Range_Type trange = IMPINJ_M4_QtPara.IMPINJ_Qt_Range_Type.IMPINJ_Qt_Range_Invalid;
                    IMPINJ_M4_QtPara.IMPINJ_Qt_Persist_Type tpersist = IMPINJ_M4_QtPara.IMPINJ_Qt_Persist_Type.IMPINJ_Qt_Persist_Invalid;
                    IMPINJ_M4_QtPara.IMPINJ_Qt_Mem_Type tmem = IMPINJ_M4_QtPara.IMPINJ_Qt_Mem_Type.IMPINJ_Qt_Mem_Invalid;

                    if (rbimpinjqtnearfiled.Checked)
                        trange = IMPINJ_M4_QtPara.IMPINJ_Qt_Range_Type.IMPINJ_Qt_Range_NearField;
                    else
                        trange = IMPINJ_M4_QtPara.IMPINJ_Qt_Range_Type.IMPINJ_Qt_Range_FarField;
                    if (rbimpinjqtmemprivate.Checked)
                        tmem = IMPINJ_M4_QtPara.IMPINJ_Qt_Mem_Type.IMPINJ_Qt_Mem_Private;
                    else
                        tmem = IMPINJ_M4_QtPara.IMPINJ_Qt_Mem_Type.IMPINJ_Qt_Mem_Public;
                    if (rbimpinjqtperm.Checked)
                        tpersist = IMPINJ_M4_QtPara.IMPINJ_Qt_Persist_Type.IMPINJ_Qt_Persist_Perm;
                    else
                        tpersist = IMPINJ_M4_QtPara.IMPINJ_Qt_Persist_Type.IMPINJ_Qt_Persist_Temp;

                    IMPINJ_M4_QtPara para = new IMPINJ_M4_QtPara(pwd, tpersist, trange, tmem);
                    mrdr.CustomCmd(filter, CustomCmdType.IMPINJ_M4_Qt, para);
                }
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

        //private void btnbpll_Click(object sender, EventArgs e)
        //{
        //    int ret;
        //    ret = IsAntSet();
        //    if (ret == -1)
        //    {
        //        MessageBox.Show("请选择操作天线");
        //        return;
        //    }
        //    else if (ret == 1)
        //    {
        //        DialogResult stat = DialogResult.OK;
        //        stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
        //                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
        //                        MessageBoxDefaultButton.Button2);
        //        if (stat != DialogResult.OK)
        //            return;

        //    }

        //    byte[] pwd = null;

        //    ret = Form1.IsValidPasswd(this.tbaccesspasswd.Text.Trim());
        //    {
        //        switch (ret)
        //        {
        //            case -3:
        //                MessageBox.Show("访问密码不能为空");
        //                break;
        //            case -2:
        //            case -4:
        //                MessageBox.Show("访问密码必须是8个16进制数");
        //                break;
        //            case -1:
        //                MessageBox.Show("访问密码只能是16进制数字");
        //                break;

        //        }
        //    }
        //    if (ret != 0)
        //        return;
        //    else
        //    {
        //        pwd = ByteFormat.FromHex(this.tbaccesspasswd.Text.Trim());
        //    }

        //    byte bitmap = 0;
        //    if (cb1.Checked)
        //        bitmap |= 1 << 7;
        //    if (cb2.Checked)
        //        bitmap |= 1 << 6;
        //    if (cb3.Checked)
        //        bitmap |= 1 << 5;
        //    if (cb4.Checked)
        //        bitmap |= 1 << 4;
        //    if (cb5.Checked)
        //        bitmap |= 1 << 3;
        //    if (cb6.Checked)
        //        bitmap |= 1 << 2;
        //    if (cb7.Checked)
        //        bitmap |= 1 << 1;
        //    if (cb8.Checked)
        //        bitmap |= 1 << 0;

        //    try
        //    {
        //        ushort bitmask = (ushort)(bitmap << 8);

        //        ALIEN_Higgs3_BlockPermaLockPara para = new ALIEN_Higgs3_BlockPermaLockPara(bitmask, pwd, 
        //            ALIEN_Higgs3_BlockPermaLockPara.Cmd_Type.BPL_Lock);
        //        mrdr.CustomCmd(CustomCmdType.ALIEN_Higgs3_BlockPermaLock, para);
        //    }
        //    catch (OpFaidedException notagexp)
        //    {
        //        if (notagexp.ErrCode == 0x400)
        //            MessageBox.Show("没法发现标签");
        //        else
        //            MessageBox.Show("操作失败:" + notagexp.ToString());

        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("BlockReadLock失败" + ex.ToString());
        //        return;
        //    }

        //}

        //private void btnbplr_Click(object sender, EventArgs e)
        //{
        //    int ret;
        //    ret = IsAntSet();
        //    if (ret == -1)
        //    {
        //        MessageBox.Show("请选择操作天线");
        //        return;
        //    }
        //    else if (ret == 1)
        //    {
        //        DialogResult stat = DialogResult.OK;
        //        stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
        //                        MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
        //                        MessageBoxDefaultButton.Button2);
        //        if (stat != DialogResult.OK)
        //            return;

        //    }

        //    byte[] pwd = null;

        //    ret = Form1.IsValidPasswd(this.tbaccesspasswd.Text.Trim());
        //    {
        //        switch (ret)
        //        {
        //            case -3:
        //                MessageBox.Show("访问密码不能为空");
        //                break;
        //            case -2:
        //            case -4:
        //                MessageBox.Show("访问密码必须是8个16进制数");
        //                break;
        //            case -1:
        //                MessageBox.Show("访问密码只能是16进制数字");
        //                break;

        //        }
        //    }
        //    if (ret != 0)
        //        return;
        //    else
        //    {
        //        pwd = ByteFormat.FromHex(this.tbaccesspasswd.Text.Trim());
        //    }

        //    try
        //    {

        //        ALIEN_Higgs3_BlockPermaLockPara para = new ALIEN_Higgs3_BlockPermaLockPara((ushort)0, pwd,
        //            ALIEN_Higgs3_BlockPermaLockPara.Cmd_Type.BPL_Read);
        //        ALIEN_Higgs3_BlockPermaLockResult bplrret = (ALIEN_Higgs3_BlockPermaLockResult)mrdr.CustomCmd(CustomCmdType.ALIEN_Higgs3_BlockPermaLock, para);

        //        if (((bplrret.BlkBitMap >> 15) & 0x1) == 1)
        //            cb1.Checked = true;
        //        if (((bplrret.BlkBitMap >> 14) & 0x1) == 1)
        //            cb2.Checked = true;
        //        if (((bplrret.BlkBitMap >> 13) & 0x1) == 1)
        //            cb3.Checked = true;
        //        if (((bplrret.BlkBitMap >> 12) & 0x1) == 1)
        //            cb4.Checked = true;
        //        if (((bplrret.BlkBitMap >> 11) & 0x1) == 1)
        //            cb5.Checked = true;
        //        if (((bplrret.BlkBitMap >> 10) & 0x1) == 1)
        //            cb6.Checked = true;
        //        if (((bplrret.BlkBitMap >> 9) & 0x1) == 1)
        //            cb7.Checked = true;
        //        if (((bplrret.BlkBitMap >> 8) & 0x1) == 1)
        //            cb8.Checked = true;


        //    }
        //    catch (OpFaidedException notagexp)
        //    {
        //        if (notagexp.ErrCode == 0x400)
        //            MessageBox.Show("没法发现标签");
        //        else
        //            MessageBox.Show("操作失败:" + notagexp.ToString());

        //        return;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("BlockReadLock失败" + ex.ToString());
        //        return;
        //    }

        //}

    }
}