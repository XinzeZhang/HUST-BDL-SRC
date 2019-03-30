using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;
using ModuleTech.Gen2;
using ModuleLibrary;
using System.Diagnostics;
using System.Threading;

namespace ModuleReaderManager
{
    public partial class gen2opForm : Form
    {
        public gen2opForm(Reader rdr, ReaderParams param)
        {
            InitializeComponent();
            mordr = rdr;
            rparam = param;
        }
        ReaderParams rparam = null;
        Reader mordr = null;

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

        private bool IsValidAddr(string addr, int bank, int rorw)
        {
            if (addr == "")
                return false;

            int addr_;
            try
            {
                addr_ = int.Parse(addr);
            }
            catch (Exception exxx)
            {
                return false;
            }

            switch (bank)
            {
                case 0:
                    {
                        if (addr_ >= 0 && addr_ <= 3)
                            return true;
                        break;
                    }
                case 1:
                    {
                        if (rorw == 2)
                        {
                            if (addr_ >= 2 && addr_ <= 32)
                                return true;
                        }
                        else if (rorw == 1)
                        {
                            if (addr_ >= 0 && addr_ <= 32)
                                return true;
                        }
                        break;
                    }
                case 2:
                    {
                        if (addr_ >= 0)
                            return true;
                        break;
                    }
                case 3:
                    {
                        if (addr_ >= 0 && addr_ <= 8000)
                            return true;
                        break;
                    }

            }
            
            return false;
        }

        private bool IsValidCnt(string cnt, int bank, string addr)
        {
            if (cnt == "")
                return false;

            int addr_;
            int cnt_;
            try
            {
                addr_ = int.Parse(addr);
                cnt_ = int.Parse(cnt);
            }
            catch (Exception exx)
            {
                return false;
            }

            int sum = addr_ + cnt_;
            switch (bank)
            {
                case 0:
                    {
                        if (sum <= 4)
                            return true;
                        break;
                    }
                case 1:
                    {
                        if (sum <= 64)
                            return true;
                        break;
                    }
                case 2:
                    {
                        if (sum <= 16)
                            return true;
                        break;
                    }
                case 3:
                    {
                        if (sum <= 8000)
                            return true;
                        break;
                    }
            }
            return false;
        }


        private int IsAntSet()
        {
            int ret = -1;
            if (rparam.readertype != ReaderType.MT_A7_16ANTS)
            {
                for (int i = 1; i <= allants.Count; ++i)
                {
                    if (allants[i].Checked)
                    {
                        mordr.ParamSet("TagopAntenna", i);
                        //if (isSetRp)
                        //{
                        //    mordr.ParamSet("ReadPlan", new SimpleReadPlan(new int[] { i }));
                        //}
                        if (allants[i].ForeColor == Color.Red)
                            ret = 1;
                        else
                            ret = 0;
                    }
                }
            }
            else
            {
                if (this.tb16antssel.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入操作天线编号1-16");
                    return -1;
                }
                
                int ant = int.Parse(this.tb16antssel.Text.Trim());
                if (ant < 1 || ant > 16)
                {
                    MessageBox.Show("请输入操作天线编号1-16");
                    return -1;
                }

                bool isdet = false;
                for (int i = 0; i < rparam.SixteenDevConAnts.Length; ++i)
                {
                    if (ant == rparam.SixteenDevConAnts[i])
                    {
                        isdet = true;
                        break;
                    }
                }
                if (isdet)
                    ret = 0;
                else
                    ret = 1;
                mordr.ParamSet("TagopAntenna", ant);
            }

            return ret;
        }

        private void btnread_Click(object sender, EventArgs e)
        {
            int ret;
            Gen2TagFilter filter = null;

            if (this.cbbopbank.SelectedIndex == -1)
            {
                MessageBox.Show("请选择bank");
                return;
            }

            if (!IsValidAddr(this.tbstartaddr.Text.Trim(), this.cbbopbank.SelectedIndex, 1))
            {
                MessageBox.Show("所设置的起始地址超过了对应bank的范围");
                return;
            }

            if (!IsValidCnt(this.tbblocks.Text.Trim(), this.cbbopbank.SelectedIndex, this.tbstartaddr.Text))
            {
                MessageBox.Show("所设置的块数超过了对应的bank和起始地址的范围");
                return;
            }

            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            ushort[] readdata = null;

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

            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }
     

