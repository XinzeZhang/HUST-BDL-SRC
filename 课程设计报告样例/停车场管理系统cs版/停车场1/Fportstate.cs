using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarManager
{
    public partial class Fportstate : Form
    {
       
        
        Draw Draw1 = new Draw();
        DataAccess DataAccess1 = new DataAccess();
        Functions functions1 = new Functions();

        public Fportstate()
        {
             InitializeComponent();
             
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Fabout = new Fabout();
            Fabout.ShowDialog();
        }

        private void 车辆入库ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Draw1.myportno = "";
            Form FCarin = new FCarin();
            FCarin.Show();
        }

        private void 费率管理ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form Ffare = new Ffare();
            Ffare.ShowDialog();
        }

        private void 容量管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Fcapacity = new Fcapacity();
            Fcapacity.ShowDialog();
        }

        private void 帮助ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form Fhelp = new Fhelp();
            Fhelp.ShowDialog();
        }

        private void Fportstate_Shown(object sender, EventArgs e)
        {
            PportA.Controls.Clear();
            PportB.Controls.Clear();
           

            PportA.Refresh();
            PportB.Refresh();
            
            Pdrawdetail.Refresh();
            Pdrawstate.Refresh();
            panel1.Refresh();

            Draw1.drawport(PportA, "PortA");
            Draw1.drawport(PportB, "PortB");
            Draw1.drawdetail(Pdrawdetail, "axis");
            Draw1.drawstate(Pdrawstate);
            Draw1.drawpic(panel1);

            PportA.MouseDoubleClick += new MouseEventHandler(Draw1.addcar);
            PportB.MouseDoubleClick += new MouseEventHandler(Draw1.addcar);


            PportA.Paint += new PaintEventHandler(Now_Paint);
            PportB.Paint += new PaintEventHandler(Now_Paint);
            Pdrawdetail.Paint += new PaintEventHandler(Now_Paint);
            Pdrawstate.Paint += new PaintEventHandler(Now_Paint);
            panel1.Paint += new PaintEventHandler(Now_Paint);
        }

        void Now_Paint(object sender, PaintEventArgs e)
        {
            if (((Panel)sender).Name == "PportA")
            {
                Draw1.drawport((Panel)sender, "PortA");
            }
            else if (((Panel)sender).Name == "PportB")
            {
                Draw1.drawport((Panel)sender, "PortB");
            }
           
            else if (((Panel)sender).Name == "Pdrawdetail")
            {
                int arrayno = DataAccess1.getDataNum("CarIn");
                string[] Cararray = new string[arrayno];
                Cararray = DataAccess1.getCarNo();

                Panel[] panelarray = new Panel[arrayno];

                for (int i = 0; i < arrayno; i++)
                {
                    panelarray[i] = (Panel)(panel1.FindForm().Controls.Find(Cararray[i], true)[0]);

                    if (panelarray[i].BackColor == Color.Red)
                    {
                        Draw1.drawdetail(Pdrawdetail, panelarray[i].Name); 
                    }
                }                          
            }
            else if (((Panel)sender).Name == "Pdrawstate")
            {
                Draw1.drawstate(Pdrawstate);
            }
            else
            {
              Draw1.drawpic(panel1);        
            }
        }

        private void 车辆管理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Fdata = new Fdata();
            Fdata.Show();
        }

        private void 出入日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Flog = new Flog();
            Flog.Show();
        }

        private void 重置ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show("是否重置应用程序？", "请确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.OK)
            {
                functions1.ProgramReset();
            }


        }

        private void 清零ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult MsgBoxResult;
            MsgBoxResult = MessageBox.Show("是否清零全部数据？", "请确定", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (MsgBoxResult == DialogResult.OK)
            {
                functions1.ProgramDataClear();
            }

        }




        private void 退出button1_Click(object sender, EventArgs e)
        {
            label7.Text = "离线";

            Login newlogin = new Login();
            newlogin.ShowDialog();
            if (newlogin.openport)
            {
                newlogin.Close();

                this.label6.Text = newlogin.mcla;
                this.label7.Text = "在线";
               
            }
          
        }

        private void 管理员添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
            string str = label6.Text;
            if (str.Substring(0,2).Equals("超级"))
            {
                AddManager addm = new AddManager();
                addm.ShowDialog();
            }
            else if (str.Substring(0, 2).Equals("普通"))
            {
                MessageBox.Show("没有权限！");
            }
        }

        private void 修改信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string str = label6.Text;
            if (str.Substring(0, 2).Equals("超级"))
            {
                EditManager Editm = new EditManager();
                Editm.ShowDialog();
            }
            else if (str.Substring(0, 2).Equals("普通"))
            {
                MessageBox.Show("没有权限！");
            }
        }

        private void 管理员查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LookManager lookm = new LookManager();
            lookm.ShowDialog();
        }

        private void 车主信息添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Fuser = new Fuser();
            Fuser.Show();
        }

        private void 车主信息查询ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form Fuserall1 = new Fuserall();
            Fuserall1.Show();
        }

       

    }
}