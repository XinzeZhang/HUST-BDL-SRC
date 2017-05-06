using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DesktopRfidApi;

namespace FinalInsert
{
    public partial class FinalInsert : Form
    {
        // initiallize the rfid api
        DesktopRfidApi.RfidApi Api = new DesktopRfidApi.RfidApi();

        //initiallize the rfid tagcount
        public byte TagReading = 0;
        public int Tagcount = 0;

        public int Read_Counter = 0;

        public FinalInsert()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化主窗体内变量参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinalInsert_Load(object sender , EventArgs e)
        {
            //初始化playingfield_comboBox，并默认为场地1
            GetFieldId_To_playingfield_comboBox();
            playingfield_comboBox.SelectedIndex = 0;

            //初始化Contestant_comboBox
            GetContestantName_To_Contestant_comboBox();

            //初始化dataGridView_ProjectInfo
            GetCompetitionProject_To_dataGridView_ProjectInfo(playingfield_comboBox.Text);
            project_textBox.Text = dataGridView_projectinfo.Rows[0].Cells[1].Value.ToString().Trim();

            try
            {
                //RFID设备 端口号初始化设置，设置为COM3
                commport_toolStripComboBox.SelectedIndex = 2;
                //RFID设备 波段初始化设置，设置为9600
                boundrate_toolStripComboBox.SelectedIndex = 0;
                //RFID设备 频率范围: 0 - 2;
                //0 for 中国China(920 m ~920 m); 1 for 美国American(902 m ~902 m)
                //其他the other for 特殊special models (868 m)
                //RFID设备 频率初始化设置，设置为美国, frequency default as 1;
                freqtype_toolStripComboBox.SelectedIndex = 1;

                Connect_ToolStripMenuItem.Enabled = true;//配置初始化，连接按钮启用
                Disconnect_ToolStripMenuItem.Enabled = false;//配置初始化，断开连接按钮禁用

                pf_ToolStripMenuItem.Enabled = false;//配置初始化，功率与频率groupbox禁用
                ant_ToolStripMenuItem.Enabled = false;//配置初始化，天线设置禁用

                dataregion_toolStripComboBox.SelectedIndex = 1;//配置初始化，标签读写区初始化为EPC区域

                //initialize data address comboBox//配置初始化，标签起始地址为2
                int nloop = 0;
                for(nloop = 0 ; nloop < 256 ; nloop++)
                {
                    dataaddress_toolStripComboBox.Items.Add(Convert.ToString(nloop));
                }
                dataaddress_toolStripComboBox.SelectedIndex = 2;

                //initiallize power index
                for(nloop = 1 ; nloop < 31 ; nloop++)
                {
                    power_toolStripComboBox.Items.Add(Convert.ToString(nloop));
                }

                //initiallize data length comboBox//配置初始化，标签读写长度为1, 4位16进制数字
                for(nloop = 1 ; nloop < 14 ; nloop++)
                {
                    datalength_toolStripComboBox.Items.Add(Convert.ToString(nloop));
                }
                datalength_toolStripComboBox.SelectedIndex = 0;

                //初始读取和写入按钮为不启用
                read_button.Enabled = false;
                write_button.Enabled = false;
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        /// <summary>
        /// 连接读写器：打开读写器端口->调用读写器固件版本->设置读写器功率与使用频段（地区）->设置读写器天线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            int status;
            byte v1 = 0;
            byte v2 = 0;
            //string s = "";

            //open comm port//打开读写器端口
            status = Api.OpenCommPort(commport_toolStripComboBox.SelectedItem.ToString());
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

            Connect_ToolStripMenuItem.Enabled = false;//连接成功，连接按钮禁用
            Connect_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            Disconnect_ToolStripMenuItem.Enabled = true;//连接成功，断开连接按钮启用

            pf_ToolStripMenuItem.Enabled = true;//连接成功，功率与频率groupbox启用
            ant_ToolStripMenuItem.Enabled = true;//连接成功，天线设置区域启用

            pfconfig_ToolStripMenuItem_Click(null , null);//连接成功，功率和频率进行默认设置
            antconfig_ToolStripMenuItem_Click(null , null);//连接成功，天线进行默认设置

            read_button.Enabled = true;//读取按钮启用
            write_button.Enabled = true;//写入按钮启用
        }

        /// <summary>
        /// 获取读写器功率与使用地区（频段）参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pfconfig_ToolStripMenuItem_Click(object sender , EventArgs e)
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
            power_toolStripComboBox.SelectedIndex = power;
            freqtype_toolStripComboBox.SelectedIndex = frequency;
            //MessageBox.Show("Get Power & Freq settings success!");
        }

