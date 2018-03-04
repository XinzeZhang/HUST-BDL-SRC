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
    public partial class AddManager : Form
    {
        public AddManager()
        {
            InitializeComponent();
        }

        private string mcla;
        private string mname;
        private string mpass;

        SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings[0].ToString());
        string strin;
        string strselect;

        private void button1_Click(object sender, EventArgs e)
        {
            mcla = comboBox1.Text;
            mname = textBox1.Text;
            mpass = textBox2.Text;

            Conn.Open();
            strselect = "select MName from Manager where MName='" + mname + "'";
            strin = "insert into Manager(MName,Password,Power) values('" + mname + "' ,'" + mpass + "','" + mcla + "')";
            SqlCommand commands = new SqlCommand(strselect, Conn);
            SqlCommand commandin = new SqlCommand(strin, Conn);
            SqlDataReader reader = commands.ExecuteReader();
            int intrue =0;
            if (!reader.Read())
            {
                reader.Close();
                if (mpass == textBox3.Text)
                {
                  
                    intrue = commandin.ExecuteNonQuery();//使用执行对象的方法ExecuteNonQuery()返回受影响行数。
                    if (intrue > 0)
                    {
                        MessageBox.Show("添加成功了！");//如果大于0说明添加成功了  
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("数据出错，请联系管理员！");//否则的话添加失败。
                    }
                }
                else
                {
                    MessageBox.Show("密码不一致，请重新输入！");
                }
               
            }
            else
            {
                MessageBox.Show("已存在该管理员名称，请重新输入！");
                reader.Close();
            }
            Conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
