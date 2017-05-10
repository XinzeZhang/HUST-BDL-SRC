using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;
using ModuleTech.Gen2;
using ModuleTech.ISO180006b;
using ModuleLibrary;
using System.Diagnostics;

namespace ModuleReaderManager
{
    public partial class readerParaform : Form
    {
        ReaderParams m_params;
        Reader rdr;
        public readerParaform(ReaderParams paras, Reader rd)
        {
            InitializeComponent();
            m_params = paras;
            rdr = rd;
        }

        Dictionary<int, TextBox> rants = new Dictionary<int, TextBox>();
        Dictionary<int, TextBox> wants = new Dictionary<int, TextBox>();
        private void readerParaform_Load(object sender, EventArgs e)
        {
            this.tbMacAddr.Enabled = false;
            this.cbgpi1.Enabled = false;
            this.cbgpi2.Enabled = false;
            this.cbgpi3.Enabled = false;
            this.cbgpi4.Enabled = false;

            this.cbgpo1.Enabled = false;
            this.cbgpo2.Enabled = false;
            this.cbgpo3.Enabled = false;
            this.cbgpo4.Enabled = false;

            if (m_params.readertype == ReaderType.MT_THREEANTS)
            {
                this.cbgpi1.Enabled = true;
                this.cbgpi2.Enabled = true;
                this.cbgpo2.Enabled = true;
            }
            else if (m_params.readertype == ReaderType.MT_TWOANTS || m_params.readertype == ReaderType.MT_ONEANT||m_params.readertype==ReaderType.MT100)
            {
                this.cbgpi1.Enabled = true;
                this.cbgpi2.Enabled = true;
                this.cbgpo1.Enabled = true;
                this.cbgpo2.Enabled = true;
            }
            else if (m_params.readertype == ReaderType.MT_FOURANTS)
            {
                this.cbgpi1.Enabled = true;
                this.cbgpi2.Enabled = true;
                this.cbgpo1.Enabled = true;
            }
            else if (m_params.readertype == ReaderType.MT_A7_TWOANTS ||
                m_params.readertype == ReaderType.MT_A7_FOURANTS ||
                m_params.readertype == ReaderType.SL_FOURANTS ||
                m_params.readertype == ReaderType.M6_A7_FOURANTS ||
                m_params.readertype == ReaderType.M56_A7_FOURANTS ||
                m_params.readertype == ReaderType.SL_FOURANTS)
            {
                if (rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM7 ||
                    rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_WIFI
                    || rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM9
                    || rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM9_WIFI)
                {
                    this.cbgpi1.Enabled = true;
                    this.cbgpi2.Enabled = true;
                    this.cbgpi3.Enabled = true;
                    this.cbgpi4.Enabled = true;

                    this.cbgpo1.Enabled = true;
                    this.cbgpo2.Enabled = true;
                    this.cbgpo3.Enabled = true;
                    this.cbgpo4.Enabled = true;
                }
            }



            rants.Add(1, this.tbant1rpwr);
            rants.Add(2, this.tbant2rpwr);
            rants.Add(3, this.tbant3rpwr);
            rants.Add(4, this.tbant4rpwr);

            wants.Add(1, this.tbant1wpwr);
            wants.Add(2, this.tbant2wpwr);
            wants.Add(3, this.tbant3wpwr);
            wants.Add(4, this.tbant4wpwr);

            for (int u = 0; u < 4; ++u)
            {
                rants[u + 1].Enabled = false;
                wants[u + 1].Enabled = false;
            }

            for (int j = 0; j < m_params.antcnt; ++j)
            {
                rants[j + 1].Enabled = true;
                wants[j + 1].Enabled = true;
            }

            this.labMoudevir.Text = m_params.hardvir;
            this.labfirmware.Text = m_params.softvir;

            if (m_params.hasIP)
            {
                if (rdr.HwDetails.board != Reader.MaindBoard_Type.MAINBOARD_ARM9_WIFI)
                {
                    this.gpwificonf.Enabled = false;
                    this.rbnettypeeth.Enabled = false;
                    this.rbnettypewifi.Enabled = false;
                }
                this.btnipget_Click(null, null);
            }
            else
                this.gpipinfo.Enabled = false;

            if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E ||
                rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC)
                this.gbantvswr.Enabled = true;
            else
                this.gbantvswr.Enabled = false;

            if (rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM7)
                this.labmainboard.Text = "arm7 主板";
            else if (rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_SERIAL)
                this.labmainboard.Text = "串口主板";
            else if (rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_WIFI)
                this.labmainboard.Text = "wifi 主板";
            else if (rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM9)
                this.labmainboard.Text = "arm9 主板";
            else if (rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM9_WIFI)
                this.labmainboard.Text = "arm9_wifi 主板";


