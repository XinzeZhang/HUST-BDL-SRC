namespace InsertAthlete
{
    partial class InsertAthlete
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip_panel = new System.Windows.Forms.MenuStrip();
            this.InsertAthlete_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RFIDConfigure_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_InsertAthlete = new System.Windows.Forms.Panel();
            this.groupBox_checkinfo = new System.Windows.Forms.GroupBox();
            this.dataGridView_checkinfo = new System.Windows.Forms.DataGridView();
            this.AtheleteName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContestantName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AthleteOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AthleteStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_project = new System.Windows.Forms.GroupBox();
            this.dataGridView_projectinfo = new System.Windows.Forms.DataGridView();
            this.ProjectId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AthletesNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProjectOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FieldId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox_checkin = new System.Windows.Forms.GroupBox();
            this.abstention_button = new System.Windows.Forms.Button();
            this.contestant_textBox = new System.Windows.Forms.TextBox();
            this.name_textBox = new System.Windows.Forms.TextBox();
            this.label_contestant = new System.Windows.Forms.Label();
            this.label_name = new System.Windows.Forms.Label();
            this.project_textBox = new System.Windows.Forms.TextBox();
            this.label_project = new System.Windows.Forms.Label();
            this.label_playingfield = new System.Windows.Forms.Label();
            this.playingfield_comboBox = new System.Windows.Forms.ComboBox();
            this.panel_RFIDConfigure = new System.Windows.Forms.Panel();
            this.connection_groupBox = new System.Windows.Forms.GroupBox();
            this.disconnect_button = new System.Windows.Forms.Button();
            this.connect_button = new System.Windows.Forms.Button();
            this.boudrate_comboBox = new System.Windows.Forms.ComboBox();
            this.commport_comboBox = new System.Windows.Forms.ComboBox();
            this.boudrate_label = new System.Windows.Forms.Label();
            this.commport_label = new System.Windows.Forms.Label();
            this.pf_groupBox = new System.Windows.Forms.GroupBox();
            this.pfconfig_button = new System.Windows.Forms.Button();
            this.freqtype_label = new System.Windows.Forms.Label();
            this.freqtype_comboBox = new System.Windows.Forms.ComboBox();
            this.power_trackBar = new System.Windows.Forms.TrackBar();
            this.pfset_button = new System.Windows.Forms.Button();
            this.power_label = new System.Windows.Forms.Label();
            this.ant_groupBox = new System.Windows.Forms.GroupBox();
            this.ant3_checkBox = new System.Windows.Forms.CheckBox();
            this.antconfig_button = new System.Windows.Forms.Button();
            this.ant4_checkBox = new System.Windows.Forms.CheckBox();
            this.antset_button = new System.Windows.Forms.Button();
            this.ant2_checkBox = new System.Windows.Forms.CheckBox();
            this.ant1_checkBox = new System.Windows.Forms.CheckBox();
            this.tagidentify_groupBox = new System.Windows.Forms.GroupBox();
            this.write_textBox = new System.Windows.Forms.TextBox();
            this.write_button = new System.Windows.Forms.Button();
            this.read_button = new System.Windows.Forms.Button();
            this.datalength_comboBox = new System.Windows.Forms.ComboBox();
            this.datalength_label = new System.Windows.Forms.Label();
            this.dataaddress_comboBox = new System.Windows.Forms.ComboBox();
            this.dataregion_comboBox = new System.Windows.Forms.ComboBox();
            this.dataaddr_label = new System.Windows.Forms.Label();
            this.dataregion_label = new System.Windows.Forms.Label();
            this.menuStrip_panel.SuspendLayout();
            this.panel_InsertAthlete.SuspendLayout();
            this.groupBox_checkinfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_checkinfo)).BeginInit();
            this.groupBox_project.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_projectinfo)).BeginInit();
            this.groupBox_checkin.SuspendLayout();
            this.panel_RFIDConfigure.SuspendLayout();
            this.connection_groupBox.SuspendLayout();
            this.pf_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.power_trackBar)).BeginInit();
            this.ant_groupBox.SuspendLayout();
            this.tagidentify_groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip_panel
            // 
            this.menuStrip_panel.Font = new System.Drawing.Font("Microsoft YaHei", 12F);
            this.menuStrip_panel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InsertAthlete_ToolStripMenuItem,
            this.RFIDConfigure_ToolStripMenuItem});
            this.menuStrip_panel.Location = new System.Drawing.Point(0, 0);
            this.menuStrip_panel.Name = "menuStrip_panel";
            this.menuStrip_panel.Size = new System.Drawing.Size(1350, 29);
            this.menuStrip_panel.TabIndex = 0;
            this.menuStrip_panel.Text = "menuStrip1";
            // 
            // InsertAthlete_ToolStripMenuItem
            // 
            this.InsertAthlete_ToolStripMenuItem.Name = "InsertAthlete_ToolStripMenuItem";
            this.InsertAthlete_ToolStripMenuItem.Size = new System.Drawing.Size(102, 25);
            this.InsertAthlete_ToolStripMenuItem.Text = "运动员管理";
            this.InsertAthlete_ToolStripMenuItem.Click += new System.EventHandler(this.AthletesCheckIn_ToolStripMenuItem_Click);
            // 
            // RFIDConfigure_ToolStripMenuItem
            // 
            this.RFIDConfigure_ToolStripMenuItem.Name = "RFIDConfigure_ToolStripMenuItem";
            this.RFIDConfigure_ToolStripMenuItem.Size = new System.Drawing.Size(102, 25);
            this.RFIDConfigure_ToolStripMenuItem.Text = "读卡器配置";
            this.RFIDConfigure_ToolStripMenuItem.Click += new System.EventHandler(this.RFIDConfigure_ToolStripMenuItem_Click);
            // 
            // panel_InsertAthlete
            // 
            this.panel_InsertAthlete.Controls.Add(this.groupBox_checkinfo);
            this.panel_InsertAthlete.Controls.Add(this.groupBox_project);
            this.panel_InsertAthlete.Controls.Add(this.groupBox_checkin);
            this.panel_InsertAthlete.Location = new System.Drawing.Point(0, 222);
            this.panel_InsertAthlete.Name = "panel_InsertAthlete";
            this.panel_InsertAthlete.Size = new System.Drawing.Size(1051, 507);
            this.panel_InsertAthlete.TabIndex = 1;
            // 
            // groupBox_checkinfo
            // 
            this.groupBox_checkinfo.Controls.Add(this.dataGridView_checkinfo);
            this.groupBox_checkinfo.Location = new System.Drawing.Point(728, 173);
            this.groupBox_checkinfo.Name = "groupBox_checkinfo";
            this.groupBox_checkinfo.Size = new System.Drawing.Size(604, 498);
            this.groupBox_checkinfo.TabIndex = 5;
            this.groupBox_checkinfo.TabStop = false;
            this.groupBox_checkinfo.Text = "检录信息";
            // 
            // dataGridView_checkinfo
            // 
            this.dataGridView_checkinfo.AllowUserToAddRows = false;
            this.dataGridView_checkinfo.AllowUserToDeleteRows = false;
            this.dataGridView_checkinfo.AllowUserToOrderColumns = true;
            this.dataGridView_checkinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_checkinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AtheleteName,
            this.ContestantName,
            this.AthleteOrder,
            this.AthleteStatus});
            this.dataGridView_checkinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_checkinfo.Location = new System.Drawing.Point(3, 25);
            this.dataGridView_checkinfo.Name = "dataGridView_checkinfo";
            this.dataGridView_checkinfo.Size = new System.Drawing.Size(598, 470);
            this.dataGridView_checkinfo.TabIndex = 0;
            this.dataGridView_checkinfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_checkinfo_CellClick);
            this.dataGridView_checkinfo.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_checkinfo_CellEnter);
            this.dataGridView_checkinfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_checkinfo_CellFormatting);
            // 
            // AtheleteName
            // 
            this.AtheleteName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.AtheleteName.DataPropertyName = "Name";
            this.AtheleteName.HeaderText = "姓名";
            this.AtheleteName.Name = "AtheleteName";
            this.AtheleteName.Width = 67;
            // 
            // ContestantName
            // 
            this.ContestantName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ContestantName.DataPropertyName = "ContestantName";
            this.ContestantName.HeaderText = "参赛单位";
            this.ContestantName.Name = "ContestantName";
            // 
            // AthleteOrder
            // 
            this.AthleteOrder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.AthleteOrder.DataPropertyName = "AthleteOrder";
            this.AthleteOrder.HeaderText = "顺序";
            this.AthleteOrder.Name = "AthleteOrder";
            this.AthleteOrder.Width = 67;
            // 
            // AthleteStatus
            // 
            this.AthleteStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.AthleteStatus.DataPropertyName = "AthleteStatu";
            this.AthleteStatus.HeaderText = "状态";
            this.AthleteStatus.Name = "AthleteStatus";
            this.AthleteStatus.Width = 67;
            // 
            // groupBox_project
            // 
            this.groupBox_project.Controls.Add(this.dataGridView_projectinfo);
            this.groupBox_project.Location = new System.Drawing.Point(12, 170);
            this.groupBox_project.Name = "groupBox_project";
            this.groupBox_project.Size = new System.Drawing.Size(648, 501);
            this.groupBox_project.TabIndex = 4;
            this.groupBox_project.TabStop = false;
            this.groupBox_project.Text = "竞赛项目信息";
            // 
            // dataGridView_projectinfo
            // 
            this.dataGridView_projectinfo.AllowUserToAddRows = false;
            this.dataGridView_projectinfo.AllowUserToDeleteRows = false;
            this.dataGridView_projectinfo.AllowUserToOrderColumns = true;
            this.dataGridView_projectinfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_projectinfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProjectId,
            this.ProjectName,
            this.AthletesNum,
            this.ProjectOrder,
            this.FieldId,
            this.Status});
            this.dataGridView_projectinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_projectinfo.Location = new System.Drawing.Point(3, 25);
            this.dataGridView_projectinfo.Name = "dataGridView_projectinfo";
            this.dataGridView_projectinfo.ReadOnly = true;
            this.dataGridView_projectinfo.Size = new System.Drawing.Size(642, 473);
            this.dataGridView_projectinfo.TabIndex = 0;
            this.dataGridView_projectinfo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_projectinfo_CellClick);
            this.dataGridView_projectinfo.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_projectinfo_CellEnter);
            this.dataGridView_projectinfo.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_projectinfo_CellFormatting);
            // 
            // ProjectId
            // 
            this.ProjectId.DataPropertyName = "Id";
            this.ProjectId.HeaderText = "ID";
            this.ProjectId.Name = "ProjectId";
            this.ProjectId.ReadOnly = true;
            this.ProjectId.Visible = false;
            // 
            // ProjectName
            // 
            this.ProjectName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.ProjectName.DataPropertyName = "CompetitionProjectName";
            this.ProjectName.HeaderText = "竞赛项目";
            this.ProjectName.Name = "ProjectName";
            this.ProjectName.ReadOnly = true;
            this.ProjectName.Width = 99;
            // 
            // AthletesNum
            // 
            this.AthletesNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AthletesNum.DataPropertyName = "AthletesNum";
            this.AthletesNum.HeaderText = "人数";
            this.AthletesNum.Name = "AthletesNum";
            this.AthletesNum.ReadOnly = true;
            // 
            // ProjectOrder
            // 
            this.ProjectOrder.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProjectOrder.DataPropertyName = "CompetitionProjectOrder";
            this.ProjectOrder.HeaderText = "顺序";
            this.ProjectOrder.Name = "ProjectOrder";
            this.ProjectOrder.ReadOnly = true;
            // 
            // FieldId
            // 
            this.FieldId.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FieldId.DataPropertyName = "PlayingFiledId";
            this.FieldId.HeaderText = "场地";
            this.FieldId.Name = "FieldId";
            this.FieldId.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Status.DataPropertyName = "Statu";
            this.Status.HeaderText = "状态";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 67;
            // 
            // groupBox_checkin
            // 
            this.groupBox_checkin.Controls.Add(this.abstention_button);
            this.groupBox_checkin.Controls.Add(this.contestant_textBox);
            this.groupBox_checkin.Controls.Add(this.name_textBox);
            this.groupBox_checkin.Controls.Add(this.label_contestant);
            this.groupBox_checkin.Controls.Add(this.label_name);
            this.groupBox_checkin.Controls.Add(this.project_textBox);
            this.groupBox_checkin.Controls.Add(this.label_project);
            this.groupBox_checkin.Controls.Add(this.label_playingfield);
            this.groupBox_checkin.Controls.Add(this.playingfield_comboBox);
            this.groupBox_checkin.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox_checkin.Location = new System.Drawing.Point(12, 27);
            this.groupBox_checkin.Name = "groupBox_checkin";
            this.groupBox_checkin.Size = new System.Drawing.Size(1317, 136);
            this.groupBox_checkin.TabIndex = 3;
            this.groupBox_checkin.TabStop = false;
            // 
            // abstention_button
            // 
            this.abstention_button.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.abstention_button.Location = new System.Drawing.Point(1195, 36);
            this.abstention_button.Name = "abstention_button";
            this.abstention_button.Size = new System.Drawing.Size(102, 85);
            this.abstention_button.TabIndex = 10;
            this.abstention_button.Text = "弃权";
            this.abstention_button.UseVisualStyleBackColor = true;
            this.abstention_button.Click += new System.EventHandler(this.abstention_button_Click);
            // 
            // contestant_textBox
            // 
            this.contestant_textBox.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.contestant_textBox.Location = new System.Drawing.Point(931, 82);
            this.contestant_textBox.Name = "contestant_textBox";
            this.contestant_textBox.Size = new System.Drawing.Size(244, 29);
            this.contestant_textBox.TabIndex = 9;
            // 
            // name_textBox
            // 
            this.name_textBox.Location = new System.Drawing.Point(931, 36);
            this.name_textBox.Name = "name_textBox";
            this.name_textBox.Size = new System.Drawing.Size(244, 35);
            this.name_textBox.TabIndex = 8;
            // 
            // label_contestant
            // 
            this.label_contestant.AutoSize = true;
            this.label_contestant.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_contestant.Location = new System.Drawing.Point(791, 82);
            this.label_contestant.Name = "label_contestant";
            this.label_contestant.Size = new System.Drawing.Size(96, 28);
            this.label_contestant.TabIndex = 7;
            this.label_contestant.Text = "参赛单位";
            // 
            // label_name
            // 
            this.label_name.AutoSize = true;
            this.label_name.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_name.Location = new System.Drawing.Point(791, 36);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(117, 28);
            this.label_name.TabIndex = 6;
            this.label_name.Text = "运动员姓名";
            // 
            // project_textBox
            // 
            this.project_textBox.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.project_textBox.Location = new System.Drawing.Point(273, 21);
            this.project_textBox.Name = "project_textBox";
            this.project_textBox.Size = new System.Drawing.Size(372, 33);
            this.project_textBox.TabIndex = 3;
            this.project_textBox.TextChanged += new System.EventHandler(this.project_textBox_TextChanged);
            // 
            // label_project
            // 
            this.label_project.AutoSize = true;
            this.label_project.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_project.Location = new System.Drawing.Point(171, 18);
            this.label_project.Name = "label_project";
            this.label_project.Size = new System.Drawing.Size(96, 28);
            this.label_project.TabIndex = 2;
            this.label_project.Text = "竞赛项目";
            // 
            // label_playingfield
            // 
            this.label_playingfield.AutoSize = true;
            this.label_playingfield.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label_playingfield.Location = new System.Drawing.Point(9, 18);
            this.label_playingfield.Name = "label_playingfield";
            this.label_playingfield.Size = new System.Drawing.Size(96, 28);
            this.label_playingfield.TabIndex = 1;
            this.label_playingfield.Text = "竞赛场地";
            // 
            // playingfield_comboBox
            // 
            this.playingfield_comboBox.DisplayMember = "PlayingFiledId";
            this.playingfield_comboBox.Font = new System.Drawing.Font("Microsoft YaHei", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.playingfield_comboBox.FormattingEnabled = true;
            this.playingfield_comboBox.Location = new System.Drawing.Point(111, 20);
            this.playingfield_comboBox.Name = "playingfield_comboBox";
            this.playingfield_comboBox.Size = new System.Drawing.Size(54, 33);
            this.playingfield_comboBox.TabIndex = 0;
            this.playingfield_comboBox.ValueMember = "PlayingFiledId";
            this.playingfield_comboBox.SelectedIndexChanged += new System.EventHandler(this.playingfield_comboBox_SelectedIndexChanged);
            // 
            // panel_RFIDConfigure
            // 
            this.panel_RFIDConfigure.Controls.Add(this.connection_groupBox);
            this.panel_RFIDConfigure.Controls.Add(this.pf_groupBox);
            this.panel_RFIDConfigure.Controls.Add(this.ant_groupBox);
            this.panel_RFIDConfigure.Controls.Add(this.tagidentify_groupBox);
            this.panel_RFIDConfigure.Location = new System.Drawing.Point(0, 32);
            this.panel_RFIDConfigure.Name = "panel_RFIDConfigure";
            this.panel_RFIDConfigure.Size = new System.Drawing.Size(1008, 700);
            this.panel_RFIDConfigure.TabIndex = 2;
            // 
            // connection_groupBox
            // 
            this.connection_groupBox.AutoSize = true;
            this.connection_groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.connection_groupBox.Controls.Add(this.disconnect_button);
            this.connection_groupBox.Controls.Add(this.connect_button);
            this.connection_groupBox.Controls.Add(this.boudrate_comboBox);
            this.connection_groupBox.Controls.Add(this.commport_comboBox);
            this.connection_groupBox.Controls.Add(this.boudrate_label);
            this.connection_groupBox.Controls.Add(this.commport_label);
            this.connection_groupBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.connection_groupBox.Location = new System.Drawing.Point(9, 23);
            this.connection_groupBox.Name = "connection_groupBox";
            this.connection_groupBox.Size = new System.Drawing.Size(249, 161);
            this.connection_groupBox.TabIndex = 16;
            this.connection_groupBox.TabStop = false;
            this.connection_groupBox.Text = "连接设置";
            // 
            // disconnect_button
            // 
            this.disconnect_button.Location = new System.Drawing.Point(122, 24);
            this.disconnect_button.Name = "disconnect_button";
            this.disconnect_button.Size = new System.Drawing.Size(121, 37);
            this.disconnect_button.TabIndex = 5;
            this.disconnect_button.Text = "断开连接";
            this.disconnect_button.UseVisualStyleBackColor = true;
            this.disconnect_button.Click += new System.EventHandler(this.disconnect_button_Click);
            // 
            // connect_button
            // 
            this.connect_button.Location = new System.Drawing.Point(6, 24);
            this.connect_button.Name = "connect_button";
            this.connect_button.Size = new System.Drawing.Size(95, 37);
            this.connect_button.TabIndex = 4;
            this.connect_button.Text = "连接设备";
            this.connect_button.UseVisualStyleBackColor = true;
            this.connect_button.Click += new System.EventHandler(this.connect_button_Click);
            // 
            // boudrate_comboBox
            // 
            this.boudrate_comboBox.FormattingEnabled = true;
            this.boudrate_comboBox.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.boudrate_comboBox.Location = new System.Drawing.Point(122, 105);
            this.boudrate_comboBox.Name = "boudrate_comboBox";
            this.boudrate_comboBox.Size = new System.Drawing.Size(121, 29);
            this.boudrate_comboBox.TabIndex = 3;
            // 
            // commport_comboBox
            // 
            this.commport_comboBox.FormattingEnabled = true;
            this.commport_comboBox.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.commport_comboBox.Location = new System.Drawing.Point(122, 73);
            this.commport_comboBox.Name = "commport_comboBox";
            this.commport_comboBox.Size = new System.Drawing.Size(121, 29);
            this.commport_comboBox.TabIndex = 2;
            // 
            // boudrate_label
            // 
            this.boudrate_label.AutoSize = true;
            this.boudrate_label.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boudrate_label.Location = new System.Drawing.Point(11, 108);
            this.boudrate_label.Name = "boudrate_label";
            this.boudrate_label.Size = new System.Drawing.Size(74, 21);
            this.boudrate_label.TabIndex = 1;
            this.boudrate_label.Text = "设备频率";
            // 
            // commport_label
            // 
            this.commport_label.AutoSize = true;
            this.commport_label.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.commport_label.Location = new System.Drawing.Point(11, 74);
            this.commport_label.Name = "commport_label";
            this.commport_label.Size = new System.Drawing.Size(90, 21);
            this.commport_label.TabIndex = 0;
            this.commport_label.Text = "设备端口号";
            // 
            // pf_groupBox
            // 
            this.pf_groupBox.AutoSize = true;
            this.pf_groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pf_groupBox.Controls.Add(this.pfconfig_button);
            this.pf_groupBox.Controls.Add(this.freqtype_label);
            this.pf_groupBox.Controls.Add(this.freqtype_comboBox);
            this.pf_groupBox.Controls.Add(this.power_trackBar);
            this.pf_groupBox.Controls.Add(this.pfset_button);
            this.pf_groupBox.Controls.Add(this.power_label);
            this.pf_groupBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pf_groupBox.Location = new System.Drawing.Point(9, 184);
            this.pf_groupBox.Name = "pf_groupBox";
            this.pf_groupBox.Size = new System.Drawing.Size(249, 178);
            this.pf_groupBox.TabIndex = 17;
            this.pf_groupBox.TabStop = false;
            this.pf_groupBox.Text = "功率与使用地区";
            // 
            // pfconfig_button
            // 
            this.pfconfig_button.Location = new System.Drawing.Point(122, 24);
            this.pfconfig_button.Name = "pfconfig_button";
            this.pfconfig_button.Size = new System.Drawing.Size(121, 37);
            this.pfconfig_button.TabIndex = 5;
            this.pfconfig_button.Text = "获取";
            this.pfconfig_button.UseVisualStyleBackColor = true;
            this.pfconfig_button.Click += new System.EventHandler(this.pfconfig_button_Click);
            // 
            // freqtype_label
            // 
            this.freqtype_label.AutoSize = true;
            this.freqtype_label.Location = new System.Drawing.Point(3, 126);
            this.freqtype_label.Name = "freqtype_label";
            this.freqtype_label.Size = new System.Drawing.Size(74, 21);
            this.freqtype_label.TabIndex = 8;
            this.freqtype_label.Text = "使用地区";
            // 
            // freqtype_comboBox
            // 
            this.freqtype_comboBox.FormattingEnabled = true;
            this.freqtype_comboBox.Items.AddRange(new object[] {
            "CHINA",
            "AMERICA",
            "EUROUP",
            "OTHER"});
            this.freqtype_comboBox.Location = new System.Drawing.Point(122, 122);
            this.freqtype_comboBox.Name = "freqtype_comboBox";
            this.freqtype_comboBox.Size = new System.Drawing.Size(121, 29);
            this.freqtype_comboBox.TabIndex = 7;
            // 
            // power_trackBar
            // 
            this.power_trackBar.Location = new System.Drawing.Point(122, 67);
            this.power_trackBar.Maximum = 30;
            this.power_trackBar.Name = "power_trackBar";
            this.power_trackBar.Size = new System.Drawing.Size(121, 45);
            this.power_trackBar.SmallChange = 3;
            this.power_trackBar.TabIndex = 5;
            this.power_trackBar.Value = 15;
            // 
            // pfset_button
            // 
            this.pfset_button.Location = new System.Drawing.Point(6, 24);
            this.pfset_button.Name = "pfset_button";
            this.pfset_button.Size = new System.Drawing.Size(95, 37);
            this.pfset_button.TabIndex = 4;
            this.pfset_button.Text = "设置";
            this.pfset_button.UseVisualStyleBackColor = true;
            this.pfset_button.Click += new System.EventHandler(this.pfset_button_Click);
            // 
            // power_label
            // 
            this.power_label.AutoSize = true;
            this.power_label.Location = new System.Drawing.Point(6, 77);
            this.power_label.Name = "power_label";
            this.power_label.Size = new System.Drawing.Size(42, 21);
            this.power_label.TabIndex = 6;
            this.power_label.Text = "功率";
            // 
            // ant_groupBox
            // 
            this.ant_groupBox.AutoSize = true;
            this.ant_groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ant_groupBox.Controls.Add(this.ant3_checkBox);
            this.ant_groupBox.Controls.Add(this.antconfig_button);
            this.ant_groupBox.Controls.Add(this.ant4_checkBox);
            this.ant_groupBox.Controls.Add(this.antset_button);
            this.ant_groupBox.Controls.Add(this.ant2_checkBox);
            this.ant_groupBox.Controls.Add(this.ant1_checkBox);
            this.ant_groupBox.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ant_groupBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ant_groupBox.Location = new System.Drawing.Point(9, 363);
            this.ant_groupBox.Name = "ant_groupBox";
            this.ant_groupBox.Size = new System.Drawing.Size(249, 148);
            this.ant_groupBox.TabIndex = 18;
            this.ant_groupBox.TabStop = false;
            this.ant_groupBox.Text = "天线";
            // 
            // ant3_checkBox
            // 
            this.ant3_checkBox.AutoSize = true;
            this.ant3_checkBox.Checked = true;
            this.ant3_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ant3_checkBox.Location = new System.Drawing.Point(22, 95);
            this.ant3_checkBox.Name = "ant3_checkBox";
            this.ant3_checkBox.Size = new System.Drawing.Size(70, 25);
            this.ant3_checkBox.TabIndex = 8;
            this.ant3_checkBox.Text = "天线3";
            this.ant3_checkBox.UseVisualStyleBackColor = true;
            // 
            // antconfig_button
            // 
            this.antconfig_button.Location = new System.Drawing.Point(122, 24);
            this.antconfig_button.Name = "antconfig_button";
            this.antconfig_button.Size = new System.Drawing.Size(121, 37);
            this.antconfig_button.TabIndex = 7;
            this.antconfig_button.Text = "获取";
            this.antconfig_button.UseVisualStyleBackColor = true;
            this.antconfig_button.Click += new System.EventHandler(this.antconfig_button_Click);
            // 
            // ant4_checkBox
            // 
            this.ant4_checkBox.AutoSize = true;
            this.ant4_checkBox.Checked = true;
            this.ant4_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ant4_checkBox.Location = new System.Drawing.Point(143, 95);
            this.ant4_checkBox.Name = "ant4_checkBox";
            this.ant4_checkBox.Size = new System.Drawing.Size(70, 25);
            this.ant4_checkBox.TabIndex = 9;
            this.ant4_checkBox.Text = "天线4";
            this.ant4_checkBox.UseVisualStyleBackColor = true;
            // 
            // antset_button
            // 
            this.antset_button.Location = new System.Drawing.Point(9, 24);
            this.antset_button.Name = "antset_button";
            this.antset_button.Size = new System.Drawing.Size(95, 37);
            this.antset_button.TabIndex = 6;
            this.antset_button.Text = "设置";
            this.antset_button.UseVisualStyleBackColor = true;
            this.antset_button.Click += new System.EventHandler(this.antset_button_Click);
            // 
            // ant2_checkBox
            // 
            this.ant2_checkBox.AutoSize = true;
            this.ant2_checkBox.Checked = true;
            this.ant2_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ant2_checkBox.Location = new System.Drawing.Point(143, 67);
            this.ant2_checkBox.Name = "ant2_checkBox";
            this.ant2_checkBox.Size = new System.Drawing.Size(70, 25);
            this.ant2_checkBox.TabIndex = 7;
            this.ant2_checkBox.Text = "天线2";
            this.ant2_checkBox.UseVisualStyleBackColor = true;
            // 
            // ant1_checkBox
            // 
            this.ant1_checkBox.AutoSize = true;
            this.ant1_checkBox.Checked = true;
            this.ant1_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ant1_checkBox.Location = new System.Drawing.Point(22, 67);
            this.ant1_checkBox.Name = "ant1_checkBox";
            this.ant1_checkBox.Size = new System.Drawing.Size(70, 25);
            this.ant1_checkBox.TabIndex = 6;
            this.ant1_checkBox.Text = "天线1";
            this.ant1_checkBox.UseVisualStyleBackColor = true;
            // 
            // tagidentify_groupBox
            // 
            this.tagidentify_groupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tagidentify_groupBox.Controls.Add(this.write_textBox);
            this.tagidentify_groupBox.Controls.Add(this.write_button);
            this.tagidentify_groupBox.Controls.Add(this.read_button);
            this.tagidentify_groupBox.Controls.Add(this.datalength_comboBox);
            this.tagidentify_groupBox.Controls.Add(this.datalength_label);
            this.tagidentify_groupBox.Controls.Add(this.dataaddress_comboBox);
            this.tagidentify_groupBox.Controls.Add(this.dataregion_comboBox);
            this.tagidentify_groupBox.Controls.Add(this.dataaddr_label);
            this.tagidentify_groupBox.Controls.Add(this.dataregion_label);
            this.tagidentify_groupBox.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagidentify_groupBox.Location = new System.Drawing.Point(285, 23);
            this.tagidentify_groupBox.Name = "tagidentify_groupBox";
            this.tagidentify_groupBox.Size = new System.Drawing.Size(708, 129);
            this.tagidentify_groupBox.TabIndex = 19;
            this.tagidentify_groupBox.TabStop = false;
            this.tagidentify_groupBox.Text = "标签识别";
            // 
            // write_textBox
            // 
            this.write_textBox.Location = new System.Drawing.Point(7, 72);
            this.write_textBox.Name = "write_textBox";
            this.write_textBox.Size = new System.Drawing.Size(224, 28);
            this.write_textBox.TabIndex = 14;
            this.write_textBox.Enter += new System.EventHandler(this.write_textBox_Enter);
            // 
            // write_button
            // 
            this.write_button.Location = new System.Drawing.Point(126, 29);
            this.write_button.Name = "write_button";
            this.write_button.Size = new System.Drawing.Size(95, 37);
            this.write_button.TabIndex = 9;
            this.write_button.Text = "写入";
            this.write_button.UseVisualStyleBackColor = true;
            this.write_button.Click += new System.EventHandler(this.write_button_Click);
            // 
            // read_button
            // 
            this.read_button.Location = new System.Drawing.Point(7, 31);
            this.read_button.Name = "read_button";
            this.read_button.Size = new System.Drawing.Size(95, 37);
            this.read_button.TabIndex = 8;
            this.read_button.Text = "读取";
            this.read_button.UseVisualStyleBackColor = true;
            this.read_button.Click += new System.EventHandler(this.read_button_Click);
            // 
            // datalength_comboBox
            // 
            this.datalength_comboBox.FormattingEnabled = true;
            this.datalength_comboBox.Location = new System.Drawing.Point(393, 66);
            this.datalength_comboBox.Name = "datalength_comboBox";
            this.datalength_comboBox.Size = new System.Drawing.Size(44, 29);
            this.datalength_comboBox.TabIndex = 7;
            // 
            // datalength_label
            // 
            this.datalength_label.AutoSize = true;
            this.datalength_label.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datalength_label.Location = new System.Drawing.Point(250, 72);
            this.datalength_label.Name = "datalength_label";
            this.datalength_label.Size = new System.Drawing.Size(74, 21);
            this.datalength_label.TabIndex = 6;
            this.datalength_label.Text = "数据单元";
            // 
            // dataaddress_comboBox
            // 
            this.dataaddress_comboBox.FormattingEnabled = true;
            this.dataaddress_comboBox.Location = new System.Drawing.Point(658, 48);
            this.dataaddress_comboBox.Name = "dataaddress_comboBox";
            this.dataaddress_comboBox.Size = new System.Drawing.Size(44, 29);
            this.dataaddress_comboBox.TabIndex = 3;
            this.dataaddress_comboBox.Visible = false;
            // 
            // dataregion_comboBox
            // 
            this.dataregion_comboBox.FormattingEnabled = true;
            this.dataregion_comboBox.Items.AddRange(new object[] {
            "RESERVE",
            "EPC",
            "TID",
            "USER"});
            this.dataregion_comboBox.Location = new System.Drawing.Point(393, 31);
            this.dataregion_comboBox.Name = "dataregion_comboBox";
            this.dataregion_comboBox.Size = new System.Drawing.Size(109, 29);
            this.dataregion_comboBox.TabIndex = 2;
            // 
            // dataaddr_label
            // 
            this.dataaddr_label.AutoSize = true;
            this.dataaddr_label.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataaddr_label.Location = new System.Drawing.Point(628, 24);
            this.dataaddr_label.Name = "dataaddr_label";
            this.dataaddr_label.Size = new System.Drawing.Size(74, 21);
            this.dataaddr_label.TabIndex = 1;
            this.dataaddr_label.Text = "起始地址";
            this.dataaddr_label.Visible = false;
            // 
            // dataregion_label
            // 
            this.dataregion_label.AutoSize = true;
            this.dataregion_label.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataregion_label.Location = new System.Drawing.Point(250, 34);
            this.dataregion_label.Name = "dataregion_label";
            this.dataregion_label.Size = new System.Drawing.Size(74, 21);
            this.dataregion_label.TabIndex = 0;
            this.dataregion_label.Text = "数据区域";
            // 
            // InsertAthlete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.panel_InsertAthlete);
            this.Controls.Add(this.panel_RFIDConfigure);
            this.Controls.Add(this.menuStrip_panel);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainMenuStrip = this.menuStrip_panel;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "InsertAthlete";
            this.Text = "竞赛管理信息系统—运动员管理";
            this.Load += new System.EventHandler(this.CheckIn_Load);
            this.menuStrip_panel.ResumeLayout(false);
            this.menuStrip_panel.PerformLayout();
            this.panel_InsertAthlete.ResumeLayout(false);
            this.groupBox_checkinfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_checkinfo)).EndInit();
            this.groupBox_project.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_projectinfo)).EndInit();
            this.groupBox_checkin.ResumeLayout(false);
            this.groupBox_checkin.PerformLayout();
            this.panel_RFIDConfigure.ResumeLayout(false);
            this.panel_RFIDConfigure.PerformLayout();
            this.connection_groupBox.ResumeLayout(false);
            this.connection_groupBox.PerformLayout();
            this.pf_groupBox.ResumeLayout(false);
            this.pf_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.power_trackBar)).EndInit();
            this.ant_groupBox.ResumeLayout(false);
            this.ant_groupBox.PerformLayout();
            this.tagidentify_groupBox.ResumeLayout(false);
            this.tagidentify_groupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip_panel;
        private System.Windows.Forms.ToolStripMenuItem InsertAthlete_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RFIDConfigure_ToolStripMenuItem;
        private System.Windows.Forms.Panel panel_InsertAthlete;
        private System.Windows.Forms.Panel panel_RFIDConfigure;
        private System.Windows.Forms.GroupBox connection_groupBox;
        private System.Windows.Forms.Button disconnect_button;
        private System.Windows.Forms.Button connect_button;
        private System.Windows.Forms.ComboBox boudrate_comboBox;
        private System.Windows.Forms.ComboBox commport_comboBox;
        private System.Windows.Forms.Label boudrate_label;
        private System.Windows.Forms.Label commport_label;
        private System.Windows.Forms.GroupBox pf_groupBox;
        private System.Windows.Forms.Button pfconfig_button;
        private System.Windows.Forms.Label freqtype_label;
        private System.Windows.Forms.ComboBox freqtype_comboBox;
        private System.Windows.Forms.TrackBar power_trackBar;
        private System.Windows.Forms.Button pfset_button;
        private System.Windows.Forms.Label power_label;
        private System.Windows.Forms.GroupBox ant_groupBox;
        private System.Windows.Forms.CheckBox ant3_checkBox;
        private System.Windows.Forms.Button antconfig_button;
        private System.Windows.Forms.CheckBox ant4_checkBox;
        private System.Windows.Forms.Button antset_button;
        private System.Windows.Forms.CheckBox ant2_checkBox;
        private System.Windows.Forms.CheckBox ant1_checkBox;
        private System.Windows.Forms.GroupBox tagidentify_groupBox;
        private System.Windows.Forms.TextBox write_textBox;
        private System.Windows.Forms.Button write_button;
        private System.Windows.Forms.Button read_button;
        private System.Windows.Forms.ComboBox datalength_comboBox;
        private System.Windows.Forms.Label datalength_label;
        private System.Windows.Forms.ComboBox dataaddress_comboBox;
        private System.Windows.Forms.ComboBox dataregion_comboBox;
        private System.Windows.Forms.Label dataaddr_label;
        private System.Windows.Forms.Label dataregion_label;
        private System.Windows.Forms.GroupBox groupBox_checkin;
        private System.Windows.Forms.TextBox project_textBox;
        private System.Windows.Forms.Label label_project;
        private System.Windows.Forms.Label label_playingfield;
        private System.Windows.Forms.ComboBox playingfield_comboBox;
        private System.Windows.Forms.GroupBox groupBox_project;
        private System.Windows.Forms.DataGridView dataGridView_projectinfo;
        private System.Windows.Forms.GroupBox groupBox_checkinfo;
        private System.Windows.Forms.DataGridView dataGridView_checkinfo;
        private System.Windows.Forms.TextBox contestant_textBox;
        private System.Windows.Forms.TextBox name_textBox;
        private System.Windows.Forms.Label label_contestant;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.Button abstention_button;
        private System.Windows.Forms.DataGridViewTextBoxColumn AtheleteName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContestantName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AthleteOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn AthleteStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AthletesNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProjectOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn FieldId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
    }
}