            try
            {
                int st = Environment.TickCount;
                readdata = mordr.ReadTagMemWords(filter,
                    (MemBank)this.cbbopbank.SelectedIndex, int.Parse(this.tbstartaddr.Text.Trim())
                    , int.Parse(this.tbblocks.Text.Trim()));
                Debug.WriteLine("read dur :" + (Environment.TickCount - st).ToString());
                if (rparam.setGPO1)
                {
                    mordr.GPOSet(1, true);
                    System.Threading.Thread.Sleep(20);
                    mordr.GPOSet(1, false);
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

            string readdatastr = "";
            for (int i = 0; i < readdata.Length; ++i)
                readdatastr += readdata[i].ToString("X4");

            this.rtbdata.Text = readdatastr;
        }

        private void btnwrite_Click(object sender, EventArgs e)
        {
            int ret;
            Gen2TagFilter filter = null;

            if (this.cbbopbank.SelectedIndex == -1)
            {
                MessageBox.Show("请选择bank");
                return;
            }
            if (!IsValidAddr(this.tbstartaddr.Text.Trim(), this.cbbopbank.SelectedIndex, 2))
            {
                MessageBox.Show("所设置的起始地址超过了对应bank的范围");
                return;
            }

            if (IsValidHexstr(this.rtbdata.Text.Trim(), 16384) != 0)
            {
                MessageBox.Show("将要写入的数据是16进制的字符,且长度为4字符的整数倍");
                return;
            }

            int cnt = this.rtbdata.Text.Length / 4;

            if (!IsValidCnt(cnt.ToString(), this.cbbopbank.SelectedIndex, this.tbstartaddr.Text.Trim()))
            {
                MessageBox.Show("将要写入的数据长度大于写入区域的容量");
                return;
            }



            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

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

            ushort[] writedata = new ushort[this.rtbdata.Text.Trim().Length / 4];

            for (int a = 0; a < writedata.Length; ++a)
                writedata[a] = ushort.Parse(this.rtbdata.Text.Trim().Substring(a * 4, 4), System.Globalization.NumberStyles.AllowHexSpecifier);



            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }
            int dur = 0;
            try
            {                
                int st = System.Environment.TickCount;
                mordr.WriteTagMemWords(filter,
                    (MemBank)this.cbbopbank.SelectedIndex, int.Parse(this.tbstartaddr.Text.Trim())
                    , writedata);
                dur = System.Environment.TickCount - st;                
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

            this.rtbdata.Text = "写成功,耗时：" + dur.ToString();
            if (rparam.setGPO1)
            {
                mordr.GPOSet(1, true);
                System.Threading.Thread.Sleep(20);
                mordr.GPOSet(1, false);
            }

        }

        private void btnlock_Click(object sender, EventArgs e)
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

            if (this.cbblocktype.SelectedIndex == -1)
            {
                MessageBox.Show("请选择锁定类型");
                return;
            }
            if (this.cbblockunit.SelectedIndex == -1)
            {
                MessageBox.Show("请选择锁定区域");
                return;
            }

            
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
                uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                mordr.ParamSet("AccessPassword", passwd);
            }

