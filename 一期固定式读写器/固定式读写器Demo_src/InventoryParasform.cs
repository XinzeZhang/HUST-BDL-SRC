using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech.Gen2;
using ModuleTech;

namespace ModuleReaderManager
{
    public partial class InventoryParasform : Form
    {
        Form1 frm1 = null;
        public InventoryParasform(Form1 frm)
        {
            frm1 = frm;
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            int rdur = 0;
            int sdur = 0;

            //added on 3-26
            if (this.cbisidtant.Checked)
            {
                if ((!this.rbidtantbydur.Checked) && (!this.rbidtantafter.Checked))
                {
                    MessageBox.Show("请选择判决模式");
                    return;
                }
                if (this.rbidtantbydur.Checked)
                {
                    if (this.tbidtdur.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入判决时间");
                        return;
                    }
                    try
                    {
                        int pjsj = int.Parse(this.tbidtdur.Text.Trim());
                        if (pjsj <= 0)
                        {
                            MessageBox.Show("判决时间必须大于0");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("判决时间必须大于0");
                        return;
                    }
                }
                if (this.rbidtantafter.Checked)
                {
                    if (this.tbafttime.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入等待时间");
                        return;
                    }
                    try
                    {
                        int ddsj = int.Parse(this.tbafttime.Text.Trim());
                        if (ddsj <= 0)
                        {
                            MessageBox.Show("等待时间必须大于0");
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("等待时间必须大于0");
                        return;
                    }
                }

            }

            frm1.rParms.isUniByEmd = this.cbisunibynullemd.Checked;
            frm1.rParms.isChangeColor = this.cbischgcolor.Checked;
            frm1.rParms.isUniByAnt = this.cbisunibyant.Checked;
            try
            {
                rdur = int.Parse(this.tbreaddur.Text.Trim());
                sdur = int.Parse(this.tbsleepdur.Text.Trim());

            }
            catch (Exception ex)
            {
                MessageBox.Show("读时长和读间隔只能输入整数，单位为毫秒");
                return;
            }

            frm1.rParms.readdur = rdur;
            frm1.rParms.sleepdur = sdur;
            frm1.rParms.isRevertAnts = this.cbisrevertants.Checked;
            if (this.cbisOneReadOneTime.Checked)
                frm1.rParms.isOneReadOneTime = true;
            else
                frm1.rParms.isOneReadOneTime = false;

            if (this.cbisReadFixCount.Checked)
            {
                if (this.tbReadCount.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入固定读次数");
                    return;
                }
                try
                {
                    frm1.rParms.FixReadCount = int.Parse(this.tbReadCount.Text.Trim());
                }
                catch (System.Exception ere)
                {
                    MessageBox.Show(ere.ToString());
                    return;
                }
                if (frm1.rParms.FixReadCount <= 0)
                {
                    MessageBox.Show("读次数必须大于0");
                    return;
                }
                frm1.rParms.isReadFixCount = true;  
            }
            else
            {
                frm1.rParms.isReadFixCount = false;
                frm1.rParms.FixReadCount = 0;
            }

            if (this.cbisfilter.Checked)
            {

                int ret = Form1.IsValidBinaryStr(this.tbfilterdata.Text.Trim());
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

                byte[] filterbytes = new byte[(this.tbfilterdata.Text.Trim().Length - 1) / 8 + 1];
                for (int c = 0; c < filterbytes.Length; ++c)
                    filterbytes[c] = 0;

                int bitcnt = 0;
                foreach (Char ch in this.tbfilterdata.Text.Trim())
                {
                    if (ch == '1')
                        filterbytes[bitcnt / 8] |= (byte)(0x01 << (7 - bitcnt % 8));
                    bitcnt++;

                }

                Gen2TagFilter filter = new Gen2TagFilter(this.tbfilterdata.Text.Trim().Length, filterbytes,
                    (MemBank)this.cbbfilterbank.SelectedIndex + 1, bitaddr,
                    this.cbbfilterrule.SelectedIndex == 0 ? false : true);
                frm1.modulerdr.ParamSet("Singulation", filter);
            }
            else
                frm1.modulerdr.ParamSet("Singulation", null);

            //|| frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_R902_MT100
            if (!(frm1.rParms.readertype == ReaderType.PR_ONEANT ))
            {

                if (this.cbisaddiondata.Checked)
                {
                    int wordaddr = 0;
                    int ebbytescnt;

                    if (this.tbebstartaddr.Text.Trim() == "")
                    {
                        MessageBox.Show("请输入附加数据bank的起始地址,以字为最小单位");
                        return;
                    }
                    else
                    {
                        try
                        {
                            wordaddr = int.Parse(this.tbebstartaddr.Text.Trim());
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("起始地址请输入数字");
                            return;
                        }
                        if (wordaddr < 0)
                        {
                            MessageBox.Show("地址必须大于零");
                            return;
                        }
                    }

                    if (this.tbebbytescnt.Text.Trim() == "")
                    {
                        MessageBox.Show("请输入附加数据bank的起始地址,以字为最小单位");
                        return;
                    }
                    else
                    {
                        try
                        {
                            ebbytescnt = int.Parse(this.tbebbytescnt.Text.Trim());
                        }
                        catch (Exception exc)
                        {
                            MessageBox.Show("附加数据字节数请输入数字");
                            return;
                        }
                        if (ebbytescnt < 0 || ebbytescnt > 128)
                        {
                            MessageBox.Show("附加数据字节数必须大于零且小于等于128");
                            return;
                        }
                    }


                    if (this.cbbebbank.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择附加数据的bank");
                        return;
                    }

                    EmbededCmdData ecd = new EmbededCmdData((MemBank)this.cbbebbank.SelectedIndex, (UInt32)wordaddr,
                        (byte)ebbytescnt);
                    

                    frm1.modulerdr.ParamSet("EmbededCmdOfInventory", ecd);
                }
                else
                    frm1.modulerdr.ParamSet("EmbededCmdOfInventory", null);


                if (this.cbispwd.Checked)
                {
                    int ret = Form1.IsValidPasswd(this.tbacspwd.Text.Trim());
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
                        uint passwd = uint.Parse(this.tbacspwd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        frm1.modulerdr.ParamSet("AccessPassword", passwd);
                    }
                }
                else
                    frm1.modulerdr.ParamSet("AccessPassword", (uint)0);
                //     if (frm1.modulerdr.HwDetails.board != Reader.MaindBoard_Type.MAINBOARD_ARM9)
                //     {
                if (this.gbpotlweight.Enabled)
                {
                    if (this.tbwtgen2.Text.Trim() == string.Empty || this.tbwt180006b.Text.Trim() == string.Empty ||
                        this.tbwtipx256.Text.Trim() == string.Empty || this.tbwtipx64.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入协议权重值");
                        return;
                    }
                    else
                    {
                        frm1.rParms.weight180006b = int.Parse(this.tbwt180006b.Text.Trim());
                        frm1.rParms.weightgen2 = int.Parse(this.tbwtgen2.Text.Trim());
                        frm1.rParms.weightipx256 = int.Parse(this.tbwtipx256.Text.Trim());
                        frm1.rParms.weightipx64 = int.Parse(this.tbwtipx64.Text.Trim());
                    }
                }

                if (this.gbsecureread.Enabled)
                {

                    if (this.cbissecureread.Checked)
                    {
                        uint srpwd = 0;
                        int srindexsbit = 96;
                        int srindexbitsnum = 8;
                        EmdSecureReadData.ESRD_TagType tagtype = EmdSecureReadData.ESRD_TagType.ESRD_TagType_None;

                        if (this.tbsraddress.Text.Trim() == string.Empty)
                        {
                            MessageBox.Show("请输入安全附加数据起始地址");
                            return;
                        }
                        if (this.tbsrblks.Text.Trim() == string.Empty)
                        {
                            MessageBox.Show("请输入安全附加数据读块数");
                            return;
                        }
                        if (this.cbbsrbank.SelectedIndex == -1)
                        {
                            MessageBox.Show("请选择安全附加数据bank");
                            return;
                        }
                        if (this.cbbsrpwdtype.SelectedIndex == -1)
                        {
                            MessageBox.Show("请选择密码类型");
                            return;
                        }
                        if (this.cbbsrpwdtype.SelectedIndex == 0)
                        {
                            if (this.tbsracspwd.Text.Trim() == string.Empty)
                            {
                                MessageBox.Show("请输入固定的访问密码");
                                return;
                            }
                            srpwd = uint.Parse(this.tbsracspwd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                        }
                        if (this.cbbsrpwdtype.SelectedIndex == 1)
                        {
                            if (this.tbsrindexstartbit.Text.Trim() == string.Empty)
                            {
                                MessageBox.Show("请输入索引起始 bit");
                                return;
                            }
                            srindexsbit = int.Parse(this.tbsrindexstartbit.Text.Trim());

                            if (this.tbindexbitsnum.Text.Trim() == string.Empty)
                            {
                                MessageBox.Show("请输入索引bit数");
                                return;
                            }
                            srindexbitsnum = int.Parse(this.tbindexbitsnum.Text.Trim());
                        }
                        if (this.cbbsrtagtype.SelectedIndex == -1)
                        {
                            MessageBox.Show("请选择标签类型");
                            return;
                        }
                        else if (this.cbbsrtagtype.SelectedIndex == 0)
                            tagtype = EmdSecureReadData.ESRD_TagType.ESRD_Alien_Higgs3;
                        else
                            tagtype = EmdSecureReadData.ESRD_TagType.ESRD_Impinj_M4;
                        EmdSecureReadData esrd = null;
                        if (this.cbbsrpwdtype.SelectedIndex == 0)
                            esrd = new EmdSecureReadData(tagtype,
                                (MemBank)this.cbbsrbank.SelectedIndex, int.Parse(this.tbsraddress.Text.Trim()),
                                int.Parse(this.tbsrblks.Text.Trim()), srpwd);
                        else
                            esrd = new EmdSecureReadData(tagtype,
                                srindexsbit, srindexbitsnum, (MemBank)this.cbbsrbank.SelectedIndex,
                                int.Parse(this.tbsraddress.Text.Trim()), int.Parse(this.tbsrblks.Text.Trim()));

                        frm1.modulerdr.ParamSet("EmdSecureDataOfInventory", esrd);

                    }
                    else
                        frm1.modulerdr.ParamSet("EmdSecureDataOfInventory", null);
                }
                //    }

            }

            //added on 3-26
            try
            {
                if (this.gbidentifyants.Enabled)
                {
                    if (this.cbisidtant.Checked)
                    {
                        frm1.rParms.isIdtAnts = true;
                        if (this.rbidtantbydur.Checked)
                        {
                            frm1.rParms.IdtAntsType = 1;
                            frm1.rParms.DurIdtval = int.Parse(this.tbidtdur.Text.Trim());

                        }
                        else if (this.rbidtantafter.Checked)
                        {
                            frm1.rParms.IdtAntsType = 2;
                            frm1.rParms.AfterIdtWaitval = int.Parse(this.tbafttime.Text.Trim());
                        }

                        frm1.modulerdr.ParamSet("IsTagdataRecordHighestRssi", true);
                        frm1.rParms.isUniByAnt = true;
                    }
                    else
                    {
                        frm1.modulerdr.ParamSet("IsTagdataRecordHighestRssi", false);
                        frm1.rParms.IdtAntsType = 0;
                        frm1.rParms.isUniByAnt = this.cbisunibyant.Checked;
                        frm1.rParms.isIdtAnts = false;
                    }
                }
            }
            catch (System.Exception ex)
            {
            	
            }
           
   
            this.Close();
        }

        private void InventoryParasform_Load(object sender, EventArgs e)
        {
            this.cbischgcolor.Checked = frm1.rParms.isChangeColor;
            this.cbisunibynullemd.Checked = frm1.rParms.isUniByEmd;
            this.cbisunibyant.Checked = frm1.rParms.isUniByAnt;
            
            object obj = null;
            if (!(frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E ||
                frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC))
            {
                this.gbpotlweight.Enabled = false;
            }

            if (!(frm1.rParms.readertype == ReaderType.PR_ONEANT 
                || frm1.rParms.readertype == ReaderType.MT_A7_16ANTS))
            {
                    this.tbwtgen2.Text = frm1.rParms.weightgen2.ToString();
                    this.tbwt180006b.Text = frm1.rParms.weight180006b.ToString();
                    this.tbwtipx256.Text = frm1.rParms.weightipx256.ToString();
                    this.tbwtipx64.Text = frm1.rParms.weightipx64.ToString();

                    if (frm1.modulerdr.HwDetails.module != Reader.Module_Type.MODOULE_R902_MT100)
                    {
                        EmdSecureReadData esrd = (EmdSecureReadData)frm1.modulerdr.ParamGet("EmdSecureDataOfInventory");
                        if (esrd != null)
                        {
                            this.cbissecureread.Checked = true;
                            this.tbsraddress.Text = esrd.Address.ToString();
                            this.tbsrblks.Text = esrd.BlkCnt.ToString();
                            this.cbbsrbank.SelectedIndex = (int)esrd.Bank;
                            if (esrd.PwdType == EmdSecureReadData.ESRD_AccessPasswordType.ESRD_Fix_AccessPassword)
                                this.cbbsrpwdtype.SelectedIndex = 0;
                            else
                                this.cbbsrpwdtype.SelectedIndex = 1;
                            if (esrd.TagType == EmdSecureReadData.ESRD_TagType.ESRD_Alien_Higgs3)
                                this.cbbsrtagtype.SelectedIndex = 0;
                            else
                                this.cbbsrtagtype.SelectedIndex = 1;
                            this.tbsracspwd.Text = esrd.AccessPassword.ToString("X8");
                            this.tbindexbitsnum.Text = esrd.ApIndexBitsNumInEpc.ToString();
                            this.tbsrindexstartbit.Text = esrd.ApIndexStartBitsInEpc.ToString();
                        }
                        else
                            this.cbissecureread.Checked = false;
                    }
                    else
                        this.gbsecureread.Enabled = false;
    //            }
                obj = frm1.modulerdr.ParamGet("EmbededCmdOfInventory");
                if (obj != null)
                {
                    EmbededCmdData ecd = (EmbededCmdData)obj;
                    this.tbebstartaddr.Text = ecd.StartAddr.ToString();
                    this.tbebbytescnt.Text = ecd.ByteCnt.ToString();
                    this.cbbebbank.SelectedIndex = (int)ecd.Bank;
                    this.cbisaddiondata.Checked = true;
                }
                uint pwd = (uint)frm1.modulerdr.ParamGet("AccessPassword");
                if (pwd != 0)
                {
                    this.cbispwd.Checked = true;
                    this.tbacspwd.Text = pwd.ToString("X8");
                }
            }
            else
            {
                this.gbemddata.Enabled = false;
                this.gbsecureread.Enabled = false;
            }

            obj = frm1.modulerdr.ParamGet("Singulation");
            if (obj != null)
            {
                Gen2TagFilter filter = (Gen2TagFilter)obj;
                this.cbbfilterbank.SelectedIndex = (int)filter.FilterBank -1;
                this.tbfilteraddr.Text = filter.FilterAddress.ToString();
                string binarystr = "";
                foreach (byte bt in filter.FilterData)
                {
                    string tmp = Convert.ToString(bt, 2);
                    if (tmp.Length != 8)
                    {
                        for (int c = 0; c < 8-tmp.Length; ++c)
                            binarystr += "0";
                    }
                    binarystr += tmp;
                }
                this.tbfilterdata.Text = binarystr.Substring(0, filter.FilterLength);

                if (filter.IsInvert)
                    this.cbbfilterrule.SelectedIndex = 1;
                else
                    this.cbbfilterrule.SelectedIndex = 0;

                this.cbisfilter.Checked = true;
            }
            this.tbreaddur.Text = frm1.rParms.readdur.ToString();
            this.tbsleepdur.Text = frm1.rParms.sleepdur.ToString();
            if (frm1.rParms.isReadFixCount)
            {
                this.cbisReadFixCount.Checked = true;
                this.tbReadCount.Text = frm1.rParms.FixReadCount.ToString();
            }
            else
            {
                this.cbisReadFixCount.Checked = false;
                this.tbReadCount.Enabled = false;
            }
            if (frm1.rParms.isOneReadOneTime)
                this.cbisOneReadOneTime.Checked = true;
            else
                this.cbisOneReadOneTime.Checked = false;

            // added on 3-26
            this.cbisrevertants.Checked = frm1.rParms.isRevertAnts;
            if (frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E ||
                frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_C ||
                frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_PRC ||
                frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E ||
                frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC)
            {
                this.gbidentifyants.Enabled = true;
                this.cbisidtant.Checked = frm1.rParms.isIdtAnts;

                if (this.cbisidtant.Checked)
                {
                    if (frm1.rParms.IdtAntsType == 1)
                        this.rbidtantbydur.Checked = true;
                    else if (frm1.rParms.IdtAntsType == 2)
                        this.rbidtantafter.Checked = true;
                    this.tbidtdur.Text = frm1.rParms.DurIdtval.ToString();
                    this.tbafttime.Text = frm1.rParms.AfterIdtWaitval.ToString();
                }
                else
                {
                    this.tbidtdur.Text = "";
                    this.tbafttime.Text = "";
                }
            }
            else
                this.gbidentifyants.Enabled = false;
            //


            this.cbbantcnt.SelectedIndex = frm1.rParms.usecase_antcnt;

            if (frm1.rParms.usecase_ishighspeedblf)
                this.cbishighspeed.Checked = true;
            else
                this.cbishighspeed.Checked = false;

            this.cbbreadresult.SelectedIndex = frm1.rParms.usecase_readperform;
            this.cbbtagcnt.SelectedIndex = frm1.rParms.usecase_tagcnt;
        }

        private void rbidtantbydur_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbidtantbydur.Checked)
            {
                this.tbafttime.Enabled = false;
                this.tbidtdur.Enabled = true;
            }
            else
            {
                this.tbafttime.Enabled = true;
                this.tbidtdur.Enabled = false;
            }
        }

        private void rbidtantafter_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbidtantbydur.Checked)
            {
                this.tbafttime.Enabled = false;
                this.tbidtdur.Enabled = true;
            }
            else
            {
                this.tbafttime.Enabled = true;
                this.tbidtdur.Enabled = false;
            }
        }