        /// <summary>
        /// 设置读写器功率与使用地区（频段）参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pfset_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            //power range: 0-30
            //power default as 15
            byte power = 15;
            //frequency range: 0-2; 0 for China (920 m ~ 920 m), 1 for American (902 m ~ 902 m), the other for special models (868 m)
            //frequency default as 1;
            byte frequency = 1;

            int status;

            power = (byte) (power_toolStripComboBox.SelectedIndex);
            frequency = (byte) (freqtype_toolStripComboBox.SelectedIndex);
            status = Api.SetRf(power , frequency);

            if(status != 0)
            {
                MessageBox.Show("设置功率与使用地区失败!");
                return;
            }
            MessageBox.Show("设置功率与使用地区成功!");
        }

        /// <summary>
        /// 获取读写器天线配置参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void antconfig_ToolStripMenuItem_Click(object sender , EventArgs e)
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
                ant1_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            else
                ant1_ToolStripMenuItem.ForeColor = System.Drawing.Color.Gray;
            if((ant_sel & 0x02) == 0x02)
                ant2_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            else
                ant2_ToolStripMenuItem.ForeColor = System.Drawing.Color.Gray;
            if((ant_sel & 0x04) == 0x04)
                ant3_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            else
                ant3_ToolStripMenuItem.ForeColor = System.Drawing.Color.Gray;
            if((ant_sel & 0x08) == 0x08)
                ant4_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
            else
                ant4_ToolStripMenuItem.ForeColor = System.Drawing.Color.Gray;
        }

        /// <summary>
        /// 设置读写器天线参数，开启与关闭读写器天线单元
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void antset_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            //天线具有4天线，任一天线均可用
            byte ant_sel = 0;
            int status;

            if(ant1_ToolStripMenuItem.ForeColor == System.Drawing.Color.Green)
                ant_sel |= 0x01;
            if(ant2_ToolStripMenuItem.ForeColor == System.Drawing.Color.Green)
                ant_sel |= 0x02;
            if(ant3_ToolStripMenuItem.ForeColor == System.Drawing.Color.Green)
                ant_sel |= 0x04;
            if(ant4_ToolStripMenuItem.ForeColor == System.Drawing.Color.Green)
                ant_sel |= 0x08;

            status = Api.SetAnt(ant_sel);
            if(status != 0)
            {
                MessageBox.Show("天线设置失败!请重试。");
                return;
            }
            MessageBox.Show("天线设置成功!");
        }

        private void ant1_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            ant1_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
        }

        private void ant2_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            ant2_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
        }

        private void ant3_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            ant3_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
        }

        private void ant4_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            ant4_ToolStripMenuItem.ForeColor = System.Drawing.Color.Green;
        }

        /// <summary>
        /// 与读写器断开连接，关闭读写器端口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Disconnect_ToolStripMenuItem_Click(object sender , EventArgs e)
        {
            Api.CloseCommPort();//关闭读写器端口
            Connect_ToolStripMenuItem.Enabled = true;
            Disconnect_ToolStripMenuItem.Enabled = false;

            pfconfig_ToolStripMenuItem.Enabled = false;
            ant_ToolStripMenuItem.Enabled = false;

            read_button.Enabled = false;
            write_button.Enabled = false;
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
        /// 将参赛单位ContestantName数据集绑定到Contestant_comboBox中;
        /// </summary>
        //sql = "SELECT DISTINCT ContestantName FROM dbo.ContestantInfo";
        private void GetContestantName_To_Contestant_comboBox()
        {
            try
            {
                string sql = "SELECT DISTINCT ContestantName FROM dbo.ContestantInfo";
                DataSet ContestantName_dataset = DataAccess.GetDataSetBySql(sql);
                DataTable ContestantName_datatable = ContestantName_dataset.Tables[0];

                for(int i = 0 ; i < ContestantName_datatable.Rows.Count ; i++)
                {
                    this.contestant_comboBox.Items.Add(ContestantName_datatable.Rows[i][0].ToString().Trim());
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
                string sql = "SELECT Id, CompetitionProjectName, AthletesNum, PlayingFiledId, CompetitionProjectOrder, Statu FROM dbo.CompetitionProject WHERE dbo.CompetitionProject.PlayingFiledId = '" + fieldid + "';";
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
        /// 根据dataGridView_ProjectInfo中的焦点的移动，将选中行的竞赛项目名称CompetitionProjectName数据绑定到project_textBox.Text中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_projectinfo_CellClick(object sender , DataGridViewCellEventArgs e)
        {
            int row_index = this.dataGridView_projectinfo.CurrentCell.RowIndex;

            project_textBox.Text = dataGridView_projectinfo.Rows[row_index].Cells[1].Value.ToString().Trim();
        }

        /// <summary>
        /// 建立dataGridView_checkinfo所需的数据库视图View_Check，并刷新检录项目dataGridView_checkinfo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //SELECT dbo.CompetitionProject.CompetitionProjectName, dbo.CompetitionProject.AthletesNum, dbo.CompetitionProject.PlayingFiledId, dbo.CompetitionProject.CompetitionProjectOrder, dbo.CompetitionProject.Statu, dbo.CompetitionProject.StandardProjectId, dbo.CompetitionProject.StartTime, dbo.CompetitionProject.ProjectType, dbo.AthletesInfo.Name, dbo.AthletesInfo.Sex, dbo.AthletesInfo.Birthday, dbo.AthletesInfo.CardNum, dbo.AthletesInfo.GroupId, dbo.ContestantInfo.ContestantName, dbo.EnterOrderInfo.Id, dbo.EnterOrderInfo.CompetitionProjectId, dbo.EnterOrderInfo.AthleteId, dbo.EnterOrderInfo.AthleteOrder, dbo.EnterOrderInfo.AthleteStatu FROM dbo.EnterOrderInfo INNER JOIN dbo.AthletesInfo ON dbo.EnterOrderInfo.AthleteId = dbo.AthletesInfo.Id INNER JOIN dbo.CompetitionProject ON dbo.EnterOrderInfo.CompetitionProjectId = dbo.CompetitionProject.Id INNER JOIN dbo.ContestantInfo ON dbo.AthletesInfo.ContestantId = dbo.ContestantInfo.Id;
        private void project_textBox_TextChanged(object sender , EventArgs e)
        {
            try
            {
                if(!DataAccess.sql_exist("SELECT View_FinalInsert.Name FROM View_FinalInsert"))
                {
                    string sql_creatview =
                        "CREATE VIEW dbo.View_FinalInsert AS SELECT dbo.CompetitionProject.CompetitionProjectName, dbo.CompetitionProject.AthletesNum, dbo.CompetitionProject.PlayingFiledId, dbo.CompetitionProject.CompetitionProjectOrder, dbo.CompetitionProject.Statu, dbo.CompetitionProject.StandardProjectId, dbo.CompetitionProject.StartTime, dbo.CompetitionProject.ProjectType, dbo.AthletesInfo.Name, dbo.AthletesInfo.Sex, dbo.AthletesInfo.Birthday, dbo.AthletesInfo.CardNum, dbo.AthletesInfo.GroupId, dbo.ContestantInfo.ContestantName, dbo.EnterOrderInfo.Id, dbo.EnterOrderInfo.CompetitionProjectId, dbo.EnterOrderInfo.AthleteId, dbo.EnterOrderInfo.AthleteOrder, dbo.EnterOrderInfo.AthleteStatu FROM dbo.EnterOrderInfo INNER JOIN dbo.AthletesInfo ON dbo.EnterOrderInfo.AthleteId = dbo.AthletesInfo.Id INNER JOIN dbo.CompetitionProject ON dbo.EnterOrderInfo.CompetitionProjectId = dbo.CompetitionProject.Id INNER JOIN dbo.ContestantInfo ON dbo.AthletesInfo.ContestantId = dbo.ContestantInfo.Id;";
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
                    "SELECT View_FinalInsert.Name,View_FinalInsert.ContestantName,View_FinalInsert.AthleteOrder,View_FinalInsert.AthleteStatu FROM View_FinalInsert WHERE View_FinalInsert.CompetitionProjectName = '" + projectname + "';";
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

        private void write_button_Click(object sender , EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(name_textBox.Text.Trim()) || string.IsNullOrWhiteSpace(contestant_comboBox.Text.Trim()) ||string.IsNullOrWhiteSpace(write_textBox.Text.Trim())|| string.IsNullOrWhiteSpace(playingfield_comboBox.Text.Trim())|| string.IsNullOrWhiteSpace(project_textBox.Text.Trim()))
            {
                MessageBox.Show("运动员信息缺失，请补充完整!");
                return;
            }
            try
            {
                /*
                 * test sql
               insert into dbo.AthletesInfo
               (
               Id,
               Name,
               Sex,
               ContestantId)
               values
               (
               '2480',
               '测试人员',
               '女',
               '12'
               );

               insert into dbo.EnterOrderInfo
               (
               Id,
               CompetitionProjectId,
               AthleteId,
               AthleteOrder,
               AthleteStatu
               )
               values
               (
               '2480',
               '527',
               '2480',
               '4',
               '0'
               )

               update dbo.CompetitionProject
               set AthletesNum= '4'
               where Id='527'

               *test successful
               */


                /*
                try
                {
                    string sql = "SELECT DISTINCT ContestantName FROM dbo.ContestantInfo";
                    DataSet ContestantName_dataset = DataAccess.GetDataSetBySql(sql);
                    DataTable ContestantName_datatable = ContestantName_dataset.Tables[0];

                    for(int i = 0 ; i < ContestantName_datatable.Rows.Count ; i++)
                    {
                        this.contestant_comboBox.Items.Add(ContestantName_datatable.Rows[i][0].ToString().Trim());
                    }
                }
                catch(Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
                */

                //get ContestantID
                string SqlSelect_ContestantId = "select dbo.ContestantInfo.Id from dbo.ContestantInfo where dbo.ContestantInfo.ContestantName='" + this.contestant_comboBox.Text.ToString().Trim() + "';";
                DataSet ContestantID_dataset = DataAccess.GetDataSetBySql(SqlSelect_ContestantId);
                int ContestantID = Convert.ToInt32(ContestantID_dataset.Tables[0].Rows[0][0].ToString().Trim());

                //get CompetitionProjectId
                string SqlSelect_CompetitionProject_Id_Num = "select dbo.CompetitionProject.Id, dbo.CompetitionProject.AthletesNum from dbo.CompetitionProject where dbo.CompetitionProject.CompetitionProjectName='" + this.project_textBox.Text.ToString().Trim() + "';";
                DataSet SqlSelect_CompetitionProject_Id_Num_dataset = DataAccess.GetDataSetBySql(SqlSelect_CompetitionProject_Id_Num);
                int CompetitionProjectId = Convert.ToInt32(SqlSelect_CompetitionProject_Id_Num_dataset.Tables[0].Rows[0][0].ToString().Trim());
                
                //get EnterOrderIDMax+1 and AthleteOrderMax+1
                string SqlSelect_EnterOrder_ID_AthleteOrder = "select MAX(EnterOrderInfo.Id) as max_enterorderId from EnterOrderInfo;select MAX(EnterOrderInfo.AthleteOrder) as max_Athleteorder from EnterOrderInfo where EnterOrderInfo.CompetitionProjectId='" + CompetitionProjectId + "';";
                DataSet EnterOrder_ID_AthleteOrder_dataset = DataAccess.GetDataSetBySql(SqlSelect_EnterOrder_ID_AthleteOrder);
                int EnterOrderIDMax_added_1 = Convert.ToInt32(EnterOrder_ID_AthleteOrder_dataset.Tables[0].Rows[0][0].ToString().Trim()) + 1;
                int AthleteOrderMax_added_1 = Convert.ToInt32(EnterOrder_ID_AthleteOrder_dataset.Tables[1].Rows[0][0].ToString().Trim()) + 1;

                //get AthleteIdMax+1
                int AthleteIdMax_added_1 = Convert.ToInt32(this.write_textBox.Text.Trim());

                //测试该ID是否已经被使用
                string sqlexist = "select AthletesInfo.Id from AthletesInfo where AthletesInfo.Id='" + AthleteIdMax_added_1 + "';";

                if(DataAccess.sql_exist(sqlexist))
                {
                    DialogResult exist = MessageBox.Show("该ID已被注册，若第一次出现此情况，请手动更换ID！若再次出现此情况，请查询文档或联系技术人员！");
                }
                else
                {
                    int i = 0;
                    ushort[] value = new ushort[16];
                    //写入操作
                    byte dataregion;
                    byte dataaddress;
                    byte datalength;

                    dataregion = (byte) (dataregion_toolStripComboBox.SelectedIndex);
                    dataaddress = (byte) (dataaddress_toolStripComboBox.SelectedIndex);
                    datalength = (byte) (datalength_toolStripComboBox.SelectedIndex + 1);
                    
                    string hexValues;
                    int status;

                    hexValues = write_textBox.Text.Trim();
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
                        MessageBox.Show("ID长度超出数据单元限制！");
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

                    DialogResult goon = MessageBox.Show("" + this.name_textBox.Text.Trim() + " 进行注册，请确认！" , "Prompt Message" , MessageBoxButtons.OKCancel);
                    if(goon == DialogResult.OK)
                    {
                        string SqlInsert =
                            string.Format("insert into dbo.AthletesInfo(Id,Name,Sex,ContestantId) values('{0}','{1}','{2}','{3}');insert into dbo.EnterOrderInfo(Id,CompetitionProjectId,AthleteId,AthleteOrder,AthleteStatu)values('{4}','{5}','{6}','{7}','0');" , AthleteIdMax_added_1 , name_textBox.Text.Trim() , sex_comboBox.Text.Trim() , ContestantID , EnterOrderIDMax_added_1 , CompetitionProjectId , AthleteIdMax_added_1 , AthleteOrderMax_added_1);
                        
                        //select count(*) as ProjectNumUpdate from EnterOrderInfo where EnterOrderInfo.CompetitionProjectId=1
                        string SqlSelect_AthleteNum_update = "select count(*) as ProjectNumUpdate from EnterOrderInfo where EnterOrderInfo.CompetitionProjectId=" + CompetitionProjectId + "";
                        DataSet AthleteNum_update_dataset = DataAccess.GetDataSetBySql(SqlSelect_AthleteNum_update);
                        int CompetitionProject_AthleteNum_update = Convert.ToInt32(AthleteNum_update_dataset.Tables[0].Rows[0][0].ToString().Trim());

                        string Sql_Project_AthleteNum_update = "update dbo.CompetitionProject set AthletesNum= '" + CompetitionProject_AthleteNum_update + "' where Id='" + CompetitionProjectId + "';";

                        if(DataAccess.sql_command(SqlInsert))
                        {
                            project_textBox_TextChanged(null , null);
                            DataAccess.sql_command(Sql_Project_AthleteNum_update);
                            GetCompetitionProject_To_dataGridView_ProjectInfo(playingfield_comboBox.Text);
                            DialogResult answer = MessageBox.Show("注册成功!");
                            
                                //playingfield_comboBox_SelectedIndexChanged(null,null);
                                //dataGridView_projectinfo.Refresh();
                            
                            name_textBox.Clear();
                            contestant_comboBox.SelectedIndex = -1;
                            sex_comboBox.SelectedIndex = -1;
                            write_textBox.Clear();
                           
                        }
                        else
                        {
                            DialogResult answer = MessageBox.Show("运动员信息写入数据库失败，请重试!");
                            return;
                        }
                    }

                }
                
            }
              
            catch(Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// get AthleteIdMax+1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void write_textBox_Click(object sender , EventArgs e)
        {
            try
            {
                //get AthleteIdMax+1
                string SqlSelect_AthleteIdMax = "select MAX(AthletesInfo.Id) as max_athleteID from AthletesInfo";
                DataSet AthleteIdMax_dataset = DataAccess.GetDataSetBySql(SqlSelect_AthleteIdMax);
                int AthleteIdMax_added_1 = Convert.ToInt32(AthleteIdMax_dataset.Tables[0].Rows[0][0].ToString().Trim()) + 1;

                write_textBox.Text = AthleteIdMax_added_1.ToString().Trim();
            }
            catch(Exception exception)
            {

                MessageBox.Show(exception.Message);
            }

        }

        private void read_button_Click(object sender , EventArgs e)
        {
            name_textBox.Clear();
            contestant_comboBox.SelectedIndex = -1;
            sex_comboBox.SelectedIndex = -1;
            write_textBox.Clear();

            Read_timer.Interval = 250;
            Read_timer.Enabled = true;

            read_button.Enabled = false;
            
        }

        private void Read_timer_Tick(object sender , EventArgs e)
        {
            Read_Counter++;//每次Tick计时指数++

            if(Read_Counter >= 16)
            {
                Read_timer.Enabled = false;

                Read_Counter = 0;

                MessageBox.Show("未读取到芯片卡，请更换芯片卡！并检查设备端口是否正常!");
                read_button.Enabled = true;

                return;
            }

            int dataregion;
            int dataaddress;
            int datalength;

            int status = 0;
            byte[] value = new byte[16];

            string s = "";

            dataregion = dataregion_toolStripComboBox.SelectedIndex;
            dataaddress = dataaddress_toolStripComboBox.SelectedIndex;
            datalength = datalength_toolStripComboBox.SelectedIndex + 1;

            status = Api.EpcRead((byte) dataregion , (byte) dataaddress , (byte) datalength , ref value);

            if(status == 0)
            {
                Read_Counter = 0;//计时指数归0

                read_button.Enabled = true;

                Read_timer.Enabled = false;

                for(int i = 0 ; i < datalength * 2 ; i++)
                {
                    s += string.Format("{0:X2}" , value[i]);
                }
                write_textBox.Text = (s);

                int AtheleteIdValue = Convert.ToInt32(s);

                try
                {
                    string projectname = project_textBox.Text;
                    string SqlRead = "SELECT View_FinalInsert.Name,View_FinalInsert.ContestantName FROM View_FinalInsert WHERE View_FinalInsert.CompetitionProjectName = '" + projectname + "'AND View_FinalInsert.AthleteId = '" + AtheleteIdValue + "'";
                    if(DataAccess.sql_exist(SqlRead))
                    {
                        DataSet Name_Contestant_dataset = DataAccess.GetDataSetBySql(SqlRead);
                        name_textBox.Text = Name_Contestant_dataset.Tables[0].Rows[0][0].ToString().Trim();
                        contestant_comboBox.Text = Name_Contestant_dataset.Tables[0].Rows[0][1].ToString().Trim();
                    }
                    else
                    {
                        DialogResult answer = MessageBox.Show("该运动员ID写卡成功，但不在当前竞赛项目中！");
                        return;
                    }

                }
                catch(Exception exception)
                {

                    MessageBox.Show(exception.Message);
                }
                MessageBox.Show("读取成功!");

                //detail_listBox.Items.Add(s);
            }
        }
    }
}
