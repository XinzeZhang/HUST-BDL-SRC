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


namespace ModuleReaderManager
{
    public partial class Frm16portAnts : Form
    {
        List<CheckBox> cbants = new List<CheckBox>();

        public Frm16portAnts(Reader rdr_, ReaderParams rdrparams_)
        {
            InitializeComponent();
            rdr = rdr_;
            rdrparams = rdrparams_;
        }

        Reader rdr = null;
        ReaderParams rdrparams = null;

        private void Frm16portAnts_Load(object sender, EventArgs e)
        {
            cbants.Add(cbant1);
            cbants.Add(cbant2);
            cbants.Add(cbant3);
            cbants.Add(cbant4);
            cbants.Add(cbant5);
            cbants.Add(cbant6);
            cbants.Add(cbant7);
            cbants.Add(cbant8);
            cbants.Add(cbant9);
            cbants.Add(cbant10);
            cbants.Add(cbant11);
            cbants.Add(cbant12);
            cbants.Add(cbant13);
            cbants.Add(cbant14);
            cbants.Add(cbant15);
            cbants.Add(cbant16);

            foreach (CheckBox ant in cbants)
            {
                ant.ForeColor = Color.Red;
            }

            foreach (int ant in rdrparams.SixteenDevConAnts)
            {
                cbants[ant-1].ForeColor = Color.Green;
            }
            if (rdrparams.SixteenDevsrp != null)
            {
                foreach (int ant in rdrparams.SixteenDevsrp.Antennas)
                {
                    cbants[ant - 1].Checked = true;
                }
                if (rdrparams.SixteenDevsrp.Protocol == TagProtocol.ISO180006B)
                    rbpotl6b.Checked = true;
                else if (rdrparams.SixteenDevsrp.Protocol == TagProtocol.GEN2)
                    rbpotlgen2.Checked = true;
                else if (rdrparams.SixteenDevsrp.Protocol == TagProtocol.IPX64)
                    rbpotlipx64.Checked = true;
                else if (rdrparams.SixteenDevsrp.Protocol == TagProtocol.IPX256)
                    rbpotlipx256.Checked = true;
            }

            AntPower[] pwrs = (AntPower[])rdr.ParamGet("AntPowerConf");
            this.tbreadpwr.Text = pwrs[0].ReadPower.ToString();
            this.tbwritepwr.Text = pwrs[0].WritePower.ToString();
            this.tbSession0dur.Enabled = false;
            this.rbdisablerf.Checked = true;
            btnsetrssif.Enabled = false;

            bool isrssift = (bool)rdr.ParamGet("IsRssiFilter");
            int rfdur = (int)rdr.ParamGet("Session0FTime");
            if (isrssift)
            {
                this.rbenablerf.Checked = true;
                btnsetrssif.Enabled = true;
                tbSession0dur.Text = rfdur.ToString();
            }
            else
            {
                this.rbdisablerf.Checked = true;
                tbSession0dur.Enabled = false;
            }

            
        }

        private void btnselants_Click(object sender, EventArgs e)
        {
            List<int> selants = new List<int>();

            for (int i = 0; i < cbants.Count; ++i)
            {
                if (cbants[i].Checked)
                    selants.Add(i + 1);
            }
            if (selants.Count == 0)
            {
                MessageBox.Show("请选择天线");
                return;
            }
            List<int> unDetAnts = new List<int>();
            foreach (int ant in selants)
            {
                if (cbants[ant - 1].ForeColor == Color.Red)
                    unDetAnts.Add(ant);
            }

            if (unDetAnts.Count > 0)
            {
                string antstr = "";
                foreach (int ant in unDetAnts)
                {
                    antstr += "端口" + ant.ToString() + ",";
                }
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show(antstr + "未检测到天线，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;
            }

            TagProtocol pl = TagProtocol.NONE;

            if (rbpotl6b.Checked)
                pl = TagProtocol.ISO180006B;
            else if (rbpotlgen2.Checked)
                pl = TagProtocol.GEN2;
            else if (rbpotlipx256.Checked)
                pl = TagProtocol.IPX256;
            else if (rbpotlipx64.Checked)
                pl = TagProtocol.IPX64;
            else
            {
                MessageBox.Show("请选择协议");
                return;
            }

            rdrparams.SixteenDevsrp = new SimpleReadPlan(pl, selants.ToArray());
            this.Close();
        }

        private void btnsetpwr_Click(object sender, EventArgs e)
        {
            if (this.tbreadpwr.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入读功率");
                return;
            }
            if (this.tbwritepwr.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入写功率");
                return;
            }

            AntPower[] pwrs = new AntPower[16];
            ushort rpwr = ushort.Parse(this.tbreadpwr.Text.Trim());
            ushort wpwr = ushort.Parse(this.tbwritepwr.Text.Trim());
            int powermax = (int)rdr.ParamGet("RfPowerMax");
            int powermin = (int)rdr.ParamGet("RfPowerMin");
            if (rpwr < powermin || rpwr > powermax || wpwr < powermin || wpwr > powermax)
            {
                MessageBox.Show("功率值只能在" + powermin.ToString() + "-" + powermax.ToString()+"之间");
                return;
            }

            for (int i = 0; i < 16; ++i)
            {
                pwrs[i].AntId = (byte)(i + 1);
                pwrs[i].ReadPower = rpwr;
                pwrs[i].WritePower = wpwr;
            }

            try
            {
                rdr.ParamSet("AntPowerConf", pwrs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置失败：" + ex.ToString());
            }
        }

        private void rbenablerf_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbenablerf.Checked)
            {
                this.tbSession0dur.Enabled = true;
                btnsetrssif.Enabled = true;
                
            }
        }

        private void rbdisablerf_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbdisablerf.Checked)
            {
                this.tbSession0dur.Enabled = false;
            }
        }

        private void btnsetrssif_Click(object sender, EventArgs e)
        {
            if (this.rbenablerf.Checked)
            {
                if (this.tbSession0dur.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入过滤时间");
                    return;
                }
                rdr.ParamSet("IsRssiFilter", true);
                rdr.ParamSet("Session0FTime", int.Parse(this.tbSession0dur.Text.Trim()));
            }
            else
                rdr.ParamSet("IsRssiFilter", false);

        }

    }
}
