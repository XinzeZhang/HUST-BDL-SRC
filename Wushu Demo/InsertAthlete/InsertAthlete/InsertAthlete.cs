using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using SQL Connection
using System.Data.SqlClient;
using System.Data.Sql;
//使用rfid api
using DesktopRfidApi;

namespace InsertAthlete
{
    public partial class InsertAthlete : Form
    {
        //实例化panel
        List<Panel> ListPanel = new List<Panel>();
        int PanelIndex=0;

        public InsertAthlete()
        {
            InitializeComponent();
        }

        // initiallize the rfid api
        DesktopRfidApi.RfidApi Api = new DesktopRfidApi.RfidApi();

        //initiallize the rfid tagcount
        public byte TagReading = 0;
        public int Tagcount = 0;


        /// <summary>
        /// CheckIn_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckIn_Load(object sender , EventArgs e)
        {
            //panel operation
            ListPanel.Add(panel_InsertAthlete);
            ListPanel.Add(panel_RFIDConfigure);
            ListPanel[PanelIndex].BringToFront();
            
            try
            {

                //RFID设备 端口号初始化设置，设置为COM3
                commport_comboBox.SelectedIndex = 2;
                //RFID设备 波段初始化设置，设置为9600
                boudrate_comboBox.SelectedIndex = 0;
                //RFID设备 频率范围: 0 - 2;
                //0 for 中国China(920 m ~920 m); 1 for 美国American(902 m ~902 m)
                //其他the other for 特殊special models (868 m)
                //RFID设备 频率初始化设置，设置为美国, frequency default as 1;
                freqtype_comboBox.SelectedIndex = 1;

                connect_button.Enabled = true;//配置初始化，连接按钮启用
                disconnect_button.Enabled = false;//配置初始化，断开连接按钮禁用

                pf_groupBox.Enabled = false;//配置初始化，功率与频率groupbox禁用
                ant_groupBox.Enabled = false;//配置初始化，天线设置禁用
                tagidentify_groupBox.Enabled = false;//配置初始化，标签读写区域禁用

                dataregion_comboBox.SelectedIndex = 1;//配置初始化，标签读写区初始化为EPC区域

                //initialize data address comboBox//配置初始化，标签起始地址为2
                int nloop = 0;
                for(nloop = 0 ; nloop < 256 ; nloop++)
                {
                    dataaddress_comboBox.Items.Add(Convert.ToString(nloop));
                }
                dataaddress_comboBox.SelectedIndex = 2;

                //initiallize data length comboBox//配置初始化，标签读写长度为1, 4位16进制数字
                for(nloop = 1 ; nloop < 14 ; nloop++)
                {
                    datalength_comboBox.Items.Add(Convert.ToString(nloop));
                }
                datalength_comboBox.SelectedIndex = 0;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            //初始化playingfield_comboBox，并默认为场地1
            GetFieldId_To_playingfield_comboBox();
            playingfield_comboBox.SelectedIndex = 0;



            //初始化dataGridView_ProjectInfo
            GetCompetitionProject_To_dataGridView_ProjectInfo(playingfield_comboBox.Text);
            project_textBox.Text = dataGridView_projectinfo.Rows[0].Cells[1].Value.ToString().Trim();
            

        }

        /// <summary>
        /// 显示运动员检录界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AthletesCheckIn_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            if(PanelIndex != 0)
            {
                PanelIndex = 0;
                ListPanel[0].Visible = true;
                ListPanel[0].BringToFront();
                ListPanel[1].Visible = false;
            }
                
        }

        /// <summary>
        /// 显示读写器配置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RFIDConfigure_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            if(PanelIndex != 1)
            {
                PanelIndex = 1;
                ListPanel[1].Visible = true;
                ListPanel[1].BringToFront();
                ListPanel[0].Visible=false;
            }
                
        }

        /// <summary>
        /// 打开读写器端口，调用读写器固件版本，默认设置波段、频率、功率、天线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connect_button_Click(object sender , EventArgs e)
        {
            int status;
            byte v1 = 0;
            byte v2 = 0;
            

            //open comm port//打开读写器端口
            status = Api.OpenCommPort(commport_comboBox.SelectedItem.ToString());
            if(status != 0)
            {
                MessageBox.Show("获取设备端口失败，请检查设备端口是否正常!");
                return;
            }

            //get firmware version//调用读写器固件版本
            status = Api.GetFirmwareVersion(ref v1 , ref v2);
            if(status != 0)
            {
                MessageBox.Show("无法获取设备固件信息!");
                Api.CloseCommPort();
                return;
            }
            MessageBox.Show("设备连接成功!");

            connect_button.Enabled = false;//连接成功，连接按钮禁用
            disconnect_button.Enabled = true;//连接成功，断开连接按钮启用

            pf_groupBox.Enabled = true;//连接成功，功率与频率groupbox启用
            ant_groupBox.Enabled = true;//连接成功，天线设置区域启用
            tagidentify_groupBox.Enabled = true;//连接成功，标签读写区域启用

            pfconfig_button_Click(null , null);//连接成功，功率和频率进行默认设置
            antconfig_button_Click(null , null);//连接成功，天线进行默认设置
        }

