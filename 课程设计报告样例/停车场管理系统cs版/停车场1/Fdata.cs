using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace CarManager
{
    public partial class Fdata : Form
    {
        DataAccess DataAccess1 = new DataAccess();
        Functions functions1 = new Functions();
        Draw Draw1 = new Draw();
        string sqlstr;
        string earlier = "";
        string RegCarNo = "[\u4e00-\u9fa5][A-Z]-[0-9]{5}";
        string RegInTime = "([0-1][0-9]|2[0-3]):([0-5][0-9]):([0-5][0-9])";

        DataSet DataSet1 = new DataSet();
        ComboBox cmbportno = new ComboBox();

        public Fdata()
        {
            InitializeComponent();
        }

        private void Fdata_Shown(object sender, EventArgs e)
        {
            sqlstr = "select CarNo,CarCla,InTime,PortNo from CarIn";
            DataSet1 = DataAccess1.getDataset(sqlstr);
            dataGridView1.DataSource = DataSet1.Tables[0];    
 
            dataGridView1.Columns[0].HeaderText = "车牌号码";
            dataGridView1.Columns[1].HeaderText = "车辆类型";
            dataGridView1.Columns[2].HeaderText = "进库时间";
            dataGridView1.Columns[3].HeaderText = "停车车位";

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 80;
            dataGridView1.Columns[2].Width = 80;
            dataGridView1.Columns[3].Width = 80;
            dataGridView1.Width = 380;

            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;


            dataGridView1.Columns[1].ReadOnly = true;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;

            cmbportno.Visible = false;
            cmbportno.SelectedIndexChanged += new EventHandler(cmbportno_SelectedIndexChanged);
            dataGridView1.CurrentCellChanged += new EventHandler(dataGridView1_CurrentCellChanged);
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            dataGridView1.CellBeginEdit += new DataGridViewCellCancelEventHandler(dataGridView1_CellBeginEdit);
            dataGridView1.Scroll += new ScrollEventHandler(dataGridView1_Scroll);


            this.dataGridView1.Controls.Add(cmbportno);
        }

        void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 0 || dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                earlier = dataGridView1.CurrentCell.Value.ToString().Trim();
            }

        }

        void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string ToMath = dataGridView1.CurrentCell.Value.ToString().Trim();
            if (dataGridView1.CurrentCell.ColumnIndex == 0)
            {
                if (!Regex.IsMatch(ToMath, RegCarNo) || ToMath.Length != 8)
                {
                    MessageBox.Show("Invalid input!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = earlier;
                }
            }
            else if (dataGridView1.CurrentCell.ColumnIndex == 2)
            {
                if (!Regex.IsMatch(ToMath, RegInTime) || ToMath.Length != 8)
                {
                    MessageBox.Show("Invalid input!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    dataGridView1.ClearSelection();
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = earlier;
                }
            }

        }


        void cmbportno_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = ((ComboBox)sender).SelectedItem.ToString();
            if (dataGridView1.CurrentCell.Value.ToString().Substring(0, 1) == "A")
            {
                dataGridView1.CurrentRow.Cells[1].Value = "固定用户";
            }
            else if (dataGridView1.CurrentCell.Value.ToString().Substring(0, 1) == "B")
            {
                dataGridView1.CurrentRow.Cells[1].Value = "临时用户";
            }
           
        }

        void dataGridView1_Scroll(object sender, ScrollEventArgs e)
        {
            //cmbcarcla.Visible = false;
            cmbportno.Visible = false;
        }

        void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            Rectangle rect = dataGridView1.GetCellDisplayRectangle(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, false);

            if(dataGridView1.CurrentCell.ColumnIndex == 3)
            {
                string carportno = dataGridView1.CurrentCell.Value.ToString();
                BindPortno(carportno);

                cmbportno.Text = carportno;
                cmbportno.Left = rect.Left;
                cmbportno.Top = rect.Top;
                cmbportno.Width = rect.Width;
                cmbportno.Height = rect.Height;
                cmbportno.Visible = true;
            }
            else
            {
                cmbportno.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show("确定更新数据？", "请确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.OK)
            {
                DataAccess1.updatedata(DataSet1, sqlstr);
                FormCollection fmCollection = System.Windows.Forms.Application.OpenForms;

                Panel PportA = (Panel)(fmCollection[0].Controls.Find("PportA", true)[0]);
                Panel PportB = (Panel)(fmCollection[0].Controls.Find("PportB", true)[0]);
               
                Panel Pdrawstate = (Panel)(fmCollection[0].Controls.Find("Pdrawstate", true)[0]);
                Panel panel1 = (Panel)(fmCollection[0].Controls.Find("panel1", true)[0]);
                PportA.Refresh();
                PportB.Refresh();
               
                Pdrawstate.Refresh();
                panel1.Refresh();
                PportA.Controls.Clear();
                PportB.Controls.Clear();
               
                Draw1.drawport(PportA, "PortA");
                Draw1.drawport(PportB, "PortB");
               
                Draw1.drawstate(Pdrawstate);
                Draw1.drawpic(panel1);
            }

        }

        private void BindPortno(string portno)
        {
            cmbportno.Items.Clear();
            cmbportno.DropDownStyle = ComboBoxStyle.DropDownList;

            if (portno == "")
            {
                int portnum1 = DataAccess1.getPortnum("PortA");
                for (int i = 1; i < portnum1 + 1; i++)
                {
                    if (!isExist("A" + i.ToString()))
                    {
                        cmbportno.Items.Add("A" + i.ToString());
                    }
                }
                int portnum2 = DataAccess1.getPortnum("PortB");
                for (int i = 1; i < portnum2 + 1; i++)
                {
                    if (!isExist("B" + i.ToString()))
                    {
                        cmbportno.Items.Add("B" + i.ToString());
                    }
                }
                int portnum3 = DataAccess1.getPortnum("PortC");
                for (int i = 1; i < portnum3 + 1; i++)
                {
                    if (!isExist("C" + i.ToString()))
                    {
                        cmbportno.Items.Add("C" + i.ToString());
                    }
                }
            }
            else
            { 
                cmbportno.Items.Add(portno);
                if (portno.Substring(0, 1) == "A")
                {
                    int portnum = DataAccess1.getPortnum("PortA");
                    for (int i = 1; i < portnum + 1; i++)
                    {
                        if (!isExist("A" + i.ToString()))
                        {
                            cmbportno.Items.Add("A" + i.ToString());
                        }
                    }
                }
                else if (portno.Substring(0, 1) == "B")
                {
                    int portnum = DataAccess1.getPortnum("PortB");
                    for (int i = 1; i < portnum + 1; i++)
                    {
                        if (!isExist("B" + i.ToString()))
                        {
                            cmbportno.Items.Add("B" + i.ToString());
                        }
                    }
                }
                else
                {
                    int portnum = DataAccess1.getPortnum("PortC");
                    for (int i = 1; i < portnum + 1; i++)
                    {
                        if (!isExist("C" + i.ToString()))
                        {
                            cmbportno.Items.Add("C" + i.ToString());
                        }
                    }          
                }                 
            }



        }

        private bool isExist(string portno)
        {
            bool exist = false;
            for (int i = 0; i < DataSet1.Tables[0].Rows.Count; i++)
            {
                if (DataSet1.Tables[0].Rows[i][3].ToString().Trim() == portno)
                {
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //DataSet1.Reset();
            //DataSet1 = DataAccess1.getDataset(sqlstr);
            cmbportno.Visible = false;
            dataGridView1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            functions1.ToExcel("D:\\CarIn.xls", DataSet1.Tables[0]);
            MessageBox.Show("It's OK!");
        }

    }
}