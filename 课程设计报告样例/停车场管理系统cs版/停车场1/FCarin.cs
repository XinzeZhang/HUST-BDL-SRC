using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarManager
{
    public partial class FCarin : Form
    {
        DataAccess DataAccess1 = new DataAccess();
        Functions functions1 = new Functions();
        Draw Draw1 = new Draw();
        string tempportno;

        public FCarin()
        {
            InitializeComponent();
            initial();
        }

        private void B_in_Click(object sender, EventArgs e)
        {            
            string PortNo1;
            string CarNo1 = CarNo.Text;
            string CarClass1 = CarClass.Text;
            string InTime1 = Hour.Text +":"+ Minute.Text+":00";

            if (tempportno == "")
            {
                PortNo1 = DataAccess1.recPortNo(CarClass1);
            }
            else
            {
                PortNo1 = tempportno;
            }

            string showstr = "车牌号码：" + CarNo1 + "\n车辆类型：" + CarClass1 + "\n入库时间：" + Hour.Text + "时" + Minute.Text + "分\n停放车位：" + PortNo1;
            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show(showstr, "确定入库", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.OK)
            {
                    DataAccess1.addcar(CarNo1, CarClass1, InTime1, PortNo1);
            }

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

            if (tempportno != "")
            {
                this.Close();
            }       
        }

        private void initial()
        {
            for (int i = 0; i < 10; i++)
            {
                Hour.Items.Add("0" + i);
            }
            for (int i = 10; i < 24; i++)
            {
                Hour.Items.Add(i);
            }
            for (int i = 0; i < 10; i++)
            {
                Minute.Items.Add("0" + i);
            }
            for (int i = 10; i < 60; i++)
            {
                Minute.Items.Add(i);
            }

            CarClass.Items.Add("固定用户");
            CarClass.Items.Add("临时用户");
            tempportno = Draw1.myportno;//获取车位名
            if (tempportno != "")
            {
                this.Text = "车辆入库：第" + tempportno + "号车位";//将该窗体上面显示的名字改为“车辆入库……”

                if (tempportno.Substring(0, 1) == "A")
                {
                    CarClass.SelectedIndex = 0;
                    CarClass.Enabled = false;          
                }
                else 
                {
                    CarClass.SelectedIndex = 1;
                    CarClass.Enabled = false;
                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            CarNo.Text = getCarNo();
        }

        private string getCarNo()
        {
            string text = functions1.RandNum(8);
            return text;       
        }

        private void FCarin_Activated(object sender, EventArgs e)
        {
            CarNo.Text = getCarNo();
            Hour.SelectedIndex = Convert.ToInt32(functions1.getOuttime().Substring(0, 2));
            Minute.SelectedIndex = Convert.ToInt32(functions1.getOuttime().Substring(3, 2));
        }

    }
}