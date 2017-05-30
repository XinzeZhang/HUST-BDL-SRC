using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;
using ModuleLibrary;

namespace ModuleReaderManager
{
    public partial class FrmPsam : Form
    {
        public FrmPsam(Reader rdr_)
        {
            InitializeComponent();
            rdr = rdr_;
        }
        Reader rdr = null;
        private void btnexec_Click(object sender, EventArgs e)
        {
            if (this.cbbslot.SelectedIndex == -1)
            {
                MessageBox.Show("请选择卡槽");
                return;
            }
            if (this.tbsend.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入cos指令");
                return;
            }
            byte[] cosresp = null;
            try
            {
                cosresp = rdr.PsamTransceiver(this.cbbslot.SelectedIndex + 1,
                    ByteFormat.FromHex(this.tbsend.Text.Trim()));
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("操作失败," + ex.ToString());
                return;
            }
            this.tbrecv.Text = ByteFormat.ToHex(cosresp);
        }
    }
}
