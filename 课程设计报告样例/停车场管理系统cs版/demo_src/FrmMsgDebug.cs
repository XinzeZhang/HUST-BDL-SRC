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
    public partial class FrmMsgDebug : Form
    {
        public FrmMsgDebug(Form1 mainfrm_)
        {
            mainfrm = mainfrm_;
            InitializeComponent();
        }
        Form1 mainfrm = null;

        void MsgLogHandler(string outmsg)
        {
            mainfrm.serialcommunicationmsg += outmsg;
        }

        private void btnupdmsglog_Click(object sender, EventArgs e)
        {
            this.rtbmsglog.Text = mainfrm.serialcommunicationmsg;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mainfrm.serialcommunicationmsg = "";
            this.rtbmsglog.Text = "";
        }

        private void btngetdebugop_Click(object sender, EventArgs e)
        {
            try
            {
                Reader.SerialCommOutput handler = (Reader.SerialCommOutput)mainfrm.modulerdr.ParamGet("SerialCommMsgDebuger");
                if (handler == null)
                    cbbdebugop.SelectedIndex = 0;
                else
                    cbbdebugop.SelectedIndex = 1;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("获取失败，此款读写器不支持此功能");
            }
        }

        private void btnsetdebugop_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (cbbdebugop.SelectedIndex == 1)
                    mainfrm.modulerdr.ParamSet("SerialCommMsgDebuger", new Reader.SerialCommOutput(MsgLogHandler));
                else if (cbbdebugop.SelectedIndex == 0)
                    mainfrm.modulerdr.ParamSet("SerialCommMsgDebuger", null);
                else
                {
                    MessageBox.Show("请选择调试选项");
                    return;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("设置失败，此款读写器不支持此功能");
            }
        }

    }
}
