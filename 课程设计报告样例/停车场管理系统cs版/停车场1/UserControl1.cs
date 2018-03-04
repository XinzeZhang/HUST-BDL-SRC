using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;


namespace CarManager
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            progressBar1.Value = Convert.ToInt32(comboBox1.Text);
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                comboBox1.Items.Add(i + 1);
                comboBox2.Items.Add(i + 1);
                comboBox3.Items.Add(i + 1);
            }
            progressBar1.Value = 10;
            progressBar2.Value = 50;
            progressBar3.Value = 90;
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            progressBar2.Value = Convert.ToInt32(comboBox2.Text);
        }

        private void comboBox3_SelectedValueChanged(object sender, EventArgs e)
        {
            progressBar3.Value = Convert.ToInt32(comboBox3.Text);
        }
    }
}