        private void disconnect_button_Click(object sender , EventArgs e)
        {
            Api.CloseCommPort();//关闭读写器端口
            connect_button.Enabled = true;
            disconnect_button.Enabled = false;

            pf_groupBox.Enabled = false;
            ant_groupBox.Enabled = false;
            tagidentify_groupBox.Enabled = false;
        }

        private void pfconfig_button_Click(object sender , EventArgs e)
        {
            //power range: 0-30//功率范围：0-30
            //power default as 15//功率初始化为15
            byte power = 15;
            //frequency range: 0-2; 0 for China (920 m ~ 920 m), 1 for American (902 m ~ 902 m), the other for special models (868 m)
            //frequency default as 1; 初始化设置，设置为美国
            byte frequency = 1;

            int status;

            status = Api.GetRf(ref power , ref frequency);
            if(status != 0)
            {
                MessageBox.Show("获取功率与使用地区失败！");
                return;
            }
            power_trackBar.Value = power;
            freqtype_comboBox.SelectedIndex = frequency;
            //MessageBox.Show("Get Power & Freq settings success!");
        }

        private void pfset_button_Click(object sender , EventArgs e)
        {
            //power range: 0-30
            //power default as 15
            byte power = 15;
            //frequency range: 0-2; 0 for China (920 m ~ 920 m), 1 for American (902 m ~ 902 m), the other for special models (868 m)
            //frequency default as 1;
            byte frequency = 1;

            int status;

            power = (byte) (power_trackBar.Value);
            frequency = (byte) (freqtype_comboBox.SelectedIndex);
            status = Api.SetRf(power , frequency);

            if(status != 0)
            {
                MessageBox.Show("设置功率与使用地区失败!");
                return;
            }
            MessageBox.Show("设置功率与使用地区成功!");
        }

        private void antconfig_button_Click(object sender , EventArgs e)
        {
            byte ant_sel = 0;

            int status;

            status = Api.GetAnt(ref ant_sel);
            if(status != 0)
            {
                MessageBox.Show("获取天线信息失败!");
                return;
            }
            //MessageBox.Show("Get Ant settings success!");

            if((ant_sel & 0x01) == 0x01)
                ant1_checkBox.Checked = true;
            else
                ant1_checkBox.Checked = false;
            if((ant_sel & 0x02) == 0x02)
                ant2_checkBox.Checked = true;
            else
                ant2_checkBox.Checked = false;
            if((ant_sel & 0x04) == 0x04)
                ant3_checkBox.Checked = true;
            else
                ant3_checkBox.Checked = false;
            if((ant_sel & 0x08) == 0x08)
                ant4_checkBox.Checked = true;
            else
                ant4_checkBox.Checked = false;
        }

        private void antset_button_Click(object sender , EventArgs e)
        {
            //天线具有4天线，任一天线均可用
            byte ant_sel = 0;
            int status;

            if(ant1_checkBox.Checked)
                ant_sel |= 0x01;
            if(ant2_checkBox.Checked)
                ant_sel |= 0x02;
            if(ant3_checkBox.Checked)
                ant_sel |= 0x04;
            if(ant4_checkBox.Checked)
                ant_sel |= 0x08;

            status = Api.SetAnt(ant_sel);
            if(status != 0)
            {
                MessageBox.Show("天线设置失败!请重试。");
                return;
            }
            MessageBox.Show("天线设置成功!");
        }

