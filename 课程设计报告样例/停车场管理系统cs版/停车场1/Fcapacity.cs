using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarManager
{
    public partial class Fcapacity : Form
    {
        DataAccess DataAccess1 = new DataAccess();
        Draw Draw1 = new Draw();
        public Fcapacity()
        {
            InitializeComponent();
        }

        private void Fcapacity_Shown(object sender, EventArgs e)
        {
            comboBox1.Items.Add("PortA");
            comboBox1.Items.Add("PortB");
           
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int PortNum, PortUsed;
            DataAccess1.getPortused(comboBox1.Text, out PortNum, out PortUsed);
            textBox1.Text = PortNum.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataAccess1.updatestate(comboBox1.Text, Convert.ToInt32(textBox1.Text));

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
}