            Gen2TagFilter filter = null;

            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }
            Gen2LockAct[] act = new Gen2LockAct[1];
  //          Gen2LockAct[] act2 = new Gen2LockAct[2];
            switch (this.cbblockunit.SelectedIndex)
            {
                case 0:
                   {
                       if (this.cbblocktype.SelectedIndex == 0)
                           act[0] = Gen2LockAct.ACCESS_UNLOCK;
                       else if (this.cbblocktype.SelectedIndex == 1)
                           act[0] = Gen2LockAct.ACCESS_LOCK;
                       else if (this.cbblocktype.SelectedIndex == 2)
                           act[0] = Gen2LockAct.ACCESS_PERMALOCK;
                       break;
                   }
                case 1:
                   {
                       if (this.cbblocktype.SelectedIndex == 0)
                           act[0] = Gen2LockAct.KILL_UNLOCK;
                       else if (this.cbblocktype.SelectedIndex == 1)
                           act[0] = Gen2LockAct.KILL_LOCK;
                       else if (this.cbblocktype.SelectedIndex == 2)
                           act[0] = Gen2LockAct.KILL_PERMALOCK;
                       break;
                   }
                case 2:
                   {
                       if (this.cbblocktype.SelectedIndex == 0)
                           act[0] = Gen2LockAct.EPC_UNLOCK;
                       else if (this.cbblocktype.SelectedIndex == 1)
                           act[0] = Gen2LockAct.EPC_LOCK;
                       else if (this.cbblocktype.SelectedIndex == 2)
                           act[0] = Gen2LockAct.EPC_PERMALOCK;
                       break;
                   }
                case 3:
                   {
                       if (this.cbblocktype.SelectedIndex == 0)
                           act[0] = Gen2LockAct.TID_UNLOCK;
                       else if (this.cbblocktype.SelectedIndex == 1)
                           act[0] = Gen2LockAct.TID_LOCK;
                       else if (this.cbblocktype.SelectedIndex == 2)
                           act[0] = Gen2LockAct.TID_PERMALOCK;
                       break;
                   }
                case 4:
                   {
                       if (this.cbblocktype.SelectedIndex == 0)
                           act[0] = Gen2LockAct.USER_UNLOCK;
                       else if (this.cbblocktype.SelectedIndex == 1)
                           act[0] = Gen2LockAct.USER_LOCK;
                       else if (this.cbblocktype.SelectedIndex == 2)
                           act[0] = Gen2LockAct.USER_PERMALOCK;
                       break;
                   }
            }

            try
            {
                mordr.LockTag(filter, new Gen2LockAction(act));
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

            this.rtbdata.Text = "锁定成功";
            if (rparam.setGPO1)
            {
                mordr.GPOSet(1, true);
                System.Threading.Thread.Sleep(20);
                mordr.GPOSet(1, false);
            }
        }

        private void btnkill_Click(object sender, EventArgs e)
        {
            int ret;
            uint killpasswd;
            ret = Form1.IsValidPasswd(this.tbkillpasswd.Text.Trim());
            {
                switch (ret)
                {
                    case -3:
                        MessageBox.Show("销毁密码不能为空");
                        break;
                    case -2:
                    case -4:
                        MessageBox.Show("销毁密码必须是8个16进制数");
                        break;
                    case -1:
                        MessageBox.Show("销毁数据只能是16进制数字");
                        break;

                }
            }
            if (ret != 0)
                return;
            else
                killpasswd = uint.Parse(this.tbkillpasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);


            Gen2TagFilter filter = null;

            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }
            try
            {
                mordr.KillTag(filter, killpasswd);
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
            }

