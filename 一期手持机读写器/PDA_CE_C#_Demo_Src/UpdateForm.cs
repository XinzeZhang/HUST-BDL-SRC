using System;
//using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;
using System.Threading;
namespace PDADemo_CF2._0
{
    public partial class UpdateForm : Form
    {
        public UpdateForm(string cm)
        {
            comv = cm;
            InitializeComponent();
        }
        string comv;
        delegate void updateprog(float prg);
        delegate void EnableQuit();
        void EnableQuitbtn()
        {
            Viewbutton.Enabled = true;
            Updatebutton.Enabled = true;
            Leavebutton.Enabled = true;
        }

        void EnableQuitbtnforsuc()
        {
            Viewbutton.Enabled = true;
            Updatebutton.Enabled = true;
            Leavebutton.Enabled = true;
          
        }

        void updateprogbar(float prg)
        {
            this.Invoke(new updateprog(updateui), prg);
        }

        void updateui(float prog)
        {
            int curstep = (int)(prog * 100);
            this.UpdateprogressBar.Value = curstep;
        }

        Reader rd = null;

        Thread updatehread;

        int readertype = -1;

        bool isBootFirmware = false;

        string fwpath;
        void updatefirmware()
        {
            
            try
            {
                rd.FirmwareLoad(this.fwpath.Trim(), new Reader.FirmwareUpdate(updateprogbar));
                isBootFirmware = true;
                MessageBox.Show("升级成功");
                this.Invoke(new EnableQuit(EnableQuitbtnforsuc));

            }
            catch (Exception eex)
            {
                isBootFirmware = false;
                MessageBox.Show("升级失败: " + eex.ToString());
                this.Invoke(new EnableQuit(EnableQuitbtn));
                return;
            }


        }
        private void Viewbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            if (this.UpdatetypecomboBox.SelectedIndex == 1 || this.UpdatetypecomboBox.SelectedIndex == 2)
                of.Filter = "maf|*.maf";
            else if (this.UpdatetypecomboBox.SelectedIndex == 4)
            {
            }
            else if (this.UpdatetypecomboBox.SelectedIndex == 5)
            {
                of.Filter = "bin|*.bin";
            }
            else
                of.Filter = "sim|*.sim";

            if (of.ShowDialog() == DialogResult.OK)
            {
                this.filetextBox.Text = of.FileName;
            }

        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            if (this.UpdatetypecomboBox.SelectedIndex == -1)
            {
                MessageBox.Show("请输入读写器类型");
                return;
            }
            if (this.filetextBox.Text.Trim() == "")
            {
                MessageBox.Show("请输入升级文件路径");
                return;
            }

            

            if (this.UpdatetypecomboBox.SelectedIndex == 1)
            {
              
                if (this.addrtextBox.Text.Trim().ToLower().IndexOf("com")>0)
                {
                    MessageBox.Show("此系列读写器只能通过网口升级");
                    return;
                }
            }

            fwpath = this.filetextBox.Text.Trim();
            char[] dep = new char[1];
            dep[0] = '\\';
            string[] strs = fwpath.Split(dep);
            string fwfilename = strs[strs.Length - 1];

            if (this.UpdatetypecomboBox.SelectedIndex == 1)
            {
                if (!fwfilename.StartsWith("m5e"))
                {
                    MessageBox.Show("升级文件名错误");
                    return;
                }
            }
            if (this.UpdatetypecomboBox.SelectedIndex == 2)
            {
                if (!fwfilename.StartsWith("m6e"))
                {
                    MessageBox.Show("升级文件名错误");
                    return;
                }
            }

            if (rd == null)
            {
                try
                {
                    rd = Reader.Create(this.addrtextBox.Text.Trim(), this.UpdatetypecomboBox.SelectedIndex);
                    readertype = this.UpdatetypecomboBox.SelectedIndex;

                }
                catch (Exception exx)
                {
                    MessageBox.Show("连接读写器失败：" + exx.ToString());
                    this.Invoke(new EnableQuit(EnableQuitbtn));
                    return;
                }
            }


            if (updatehread != null)
            {
                updatehread.Join();

            }

            updatehread = new Thread(updatefirmware);
            updatehread.Start();

            Viewbutton.Enabled = false;
            Updatebutton.Enabled = false;
            Leavebutton.Enabled = false;
        }

        private void Leavebutton_Click(object sender, EventArgs e)
        {
            if (rd != null)
            {
                if (isBootFirmware && (readertype == 1 || readertype == 2))
                    rd.ParamSet("BootFirmware", true);
            }
            this.Close();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {
            addrtextBox.Text = comv;
        }
    }
}