        private void cbisReadFixCount_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbisReadFixCount.Checked)
                this.tbReadCount.Enabled = true;
            else
                this.tbReadCount.Enabled = false;
        }

        private void btnSetCase_Click(object sender, EventArgs e)
        {
            if (this.cbbantcnt.SelectedIndex == -1)
            {
                MessageBox.Show("请选择天线使用个数");
                return;
            }
            int antcnt = this.cbbantcnt.SelectedIndex;

            if (this.cbbtagcnt.SelectedIndex == -1)
            {
                MessageBox.Show("请选择识读标签数量");
                return;
            }
            int tagcnt = this.cbbtagcnt.SelectedIndex;

            if (this.cbbreadresult.SelectedIndex == -1)
            {
                MessageBox.Show("请选择希望获得的识读效果");
                return;
            }
            int readperform = this.cbbreadresult.SelectedIndex;

            frm1.rParms.usecase_antcnt = antcnt;
            frm1.rParms.usecase_tagcnt = tagcnt;
            frm1.rParms.usecase_readperform = readperform;
            frm1.rParms.usecase_ishighspeedblf = this.cbishighspeed.Checked;

            try
            {
                int timeduroneant = 0;
                if (tagcnt == 0)
                    timeduroneant = 125;
                else if (tagcnt == 1)
                    timeduroneant = 150;
                else if (tagcnt == 2)
                    timeduroneant = 200;
                else if (tagcnt == 3)
                    timeduroneant = 250;
                else if (tagcnt == 4)
                    timeduroneant = 350;

                frm1.rParms.readdur = (antcnt+1) * timeduroneant;
                frm1.rParms.sleepdur = 0;
                this.tbreaddur.Text = frm1.rParms.readdur.ToString();
                this.tbsleepdur.Text = 0.ToString();
             
                try
                {
                    if (readperform == 0)
                    {
                        frm1.modulerdr.ParamSet("Gen2Target", ModuleTech.Gen2.Target.A);
                        frm1.modulerdr.ParamSet("Gen2Session", ModuleTech.Gen2.Session.Session0);
                    }
                    else if (readperform == 2)
                    {
                        frm1.modulerdr.ParamSet("Gen2Target", ModuleTech.Gen2.Target.AB);
                        frm1.modulerdr.ParamSet("Gen2Session", ModuleTech.Gen2.Session.Session0);
                    }
                    else if (readperform == 1)
                    {
                        frm1.modulerdr.ParamSet("Gen2Target", ModuleTech.Gen2.Target.A);
                        frm1.modulerdr.ParamSet("Gen2Session", ModuleTech.Gen2.Session.Session1);
                    }
                }
                catch
                {
                	
                }

                if (frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E
                    || frm1.modulerdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC)
                {
                    if (readperform == 1 || readperform == 0)
                        frm1.modulerdr.ParamSet("IsFastReadMode", true);
                    else
                        frm1.modulerdr.ParamSet("IsFastReadMode", false);

                    if (this.cbishighspeed.Checked)
                    {
                        frm1.modulerdr.ParamSet("gen2tagEncoding", 0);
                        frm1.modulerdr.ParamSet("gen2BLF", 640);
                        frm1.modulerdr.ParamSet("Gen2Tari", Tari.Tari_6_25US);
                    }
                    else
                    {
                        frm1.modulerdr.ParamSet("gen2BLF", 250);
                        frm1.modulerdr.ParamSet("gen2tagEncoding", 2);
                        frm1.modulerdr.ParamSet("Gen2Tari", Tari.Tari_25US);
                    }
                }
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("设置失败:" + exx.ToString());
                return;
            }
        }

    }
}