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
    public partial class EditManager : Form
    {
        public EditManager()
        {
            InitializeComponent();
        }

        SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings[0].ToString());
        string str;
        DataSet ds = new DataSet();

      
        //ComboBox Datatable数据源
        private DataTable GetBData(string sel)
        {
            SqlDataAdapter dapt = new SqlDataAdapter("select " + sel + " from Manager group by " + sel + " having count(*) > =1", Conn);
            if (ds.Tables[sel ] != null) ds.Tables[sel].Clear();
            dapt.Fill(ds, sel );
            return ds.Tables[sel];
            
        }

        //DATAGridVIEW Datatable数据源
        private DataTable GetGData(string sel, string box)
        {
            SqlDataAdapter dapt = new SqlDataAdapter("select * from Manager Where " + sel + "='" + box + "'", Conn);
            if (ds.Tables[sel + "1"] != null) ds.Tables[sel + "1"].Clear();
            dapt.Fill(ds, sel + "1");
            return ds.Tables[sel + "1"];

        }

        //全选按钮
        private void button3_Click(object sender, EventArgs e)
        {
            SqlDataAdapter dapt = new SqlDataAdapter("select * from Manager", Conn);
            DataTable dt = new DataTable();
            if (dt != null) dt.Clear();
            dapt.Fill(dt);
            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;//最后的一个空白行
        }
        
        //comboBox的值改变后，dataGridView1更新
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetGData(str, comboBox1.Text);
            dataGridView1.AllowUserToAddRows = false;//最后的一个空白行
            
        }


 

        //单元格被编辑后
        int index;//被更改的单元格索引
        string a ;//记录每列单元格值
        string b ;
        string c;

     
        //更改，并更新到数据库
        private void button1_Click(object sender, EventArgs e)
        {
            
                if (index != -1 && !dataGridView1.Rows[index].IsNewRow)
               {
                   Conn.Open();//打开数据库连接
                   SqlCommand cmd = new SqlCommand("update  Manager set Password='" + b + "',Power='" + c + "'Where MName='" + a + "'", Conn);
                   int upd = cmd.ExecuteNonQuery();//使用执行对象cmd的方法ExecuteNonQuery()返回受影响行数。
                   if (upd > 0)
                   {
                       MessageBox.Show("修改成功！");//如果大于0说明添加成功了  

                   }
                   else
                   {
                       MessageBox.Show("数据出错，请联系管理员！");//否则的话添加失败。
                   }
                   Conn.Close();//关闭数据库连接
               }
           
        }
        //获取当前单元格更改后的值
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            index = e.RowIndex;
            if (index != -1 && !dataGridView1.Rows[index].IsNewRow)
            {
                a = this.dataGridView1.Rows[index].Cells[0].Value.ToString();
                b = this.dataGridView1.Rows[index].Cells[1].Value.ToString();
                c = this.dataGridView1.Rows[index].Cells[2].Value.ToString();
            }
        }



        //删除行的操作   

 
        //某行被选中
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            index = e.RowIndex;
            if (index != -1 && !dataGridView1.Rows[index].IsNewRow)
            {
                a = this.dataGridView1.Rows[index].Cells[0].Value.ToString();
            }
            
        }

        //删除按钮
        private void button2_Click(object sender, EventArgs e)
        {
            if (index != -1 && !dataGridView1.Rows[index].IsNewRow)
            {
                Conn.Open();//打开数据库连接
                SqlCommand cmd = new SqlCommand("delete from  Manager Where MName='" + a + "'", Conn);
                int del = cmd.ExecuteNonQuery();//使用执行对象cmd的方法ExecuteNonQuery()返回受影响行数。
                if (del > 0)
                {
                    MessageBox.Show("删除成功！");//如果大于0说明添加成功了  

                }
                else
                {
                    MessageBox.Show("删除出错，请联系管理员！");//否则的话添加失败。
                }
                Conn.Close();//关闭数据库连接
            }
        }


        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox1.Text=="名称")
            {
                str = "MName";
            }
            else if (toolStripTextBox1.Text == "权限")
            {
                str = "Power";
            }
            comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox1.DataSource = GetBData(str);
            comboBox1.ValueMember = str ;
            comboBox1.DisplayMember = str ;

        }
    }
}
