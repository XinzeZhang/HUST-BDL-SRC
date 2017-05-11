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

namespace CheckIn
{
    public partial class CheckIn : Form
    {
        public CheckIn()
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
            //ListPanel.Add(panel_AthletesCheckIn);
            //ListPanel.Add(panel_RFIDConfigure);
            //ListPanel[PanelIndex].BringToFront();
            
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
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

            stopcheck_button.Enabled = false;

            //初始化playingfield_comboBox，并默认为场地1
            GetFieldId_To_playingfield_comboBox();
            playingfield_comboBox.SelectedIndex = 0;



            //初始化dataGridView_ProjectInfo
            GetCompetitionProject_To_dataGridView_ProjectInfo(playingfield_comboBox.Text);
            project_textBox.Text = dataGridView_projectinfo.Rows[0].Cells[1].Value.ToString().Trim();
            

        }

        /*
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
            string s = "";

            //open comm port//打开读写器端口
            status = Api.OpenCommPort(commport_comboBox.SelectedItem.ToString());
            if(status != 0)
            {
                detail_listBox.Items.Add("Open Comm Port Failed!");
                return;
            }

            //get firmware version//调用读写器固件版本
            status = Api.GetFirmwareVersion(ref v1 , ref v2);
            if(status != 0)
            {
                detail_listBox.Items.Add("Can not connect with the Reader!");
                Api.CloseCommPort();
                return;
            }
            detail_listBox.Items.Add("Connect the reader success!");

            s = string.Format("The reader's firmware version is:V{0:d2}.{1:d2}" , v1 , v2);
            detail_listBox.Items.Add(s);

            connect_button.Enabled = false;//连接成功，连接按钮禁用
            disconnect_button.Enabled = true;//连接成功，断开连接按钮启用

            pf_groupBox.Enabled = true;//连接成功，功率与频率groupbox启用
            ant_groupBox.Enabled = true;//连接成功，天线设置区域启用
            tagidentify_groupBox.Enabled = true;//连接成功，标签读写区域启用

            pfconfig_button_Click(null , null);//连接成功，功率和频率进行默认设置
            antconfig_button_Click(null , null);//连接成功，天线进行默认设置
        }

        
        private void multiple_identify_button_Click(object sender , EventArgs e)
        {
            if(TagReading == 0)
            {
                Api.ClearIdBuf();
                detail_listBox.Items.Clear();
                detail_listBox.Items.Add("Start multiply tags identify!");

                Tagcount = 0;
                multiple_tag_timer.Interval = 150;
                multiple_tag_timer.Enabled = true;
                multiple_identify_button.Text = "Stop";
                TagReading = 1;
            }
            else
            {
                multiple_tag_timer.Enabled = false;
                TagReading = 0;
                multiple_identify_button.Text = "Multiple-\r\nIdentify\r\n";
            }
        }

        private void multiple_tag_timer_Tick(object sender , EventArgs e)
        {
            int status;
            int i, j;
            byte[,] IsoBuf = new byte[100 , 12];
            byte tag_count = 0;
            string s = "";
            string s1 = "";
            byte tag_flag = 0;
            int listrow = 0;

            //filter
            if(!filter_checkBox.Checked)
            {
                Api.ClearIdBuf();
            }

            status = Api.EpcMultiTagIdentify(ref IsoBuf , ref tag_count , ref tag_flag);

            if(tag_count > 0)
            {
                for(i = 0 ; i < tag_count ; i++)
                {
                    s1 = "";
                    for(j = 0 ; j < Convert.ToInt16(datalength_comboBox.Text) * 2 ; j++)
                    {
                        s = string.Format("{0:X2} " , IsoBuf[i , j]);
                        s1 += s;
                    }
                    detail_listBox.Items.Add(s1);

                    ListViewItem listview = new ListViewItem();

                    if(tag_listView.Items.Count <= 0)
                    {
                        listview.SubItems[0].Text = "1";

                        for(i = 0 ; i <= 2 ; i++)
                        {
                            listview.SubItems.Add("");
                        }
                        tag_listView.Items.Add(listview);

                        listrow = 0;
                        tag_listView.Items[listrow].SubItems[1].Text = s1;
                        tag_listView.Items[listrow].SubItems[2].Text = "1";
                    }
                    else
                    {
                        listrow = -1;

                        for(i = 0 ; i < tag_listView.Items.Count ; i++)
                        {
                            if(tag_listView.Items[i].SubItems[1].Text == s1)
                            {
                                listrow = i;
                                break;
                            }
                        }

                        if(listrow < 0)
                        {
                            listrow = tag_listView.Items.Count;
                            listview.SubItems[0].Text = Convert.ToString(listrow + 1);

                            for(i = 0 ; i <= 2 ; i++)
                            {
                                listview.SubItems.Add("");
                            }
                            tag_listView.Items.Add(listview);
                        }

                        tag_listView.Items[listrow].SubItems[1].Text = s1;

                        if(tag_listView.Items[listrow].SubItems[2].Text == "")
                        {
                            tag_listView.Items[listrow].SubItems[2].Text = "0";
                        }

                        Int64 inttimes = Convert.ToInt64(tag_listView.Items[listrow].SubItems[2].Text);
                        tag_listView.Items[listrow].SubItems[2].Text = Convert.ToString(inttimes + 1);
                    }

                }
            }
        }

        private void single_identify_button_Click(object sender , EventArgs e)
        {

            if(TagReading == 0)
            {
                Api.ClearIdBuf();
                detail_listBox.Items.Clear();
                detail_listBox.Items.Add("Start single tags identify!");

                Tagcount = 0;
                single_tag_timer.Interval = 500;
                single_tag_timer.Enabled = true;
                single_identify_button.Text = "Stop";
                TagReading = 1;
            }
            else
            {
                single_tag_timer.Enabled = false;
                TagReading = 0;
                single_identify_button.Text = "Single-\r\nIdentify\r\n\r\n";
            }
        }

        private void single_tag_timer_Tick(object sender , EventArgs e)
        {
            int dataregion_timer;
            int dataaddress_timer;
            int datalength_timer;

            int status = 0;
            byte[] value = new byte[16];

            string s = "The data is: ";
            string s1 = "";

            dataregion_timer = dataregion_comboBox.SelectedIndex;
            dataaddress_timer = dataaddress_comboBox.SelectedIndex;
            datalength_timer = datalength_comboBox.SelectedIndex + 1;

            status = Api.EpcRead((byte) dataregion_timer , (byte) dataaddress_timer , (byte) datalength_timer , ref value);

            if(status == 0)
            {
                for(int i = 0 ; i < datalength_timer * 2 ; i++)
                {
                    s1 = string.Format("{0:X2}" , value[i]);
                    s += s1;
                }
                detail_listBox.Items.Add("Read success!");
                detail_listBox.Items.Add(s);
            }
        }
        */


