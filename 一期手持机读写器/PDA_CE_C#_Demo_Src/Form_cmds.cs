using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PDADemo_CF2._0
{
    public partial class Form_cmds : Form
    {
        public Form_cmds()
        {
            InitializeComponent();
        }
        public string cmds;
        private void button_ok_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            cmds = listBox1.SelectedItem.ToString();
            this.Close();
        }
    }
}