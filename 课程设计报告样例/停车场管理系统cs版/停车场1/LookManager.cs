using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarManager
{
    public partial class LookManager : Form
    {
        public LookManager()
        {
            InitializeComponent();
        }

        SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings[0].ToString());
        string str;
        DataSet ds = new DataSet();

        private void 权限ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            str = "Power";
            
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.DataSource = GetBData(str);
            comboBox1.ValueMember = "Power";
            comboBox1.DisplayMember = "Power";

        }

        private void 名称ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            str="MName";
            
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.DataSource = GetBData(str );
            comboBox1.ValueMember = "MName";
            comboBox1.DisplayMember = "MName";
           
        }

        //ComboBox Datatable数据源
        private DataTable GetBData(string sel)
        {
            SqlDataAdapter dapt = new SqlDataAdapter("select " + sel + " from Manager group by "+sel +" having count(*) > =1", Conn);
            if (ds.Tables[sel]!=null ) ds.Tables[sel].Clear();
            dapt.Fill(ds, sel );
            return ds.Tables[sel];
        }

        //DATAGridVIEW Datatable数据源
        private DataTable GetGData(string sel,string box)
        {
            SqlDataAdapter dapt = new SqlDataAdapter("select MName,Power from Manager Where "+sel+"='"+box+ "'", Conn);
            if (ds.Tables[sel + "1"] != null) ds.Tables[sel + "1"].Clear();
            dapt.Fill(ds, sel + "1");
            return ds.Tables[sel + "1"];
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
                dataGridView1.DataSource = GetGData(str, comboBox1.Text);
                dataGridView1.AllowUserToAddRows = false;//最后的一个空白行
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dapt = new SqlDataAdapter("select MName,Power from Manager", Conn);
            DataTable dt = new DataTable();
            if (dt != null)dt.Clear();
            dapt.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;//最后的一个空白行
        }

        //将当前显示的数据导入EXCL表格中
        public bool ExportDataGridview(DataGridView gridView)
        {
            if (gridView.Rows.Count == 0)
                return false;
            //建立Excel对象
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            //新建一张excel工作簿
            excel.Application.Workbooks.Add(true);
            //生成字段名称
            for (int i = 0; i < gridView.ColumnCount; i++)
            {
                excel.Cells[1, i + 1] = gridView.Columns[i].HeaderText;
            } //填充数据
            for (int i = 0; i <= gridView.RowCount - 1; i++)// 行
            {
                for (int j = 0; j < gridView.ColumnCount; j++)//列
                {
                    if (gridView[j, i].ValueType == typeof(string))
                    {
                        excel.Cells[i + 2, j + 1] = "'" +
                        gridView[j, i].Value.ToString();
                    }
                    else
                    {
                        excel.Cells[i + 2, j + 1] =
                        gridView[j, i].Value.ToString();
                    }
                }
            }
            excel.Visible = true;
            return true;
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            if (ExportDataGridview(dataGridView1))
            {
                MessageBox.Show("导出成功了！");//如果大于0说明删除成功了  

            }
            else
            {
                MessageBox.Show("没有数据，不能导出！");//否则的话添加失败。
            }
        }
    }
}
