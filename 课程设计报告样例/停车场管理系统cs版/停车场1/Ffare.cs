using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarManager
{
    public partial class Ffare : Form
    {
        DataAccess DataAccess1 = new DataAccess();
        public Ffare()
        {
            InitializeComponent();
            Initial();
        }
        private void Initial()
        {
            Ccarclass.Items.Add("固定用户");
            Ccarclass.Items.Add("临时用户");
            
            Ct12.Enabled = false;
            Ct21.Enabled = false;
            Ct22.Enabled = false;
            Ct31.Enabled = false;
            Ct32.Enabled = false;
            for (int i = 0; i < 24; i++)
            {
                Ct11.Items.Add(i);
            }
        }

        private void Ct11_SelectedValueChanged(object sender, EventArgs e)
        {
            Ct12.Enabled = true;
            Ct32.Text = Ct11.Text;
            Ct12.Items.Clear();
            for (int i = Convert.ToInt32(Ct11.Text) + 1; i < 24 + Convert.ToInt32(Ct11.Text); i++)
            {
                int item = i % 24;
                Ct12.Items.Add(item);
            }
        }

        private void Ct12_SelectedValueChanged(object sender, EventArgs e)
        {
            Ct21.Text = Ct12.Text;
            Ct22.Enabled = true;
            Ct22.Items.Clear();

            if (Convert.ToInt32(Ct11.Text) < Convert.ToInt32(Ct12.Text))
            {
                for (int i = Convert.ToInt32(Ct12.Text) + 1; i < 24 + Convert.ToInt32(Ct11.Text); i++)
                {
                    int item = i % 24;
                    Ct22.Items.Add(item);
                }
            }
            else 
            {
                for (int i = Convert.ToInt32(Ct12.Text) + 1; i < Convert.ToInt32(Ct11.Text); i++)
                {
                    Ct22.Items.Add(i);
                }
            }

        }

        private void Ct22_SelectedValueChanged(object sender, EventArgs e)
        {
            Ct31.Text = Ct22.Text;
        }

        private void Ccarclass_SelectedValueChanged(object sender, EventArgs e)
        {
            int time1, time2, time3;
            double  rate1, rate2, rate3;
            DataAccess1.getRate(Ccarclass.Text, out time1, out time2, out time3, out rate1,out rate2,out rate3);
            Ct11.Text = time1.ToString();
            Ct12.Text = time2.ToString();
            Ct21.Text = time2.ToString();
            Ct22.Text = time3.ToString();
            Ct31.Text = time3.ToString();
            Ct32.Text = time1.ToString();
            Tfare1.Text = rate1.ToString();
            Tfare2.Text = rate2.ToString();
            Tfare3.Text = rate3.ToString();
        }

        private void Bsubmit_Click(object sender, EventArgs e)
        {
            int time1, time2, time3;
            string rate1, rate2, rate3;
            time1 = Convert.ToInt32(Ct11.Text);
            time2 = Convert.ToInt32(Ct21.Text);
            time3 = Convert.ToInt32(Ct31.Text);
            rate1 = Tfare1.Text;
            rate2 = Tfare2.Text;
            rate3 = Tfare3.Text;
            DataAccess1.updaterage(Ccarclass.Text, time1, time2, time3, rate1, rate2, rate3);
        }

        private void Breset_Click(object sender, EventArgs e)
        {
            Ccarclass.ResetText();
            Ct11.ResetText();
            Ct12.ResetText();
            Ct21.ResetText();
            Ct22.ResetText();
            Ct31.ResetText();
            Ct32.ResetText();
            Tfare1.ResetText();
            Tfare2.ResetText();
            Tfare3.ResetText();
        }


    }
}