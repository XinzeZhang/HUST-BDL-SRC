using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;
using System.Diagnostics;
using ModuleTech.Gen2;
using ModuleLibrary;

namespace ModuleReaderManager
{
    public partial class CountTagsFrm : Form
    {
        Reader rdr = null;
        ReaderParams rparam = null;

        public CountTagsFrm(Reader rdr_, ReaderParams rparam_)
        {
            rdr = rdr_;
            rparam = rparam_;
            InitializeComponent();
        }

        Dictionary<int, CheckBox> allants = new Dictionary<int, CheckBox>();

        private void button1_Click(object sender, EventArgs e)
        {
            this.label2.Text = "0";
            try
            {
                rdr.ResetTagCount();
            }
            catch
            {
                MessageBox.Show("重置失败");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<int> selants = new List<int>();
            bool isalert = false;
            foreach (int antindex in allants.Keys)
            {
                if (allants[antindex].Enabled)
                {
                    if (allants[antindex].Checked)
                    {
                        selants.Add(antindex);
                        if (allants[antindex].ForeColor == Color.Red)
                            isalert = true;
                    }
                }
            }

            if (selants.Count != 1)
            {
                MessageBox.Show("必须且只能选择个一个天线");
                return;
            }

            if (isalert)
            {
                DialogResult stat = DialogResult.OK;
                stat = MessageBox.Show("在未检测到天线的端口执行操作，真的要执行吗?", "警告",
                                MessageBoxButtons.OKCancel, MessageBoxIcon.Question,
                                MessageBoxDefaultButton.Button2);
                if (stat != DialogResult.OK)
                    return;
            }

            if (this.tbcountdur.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入操作时长");
                return;
            }

            try
            {
                rdr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, selants.ToArray()));
                int count = rdr.ReadTagCount(int.Parse(this.tbcountdur.Text.Trim()));
                this.label2.Text = count.ToString();

            }
            catch (ModuleException exe)
            {
                MessageBox.Show("清点失败："+exe.ToString());
            }
        }

        private void CountTagsFrm_Load(object sender, EventArgs e)
        {
            this.label2.Text = "0";
            allants.Add(1, cbant1);
            allants.Add(2, cbant2);
            allants.Add(3, cbant3);
            allants.Add(4, cbant4);

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
        }
    }
}
