using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 

namespace CarManager
{
    public partial class Flog : Form
    {
        DataAccess DataAccess1 = new DataAccess();
        string sqlstr;
        double InCome = 0;
        DataSet DataSet1 = new DataSet();

        Functions functions1 = new Functions();

        public Flog()
        {
            InitializeComponent();
        }

        private void Flog_Shown(object sender, EventArgs e)
        {
            sqlstr = "select * from Carlog";
            DataSet1 = DataAccess1.getDataset(sqlstr);
            dataGridView1.DataSource = DataSet1.Tables[0];

            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[7].Value.ToString() != "")
                {
                    InCome = InCome + Convert.ToDouble(dataGridView1.Rows[i].Cells[7].Value.ToString().Trim());
                }
            }
            label1.Text ="AllIncome( Form "  + dataGridView1.Rows[0].Cells[0].Value.ToString() + " To " + dataGridView1.Rows[dataGridView1.RowCount - 2].Cells[0].Value.ToString() + " )：";
            label2.Text = InCome.ToString() + "元";
            label2.Location = new Point(label1.Size.Width + 5, 2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            functions1.ToExcel("D:\\CarLog.xls", DataSet1.Tables[0]);
            MessageBox.Show("It's OK!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show("是否清空车辆出入日志？", "请确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.OK)
            {
                DataAccess1.Emptyit("Carlog");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show("是否重置车辆出入日志？", "请确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.OK)
            {
                DataAccess1.Emptyit("Carlog");
                DataAccess1.CarlogInitial();
            }
        }
    }
}