        private void read_button_Click(object sender , EventArgs e)
        {
            int dataregion;
            int dataaddress;
            int datalength;

            int status = 0;
            byte[] value = new byte[16];

            string s = "The data is: ";
            string s1 = "";

            dataregion = dataregion_comboBox.SelectedIndex;
            dataaddress = dataaddress_comboBox.SelectedIndex;
            datalength = datalength_comboBox.SelectedIndex + 1;

            status = Api.EpcRead((byte) dataregion , (byte) dataaddress , (byte) datalength , ref value);

            if(status != 0)
            {
                MessageBox.Show("信息读取失败! 请重试。");
                return;
            }
            else
            {
                for(int i = 0 ; i < datalength * 2 ; i++)
                {
                    s1 = string.Format("{0:X2}" , value[i]);
                    s += s1;
                }
                MessageBox.Show("读取成功!");
                write_textBox.Text = (s);
                //detail_listBox.Items.Add(s);
            }
        }

        private void write_button_Click(object sender , EventArgs e)
        {
            ushort[] value = new ushort[16];
            int i = 0;
            byte dataregion;
            byte dataaddress;
            byte datalength;

            dataregion = (byte) (dataregion_comboBox.SelectedIndex);
            dataaddress = (byte) (dataaddress_comboBox.SelectedIndex);
            datalength = (byte) (datalength_comboBox.SelectedIndex + 1);

            int status;
            string hexValues;

            hexValues = write_textBox.Text;
            string[] hexValuesSplit = hexValues.Split('.');
            //写数据为16进制，每4位用"."进行分割，如输入01.ABCF.003,写入数据为0001ABCF0003

            foreach(String hex in hexValuesSplit)
            {
                // Convert the number expressed in base-16 to an integer.
                if(hex != "")
                {
                    int x = Convert.ToInt32(hex , 16);
                    value[i++] = (ushort) x;
                }
            }

            if(i != datalength)
            {
                MessageBox.Show("数据长度超出限制！");
                return;
            }

            for(byte j = 0 ; j < datalength ; j++)
            {
                status = Api.EpcWrite(dataregion , (byte) (dataaddress + j) , value[j]);
                if(status != 0)
                {
                    MessageBox.Show("写入失败!请重试。");
                    return;
                }
            }
            MessageBox.Show("写入成功!");
        }

        private void write_textBox_Enter(object sender , EventArgs e)
        {
            write_textBox.SelectionStart = write_textBox.Text.Length;
        }

