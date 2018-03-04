using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech.Gen2;
using ModuleLibrary;
using ModuleTech;

namespace ModuleReaderManager
{
    public partial class MulperlockFrm : Form
    {
        Reader rdr = null;
        ReaderParams rparam = null;

        public MulperlockFrm(Reader rdr_, ReaderParams rparam_)
        {
            rdr = rdr_;
            rparam = rparam_;
            InitializeComponent();
        }

        private void btnlock_Click(object sender, EventArgs e)
        {
            List<Gen2LockAct> ltlact = new List<Gen2LockAct>();
            ltlact.Add(Gen2LockAct.ACCESS_PERMALOCK);
            ltlact.Add(Gen2LockAct.EPC_PERMALOCK);
            ltlact.Add(Gen2LockAct.USER_PERMALOCK);

            try
            {
                rdr.ParamSet("AccessPassword", (uint)0x11112222);
                rdr.ParamSet("TagopAntenna", 1);
                rdr.LockTag(null, new Gen2LockAction(ltlact.ToArray()));
                MessageBox.Show("锁定成功");
            }
            catch
            {
                MessageBox.Show("锁定失败");
            }
            finally
            {
                rdr.ParamSet("AccessPassword", (uint)0);
            }

        }
    }
}
