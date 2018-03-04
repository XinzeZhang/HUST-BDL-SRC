using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarManager
{
    public partial class Fuserall : Form
    {
        DataAccess DataAccess1 = new DataAccess();
        string sqlstr;
        DataSet DataSet1 = new DataSet();

        public Fuserall()
        {
            InitializeComponent();
            Show();
        }

        private void Show()
        {
            sqlstr = "select * from Users";
            DataSet1 = DataAccess1.getDataset(sqlstr);
            dataGridView1.DataSource = DataSet1.Tables[0];
        }
    }
}