        /// <summary>
        /// 将运动场地id数据集绑定到playingfield_comboBox中;
        /// </summary>
        //sql = "SELECT DISTINCT PlayingFiledId FROM dbo.CompetitionProject";
        private void GetFieldId_To_playingfield_comboBox()
        {
            try
            {
                string sql = "SELECT DISTINCT PlayingFiledId FROM dbo.CompetitionProject";
                DataSet playingfieldid_dataset = DataAccess.GetDataSetBySql(sql);
                DataTable playingfieldid_datatable = playingfieldid_dataset.Tables[0];

                for(int i = 0 ; i < playingfieldid_datatable.Rows.Count ; i++)
                {
                    this.playingfield_comboBox.Items.Add(playingfieldid_datatable.Rows[i][0].ToString().Trim());   
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 根据playingfield_comboBox中的场地号码，将竞赛项目数据绑定到dataGridView_ProjectInfo中;
        /// </summary>
        // sql = "sql = "SELECT Id, CompetitionProjectName, AthletesNum, PlayingFiledId, CompetitionProjectOrder, Statu 
        // FROM dbo.CompetitionProject";   
        private void GetCompetitionProject_To_dataGridView_ProjectInfo(string fieldid)
        {
            try
            {
                string sql = "SELECT Id, CompetitionProjectName, AthletesNum, PlayingFiledId, CompetitionProjectOrder, Statu FROM dbo.CompetitionProject WHERE dbo.CompetitionProject.PlayingFiledId = '"+ fieldid+ "';";
                DataSet dataset_ProjectInfo = DataAccess.GetDataSetBySql(sql);
                DataTable table_ProjectInfo = dataset_ProjectInfo.Tables[0];

                dataGridView_projectinfo.DataSource = table_ProjectInfo;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 建立dataGridView_checkinfo所需的数据库视图View_Check，并刷新检录项目dataGridView_checkinfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // CREATE VIEW dbo.View_Check AS SELECT dbo.CompetitionProject.CompetitionProjectName, dbo.CompetitionProject.AthletesNum, dbo.CompetitionProject.PlayingFiledId, dbo.CompetitionProject.CompetitionProjectOrder, dbo.CompetitionProject.Statu, dbo.CompetitionProject.StandardProjectId, dbo.CompetitionProject.StartTime, dbo.CompetitionProject.ProjectType, dbo.AthletesInfo.Name, dbo.AthletesInfo.Sex, dbo.AthletesInfo.Birthday, dbo.AthletesInfo.CardNum, dbo.AthletesInfo.GroupId, dbo.GroupInfo.GroupName, dbo.ContestantInfo.ContestantName, dbo.EnterOrderInfo.Id, dbo.EnterOrderInfo.CompetitionProjectId, dbo.EnterOrderInfo.AthleteId, dbo.EnterOrderInfo.AthleteOrder, dbo.EnterOrderInfo.AthleteStatu FROM dbo.EnterOrderInfo INNER JOIN dbo.AthletesInfo ON dbo.EnterOrderInfo.AthleteId = dbo.AthletesInfo.Id INNER JOIN dbo.GroupInfo ON dbo.AthletesInfo.GroupId = dbo.GroupInfo.Id INNER JOIN dbo.CompetitionProject ON dbo.EnterOrderInfo.CompetitionProjectId = dbo.CompetitionProject.Id INNER JOIN dbo.ContestantInfo ON dbo.AthletesInfo.ContestantId = dbo.ContestantInfo.Id;
        private void project_textBox_TextChanged(object sender , EventArgs e)
        {
            try
            {
                if(!DataAccess.sql_exist("SELECT View_Check.Name FROM View_Check"))
                {
                    string sql_creatview = "CREATE VIEW dbo.View_Check AS SELECT dbo.CompetitionProject.CompetitionProjectName, dbo.CompetitionProject.AthletesNum, dbo.CompetitionProject.PlayingFiledId, dbo.CompetitionProject.CompetitionProjectOrder, dbo.CompetitionProject.Statu, dbo.CompetitionProject.StandardProjectId, dbo.CompetitionProject.StartTime, dbo.CompetitionProject.ProjectType, dbo.AthletesInfo.Name, dbo.AthletesInfo.Sex, dbo.AthletesInfo.Birthday, dbo.AthletesInfo.CardNum, dbo.AthletesInfo.GroupId, dbo.GroupInfo.GroupName, dbo.ContestantInfo.ContestantName, dbo.EnterOrderInfo.Id, dbo.EnterOrderInfo.CompetitionProjectId, dbo.EnterOrderInfo.AthleteId, dbo.EnterOrderInfo.AthleteOrder, dbo.EnterOrderInfo.AthleteStatu FROM dbo.EnterOrderInfo INNER JOIN dbo.AthletesInfo ON dbo.EnterOrderInfo.AthleteId = dbo.AthletesInfo.Id INNER JOIN dbo.GroupInfo ON dbo.AthletesInfo.GroupId = dbo.GroupInfo.Id INNER JOIN dbo.CompetitionProject ON dbo.EnterOrderInfo.CompetitionProjectId = dbo.CompetitionProject.Id INNER JOIN dbo.ContestantInfo ON dbo.AthletesInfo.ContestantId = dbo.ContestantInfo.Id;";
                    DataAccess.sql_command(sql_creatview);
                }
            }
            catch(Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

            try
            {
                groupBox_checkinfo.Text = project_textBox.Text;

                string projectname = project_textBox.Text;
                string sql =
                    "SELECT View_Check.Name,View_Check.ContestantName,View_Check.AthleteOrder,View_Check.AthleteStatu FROM View_Check WHERE View_Check.CompetitionProjectName = '" + projectname + "';";
                DataSet datasets_GetCheckInfo = DataAccess.GetDataSetBySql(sql);
                DataTable table_GetCheckInfo = datasets_GetCheckInfo.Tables[0];

                dataGridView_checkinfo.DataSource = table_GetCheckInfo;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 刷新竞赛项目dataGridView_ProjectInfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void playingfield_comboBox_SelectedIndexChanged(object sender , EventArgs e)
        {
            GetCompetitionProject_To_dataGridView_ProjectInfo(playingfield_comboBox.Text);
            project_textBox.Text = dataGridView_projectinfo.Rows[0].Cells[1].Value.ToString().Trim();
        }


        /// <summary>
        /// 根据dataGridView_checkinfo中的点击，将选中行的运动员名称数据绑定到name_textBox.Text中，将选中行的参赛团体名称绑定到contestant_textbox.text中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_checkinfo_CellClick(object sender , DataGridViewCellEventArgs e)
        {
            int row_index = this.dataGridView_checkinfo.CurrentCell.RowIndex;

            name_textBox.Text = dataGridView_checkinfo.Rows[row_index].Cells[0].Value.ToString().Trim();
            contestant_textBox.Text= dataGridView_checkinfo.Rows[row_index].Cells[1].Value.ToString().Trim();
        }

        /// <summary>
        /// 根据dataGridView_ProjectInfo中的点击，将选中行的竞赛项目名称CompetitionProjectName数据绑定到project_textBox.Text中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_projectinfo_CellClick(object sender , DataGridViewCellEventArgs e)
        {
            int row_index = this.dataGridView_projectinfo.CurrentCell.RowIndex;

            project_textBox.Text = dataGridView_projectinfo.Rows[row_index].Cells[1].Value.ToString().Trim();
        }

        /// <summary>
        /// 根据dataGridView_ProjectInfo中的焦点的移动，将选中行的竞赛项目名称CompetitionProjectName数据绑定到project_textBox.Text中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_projectinfo_CellEnter(object sender , DataGridViewCellEventArgs e)
        {
            int row_index = this.dataGridView_projectinfo.CurrentCell.RowIndex;

            project_textBox.Text = dataGridView_projectinfo.Rows[row_index].Cells[1].Value.ToString().Trim();
        }

        /// <summary>
        /// 根据dataGridView_checkinfo中焦点的变动，将选中行的运动员名称数据绑定到name_textBox.Text中，将选中行的参赛团体名称绑定到contestant_textbox.text中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_checkinfo_CellEnter(object sender , DataGridViewCellEventArgs e)
        {
            int row_index = this.dataGridView_checkinfo.CurrentCell.RowIndex;

            name_textBox.Text = dataGridView_checkinfo.Rows[row_index].Cells[0].Value.ToString().Trim();
            contestant_textBox.Text = dataGridView_checkinfo.Rows[row_index].Cells[1].Value.ToString().Trim();
        }

        /// <summary>
        /// datagridview的字符串替换，根据datagridview的列绑定数据库表列名称，选择列中的字符串进行替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_projectinfo_CellFormatting(object sender , DataGridViewCellFormattingEventArgs e)
        {
            if(dataGridView_projectinfo.Columns[e.ColumnIndex].DataPropertyName == "Statu")
            {
                if(e.Value.ToString() == "0")
                {
                    e.Value = "未开始";
                }
                if(e.Value.ToString() == "1")
                {
                    e.Value = "正在比赛";
                }
                if(e.Value.ToString() == "2")
                {
                    e.Value = "比赛结束";
                }
            }
        }

        /// <summary>
        /// datagridview的字符串替换，根据datagridview的列绑定数据库表列名称，选择列中的字符串进行替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_checkinfo_CellFormatting(object sender , DataGridViewCellFormattingEventArgs e)
        {
            if(dataGridView_checkinfo.Columns[e.ColumnIndex].DataPropertyName == "AthleteStatu")
            {
                if(e.Value.ToString() == "0")
                {
                    e.Value = "未开始";
                }
                if(e.Value.ToString() == "1")
                {
                    e.Value = "正在比赛";
                }
                if(e.Value.ToString() == "2")
                {
                    e.Value = "比赛结束";
                }
                if(e.Value.ToString() == "3")
                {
                    e.Value = "检录成功";
                }
                if(e.Value.ToString() == "9")
                {
                    e.Value = "已弃权";
                }
            }
        }

        /// <summary>
        /// 修改运动员状态为9，设置为弃权，string sqlupdate = "UPDATE View_Check set AthleteStatu = '9' WHERE View_Check.Name = '" + athlete_name + "';";
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void abstention_button_Click(object sender , EventArgs e)
        {
            string athlete_name = name_textBox.Text;
            string projectname = project_textBox.Text;

            string sqlexist = "SELECT View_Check.Name FROM View_Check WHERE View_Check.Name = '" + athlete_name + "'AND View_Check.AthleteStatu='9' AND View_Check.CompetitionProjectName = '" + projectname + "';";

            if(DataAccess.sql_exist(sqlexist))
            {
                DialogResult exist = MessageBox.Show("该运动员已弃权！");
            }
            else
            {
                DialogResult goon = MessageBox.Show("" + athlete_name + " 弃权处理，请确认！" , "Prompt Message" , MessageBoxButtons.OKCancel);

                try
                {
                    if(goon == DialogResult.OK)
                    {
                        string sqlupdate = "UPDATE View_Check set AthleteStatu = '9' WHERE View_Check.Name = '" + athlete_name + "';";

                        if(DataAccess.sql_command(sqlupdate))
                        {
                            DialogResult answer = MessageBox.Show("弃权操作成功！");
                            project_textBox_TextChanged(null , null);
                        }
                    }

                }
                catch(Exception exception)
                {

                    MessageBox.Show(exception.Message);
                }
            }
        }
    }
}
