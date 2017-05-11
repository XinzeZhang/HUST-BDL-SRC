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
using System.Threading;

namespace ModuleReaderManager
{
    public partial class regulatoryFrm : Form
    {
        Reader modrdr = null;
        public regulatoryFrm(Reader rdr)
        {
            modrdr = rdr;
            InitializeComponent();
        }

        private void btnsetopfre_Click(object sender, EventArgs e)
        {
            if (this.tbopfre.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入频点");
                return;
            }
            try
            {
                modrdr.ParamSet("setOperatingFrequency", uint.Parse(this.tbopfre.Text.Trim()));
            }
            catch
            {
                MessageBox.Show("设置失败");
            }
        }

        private void btntransCW_Click(object sender, EventArgs e)
        {
            if (this.btnsetopant.Enabled)
            {
                MessageBox.Show("请先输入天线");
                return;
            }
            try
            {
                modrdr.ParamSet("transmitCWSignal", 1);
                this.btnstopCW.Enabled = true;
                this.btntransCW.Enabled = false;
            }
            catch
            {
                MessageBox.Show("发射失败");
            }
        }

        private void btnstopCW_Click(object sender, EventArgs e)
        {
            try
            {
                modrdr.ParamSet("transmitCWSignal", 0);
                this.btntransCW.Enabled = true;
                this.btnstopCW.Enabled = false;
            }
            catch
            {
                MessageBox.Show("停止失败");
            }
        }

        private void btnPRBSOn_Click(object sender, EventArgs e)
        {
            if (this.btnsetopant.Enabled)
            {
                MessageBox.Show("请先输入天线");
                return;
            }
            if (this.tbdur.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入时长");
                return;
            }
            int dur = int.Parse(this.tbdur.Text.Trim());
            if (dur > 65535)
            {
                MessageBox.Show("时长必须小于65535");
                return;
            }
            ushort usdur = (ushort)dur;
            int aa = Environment.TickCount;
            try
            {
                this.btnPRBSOn.Enabled = false;
                modrdr.ParamSet("turnPRBSOn", usdur);
            }
            catch (Exception exx)
            {
                MessageBox.Show("发射失败"+exx.ToString());
            }
 //           int bb = (dur - (Environment.TickCount - aa));
 //           if (bb > 0)
  //              Thread.Sleep(bb+1500);
            this.btnPRBSOn.Enabled = true;
        }

        private void regulatoryFrm_Load(object sender, EventArgs e)
        {
            this.btnstopCW.Enabled = false;
            this.btntransCW.Enabled = true;
            
        }

        private void btnsetopant_Click(object sender, EventArgs e)
        {
            if (this.tbopant.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入天线");
                return;
            }
            int ant = int.Parse(this.tbopant.Text);
            modrdr.ParamSet("setRegulatoryOpAnt", ant);
            this.tbopant.Enabled = false;
            this.btnsetopant.Enabled = false;
        }


    }
}