            this.rtbdata.Text = "销毁成功";
            if (rparam.setGPO1)
            {
                mordr.GPOSet(1, true);
                System.Threading.Thread.Sleep(20);
                mordr.GPOSet(1, false);
            }
        }

        Dictionary<int, RadioButton> allants = new Dictionary<int, RadioButton>();

        private void opForm_Load(object sender, EventArgs e)
        {
            allants.Add(1, rbant1);
            allants.Add(2, rbant2);
            allants.Add(3, rbant3);
            allants.Add(4, rbant4);

            for (int i = 1; i <= allants.Count; ++i)
                allants[i].Enabled = false;

            if (rparam.readertype != ReaderType.MT_A7_16ANTS)
            {
                for (int j = 0; j < rparam.AntsState.Count; ++j)
                {
                    allants[rparam.AntsState[j].antid].Enabled = true;
                    if (rparam.AntsState[j].isConn)
                        allants[rparam.AntsState[j].antid].ForeColor = Color.Green;
                    else
                        allants[rparam.AntsState[j].antid].ForeColor = Color.Red;
                }

                if (rparam.readertype == ReaderType.PR_ONEANT)
                {
                    this.tbfilteraddr.Enabled = false;
                    this.tbfldata.Enabled = false;
                    this.cbbfilterbank.Enabled = false;
                    this.cbbfilterrule.Enabled = false;
                    this.cbisfilter.Enabled = false;
                }
                this.tb16antssel.Enabled = false;
            }
   
            mordr.ParamSet("TagopProtocol", TagProtocol.GEN2);

            this.btnConwirte.Enabled = true;
            this.btnconread.Enabled = true;
            this.btnstop.Enabled = false;
            this.btnstopread.Enabled = false;
        }

        private void btnWriteEpc_Click(object sender, EventArgs e)
        {
            Gen2TagFilter filter = null;
            int ret = 0;
            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }
            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            if (IsValidHexstr(this.rtbdata.Text.Trim(), 600) != 0)
            {
                MessageBox.Show("将要写入的数据是16进制的字符,且长度为4字符的整数倍");
                return;
            }

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

            try
            {
                mordr.WriteTag(filter, new TagData(this.rtbdata.Text.Trim()));
                this.rtbdata.Text = "写EPC成功";
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败：" + ex.ToString());
                return;
            }
        }

        delegate void UpdateConWriteHandler(int cnt, int dur, int ant, bool isstop);
        delegate void UpdateConReadHandler(int cnt, int dur, int ant, ushort[] data);
        delegate void UpdateConNonOpHandler(int cnt, int dur, bool isstop);

        void updateconwirte(int cnt, int dur, int ant, bool isstop)
        {
            this.rtbdata.Text = "天线"+ant.ToString()+"写成功:" + cnt.ToString() + "--" + dur.ToString();
            if (isstop)
                this.btnstop_Click(null, null);
        }
        void updateconwirte_wfailed(int cnt, int dur, bool isstop)
        {
            this.rtbdata.Text = "写失败";
            if (isstop)
                this.btnstop_Click(null, null);
        }
        void updateconread_rfailed(int cnt, int dur, bool isstop)
        {
            this.rtbdata.Text = "读失败";
            if (isstop)
                this.btnstop_Click(null, null);
        }

        void updatecon_notag(int cnt, int dur, bool isstop)
        {
            this.rtbdata.Text = "没有标签";
            if (isstop)
                this.btnstop_Click(null, null);
        }

        void updateconread(int cnt, int dur, int ant, ushort[] data)
        {
            this.rtbdata.Text = "天线"+ant.ToString()+"读成功：" + cnt.ToString() + "--" + dur.ToString() + "\n";
            string readdatastr = "";
            for (int i = 0; i < data.Length; ++i)
                readdatastr += data[i].ToString("X4");
            this.rtbdata.Text += readdatastr;
        }

        bool isrun = false;
        ushort[] writedata__ = null;
        Thread conwriteth = null;
        Thread conreadth = null;

        MemBank membank__ = MemBank.USER;
        int startconaddr = 0;
        int conreadwordcnt = 0;
        int writecnt = 0;
        int totwtime = 0;

        private void ConReadFunc()
        {
            int st = 0;
            int dur = 0;
            writecnt = 0;
            totwtime = 0;
            ushort[] rdata = null;
            while (isconreadrun)
            {
                try
                {
                    TagReadData[] tags = mordr.Read(40);
                    if (tags.Length > 0)
                    {
                        Gen2TagFilter filter = new Gen2TagFilter(tags[0].EPC.Length * 8, tags[0].EPC, MemBank.EPC, 32, false);
                        st = System.Environment.TickCount;
                        mordr.ParamSet("TagopAntenna", tags[0].Antenna);
                        rdata = mordr.ReadTagMemWords(filter, membank__, startconaddr, conreadwordcnt);
                        dur = System.Environment.TickCount - st;
                        totwtime += dur;
                        writecnt++;
                        this.BeginInvoke(new UpdateConReadHandler(updateconread), writecnt, dur, tags[0].Antenna, rdata);
                    }
                    else
                    {
                        this.BeginInvoke(new UpdateConNonOpHandler(updatecon_notag), 0, 0, false);
                    }

                }
                catch (Exception ex)
                {
                    this.BeginInvoke(new UpdateConNonOpHandler(updateconread_rfailed), 0, 0, false);
                }
            }

        }

        private void ConWriteFunc()
        {
            int st = 0;
            int dur = 0;
            writecnt = 0;
            totwtime = 0;

            while (isrun)
            {
                try
                {   
                    TagReadData[] tags = mordr.Read(40);
                    
                    if (tags.Length > 0)
                    {
                        Gen2TagFilter filter = new Gen2TagFilter(tags[0].EPC.Length * 8, tags[0].EPC, MemBank.EPC, 32, false);
                        st = System.Environment.TickCount;
                        mordr.ParamSet("TagopAntenna", tags[0].Antenna);
                        mordr.WriteTagMemWords(filter,
                            membank__, startconaddr, writedata__);
                        dur = System.Environment.TickCount - st;
                        totwtime += dur;
                        writecnt++;
                        this.BeginInvoke(new UpdateConWriteHandler(updateconwirte), writecnt, dur, tags[0].Antenna, isrepeatwrite);
                        if (isrepeatwrite)
                            return;
                    }
                    else
                    {
                        this.BeginInvoke(new UpdateConNonOpHandler(updatecon_notag), 0, 0, false);
                    }
                }
                catch (Exception ex)
                {
                    this.BeginInvoke(new UpdateConNonOpHandler(updateconwirte_wfailed), 0, 0, false);
                }
            }
        }

        bool isrepeatwrite = false;

        private void btnConwirte_Click(object sender, EventArgs e)
        {
            int ret;

            if (this.cbbopbank.SelectedIndex == -1)
            {
                MessageBox.Show("请选择bank");
                return;
            }
            if (!IsValidAddr(this.tbstartaddr.Text.Trim(), this.cbbopbank.SelectedIndex, 2))
            {
                MessageBox.Show("所设置的起始地址超过了对应bank的范围");
                return;
            }

            try
            {
                startconaddr = int.Parse(this.tbstartaddr.Text.Trim());
            }
            catch
            {
                MessageBox.Show("请输入正确的起始地址");
                return;
            }

            if (IsValidHexstr(this.rtbdata.Text.Trim(), 600) != 0)
            {
                MessageBox.Show("将要写入的数据是16进制的字符,且长度为4字符的整数倍");
                return;
            }

            int cnt = this.rtbdata.Text.Length / 4;

            if (!IsValidCnt(cnt.ToString(), this.cbbopbank.SelectedIndex, this.tbstartaddr.Text.Trim()))
            {
                MessageBox.Show("将要写入的数据长度大于写入区域的容量");
                return;
            }

            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            if (!this.cbisconnants.Checked)
            {
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
            }
            //设置rp
            {
                if (rparam.readertype != ReaderType.MT_A7_16ANTS)
                {
                    if (this.cbisconnants.Checked)
                    {
                        int[] connants = (int[])mordr.ParamGet("ConnectedAntennas");
                        if (connants.Length == 0)
                        {
                            MessageBox.Show("没检测到有天线连接");
                            return;
                        }
                        mordr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, connants));
                    }
                    else
                    {
                        for (int i = 1; i <= allants.Count; ++i)
                        {
                            if (allants[i].Checked)
                                mordr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, new int[] { i }));
                        }
                    }
                }
                else
                {
                    int ant = int.Parse(this.tb16antssel.Text.Trim());
                    mordr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, new int[] { ant }));
                }
            }

            writedata__ = new ushort[this.rtbdata.Text.Trim().Length / 4];
            for (int a = 0; a < writedata__.Length; ++a)
                writedata__[a] = ushort.Parse(this.rtbdata.Text.Trim().Substring(a * 4, 4), 
                    System.Globalization.NumberStyles.AllowHexSpecifier);
            membank__ = (MemBank)this.cbbopbank.SelectedIndex;
            isrun = true;
            mordr.ParamSet("OpTimeout", (ushort)500);
            this.btnConwirte.Enabled = false;
            this.btnconread.Enabled = false;
            this.btnstop.Enabled = true;
            this.btnstopread.Enabled = false;
            this.labavewtime.Text = "0";
            this.labwritecnt.Text = "0";
            if (this.cbisrepeatwrite.Checked)
                isrepeatwrite = true;
            else
                isrepeatwrite = false;
            conwriteth = new Thread(ConWriteFunc);
            conwriteth.Start();
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            isrun = false;
            conwriteth.Join();
            if (writecnt != 0)
                this.labavewtime.Text = ((int)(totwtime / writecnt)).ToString();
            this.labwritecnt.Text = writecnt.ToString();
            this.btnConwirte.Enabled = true;
            this.btnconread.Enabled = true;
            this.btnstop.Enabled = false;
            this.btnstopread.Enabled = false;
        }

        bool isconreadrun = false;
        private void btnconread_Click(object sender, EventArgs e)
        {
            int ret;

            if (this.cbbopbank.SelectedIndex == -1)
            {
                MessageBox.Show("请选择bank");
                return;
            }
            if (!IsValidAddr(this.tbstartaddr.Text.Trim(), this.cbbopbank.SelectedIndex, 2))
            {
                MessageBox.Show("所设置的起始地址超过了对应bank的范围");
                return;
            }

            try
            {
                startconaddr = int.Parse(this.tbstartaddr.Text.Trim());
            }
            catch
            {
                MessageBox.Show("请输入正确的起始地址");
                return;
            }
            try
            {
                conreadwordcnt = int.Parse(this.tbblocks.Text.Trim());
            }
            catch
            {
                MessageBox.Show("输入正确块数");
                return;
            }

            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            if (!this.cbisconnants.Checked)
            {
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
            }
 
            //设置rp
            {
                if (rparam.readertype != ReaderType.MT_A7_16ANTS)
                {
                    if (this.cbisconnants.Checked)
                    {
                        int[] connants = (int[])mordr.ParamGet("ConnectedAntennas");
                        if (connants.Length == 0)
                        {
                            MessageBox.Show("没检测到有天线连接");
                            return;
                        }
                        mordr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, connants));
                    }
                    else
                    {
                        for (int i = 1; i <= allants.Count; ++i)
                        {
                            if (allants[i].Checked)
                                mordr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, new int[] { i }));
                        }
                    }
                }
                else
                {
                    int ant = int.Parse(this.tb16antssel.Text.Trim());
                    mordr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, new int[] { ant }));
                }
            }

            membank__ = (MemBank)this.cbbopbank.SelectedIndex;
            isconreadrun = true;
            mordr.ParamSet("OpTimeout", (ushort)200);
            this.btnConwirte.Enabled = false;
            this.btnconread.Enabled = false;
            this.btnstop.Enabled = false;
            this.btnstopread.Enabled = true;
            this.labavewtime.Text = "0";
            this.labwritecnt.Text = "0";
            conreadth = new Thread(ConReadFunc);
            conreadth.Start();
        }

        private void btnstopread_Click(object sender, EventArgs e)
        {
            isconreadrun = false;
            conreadth.Join();
            if (writecnt != 0)
                this.labavewtime.Text = (((int)(totwtime / writecnt)) - 4).ToString();
            this.labwritecnt.Text = writecnt.ToString();
            this.btnConwirte.Enabled = true;
            this.btnconread.Enabled = true;
            this.btnstop.Enabled = false;
            this.btnstopread.Enabled = false;
        }

        private void btnpermblklock_Click(object sender, EventArgs e)
        {
            int ret;
            Gen2TagFilter filter = null;

            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            ushort[] readdata = null;

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

            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }

            int blkstart = 0;
            if (this.tbblkstart.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入起始块");
                return;
            }
            else
                blkstart = int.Parse(this.tbblkstart.Text.Trim());

            int blkrange = 0;
            if (this.tbblkrange.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入起范围");
                return;
            }
            else
                blkrange = int.Parse(this.tbblkrange.Text.Trim());

            byte[] mask = new byte[2];
            mask[0] = 0;
            mask[1] = 0;
            if (this.cb1.Checked)
            {
                mask[0] |= 0x01 << 7; 
            }
            if (this.cb2.Checked)
            {
                mask[0] |= 0x01 << 6;
            }
            if (this.cb3.Checked)
            {
                mask[0] |= 0x01 << 5;
            }
            if (this.cb4.Checked)
            {
                mask[0] |= 0x01 << 4;
            }
            if (this.cb5.Checked)
            {
                mask[0] |= 0x01 << 3;
            }
            if (this.cb6.Checked)
            {
                mask[0] |= 0x01 << 2;
            }
            if (this.cb7.Checked)
            {
                mask[0] |= 0x01 << 1;
            }
            if (this.cb8.Checked)
            {
                mask[0] |= 0x01 << 0;
            }
            try
            {
                mordr.Gen2OpBlockPermaLock(filter, blkstart, blkrange, mask);
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

        private void btnpermblkget_Click(object sender, EventArgs e)
        {
            int ret;
            Gen2TagFilter filter = null;

            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            ushort[] readdata = null;

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

            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }

            int blkstart = 0;
            if (this.tbblkstart.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入起始块");
                return;
            }
            else
                blkstart = int.Parse(this.tbblkstart.Text.Trim());

            int blkrange = 0;
            if (this.tbblkrange.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入起范围");
                return;
            }
            else
                blkrange = int.Parse(this.tbblkrange.Text.Trim());

            try
            {
                byte[] mask = mordr.Gen2OpBlockPermaLock(filter, blkstart, blkrange, null);
                if (((mask[0] >> 7) & 0x1) == 1)
                    this.cb1.Checked = true;
                else
                    this.cb1.Checked = false;

                if (((mask[0] >> 6) & 0x1) == 1)
                    this.cb2.Checked = true;
                else
                    this.cb2.Checked = false;

                if (((mask[0] >> 5) & 0x1) == 1)
                    this.cb3.Checked = true;
                else
                    this.cb3.Checked = false;

                if (((mask[0] >> 4) & 0x1) == 1)
                    this.cb4.Checked = true;
                else
                    this.cb4.Checked = false;

                if (((mask[0] >> 3) & 0x1) == 1)
                    this.cb5.Checked = true;
                else
                    this.cb5.Checked = false;

                if (((mask[0] >> 2) & 0x1) == 1)
                    this.cb6.Checked = true;
                else
                    this.cb6.Checked = false;

                if (((mask[0] >> 1) & 0x1) == 1)
                    this.cb7.Checked = true;
                else
                    this.cb7.Checked = false;

                if (((mask[0] >> 0) & 0x1) == 1)
                    this.cb8.Checked = true;
                else
                    this.cb8.Checked = false;
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

        private string getIcString(string tid)
        {
            if (tid == null)
                return "";
            string ret = null;
            string manufactureid = tid.Substring(2, 3);
            switch (manufactureid)
            {
                case "006":
                    ret = "NXP";
                    break;
                case "003":
                    {
                        ret = "ALIEN";
                        string icmodle = tid.Substring(5, 3);
                        if (icmodle == "412")
                            ret += "-Higgs 3";
                        else if (icmodle == "411")
                            ret += "-Higgs 2";
                        break;
                    }
                case "001":
                    ret = "IMPINJ";
                    break;
                case "801":
                    {
                        ret = "IMPINJ";
                        string icmodle = tid.Substring(5, 3);
                        if (icmodle == "105")
                            ret += "-Monza 4";
                        else if (icmodle == "130")
                            ret += "-Monza 5";
                        break;
                    }                 
                case "00F":
                    ret = "坤锐";
                    break;
                default:
                    ret = "";
                    break;
            }
            return ret;
        }

        private void btnIcIdentify_Click(object sender, EventArgs e)
        {
            int ret;
            Gen2TagFilter filter = null;

            if (this.cbisaccesspasswd.Checked)
            {
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
                    uint passwd = uint.Parse(this.tbaccesspasswd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                    mordr.ParamSet("AccessPassword", passwd);
                }
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            ushort[] readdata = null;

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

            if (this.cbisfilter.Checked)
            {

                ret = Form1.IsValidBinaryStr(this.tbfldata.Text.Trim());
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
                if (this.cbbfilterbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }

                if (this.cbbfilterrule.SelectedIndex == -1)
                {
                    MessageBox.Show("请输入过滤规则");
                    return;
                }

                int bitaddr = 0;
                if (this.tbfilteraddr.Text.Trim() == "")
                {
                    MessageBox.Show("请输入过滤bank的起始地址,以字为最小单位");
                    return;
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
                        return;
                    }
                    if (bitaddr < 0)
                    {
                        MessageBox.Show("地址必须大于零");
                        return;
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

                filter = new Gen2TagFilter(this.tbfldata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
            }


            try
            {
                readdata = mordr.ReadTagMemWords(filter, MemBank.TID, 0, 2);

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

            string tid = "";
            for (int i = 0; i < readdata.Length; ++i)
                tid += readdata[i].ToString("X4");

            string ic = getIcString(tid);
            this.rtbdata.Text = "TID (前32bit):" + tid + "\n" + "IC型号:" + ic;

        }


    }
}