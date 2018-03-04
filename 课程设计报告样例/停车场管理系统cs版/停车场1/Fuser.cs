using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarManager
{
    public partial class Fuser : Form
    {
        DataAccess DataAccess1 = new DataAccess();

        public Fuser()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Uname1 = Uname.Text;//车主信息
            string CarNo1 = CarNo.Text;//车牌号
            string PortNo1 = PortNo.Text;//停车位
            string CarCla1 = CarClass.Text;//车辆类型
            int state;//车主信息插入数据库是否成功，0不成功，1成功
            if (CarCla1 == "临时用户")
            {
                PortNo1 = "空";
            }
            string showstr = "车主：" + Uname1 + "\n车牌号：" + CarNo1 + "\n停车位：" + PortNo1 + "\n车辆类型：" + CarCla1;
            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show(showstr, "确定添加车主", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.OK)
            {
                state = DataAccess1.adduser(Uname1, CarNo1, PortNo1, CarCla1);
                if (state == 1)
                {
                    MessageBox.Show("添加车主成功！");
                    this.Close();

                }

            }
            FormCollection fmCollection = System.Windows.Forms.Application.OpenForms;

            Panel PportA = (Panel)(fmCollection[0].Controls.Find("PportA", true)[0]);
            PportA.Refresh();
        }
    }
}