            if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E)
                this.labmodule.Text = "m5e";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR1100)
                this.labmodule.Text = "slr1100";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR1200)
                this.labmodule.Text = "slr1200";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR1300)
                this.labmodule.Text = "slr1300";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR5100)
                this.labmodule.Text = "slr5100";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR5200)
                this.labmodule.Text = "slr5200";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR3000)
                this.labmodule.Text = "slr3000";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR3100)
                this.labmodule.Text = "slr3100";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR3200)
                this.labmodule.Text = "slr3200";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_PRC)
                this.labmodule.Text = "m5e-prc";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_C)
                this.labmodule.Text = "m5e-c";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E)
                this.labmodule.Text = "m6e";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC)
                this.labmodule.Text = "m6e-prc";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_R902_MT100)
                this.labmodule.Text = "mt100";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_R902_MT200)
                this.labmodule.Text = "mt200";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_R2000)
                this.labmodule.Text = "r2000";
            else if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_MICRO)
                this.labmodule.Text = "m6e-micro";
            else
                this.labmodule.Text = "未知";

            try
            {
                bool isdet = (bool)rdr.ParamGet("CheckAntConnection");
                if (isdet)
                    this.cbbdetectants.SelectedIndex = 0;
                else
                    this.cbbdetectants.SelectedIndex = 1;
            }
            catch (System.Exception ex)
            {
            	
            }
           

            this.btnge2sessget_Click(null, null);
            this.btnpwrget_Click(null, null);
            this.btndetantsget_Click(null, null);
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
        private void btngethtb_Click(object sender, EventArgs e)
        {
            try
            {
                int st = Environment.TickCount;
                uint[] htb = (uint[])rdr.ParamGet("FrequencyHopTable");
                if (htb != null)
                    Sort(ref htb);
                Debug.WriteLine((Environment.TickCount - st).ToString());
                int cnt = 0;
                uint curchal = htb[htb.Length - 1];
                lvhoptb.Items.Clear();
                foreach (uint fre in htb)
                {
                    cnt++;
                    ListViewItem item = new ListViewItem(fre.ToString());
                    if (m_params.readertype == ReaderType.PR_ONEANT)
                    {
                        if (cnt == htb.Length)
                            break;
                        if (curchal == fre)
                            item.Checked = true;
            
                    }
                    else
                        item.Checked = true;
                    
                    lvhoptb.Items.Add(item);
                }
                this.allcheckBox.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取失败");
            }
        }

        private void btnsethtb_Click(object sender, EventArgs e)
        {
            List<uint> htb = new List<uint>();
            foreach (ListViewItem item in lvhoptb.Items)
            {
                if (item.Checked)
                    htb.Add(uint.Parse(item.SubItems[0].Text));
            }
            

            if (m_params.readertype == ReaderType.PR_ONEANT)
            {
                if (htb.Count != 1)
                {
                    MessageBox.Show("只能设置一个信道");
                    return;
                }
            }

            try
            {
                rdr.ParamSet("FrequencyHopTable", htb.ToArray());
            }
            catch(Exception)
            {
                MessageBox.Show("设置失败 ");
            }

        }

        private void btngetrg_Click(object sender, EventArgs e)
        {
            try
            {
                int st = Environment.TickCount;
                ModuleTech.Region rg = (ModuleTech.Region)rdr.ParamGet("Region");
                Debug.WriteLine((Environment.TickCount -st).ToString());
                switch (rg)
                {
                    case ModuleTech.Region.CN:
                        this.cbbregion.SelectedIndex = 6;
                        break;
                    case ModuleTech.Region.EU:
                    case ModuleTech.Region.EU2:
                    case ModuleTech.Region.EU3:
                        this.cbbregion.SelectedIndex = 4;
                        break;
                    case ModuleTech.Region.IN:
                        this.cbbregion.SelectedIndex = 5;
                        break;
                    case ModuleTech.Region.JP:
                        this.cbbregion.SelectedIndex = 2;
                        break;
                    case ModuleTech.Region.KR:
                        this.cbbregion.SelectedIndex = 3;
                        break;
                    case ModuleTech.Region.NA:
                        this.cbbregion.SelectedIndex = 1;
                        break;
                    case ModuleTech.Region.PRC:
                        this.cbbregion.SelectedIndex = 0;
                        break;
                    case ModuleTech.Region.OPEN:
                        this.cbbregion.SelectedIndex = 7;
                        break;
                    case ModuleTech.Region.PRC2:
                        this.cbbregion.SelectedIndex = 8;
                        break;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取失败");
            }
        }

        private void btnsetrg_Click(object sender, EventArgs e)
        {
            ModuleTech.Region rg = ModuleTech.Region.UNSPEC;
            bool is840_845 = false;
            bool is840_925 = false; ;
            if (this.cbbregion.SelectedIndex == -1)
            {
                MessageBox.Show("请选择区域");
                return;
            }
            switch (this.cbbregion.SelectedIndex)
            {
                case 0:
                    rg = ModuleTech.Region.PRC;
                    break;
                case 1:
                    rg = ModuleTech.Region.NA;
                    break;
                case 2:
                    rg = ModuleTech.Region.JP;
                    break;
                case 3:
                    rg = ModuleTech.Region.KR;
                    break;
                case 4:
                    rg = ModuleTech.Region.EU3;
                    break;
                case 5:
                    rg = ModuleTech.Region.IN;
                    break;
                case 6:
                    rg = ModuleTech.Region.CN;
                    break;
                case 7:
                    rg = ModuleTech.Region.OPEN;
                    break;
                case 8:
                    {
                        if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E ||
                            rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC ||
                            rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR1100)
                        {
                            if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC ||
                                rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR1100)
                                rg = ModuleTech.Region.PRC2;
                            else
                            {
                                is840_845 = true;
                                rg = ModuleTech.Region.OPEN;
                            }
                            break;
                        }
                        else
                        {
                            MessageBox.Show("不支持的区域");
                            return;
                        }
                    }
                case 9:
                    {
                        if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E ||
                            rdr.HwDetails.module == Reader.Module_Type.MODOULE_M6E_PRC ||
                            rdr.HwDetails.module == Reader.Module_Type.MODOULE_SLR1100)
                        {
                            rg = ModuleTech.Region.OPEN;
                            is840_925 = true;
                            break;
                        }
                        else
                        {
                            MessageBox.Show("不支持的区域");
                            return;
                        }
                    }
            
            }

            if (m_params.readertype == ReaderType.PR_ONEANT)
            {
                if (rg == ModuleTech.Region.OPEN || rg == ModuleTech.Region.CN)
                {
                    MessageBox.Show("不支持的区域");
                    return;
                }
            }
            try
            {
                rdr.ParamSet("Region", rg);
                if (is840_845 || is840_925)
                {
                    List<uint> htab = new List<uint>();
                    if (is840_845)
                    {                       
                        htab.Add(841375);
                        htab.Add(842625);
                        htab.Add(840875);
                        htab.Add(843625);
                        htab.Add(841125);
                        htab.Add(840625);
                        htab.Add(843125);
                        htab.Add(841625);
                        htab.Add(842125);
                        htab.Add(843875);
                        htab.Add(841875);
                        htab.Add(842875);
                        htab.Add(844125);
                        htab.Add(843375);
                        htab.Add(844375);
                        htab.Add(842375);     
                    }
                    else if (is840_925)
                    {
                        htab.Add(841375);
                        htab.Add(921375);

                        htab.Add(842625);
                        htab.Add(922625);

                        htab.Add(840875);
                        htab.Add(920875);

                        htab.Add(843625);
                        htab.Add(923625);

                        htab.Add(841125);
                        htab.Add(921125);

                        htab.Add(840625);
                        htab.Add(920625);

                        htab.Add(843125);
                        htab.Add(923125);

                        htab.Add(841625);
                        htab.Add(921625);

                        htab.Add(842125);
                        htab.Add(922125);

                        htab.Add(843875);
                        htab.Add(923875);

                        htab.Add(841875);
                        htab.Add(921875);

                        htab.Add(842875);
                        htab.Add(922875);

                        htab.Add(844125);
                        htab.Add(924125);

                        htab.Add(843375);
                        htab.Add(923375);

                        htab.Add(844375);
                        htab.Add(924375);

                        htab.Add(842375);
                        htab.Add(922375);
                    }
                    rdr.ParamSet("FrequencyHopTable", htab.ToArray());
                }
                else
                {

                }
            }
            catch (Exception)
            {
                MessageBox.Show("设置失败 ");
            }
        }

        private void btngetmel_Click(object sender, EventArgs e)
        {
            try
            {
                int epclen = (int)rdr.ParamGet("MaxEPCLength");
                this.cbbMaxEPCLength.SelectedIndex = epclen / 496;
            }
            catch (Exception)
            {
                MessageBox.Show("获取失败 ");
            }
        }

        private void btnsetmel_Click(object sender, EventArgs e)
        {
            int epclen = 96;
            if (this.cbbMaxEPCLength.SelectedIndex == 0)
                epclen = 96;
            else if (this.cbbMaxEPCLength.SelectedIndex == 1)
                epclen = 496;

            try
            {
                rdr.ParamSet("MaxEPCLength", epclen);
            }
            catch (Exception)
            {
                MessageBox.Show("设置失败 ");
            }

        }

        private void btngetgpi_Click(object sender, EventArgs e)
        {
            if (this.cbgpi1.Enabled)
            {
                if (rdr.GPIGet(1))
                    this.cbgpi1.Checked = true;
                else
                    this.cbgpi1.Checked = false;
            }
            if (this.cbgpi2.Enabled)
            {
                if (rdr.GPIGet(2))
                    this.cbgpi2.Checked = true;
                else
                    this.cbgpi2.Checked = false;
            }
            if (this.cbgpi3.Enabled)
            {
                if (rdr.GPIGet(3))
                    this.cbgpi3.Checked = true;
                else
                    this.cbgpi3.Checked = false;
            }
            if (this.cbgpi4.Enabled)
            {
                if (rdr.GPIGet(4))
                    this.cbgpi4.Checked = true;
                else
                    this.cbgpi4.Checked = false;
            }
        }

        private void btnsetgpo_Click(object sender, EventArgs e)
        {
            if (this.cbgpo1.Enabled)
            {
                if (this.cbgpo1.Checked)
                    rdr.GPOSet(1, true);
                else
                    rdr.GPOSet(1, false);
            }
            if (this.cbgpo2.Enabled)
            {
                if (this.cbgpo2.Checked)
                    rdr.GPOSet(2, true);
                else
                    rdr.GPOSet(2, false);
            }
            if (this.cbgpo3.Enabled)
            {
                if (this.cbgpo3.Checked)
                    rdr.GPOSet(3, true);
                else
                    rdr.GPOSet(3, false);
            }
            if (this.cbgpo4.Enabled)
            {
                if (this.cbgpo4.Checked)
                    rdr.GPOSet(4, true);
                else
                    rdr.GPOSet(4, false);
            }
        }

        private void btnGetWriteMode_Click(object sender, EventArgs e)
        {

            WriteMode wm = WriteMode.WORD_ONLY;
            try
            {
                wm = (WriteMode)rdr.ParamGet("Gen2WriteMode");
            }
            catch
            {
                MessageBox.Show("操作失败");
                return;
            }
            this.cbbwritemode.SelectedIndex = (int)wm;

        }

        private void btnSetWriteMode_Click(object sender, EventArgs e)
        {
            if (this.cbbwritemode.SelectedIndex == -1)
                MessageBox.Show("请选择写模式");
            else
            {
                try
                {
                    rdr.ParamSet("Gen2WriteMode", (WriteMode)this.cbbwritemode.SelectedIndex);
                }
                catch
                {
                    MessageBox.Show("操作失败");
                }
            }
        }

        private void cbmacset_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbmacset.Checked)
                this.tbMacAddr.Enabled = true;
            else
                this.tbMacAddr.Enabled = false;
        }

        private void btngetgen2encode_Click(object sender, EventArgs e)
        {
            try
            {
                int enc = (int)rdr.ParamGet("gen2tagEncoding");
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
                rdr.ParamSet("gen2tagEncoding", this.cbbgen2encode.SelectedIndex);
            }
            catch(Exception exx)
            {
                MessageBox.Show("操作失败:"+exx.ToString());
            }

        }

        private void btngetgen2blf_Click(object sender, EventArgs e)
        {


            try
            {
                int blf = (int)rdr.ParamGet("gen2BLF");
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
                rdr.ParamSet("gen2BLF", blf);
            }
            catch
            {
                MessageBox.Show("操作失败");
            }

        }

        private void btnpermsave_Click(object sender, EventArgs e)
        {
            ReaderConfiguration rdrconf = new ReaderConfiguration(ReaderConfiguration.SaveConfCode.SaveConf_Save);

            try
            {
                rdr.ParamSet("SaveConfiguration", rdrconf);
                MessageBox.Show("保存成功，请重新连接读写器");
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
                return;
            }
        }

        private void btnSetNtp_Click(object sender, EventArgs e)
        {
            if ((!this.rbisntpfalse.Checked) && (!this.rbntptrue.Checked))
            {
                MessageBox.Show("请选择禁用还是使能");
                return;
            }

            if (this.rbntptrue.Checked && this.tbNtpServerip.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入NTP 服务器地址");
                return;
            }
            NtpSetting ntp = new NtpSetting();
            if (this.rbntptrue.Checked)
                ntp.IsEnableNtp = true;
            else
                ntp.IsEnableNtp = false;

            ntp.NtpServerIp = this.tbNtpServerip.Text.Trim();
            try
            {
                rdr.ParamSet("NtpSetting", ntp);
                MessageBox.Show("设置成功，读写器会自动重启，请重新连接读写器");
            }
            catch
            {
                MessageBox.Show("操作失败");
                return;
            }
        }

        private void btngetntp_Click(object sender, EventArgs e)
        {
            try
            {
                NtpSetting ntp = (NtpSetting)rdr.ParamGet("NtpSetting");
                if (ntp.IsEnableNtp)
                    this.rbntptrue.Checked = true;
                else
                    this.rbisntpfalse.Checked = true;
                this.tbNtpServerip.Text = ntp.NtpServerIp;
            }
            catch
            {
                MessageBox.Show("操作失败");
                return;
            }
        }

        private void btniso183kblfget_Click(object sender, EventArgs e)
        {
            try
            {
                int blf = (int)rdr.ParamGet("Iso180006bBLF");
                this.tbIso183kblf.Text = blf.ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取失败:"+ex.ToString());
            }
        }

        private void btniso183kblfset_Click(object sender, EventArgs e)
        {
            try
            {
                int blf = int.Parse(this.tbIso183kblf.Text.Trim());
                if (blf <= 0 || blf >160)
                {
                    MessageBox.Show("blf 必须在0到160之间");
                    return;
                }
                rdr.ParamSet("Iso180006bBLF", blf);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.ToString());
            }
        }

        private void btngetgen2target_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleTech.Gen2.Target tt = (ModuleTech.Gen2.Target)rdr.ParamGet("Gen2Target");
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
                rdr.ParamSet("Gen2Target", (ModuleTech.Gen2.Target)this.cbbgen2target.SelectedIndex);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.ToString());
            }
        }

        private void btngetdataantunique_Click(object sender, EventArgs e)
        {
            try
            {
                bool is_ = (bool)rdr.ParamGet("IsTagDataUniqueByAnt");
                if (is_)
                    this.rbtagdataisant.Checked = true;
                else
                    this.rbtagdataisnoant.Checked = true;
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
        }

        private void btngetdataemdunique_Click(object sender, EventArgs e)
        {
            try
            {
                bool is_ = (bool)rdr.ParamGet("IsTagDataUniqueByEmddata");
                if (is_)
                    this.rbtagdataisemd.Checked = true;
                else
                    this.rbtagdataisnoemd.Checked = true;
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
        }

        private void btngetdatarechrssi_Click(object sender, EventArgs e)
        {
            try
            {
                bool is_ = (bool)rdr.ParamGet("IsTagdataRecordHighestRssi");
                if (is_)
                    this.rbtagdataisrecrssi.Checked = true;
                else
                    this.rbtagdataisnorecrssi.Checked = true;
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("操作失败:" + exx.ToString());
            }
        }

        private void btnsetdataantunique_Click(object sender, EventArgs e)
        {
            bool is_ = false;
            if ((!this.rbtagdataisant.Checked) && (!this.rbtagdataisnoant.Checked))
            {
                MessageBox.Show("请选择是否唯一");
                return;
            }
            if (this.rbtagdataisant.Checked)
                is_ = true;
            else
                is_ = false;
            try
            {
                rdr.ParamSet("IsTagDataUniqueByAnt", is_);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnsetdataemdunique_Click(object sender, EventArgs e)
        {
            bool is_ = false;
            if ((!this.rbtagdataisemd.Checked) && (!this.rbtagdataisnoemd.Checked))
            {
                MessageBox.Show("请选择是否唯一");
                return;
            }
            if (this.rbtagdataisemd.Checked)
                is_ = true;
            else
                is_ = false;
            try
            {
                rdr.ParamSet("IsTagDataUniqueByEmddata", is_);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnsetdatarechrssi_Click(object sender, EventArgs e)
        {
            bool is_ = false;
            if ((!this.rbtagdataisrecrssi.Checked) && (!this.rbtagdataisnorecrssi.Checked))
            {
                MessageBox.Show("请选择是否纪录");
                return;
            }
            if (this.rbtagdataisrecrssi.Checked)
                is_ = true;
            else
                is_ = false;
            try
            {
                rdr.ParamSet("IsTagdataRecordHighestRssi", is_);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btngetgen2tari_Click(object sender, EventArgs e)
        {
            try
            {
                Tari tari = (Tari)rdr.ParamGet("Gen2Tari");
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
                rdr.ParamSet("Gen2Tari", tari);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnflashwrtie_Click(object sender, EventArgs e)
        {
            if (this.tbfwdata.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填入要保存的数据");
                return;
            }
            if (this.tbfwaddress.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填入数据的保存地址");
                return;
            }
            byte[] data = ByteFormat.FromHex(this.tbfwdata.Text.Trim());
            try
            {
                rdr.EraseDataOnReader();
                rdr.SaveDataToReader(int.Parse(this.tbfwaddress.Text.Trim()), data);
                if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E || 
                    rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_C ||
                     rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_PRC)
                {
                    MessageBox.Show("写入成功，由于是m5e系列设备，请重新连接读写器");
                    return;
                }
            }
            catch (Exception exx)
            {
                MessageBox.Show("操作失败");
                return;
            }
        }

        private void btngettemperature_Click(object sender, EventArgs e)
        {
            try
            {
                byte temperature = (byte)rdr.ParamGet("Temperature");
                this.labtemperature.Text = temperature.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("获取失败:"+ex.ToString());
            }
        }

        private void btnreadflash_Click(object sender, EventArgs e)
        {
            if (this.tbfwaddress.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填入读取地址");
                return;
            }
            if (this.tbbytesreadflash.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请填入读字节数");
                return;
            }

            try
            {
                byte[] rdata = rdr.ReadDataOnReader(int.Parse(this.tbfwaddress.Text.Trim()),
                    byte.Parse(this.tbbytesreadflash.Text.Trim()));
                this.tbfwdata.Text = ByteFormat.ToHex(rdata);
                if (rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E ||
                    rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_C ||
                    rdr.HwDetails.module == Reader.Module_Type.MODOULE_M5E_PRC)
                {
                    MessageBox.Show("读取成功，由于是m5e系列设备，请重新连接读写器");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }

        }

        private void btnge2sessget_Click(object sender, EventArgs e)
        {
            try
            {
                ModuleTech.Gen2.Session sess = (ModuleTech.Gen2.Session)rdr.ParamGet("Gen2Session");
                this.cbbsession.SelectedIndex = (int)sess;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取失败:" + ex.ToString());
            }
            
        }

        private void btngen2sessset_Click(object sender, EventArgs e)
        {
            if (this.cbbsession.SelectedIndex == -1)
            {
                MessageBox.Show("请选择session");
                return;
            }
            try
            {
                rdr.ParamSet("Gen2Session", (ModuleTech.Gen2.Session)this.cbbsession.SelectedIndex);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.ToString());
            }
        }

        private void btngen2qget_Click(object sender, EventArgs e)
        {
            try
            {
                int gen2qval = (int)rdr.ParamGet("Gen2Qvalue");
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
                rdr.ParamSet("Gen2Qvalue", this.cbbGen2Q.SelectedIndex-1);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.ToString());
            }
        }

        private void btndetantsget_Click(object sender, EventArgs e)
        {
            try
            {
                bool isdet = (bool)rdr.ParamGet("CheckAntConnection");
                if (isdet)
                    this.cbbdetectants.SelectedIndex = 0;
                else
                    this.cbbdetectants.SelectedIndex = 1;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取失败:" + ex.ToString());
            }
        }

        private void btndetantsset_Click(object sender, EventArgs e)
        {
            if (this.cbbdetectants.SelectedIndex == -1)
            {
                MessageBox.Show("请选择天线检测");
                return;
            }
            try
            {
                bool isdet = false;
                if (this.cbbdetectants.SelectedIndex == 0)
                    isdet = true;
                else
                    isdet = false;

                rdr.ParamSet("CheckAntConnection", isdet);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败:" + ex.ToString());
            }
        }

        private void btnpwrget_Click(object sender, EventArgs e)
        {
            AntPower[] apwrs2 = (AntPower[])rdr.ParamGet("AntPowerConf");
            foreach (AntAndBoll a in m_params.AntsState)
            {
                foreach (AntPower b in apwrs2)
                {
                    if (a.antid == b.AntId)
                    {
                        a.rpower = b.ReadPower;
                        a.wpower = b.WritePower;
                        break;
                    }
                }
            }
            foreach (AntAndBoll a in m_params.AntsState)
            {
                if (rants[a.antid].Enabled)
                {
                    rants[a.antid].Text = a.rpower.ToString();
                    wants[a.antid].Text = a.wpower.ToString();
                }
            }
        }

        private void btnpwrset_Click(object sender, EventArgs e)
        {
            int maxp = (int)rdr.ParamGet("RfPowerMax");
            int minp = (int)rdr.ParamGet("RfPowerMin");
            foreach (AntAndBoll a in m_params.AntsState)
            {
                if (rants[a.antid].Enabled == true)
                {
                    int rpwr;
                    int wpwr;
                    try
                    {
                        rpwr = int.Parse(rants[a.antid].Text);
                        wpwr = int.Parse(wants[a.antid].Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("功率输入错误，请输入整数");
                        return;
                    }

                    if (rdr.HwDetails.module != Reader.Module_Type.MODOULE_M6E_MICRO)
                    {
                        if (rpwr < minp || rpwr > maxp || wpwr < minp || wpwr > maxp)
                        {
                            MessageBox.Show("功率值只能在" + minp.ToString() + "到" + maxp.ToString() + "之间");
                            return;
                        }
                    }

                    a.rpower = (ushort)(rpwr);
                    a.wpower = (ushort)(wpwr);

                    if (m_params.readertype == ReaderType.PR_ONEANT)
                    {
                        if (a.rpower != a.wpower)
                        {
                            MessageBox.Show("此类型读写器要求读写功率相同");
                            return;
                        }
                    }
                }
            }
            List<AntPower> antspwr = new List<AntPower>();

            foreach (AntAndBoll at in m_params.AntsState)
            {
                antspwr.Add(new AntPower((byte)at.antid, (ushort)(at.rpower),
                    (ushort)(at.wpower)));
            }
            try
            {
                rdr.ParamSet("AntPowerConf", antspwr.ToArray());
            }
            catch (System.Exception exx)
            {
                MessageBox.Show("设置失败：" + exx.ToString());
            }
            
        }

        private void btnipget_Click(object sender, EventArgs e)
        {
            if (rdr.HwDetails.board != Reader.MaindBoard_Type.MAINBOARD_ARM9_WIFI)
            {
                this.tbipaddr.Text = m_params.ip;
                this.tbsubnet.Text = m_params.subnet;
                this.tbgateway.Text = m_params.gateway;
                if (m_params.macstr != null)
                {
                    this.tbMacAddr.Text = m_params.macstr;
                }
            }
            else
            {
                try
                {
                    ReaderIPInfo_Ex ipinfo = (ReaderIPInfo_Ex)rdr.ParamGet("IPAddressEx");
                    this.tbipaddr.Text = ipinfo.IPInfo.IP;
                    this.tbsubnet.Text = ipinfo.IPInfo.SUBNET;
                    this.tbgateway.Text = ipinfo.IPInfo.GATEWAY;

                    if (ipinfo.NType == ReaderIPInfo_Ex.NetType.NetType_Ethernet)
                        this.rbnettypeeth.Checked = true;
                    else if (ipinfo.NType == ReaderIPInfo_Ex.NetType.NetType_Wifi)
                    {                       
                        this.cbbwifiauth.SelectedIndex = (int)(ipinfo.Wifi.Auth-1);
                        this.tbwifissid.Text = ipinfo.Wifi.SSID;
                        if (ipinfo.Wifi.KEY != null)
                            this.tbwifikey.Text = ipinfo.Wifi.KEY;
                        else
                            this.tbwifikey.Text = "";
                        this.rbnettypewifi.Checked = true;
                        this.cbbkeytype.SelectedIndex = (int)(ipinfo.Wifi.KType - 1);
                    }

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("获取失败:" + ex.ToString());
                }
                
            }
        }

        private void btnipset_Click(object sender, EventArgs e)
        {
            if (this.tbipaddr.Text.Trim() == string.Empty || this.tbsubnet.Text.Trim() == string.Empty
                || this.tbgateway.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入ip地址相关项");
                return;
            }
            ReaderIPInfo ipinfo = null;
            try
            {
                ipinfo = ReaderIPInfo.Create(this.tbipaddr.Text.Trim(), this.tbsubnet.Text.Trim(),
                    this.tbgateway.Text.Trim());
                if (this.cbmacset.Checked)
                {
                    if (this.tbMacAddr.Text.Trim().Length != 12)
                    {
                        MessageBox.Show("MAC地址格式错误");
                        return;
                    }
                    else
                    {
                        try
                        {
                            byte[] macb = ByteFormat.FromHex(this.tbMacAddr.Text.Trim());
                            ipinfo.MACADDR = macb;
                        }
                        catch
                        {
                            MessageBox.Show("MAC地址格式错误");
                            return;
                        }
                    }

                }
            }
            catch (OpFaidedException exp)
            {
                MessageBox.Show("ip 地址设置有错误:" + exp.ToString());
                return;
            }

            if (rdr.HwDetails.board == Reader.MaindBoard_Type.MAINBOARD_ARM9_WIFI)
            {   
                if ((!this.rbnettypewifi.Checked) && (!this.rbnettypeeth.Checked))
                {
                    MessageBox.Show("请选择是以太网，还是wifi");
                    return;
                }

                ReaderIPInfo_Ex.NetType type = ReaderIPInfo_Ex.NetType.NetType_None;
                ReaderIPInfo_Ex.WifiSetting wifi = null;
                if (this.rbnettypewifi.Checked)
                {
                    if (this.cbbwifiauth.SelectedIndex == -1 || this.tbwifissid.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("请输入wifi配置相关项");
                        return;
                    }

                    ReaderIPInfo_Ex.WifiSetting.AuthMode auth = (ReaderIPInfo_Ex.WifiSetting.AuthMode)(this.cbbwifiauth.SelectedIndex + 1);

                    if (this.cbbwifiauth.SelectedIndex == 0)
                    {
                        wifi = new ReaderIPInfo_Ex.WifiSetting(auth, this.tbwifissid.Text.Trim(),
                            ReaderIPInfo_Ex.WifiSetting.KeyType.KeyType_NONE, null);
                    }
                    else
                    {
                        if (this.tbwifikey.Text.Trim() == string.Empty || this.cbbkeytype.SelectedIndex == -1)
                        {
                            MessageBox.Show("请输入wifi配置相关项");
                            return;
                        }
                        if (this.cbbwifiauth.SelectedIndex == 3 || this.cbbwifiauth.SelectedIndex == 4)
                        {
                            if (this.cbbkeytype.SelectedIndex == 1)
                            {
                                MessageBox.Show("密钥类型只能是ASC2码");
                                return;
                            }
                        }

                        wifi = new ReaderIPInfo_Ex.WifiSetting(auth, this.tbwifissid.Text.Trim(), 
                            (ReaderIPInfo_Ex.WifiSetting.KeyType)(this.cbbkeytype.SelectedIndex+1),
                            this.tbwifikey.Text.Trim());
                    }
            
                    type = ReaderIPInfo_Ex.NetType.NetType_Wifi;
                }
                else
                    type = ReaderIPInfo_Ex.NetType.NetType_Ethernet;
          
                ReaderIPInfo_Ex ininfoex = new ReaderIPInfo_Ex(ipinfo, type, wifi);
                try
                {
                    rdr.ParamSet("IPAddressEx", ininfoex);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("设置失败:" + ex.ToString());
                }
                
            }
            else
            {
                try
                {
                    rdr.ParamSet("IPAddress", ipinfo);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("设置失败:" + ex.ToString());
                }
            }
            
            MessageBox.Show("ip设置成功，请重新连接读写器");
        }

        private void rbnettypeeth_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnettypeeth.Checked)
            {
                this.gpwificonf.Enabled = false;
            }
        }

        private void rbnettypewifi_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbnettypewifi.Checked)
            {
                this.gpwificonf.Enabled = true;
            }
        }

        private void btnset915_Click(object sender, EventArgs e)
        {
            int[] hoptb = new int[] { 915750, 915250, 917750, 914750, 913750, 917250, 914250, 916250, 916750, 913250 };
            rdr.ParamSet("FrequencyHopTable", hoptb);
        }

        private void btngetinvmode_Click(object sender, EventArgs e)
        {
            try
            {
                bool isfast = (bool)rdr.ParamGet("IsFastReadMode");
                if (isfast)
                    this.cbbinvmode.SelectedIndex = 1;
                else
                    this.cbbinvmode.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnsetinvmode_Click(object sender, EventArgs e)
        {
            if (this.cbbinvmode.SelectedIndex == -1)
            {
                MessageBox.Show("请选择盘存模式");
                return;
            }
            try
            {
                bool isfast = false;
                if (this.cbbinvmode.SelectedIndex == 1)
                    isfast = true;
                rdr.ParamSet("IsFastReadMode", isfast);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnget6bmoddepth_Click(object sender, EventArgs e)
        {
            try
            {
                ISO180006B_ModulationDepth md= (ISO180006B_ModulationDepth)rdr.ParamGet("Iso180006bModulationDepth");
                if (md == ISO180006B_ModulationDepth.ISO180006B_ModulationDepth11percent)
                    this.cbb6bmoddepth.SelectedIndex = 1;
                else if (md == ISO180006B_ModulationDepth.ISO180006B_ModulationDepth99percent)
                    this.cbb6bmoddepth.SelectedIndex = 0;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnset6bmoddepth_Click(object sender, EventArgs e)
        {
             if (this.cbb6bmoddepth.SelectedIndex == -1)
            {
                MessageBox.Show("请选择调制深度");
                return;
            }
            try
            {
                ISO180006B_ModulationDepth md = ISO180006B_ModulationDepth.ISO180006B_ModulationDepth99percent;
                if (this.cbb6bmoddepth.SelectedIndex == 1)
                    md = ISO180006B_ModulationDepth.ISO180006B_ModulationDepth11percent;
                else if (this.cbb6bmoddepth.SelectedIndex == 0)
                    md = ISO180006B_ModulationDepth.ISO180006B_ModulationDepth99percent;
                rdr.ParamSet("Iso180006bModulationDepth", md);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnget6bdelimiter_Click(object sender, EventArgs e)
        {
            try
            {
                ISO180006B_Delimiter del = (ISO180006B_Delimiter)rdr.ParamGet("Iso180006bDelimiter");
                if (del == ISO180006B_Delimiter.ISO180006B_Delimiter1)
                    this.cbb6bdelimiter.SelectedIndex = 0;
                else if (del == ISO180006B_Delimiter.ISO180006B_Delimiter4)
                    this.cbb6bdelimiter.SelectedIndex = 1;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnset6bdelimiter_Click(object sender, EventArgs e)
        {
            if (this.cbb6bdelimiter.SelectedIndex == -1)
            {
                MessageBox.Show("请选择Delimiter");
                return;
            }
            try
            {
                ISO180006B_Delimiter del = ISO180006B_Delimiter.ISO180006B_Delimiter1;
                if (this.cbb6bdelimiter.SelectedIndex == 1)
                    del = ISO180006B_Delimiter.ISO180006B_Delimiter4;
                else if (this.cbb6bdelimiter.SelectedIndex == 0)
                    del = ISO180006B_Delimiter.ISO180006B_Delimiter1;
                rdr.ParamSet("Iso180006bDelimiter", del);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btngetrdrlogname_Click(object sender, EventArgs e)
        {
            try
            {
                string name = (string)rdr.ParamGet("ReaderName");
                this.tbreaderlogname.Text = name;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnsetrdrlogname_Click(object sender, EventArgs e)
        {
            if (this.tbreaderlogname.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入读写器名称");
                return;
            }
            try
            {
                rdr.ParamSet("ReaderName", this.tbreaderlogname.Text.Trim());
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void btnvswrdetect_Click(object sender, EventArgs e)
        {
            try
            {
                AntPortVSWR[] vswrs = (AntPortVSWR[])rdr.ParamGet("AntPortsVSWR");
                this.tbant1vswr.Text = vswrs[0].VSWR.ToString();
                this.tbant2vswr.Text = vswrs[1].VSWR.ToString();
                this.tbant3vswr.Text = vswrs[2].VSWR.ToString();
                this.tbant4vswr.Text = vswrs[3].VSWR.ToString();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败:" + ex.ToString());
            }
        }

        private void allcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (allcheckBox.Checked)
            {

                for (int i = 0; i < lvhoptb.Items.Count; i++)
                {
                    lvhoptb.Items[i].Checked = true;
                }

            }
            else
            {
                for (int i = 0; i < lvhoptb.Items.Count; i++)
                {
                    lvhoptb.Items[i].Checked = false;
                }
            }
          
        }
        /*
<<<<<<< .mine
=======

        private void restcheckBox_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < lvhoptb.Items.Count; i++)
            {
                if (lvhoptb.Items[i].Checked)
                {
                    lvhoptb.Items[i].Checked = false;
                }
                else
                    lvhoptb.Items[i].Checked = true;
            }
        }

>>>>>>> .r94
         * */
        private void btnerasesave_Click(object sender, EventArgs e)
        {
            ReaderConfiguration rdrconf = new ReaderConfiguration(ReaderConfiguration.SaveConfCode.SaveConf_Erase);

            try
            {
                rdr.ParamSet("SaveConfiguration", rdrconf);
                MessageBox.Show("擦除完成，读写器重新上电后完全生效");
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作失败:"+ex.ToString());
                return;
            }
        }
/*
<<<<<<< .mine
=======

>>>>>>> .r94
 * */
    }
}