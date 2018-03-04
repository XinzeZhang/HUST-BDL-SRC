using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech.Gen2;
using ModuleTech;
using System.Threading;

namespace ModuleReaderManager
{
    public partial class MultiBankWriteFrm : Form
    {
        class Gen2WriteDataItem
        {
            public MemBank bank;
            public int addr;
            public ushort[] wdata;
        }
        class Writefilter
        {
            public MemBank bank;
            public int addr;
            public int flen;
            public bool isfilter;
        }

        delegate void UpdateUi(string str, bool isstop);
        void UpdateTipLab(string str, bool isstop)
        {
            this.labtip.Text = str;
            if (isstop)
                btnstop_Click(null, null);
        }

        Reader mordr = null;
        List<Gen2WriteDataItem> WriteDataItems = new List<Gen2WriteDataItem>();
        List<int> Invants = new List<int>();
        Writefilter writefilter = new Writefilter();
        bool isrepeatwrite = false;
        Thread thWrtie = null;
        bool IsThreadRun = false;

        void writetagfunc()
        {
            mordr.ParamSet("OpTimeout", (ushort)1000);
            mordr.ParamSet("TagopProtocol", TagProtocol.GEN2);
            EmbededCmdData emd = null;
            mordr.ParamSet("ReadPlan", new SimpleReadPlan(TagProtocol.GEN2, Invants.ToArray(), 30));
            if (writefilter.isfilter)
                emd = new EmbededCmdData(writefilter.bank, (uint)writefilter.addr, (byte)(writefilter.flen * 2));
            mordr.ParamSet("EmbededCmdOfInventory", emd);
            int readdur = 0;
            int genblf = (int)mordr.ParamGet("gen2BLF");
            if (genblf < 640)
                readdur = 60 * Invants.Count;
            else
                readdur = 30 * Invants.Count;
            TagReadData[] tags = null;
            int successcnt = 0;
            int writecnt = 0;

            while(IsThreadRun)
            {
                try
                {
                    tags = mordr.Read(readdur);
                }
                catch (ModuleLibrary.ModuleException ex)
                {
                    IsThreadRun = false;
                    this.BeginInvoke(new UpdateUi(UpdateTipLab), "异常码"+ex.ErrCode.ToString()+"，退出操作", true);
                    return;
                }

                if (tags.Length > 0)
                {
                    Gen2TagFilter tagfilter = null;
                    if (writefilter.isfilter && (tags[0].EbdData != null))
                    {
                        tagfilter = new Gen2TagFilter(writefilter.flen*16, tags[0].EbdData, writefilter.bank, 
                            writefilter.addr*16, false);
                    }
                    mordr.ParamSet("TagopAntenna", tags[0].Antenna);
                    writecnt = 0;

                    foreach (Gen2WriteDataItem item in WriteDataItems)
                    {
                        try
                        {
                            mordr.WriteTagMemWords(tagfilter, item.bank, item.addr, item.wdata);
                            writecnt++;
                        }
                        catch
                        {
                            break;
                        }
                    }

                    if (writecnt == WriteDataItems.Count)
                    {
                        if (!isrepeatwrite)
                        {
                            IsThreadRun = false;
                            this.BeginInvoke(new UpdateUi(UpdateTipLab), "成功写入，退出操作", true);
                            return;
                        }
                        else
                        {
                            successcnt++;
                            this.BeginInvoke(new UpdateUi(UpdateTipLab), ("成功写入" + successcnt.ToString()), false);
                        }
                    }
                }

            }

        }
        public MultiBankWriteFrm(Reader rdr)
        {
            InitializeComponent();
            mordr = rdr;
        }

