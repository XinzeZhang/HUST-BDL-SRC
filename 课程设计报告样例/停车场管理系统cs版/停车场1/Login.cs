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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
           
        }

        public string mname;
        public string mpass;
        public string mcla;
        public bool openport=false ;//是否打开系统
       
        SqlConnection Conn = new SqlConnection(ConfigurationSettings.AppSettings[0].ToString());
        string str;


        private void button1_Click(object sender, EventArgs e)
        {
            mname = textBox1.Text;
            mpass = textBox2.Text;
            Conn.Open();
            str = "select Power from Manager where MName='" + mname  + "'and Password='"+ mpass +"'";
            SqlCommand command = new SqlCommand(str, Conn);
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.Read())
            {
                reader.Close();
                MessageBox.Show("该管理员不存在！");
                openport = false;
            }
            else
            {
                mcla = reader.GetString(0);
                openport = true;
                reader.Close();
                this.Close();
            }
            Conn.Close();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            openport = false;
        }

     

 



    }
}