        /*
        private void write_textBox_Enter(object sender , EventArgs e)
        {
            write_textBox.SelectionStart = write_textBox.Text.Length;
        }
        */

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
                    string sql_creatview = "CREATE VIEW dbo.View_Check AS SELECT dbo.CompetitionProject.CompetitionProjectName, dbo.CompetitionProject.AthletesNum, dbo.CompetitionProject.PlayingFiledId, dbo.CompetitionProject.CompetitionProjectOrder, dbo.CompetitionProject.Statu, dbo.CompetitionProject.StandardProjectId, dbo.CompetitionProject.StartTime, dbo.CompetitionProject.ProjectType, dbo.AthletesInfo.Name, dbo.AthletesInfo.Sex, dbo.AthletesInfo.Birthday, dbo.AthletesInfo.CardNum, dbo.AthletesInfo.GroupId, dbo.ContestantInfo.ContestantName, dbo.EnterOrderInfo.Id, dbo.EnterOrderInfo.CompetitionProjectId, dbo.EnterOrderInfo.AthleteId, dbo.EnterOrderInfo.AthleteOrder, dbo.EnterOrderInfo.AthleteStatu FROM dbo.EnterOrderInfo INNER JOIN dbo.AthletesInfo ON dbo.EnterOrderInfo.AthleteId = dbo.AthletesInfo.Id INNER JOIN dbo.CompetitionProject ON dbo.EnterOrderInfo.CompetitionProjectId = dbo.CompetitionProject.Id INNER JOIN dbo.ContestantInfo ON dbo.AthletesInfo.ContestantId = dbo.ContestantInfo.Id;";
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
        /// 连接读卡器，开启stopcheck_button，关闭check_button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_button_Click(object sender , EventArgs e)
        {
            Connect_ToolStripMenuItem_Click(null,null);
            stopcheck_button.Enabled = true;
            check_button.Enabled = false;

            try
            {
                if(TagReading == 0)
                {
                    Api.ClearIdBuf();

                    Tagcount = 0;
                    check_timer.Interval = 250;
                    check_timer.Enabled = true;
                    TagReading = 1;
                }
            }
            catch(Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// 关闭读卡器，开启check_button，关闭stopcheck_button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopcheck_button_Click(object sender , EventArgs e)
        {
            Disconnect_ToolStripMenuItem_Click(null , null);
            stopcheck_button.Enabled = false;
            check_button.Enabled = true;

            try
            {
                if(TagReading==1)
                {
                    check_timer.Enabled = false;
                    TagReading = 0;
                }
            }
            catch(Exception exception)
            {

                MessageBox.Show(exception.Message);
            }
        }

        private void check_timer_Tick(object sender , EventArgs e)
        {
            int dataregion_timer;
            int dataaddress_timer;
            int datalength_timer;

            int status = 0;
            byte[] value = new byte[16];

            string s1 = "";

            int AtheleteIdValue = 0;
            string AtheleteIdString = "";

            //下面是一条测试用sql命令
            //SELECT View_Check.Name,View_Check.ContestantName,View_Check.AthleteOrder,View_Check.AthleteStatu FROM View_Check WHERE View_Check.CompetitionProjectName = '男子儿童组自选长拳A组'AND View_Check.AthleteId='30'

            dataregion_timer = dataregion_toolStripComboBox.SelectedIndex;
            dataaddress_timer = dataaddress_toolStripComboBox.SelectedIndex;
            datalength_timer = datalength_toolStripComboBox.SelectedIndex + 1;

            status = Api.EpcRead((byte) dataregion_timer , (byte) dataaddress_timer , (byte) datalength_timer , ref value);

            if(status == 0)
            {
                //test
                check_timer.Enabled = false;

                for(int i = 0 ; i < datalength_timer * 2 ; i++)
                {
                    s1 = string.Format("{0:X2}" , value[i]);
                }

                /*
                 * /detail_listBox操作
                detail_listBox.Items.Add("Read success!");
                detail_listBox.Items.Add(s1);
                */

                AtheleteIdValue = Convert.ToInt32(s1);

                /*
                 * /detail_listBox操作
                detail_listBox.Items.Add("the AtheleteIdValue is");
                AtheleteIdString = Convert.ToString(AtheleteIdValue);
                detail_listBox.Items.Add(AtheleteIdString);
                */

                try
                {
                    string projectname = project_textBox.Text;
                    string sqlexist = "SELECT View_Check.Name,View_Check.ContestantName,View_Check.AthleteOrder,View_Check.AthleteStatu FROM View_Check WHERE View_Check.CompetitionProjectName = '" + projectname+ "'AND View_Check.AthleteId='" + AtheleteIdValue + "'";
                    //运动员状态View_Check.AthleteStatu==3时，为检录成功
                    //修改运动员状态，string sql=" update View_Check set AthleteStatu = '3' WHERE View_Check.CompetitionProjectName = '男子儿童组自选长拳A组'AND View_Check.AthleteId='31';";

                    if(DataAccess.sql_exist(sqlexist))
                    {
                        string sqlupdate = "UPDATE View_Check set AthleteStatu = '3' WHERE View_Check.CompetitionProjectName = '"+ projectname + "'AND View_Check.AthleteId='" + AtheleteIdValue + "';";
                        if(DataAccess.sql_command(sqlupdate))
                        {
                            //detail_listBox.Items.Add("检录成功");
                            DialogResult answer = MessageBox.Show("检录成功！");
                            project_textBox_TextChanged(null , null);
                            //test
                            if(answer == DialogResult.OK)
                            {
                                check_timer.Enabled = true;
                            }
                        }
                        else
                        {
                            DialogResult answer = MessageBox.Show("刷卡失败，请再次刷卡！");
                            if(answer == DialogResult.OK)
                            {
                                check_timer.Enabled = true;
                            }
                        }
                    }
                    else
                    {
                        //detail_listBox.Items.Add("该运动员不在此项目中");
                        DialogResult answer = MessageBox.Show("该运动员不在此项目中");
                        
                        //test
                        if(answer==DialogResult.OK)
                        {
                            check_timer.Enabled = true;
                        }
                    }
                }
                catch(Exception exception)
                {

                    MessageBox.Show(exception.Message);
                }
            }
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
            string s = "";

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
    }
}