        private void btnwrite_Click(object sender, EventArgs e)
        {
            if (this.ckisrepeat.Checked)
                isrepeatwrite = true;
            else
                isrepeatwrite = false;

            if (this.ckispwd.Checked)
            {
                if (this.tbaccesspwd.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入访问密码");
                    return;
                }
                uint passwd = uint.Parse(this.tbaccesspwd.Text.Trim(), System.Globalization.NumberStyles.AllowHexSpecifier);
                mordr.ParamSet("AccessPassword", passwd);
            }
            else
                mordr.ParamSet("AccessPassword", (uint)0);

            if (!this.ckisnofilter.Checked)
            {
                if (this.cbbfbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择过滤bank");
                    return;
                }
                if (this.tbfaddr.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入过滤起始地址，以块为单位");
                    return;
                }
                if (this.tbflen.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("请输入过滤长度，以块为单位");
                    return;
                }
                writefilter.isfilter = true;
                writefilter.flen = int.Parse(this.tbflen.Text.Trim());
                writefilter.addr = int.Parse(this.tbfaddr.Text.Trim());
                writefilter.bank = (MemBank)this.cbbfbank.SelectedIndex;
            }
            else
                writefilter.isfilter = false;


            if (!(ckant1.Checked || ckant2.Checked || ckant3.Checked || ckant4.Checked))
            {
                MessageBox.Show("请选择天线 ");
                return;
            }
            else
            {
                Invants.Clear();
                if (ckant1.Checked)
                    Invants.Add(1);
                if (ckant2.Checked)
                    Invants.Add(2);
                if (ckant3.Checked)
                    Invants.Add(3);
                if (ckant4.Checked)
                    Invants.Add(4);
            }

            if (lvwriteitems.Items.Count == 0)
            {
                MessageBox.Show("请添加bank写入数据描述");
                return;
            }
            else
            {
                WriteDataItems.Clear();
                foreach (ListViewItem item in lvwriteitems.Items)
                {
                    Gen2WriteDataItem gitem = new  Gen2WriteDataItem();
                    if (item.SubItems[0].Text == "bank0")
                        gitem.bank = MemBank.RESERVED;
                    else if (item.SubItems[0].Text == "bank1")
                        gitem.bank = MemBank.EPC;
                    else if (item.SubItems[0].Text == "bank3")
                        gitem.bank = MemBank.USER;

                    gitem.addr = int.Parse(item.SubItems[1].Text);
                    gitem.wdata = new ushort[item.SubItems[2].Text.Length / 4];
                    for (int a = 0; a < gitem.wdata.Length; ++a)
                        gitem.wdata[a] = ushort.Parse(item.SubItems[2].Text.Substring(a * 4, 4), System.Globalization.NumberStyles.AllowHexSpecifier);

                    WriteDataItems.Add(gitem);
                }
            }
            this.labtip.Text = "提示";
            IsThreadRun = true;
            thWrtie = new Thread(writetagfunc);
            thWrtie.Start();
            this.btnstop.Enabled = true;
            this.btnwrite.Enabled = false;
        }

        private void MultiBankWriteFrm_Load(object sender, EventArgs e)
        {
            this.btnstop.Enabled = false;
            this.btnwrite.Enabled = true;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (this.cbbwbank.SelectedIndex == -1)
            {
                MessageBox.Show("请选择写入bank");
                return;
            }
            if (this.tbwaddr.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入起始地址，以块为单位");
                return;
            }
            if (this.rtbwdata.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入要写入的数据");
                return;
            }

            ListViewItem lvitem = null;
            if (this.cbbwbank.SelectedIndex == 0)
                lvitem = new ListViewItem("bank0");
            else if (this.cbbwbank.SelectedIndex == 1)
                lvitem = new ListViewItem("bank1");
            else if (this.cbbwbank.SelectedIndex == 2)
                lvitem = new ListViewItem("bank3");

            lvitem.SubItems.Add(this.tbwaddr.Text.Trim());
            lvitem.SubItems.Add(this.rtbwdata.Text.Trim());
            lvwriteitems.Items.Add(lvitem);
        }

        private void btndel_Click(object sender, EventArgs e)
        {
            if (lvwriteitems.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择要移除的项");
                return;
            }
            foreach (ListViewItem item in lvwriteitems.SelectedItems)
            {
                lvwriteitems.Items.Remove(item);
            }
        }

        private void MultiBankWriteFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mordr.ParamSet("TagopProtocol", TagProtocol.GEN2);
            mordr.ParamSet("EmbededCmdOfInventory", null);
            mordr.ParamSet("AccessPassword", (uint)0);
        }

        private void btnstop_Click(object sender, EventArgs e)
        {
            IsThreadRun = false;
            thWrtie.Join();
            this.btnstop.Enabled = false;
            this.btnwrite.Enabled = true;
        }

    }
}
