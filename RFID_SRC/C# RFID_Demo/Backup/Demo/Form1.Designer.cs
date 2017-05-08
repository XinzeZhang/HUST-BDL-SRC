namespace Demo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lInfo = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bRs232Discon = new System.Windows.Forms.Button();
            this.bRs232Con = new System.Windows.Forms.Button();
            this.cBaudrate = new System.Windows.Forms.ComboBox();
            this.cCommPort = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tPort = new System.Windows.Forms.TextBox();
            this.tIp = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bTcpDiscon = new System.Windows.Forms.Button();
            this.bTcpCon = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tRfPwr = new System.Windows.Forms.TrackBar();
            this.cRfFreq = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bRfQuery = new System.Windows.Forms.Button();
            this.bRfSet = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ant4 = new System.Windows.Forms.CheckBox();
            this.ant3 = new System.Windows.Forms.CheckBox();
            this.ant2 = new System.Windows.Forms.CheckBox();
            this.ant1 = new System.Windows.Forms.CheckBox();
            this.bAntQuery = new System.Windows.Forms.Button();
            this.bAntSet = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.tIsoData = new System.Windows.Forms.TextBox();
            this.tIsoCnt = new System.Windows.Forms.TextBox();
            this.tIsoAddr = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tIsoSpeed = new System.Windows.Forms.TrackBar();
            this.cIsoTimes = new System.Windows.Forms.ComboBox();
            this.bIsoQueryLock = new System.Windows.Forms.Button();
            this.bIsoLock = new System.Windows.Forms.Button();
            this.bIsoWrite = new System.Windows.Forms.Button();
            this.bIsoRead = new System.Windows.Forms.Button();
            this.bIsoId = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnSetAccessPWD = new System.Windows.Forms.Button();
            this.bEpcInit = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tEpcAccess = new System.Windows.Forms.TextBox();
            this.tEpcKill = new System.Windows.Forms.TextBox();
            this.tEpcData = new System.Windows.Forms.TextBox();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.cEpcWordcnt = new System.Windows.Forms.ComboBox();
            this.cEpcWordptr = new System.Windows.Forms.ComboBox();
            this.cEpcMembank = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.tEpcSpeed = new System.Windows.Forms.TrackBar();
            this.cEpcTimes = new System.Windows.Forms.ComboBox();
            this.bEpcKill = new System.Windows.Forms.Button();
            this.btnSecLock = new System.Windows.Forms.Button();
            this.btnSecWrite = new System.Windows.Forms.Button();
            this.bEpcWrite = new System.Windows.Forms.Button();
            this.btnSecRead = new System.Windows.Forms.Button();
            this.bEpcRead = new System.Windows.Forms.Button();
            this.bEpcId = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.bClear = new System.Windows.Forms.Button();
            this.bReset = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnISOReadWithID = new System.Windows.Forms.Button();
            this.btnISOWriteWithID = new System.Windows.Forms.Button();
            this.txtTagID = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tRfPwr)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tIsoSpeed)).BeginInit();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tEpcSpeed)).BeginInit();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // lInfo
            // 
            this.lInfo.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lInfo.FormattingEnabled = true;
            this.lInfo.HorizontalScrollbar = true;
            this.lInfo.ItemHeight = 16;
            this.lInfo.Location = new System.Drawing.Point(6, 23);
            this.lInfo.Name = "lInfo";
            this.lInfo.ScrollAlwaysVisible = true;
            this.lInfo.Size = new System.Drawing.Size(345, 628);
            this.lInfo.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.bRs232Discon);
            this.groupBox1.Controls.Add(this.bRs232Con);
            this.groupBox1.Controls.Add(this.cBaudrate);
            this.groupBox1.Controls.Add(this.cCommPort);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 146);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "RS232";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 7;
            this.label2.Text = "BAUDRATE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "COMM PORT";
            // 
            // bRs232Discon
            // 
            this.bRs232Discon.BackColor = System.Drawing.Color.SlateGray;
            this.bRs232Discon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bRs232Discon.ForeColor = System.Drawing.Color.Gold;
            this.bRs232Discon.Location = new System.Drawing.Point(133, 100);
            this.bRs232Discon.Name = "bRs232Discon";
            this.bRs232Discon.Size = new System.Drawing.Size(77, 40);
            this.bRs232Discon.TabIndex = 5;
            this.bRs232Discon.Text = "Discon";
            this.bRs232Discon.UseVisualStyleBackColor = false;
            this.bRs232Discon.Click += new System.EventHandler(this.bRs232Discon_Click);
            // 
            // bRs232Con
            // 
            this.bRs232Con.BackColor = System.Drawing.Color.SlateGray;
            this.bRs232Con.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bRs232Con.ForeColor = System.Drawing.Color.Gold;
            this.bRs232Con.Location = new System.Drawing.Point(35, 100);
            this.bRs232Con.Name = "bRs232Con";
            this.bRs232Con.Size = new System.Drawing.Size(77, 40);
            this.bRs232Con.TabIndex = 4;
            this.bRs232Con.Text = "Connect";
            this.bRs232Con.UseVisualStyleBackColor = false;
            this.bRs232Con.Click += new System.EventHandler(this.bRs232Con_Click);
            // 
            // cBaudrate
            // 
            this.cBaudrate.FormattingEnabled = true;
            this.cBaudrate.Items.AddRange(new object[] {
            "9600",
            "19200",
            "38400",
            "57600",
            "115200"});
            this.cBaudrate.Location = new System.Drawing.Point(123, 62);
            this.cBaudrate.Name = "cBaudrate";
            this.cBaudrate.Size = new System.Drawing.Size(87, 22);
            this.cBaudrate.TabIndex = 3;
            // 
            // cCommPort
            // 
            this.cCommPort.FormattingEnabled = true;
            this.cCommPort.Items.AddRange(new object[] {
            "COM1:",
            "COM2:",
            "COM3:",
            "COM4",
            "COM5:",
            "COM6:",
            "COM7:",
            "COM8:",
            "COM9:",
            "COM10:",
            "COM11:",
            "COM12:",
            "COM13:",
            "COM14:",
            "COM15:",
            "COM16:",
            "COM17:",
            "COM18:",
            "COM19:",
            "COM20:"});
            this.cCommPort.Location = new System.Drawing.Point(123, 28);
            this.cCommPort.Name = "cCommPort";
            this.cCommPort.Size = new System.Drawing.Size(87, 22);
            this.cCommPort.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tPort);
            this.groupBox2.Controls.Add(this.tIp);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.bTcpDiscon);
            this.groupBox2.Controls.Add(this.bTcpCon);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Location = new System.Drawing.Point(12, 176);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(237, 155);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "TCP/IP";
            // 
            // tPort
            // 
            this.tPort.Location = new System.Drawing.Point(103, 62);
            this.tPort.Name = "tPort";
            this.tPort.Size = new System.Drawing.Size(64, 23);
            this.tPort.TabIndex = 9;
            // 
            // tIp
            // 
            this.tIp.Location = new System.Drawing.Point(103, 27);
            this.tIp.Name = "tIp";
            this.tIp.Size = new System.Drawing.Size(122, 23);
            this.tIp.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 14);
            this.label3.TabIndex = 7;
            this.label3.Text = "PORT";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP ADDRESS";
            // 
            // bTcpDiscon
            // 
            this.bTcpDiscon.BackColor = System.Drawing.Color.SlateGray;
            this.bTcpDiscon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bTcpDiscon.ForeColor = System.Drawing.Color.Gold;
            this.bTcpDiscon.Location = new System.Drawing.Point(133, 100);
            this.bTcpDiscon.Name = "bTcpDiscon";
            this.bTcpDiscon.Size = new System.Drawing.Size(77, 40);
            this.bTcpDiscon.TabIndex = 5;
            this.bTcpDiscon.Text = "Discon";
            this.bTcpDiscon.UseVisualStyleBackColor = false;
            this.bTcpDiscon.Click += new System.EventHandler(this.bTcpDiscon_Click);
            // 
            // bTcpCon
            // 
            this.bTcpCon.BackColor = System.Drawing.Color.SlateGray;
            this.bTcpCon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bTcpCon.ForeColor = System.Drawing.Color.Gold;
            this.bTcpCon.Location = new System.Drawing.Point(35, 100);
            this.bTcpCon.Name = "bTcpCon";
            this.bTcpCon.Size = new System.Drawing.Size(77, 40);
            this.bTcpCon.TabIndex = 4;
            this.bTcpCon.Text = "Connect";
            this.bTcpCon.UseVisualStyleBackColor = false;
            this.bTcpCon.Click += new System.EventHandler(this.bTcpCon_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tRfPwr);
            this.groupBox3.Controls.Add(this.cRfFreq);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.bRfQuery);
            this.groupBox3.Controls.Add(this.bRfSet);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox3.Location = new System.Drawing.Point(12, 346);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(237, 153);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "RF Setting";
            // 
            // tRfPwr
            // 
            this.tRfPwr.Location = new System.Drawing.Point(66, 34);
            this.tRfPwr.Maximum = 30;
            this.tRfPwr.Name = "tRfPwr";
            this.tRfPwr.Size = new System.Drawing.Size(159, 45);
            this.tRfPwr.TabIndex = 9;
            // 
            // cRfFreq
            // 
            this.cRfFreq.FormattingEnabled = true;
            this.cRfFreq.Items.AddRange(new object[] {
            "CHINA",
            "AMERICA",
            "EUROUP",
            "OTHER"});
            this.cRfFreq.Location = new System.Drawing.Point(123, 79);
            this.cRfFreq.Name = "cRfFreq";
            this.cRfFreq.Size = new System.Drawing.Size(87, 22);
            this.cRfFreq.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "FREQ TYPE";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(18, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 14);
            this.label6.TabIndex = 6;
            this.label6.Text = "POWER";
            // 
            // bRfQuery
            // 
            this.bRfQuery.BackColor = System.Drawing.Color.SlateGray;
            this.bRfQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bRfQuery.ForeColor = System.Drawing.Color.Gold;
            this.bRfQuery.Location = new System.Drawing.Point(133, 115);
            this.bRfQuery.Name = "bRfQuery";
            this.bRfQuery.Size = new System.Drawing.Size(77, 26);
            this.bRfQuery.TabIndex = 5;
            this.bRfQuery.Text = "Query";
            this.bRfQuery.UseVisualStyleBackColor = false;
            this.bRfQuery.Click += new System.EventHandler(this.bRfQuery_Click);
            // 
            // bRfSet
            // 
            this.bRfSet.BackColor = System.Drawing.Color.SlateGray;
            this.bRfSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bRfSet.ForeColor = System.Drawing.Color.Gold;
            this.bRfSet.Location = new System.Drawing.Point(35, 115);
            this.bRfSet.Name = "bRfSet";
            this.bRfSet.Size = new System.Drawing.Size(77, 26);
            this.bRfSet.TabIndex = 4;
            this.bRfSet.Text = "Set";
            this.bRfSet.UseVisualStyleBackColor = false;
            this.bRfSet.Click += new System.EventHandler(this.bRfSet_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ant4);
            this.groupBox4.Controls.Add(this.ant3);
            this.groupBox4.Controls.Add(this.ant2);
            this.groupBox4.Controls.Add(this.ant1);
            this.groupBox4.Controls.Add(this.bAntQuery);
            this.groupBox4.Controls.Add(this.bAntSet);
            this.groupBox4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Info;
            this.groupBox4.Location = new System.Drawing.Point(12, 522);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(237, 162);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ANT Setting";
            // 
            // ant4
            // 
            this.ant4.AutoSize = true;
            this.ant4.Location = new System.Drawing.Point(143, 80);
            this.ant4.Name = "ant4";
            this.ant4.Size = new System.Drawing.Size(55, 18);
            this.ant4.TabIndex = 9;
            this.ant4.Text = "ANT4";
            this.ant4.UseVisualStyleBackColor = true;
            // 
            // ant3
            // 
            this.ant3.AutoSize = true;
            this.ant3.Location = new System.Drawing.Point(41, 80);
            this.ant3.Name = "ant3";
            this.ant3.Size = new System.Drawing.Size(55, 18);
            this.ant3.TabIndex = 8;
            this.ant3.Text = "ANT3";
            this.ant3.UseVisualStyleBackColor = true;
            // 
            // ant2
            // 
            this.ant2.AutoSize = true;
            this.ant2.Location = new System.Drawing.Point(143, 41);
            this.ant2.Name = "ant2";
            this.ant2.Size = new System.Drawing.Size(55, 18);
            this.ant2.TabIndex = 7;
            this.ant2.Text = "ANT2";
            this.ant2.UseVisualStyleBackColor = true;
            // 
            // ant1
            // 
            this.ant1.AutoSize = true;
            this.ant1.Location = new System.Drawing.Point(41, 41);
            this.ant1.Name = "ant1";
            this.ant1.Size = new System.Drawing.Size(55, 18);
            this.ant1.TabIndex = 6;
            this.ant1.Text = "ANT1";
            this.ant1.UseVisualStyleBackColor = true;
            // 
            // bAntQuery
            // 
            this.bAntQuery.BackColor = System.Drawing.Color.SlateGray;
            this.bAntQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bAntQuery.ForeColor = System.Drawing.Color.Gold;
            this.bAntQuery.Location = new System.Drawing.Point(133, 115);
            this.bAntQuery.Name = "bAntQuery";
            this.bAntQuery.Size = new System.Drawing.Size(77, 26);
            this.bAntQuery.TabIndex = 5;
            this.bAntQuery.Text = "Query";
            this.bAntQuery.UseVisualStyleBackColor = false;
            this.bAntQuery.Click += new System.EventHandler(this.bAntQuery_Click);
            // 
            // bAntSet
            // 
            this.bAntSet.BackColor = System.Drawing.Color.SlateGray;
            this.bAntSet.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bAntSet.ForeColor = System.Drawing.Color.Gold;
            this.bAntSet.Location = new System.Drawing.Point(35, 115);
            this.bAntSet.Name = "bAntSet";
            this.bAntSet.Size = new System.Drawing.Size(77, 26);
            this.bAntSet.TabIndex = 4;
            this.bAntSet.Text = "Set";
            this.bAntSet.UseVisualStyleBackColor = false;
            this.bAntSet.Click += new System.EventHandler(this.bAntSet_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.CornflowerBlue;
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txtTagID);
            this.groupBox5.Controls.Add(this.tIsoData);
            this.groupBox5.Controls.Add(this.tIsoCnt);
            this.groupBox5.Controls.Add(this.tIsoAddr);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.tIsoSpeed);
            this.groupBox5.Controls.Add(this.cIsoTimes);
            this.groupBox5.Controls.Add(this.bIsoQueryLock);
            this.groupBox5.Controls.Add(this.bIsoLock);
            this.groupBox5.Controls.Add(this.btnISOWriteWithID);
            this.groupBox5.Controls.Add(this.bIsoWrite);
            this.groupBox5.Controls.Add(this.btnISOReadWithID);
            this.groupBox5.Controls.Add(this.bIsoRead);
            this.groupBox5.Controls.Add(this.bIsoId);
            this.groupBox5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.groupBox5.Location = new System.Drawing.Point(276, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(356, 276);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "ISO18000-6B";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(16, 244);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(80, 16);
            this.label13.TabIndex = 21;
            this.label13.Text = "Data(Hex)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(16, 161);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(64, 16);
            this.label12.TabIndex = 20;
            this.label12.Text = "ByteCnt";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(16, 120);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 19;
            this.label11.Text = "ByteAddr";
            // 
            // tIsoData
            // 
            this.tIsoData.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tIsoData.Location = new System.Drawing.Point(99, 244);
            this.tIsoData.Name = "tIsoData";
            this.tIsoData.Size = new System.Drawing.Size(125, 21);
            this.tIsoData.TabIndex = 18;
            this.tIsoData.TextChanged += new System.EventHandler(this.tIsoData_TextChanged);
            // 
            // tIsoCnt
            // 
            this.tIsoCnt.Location = new System.Drawing.Point(99, 162);
            this.tIsoCnt.Name = "tIsoCnt";
            this.tIsoCnt.Size = new System.Drawing.Size(72, 26);
            this.tIsoCnt.TabIndex = 17;
            // 
            // tIsoAddr
            // 
            this.tIsoAddr.Location = new System.Drawing.Point(99, 120);
            this.tIsoAddr.Name = "tIsoAddr";
            this.tIsoAddr.Size = new System.Drawing.Size(72, 26);
            this.tIsoAddr.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(10, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 15;
            this.label8.Text = "Times";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(10, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 20);
            this.label7.TabIndex = 14;
            this.label7.Text = "Speed";
            // 
            // tIsoSpeed
            // 
            this.tIsoSpeed.Location = new System.Drawing.Point(69, 31);
            this.tIsoSpeed.Name = "tIsoSpeed";
            this.tIsoSpeed.Size = new System.Drawing.Size(155, 45);
            this.tIsoSpeed.TabIndex = 13;
            // 
            // cIsoTimes
            // 
            this.cIsoTimes.FormattingEnabled = true;
            this.cIsoTimes.Items.AddRange(new object[] {
            "Continours",
            "1",
            "10",
            "100",
            "500",
            "1000",
            "10000"});
            this.cIsoTimes.Location = new System.Drawing.Point(121, 82);
            this.cIsoTimes.Name = "cIsoTimes";
            this.cIsoTimes.Size = new System.Drawing.Size(87, 24);
            this.cIsoTimes.TabIndex = 10;
            // 
            // bIsoQueryLock
            // 
            this.bIsoQueryLock.BackColor = System.Drawing.Color.SlateGray;
            this.bIsoQueryLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bIsoQueryLock.ForeColor = System.Drawing.Color.Gold;
            this.bIsoQueryLock.Location = new System.Drawing.Point(248, 244);
            this.bIsoQueryLock.Name = "bIsoQueryLock";
            this.bIsoQueryLock.Size = new System.Drawing.Size(90, 26);
            this.bIsoQueryLock.TabIndex = 9;
            this.bIsoQueryLock.Text = "Qry Lock";
            this.bIsoQueryLock.UseVisualStyleBackColor = false;
            // 
            // bIsoLock
            // 
            this.bIsoLock.BackColor = System.Drawing.Color.SlateGray;
            this.bIsoLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bIsoLock.ForeColor = System.Drawing.Color.Gold;
            this.bIsoLock.Location = new System.Drawing.Point(248, 212);
            this.bIsoLock.Name = "bIsoLock";
            this.bIsoLock.Size = new System.Drawing.Size(90, 26);
            this.bIsoLock.TabIndex = 8;
            this.bIsoLock.Text = "Lock";
            this.bIsoLock.UseVisualStyleBackColor = false;
            this.bIsoLock.Click += new System.EventHandler(this.bIsoLock_Click);
            // 
            // bIsoWrite
            // 
            this.bIsoWrite.BackColor = System.Drawing.Color.SlateGray;
            this.bIsoWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bIsoWrite.ForeColor = System.Drawing.Color.Gold;
            this.bIsoWrite.Location = new System.Drawing.Point(248, 146);
            this.bIsoWrite.Name = "bIsoWrite";
            this.bIsoWrite.Size = new System.Drawing.Size(90, 26);
            this.bIsoWrite.TabIndex = 7;
            this.bIsoWrite.Text = "Write";
            this.bIsoWrite.UseVisualStyleBackColor = false;
            this.bIsoWrite.Click += new System.EventHandler(this.bIsoWrite_Click);
            // 
            // bIsoRead
            // 
            this.bIsoRead.BackColor = System.Drawing.Color.SlateGray;
            this.bIsoRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bIsoRead.ForeColor = System.Drawing.Color.Gold;
            this.bIsoRead.Location = new System.Drawing.Point(248, 82);
            this.bIsoRead.Name = "bIsoRead";
            this.bIsoRead.Size = new System.Drawing.Size(90, 26);
            this.bIsoRead.TabIndex = 6;
            this.bIsoRead.Text = "Read";
            this.bIsoRead.UseVisualStyleBackColor = false;
            this.bIsoRead.Click += new System.EventHandler(this.bIsoRead_Click);
            // 
            // bIsoId
            // 
            this.bIsoId.BackColor = System.Drawing.Color.SlateGray;
            this.bIsoId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bIsoId.ForeColor = System.Drawing.Color.Gold;
            this.bIsoId.Location = new System.Drawing.Point(248, 23);
            this.bIsoId.Name = "bIsoId";
            this.bIsoId.Size = new System.Drawing.Size(90, 46);
            this.bIsoId.TabIndex = 5;
            this.bIsoId.Text = "Identify";
            this.bIsoId.UseVisualStyleBackColor = false;
            this.bIsoId.Click += new System.EventHandler(this.bIsoId_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnSetAccessPWD);
            this.groupBox6.Controls.Add(this.bEpcInit);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.tEpcAccess);
            this.groupBox6.Controls.Add(this.tEpcKill);
            this.groupBox6.Controls.Add(this.tEpcData);
            this.groupBox6.Controls.Add(this.cmbLevel);
            this.groupBox6.Controls.Add(this.cEpcWordcnt);
            this.groupBox6.Controls.Add(this.cEpcWordptr);
            this.groupBox6.Controls.Add(this.cEpcMembank);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.tEpcSpeed);
            this.groupBox6.Controls.Add(this.cEpcTimes);
            this.groupBox6.Controls.Add(this.bEpcKill);
            this.groupBox6.Controls.Add(this.btnSecLock);
            this.groupBox6.Controls.Add(this.btnSecWrite);
            this.groupBox6.Controls.Add(this.bEpcWrite);
            this.groupBox6.Controls.Add(this.btnSecRead);
            this.groupBox6.Controls.Add(this.bEpcRead);
            this.groupBox6.Controls.Add(this.bEpcId);
            this.groupBox6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.groupBox6.Location = new System.Drawing.Point(274, 304);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(358, 399);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "ISO18000-6C(EPC G2)";
            // 
            // btnSetAccessPWD
            // 
            this.btnSetAccessPWD.BackColor = System.Drawing.Color.SlateGray;
            this.btnSetAccessPWD.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetAccessPWD.ForeColor = System.Drawing.Color.Gold;
            this.btnSetAccessPWD.Location = new System.Drawing.Point(16, 365);
            this.btnSetAccessPWD.Name = "btnSetAccessPWD";
            this.btnSetAccessPWD.Size = new System.Drawing.Size(157, 26);
            this.btnSetAccessPWD.TabIndex = 29;
            this.btnSetAccessPWD.Text = "Set Password";
            this.btnSetAccessPWD.UseVisualStyleBackColor = false;
            this.btnSetAccessPWD.Click += new System.EventHandler(this.btnSetAccessPWD_Click);
            // 
            // bEpcInit
            // 
            this.bEpcInit.BackColor = System.Drawing.Color.SlateGray;
            this.bEpcInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bEpcInit.ForeColor = System.Drawing.Color.Gold;
            this.bEpcInit.Location = new System.Drawing.Point(250, 156);
            this.bEpcInit.Name = "bEpcInit";
            this.bEpcInit.Size = new System.Drawing.Size(90, 26);
            this.bEpcInit.TabIndex = 29;
            this.bEpcInit.Text = "Init";
            this.bEpcInit.UseVisualStyleBackColor = false;
            this.bEpcInit.Click += new System.EventHandler(this.bEpcInit_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label19.Location = new System.Drawing.Point(10, 334);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(112, 16);
            this.label19.TabIndex = 28;
            this.label19.Text = "Kill Password";
            this.label19.Visible = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(8, 298);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(120, 16);
            this.label20.TabIndex = 27;
            this.label20.Text = "Password Level";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(8, 262);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(128, 16);
            this.label18.TabIndex = 27;
            this.label18.Text = "Access Password";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(10, 221);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(80, 16);
            this.label17.TabIndex = 26;
            this.label17.Text = "Data(Hex)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(8, 125);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 16);
            this.label16.TabIndex = 25;
            this.label16.Text = "MemBank";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(10, 195);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(64, 16);
            this.label15.TabIndex = 24;
            this.label15.Text = "WordCnt";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(8, 160);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(64, 16);
            this.label14.TabIndex = 22;
            this.label14.Text = "WordPtr";
            // 
            // tEpcAccess
            // 
            this.tEpcAccess.Location = new System.Drawing.Point(139, 261);
            this.tEpcAccess.MaxLength = 8;
            this.tEpcAccess.Name = "tEpcAccess";
            this.tEpcAccess.Size = new System.Drawing.Size(87, 26);
            this.tEpcAccess.TabIndex = 23;
            // 
            // tEpcKill
            // 
            this.tEpcKill.Location = new System.Drawing.Point(139, 333);
            this.tEpcKill.Name = "tEpcKill";
            this.tEpcKill.Size = new System.Drawing.Size(87, 26);
            this.tEpcKill.TabIndex = 22;
            this.tEpcKill.Visible = false;
            // 
            // tEpcData
            // 
            this.tEpcData.Location = new System.Drawing.Point(97, 221);
            this.tEpcData.Name = "tEpcData";
            this.tEpcData.Size = new System.Drawing.Size(125, 26);
            this.tEpcData.TabIndex = 21;
            this.tEpcData.TextChanged += new System.EventHandler(this.tEpcData_TextChanged);
            // 
            // cmbLevel
            // 
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "00",
            "01",
            "10",
            "11"});
            this.cmbLevel.Location = new System.Drawing.Point(139, 297);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(87, 24);
            this.cmbLevel.TabIndex = 20;
            // 
            // cEpcWordcnt
            // 
            this.cEpcWordcnt.FormattingEnabled = true;
            this.cEpcWordcnt.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8"});
            this.cEpcWordcnt.Location = new System.Drawing.Point(97, 191);
            this.cEpcWordcnt.Name = "cEpcWordcnt";
            this.cEpcWordcnt.Size = new System.Drawing.Size(87, 24);
            this.cEpcWordcnt.TabIndex = 20;
            // 
            // cEpcWordptr
            // 
            this.cEpcWordptr.FormattingEnabled = true;
            this.cEpcWordptr.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7"});
            this.cEpcWordptr.Location = new System.Drawing.Point(97, 157);
            this.cEpcWordptr.Name = "cEpcWordptr";
            this.cEpcWordptr.Size = new System.Drawing.Size(87, 24);
            this.cEpcWordptr.TabIndex = 19;
            // 
            // cEpcMembank
            // 
            this.cEpcMembank.FormattingEnabled = true;
            this.cEpcMembank.Items.AddRange(new object[] {
            "RESERVE",
            "EPC",
            "TID",
            "USER"});
            this.cEpcMembank.Location = new System.Drawing.Point(97, 125);
            this.cEpcMembank.Name = "cEpcMembank";
            this.cEpcMembank.Size = new System.Drawing.Size(87, 24);
            this.cEpcMembank.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(18, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 16);
            this.label10.TabIndex = 17;
            this.label10.Text = "Times";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(16, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 16);
            this.label9.TabIndex = 16;
            this.label9.Text = "Speed";
            // 
            // tEpcSpeed
            // 
            this.tEpcSpeed.Location = new System.Drawing.Point(71, 40);
            this.tEpcSpeed.Name = "tEpcSpeed";
            this.tEpcSpeed.Size = new System.Drawing.Size(155, 45);
            this.tEpcSpeed.TabIndex = 15;
            // 
            // cEpcTimes
            // 
            this.cEpcTimes.FormattingEnabled = true;
            this.cEpcTimes.Items.AddRange(new object[] {
            "Continours",
            "1",
            "10",
            "100",
            "1000",
            "5000"});
            this.cEpcTimes.Location = new System.Drawing.Point(97, 86);
            this.cEpcTimes.Name = "cEpcTimes";
            this.cEpcTimes.Size = new System.Drawing.Size(87, 24);
            this.cEpcTimes.TabIndex = 14;
            // 
            // bEpcKill
            // 
            this.bEpcKill.BackColor = System.Drawing.Color.SlateGray;
            this.bEpcKill.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bEpcKill.ForeColor = System.Drawing.Color.Gold;
            this.bEpcKill.Location = new System.Drawing.Point(250, 188);
            this.bEpcKill.Name = "bEpcKill";
            this.bEpcKill.Size = new System.Drawing.Size(90, 26);
            this.bEpcKill.TabIndex = 9;
            this.bEpcKill.Text = "Kill";
            this.bEpcKill.UseVisualStyleBackColor = false;
            this.bEpcKill.Click += new System.EventHandler(this.bEpcKill_Click);
            // 
            // btnSecLock
            // 
            this.btnSecLock.BackColor = System.Drawing.Color.SlateGray;
            this.btnSecLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSecLock.ForeColor = System.Drawing.Color.Gold;
            this.btnSecLock.Location = new System.Drawing.Point(250, 333);
            this.btnSecLock.Name = "btnSecLock";
            this.btnSecLock.Size = new System.Drawing.Size(90, 26);
            this.btnSecLock.TabIndex = 8;
            this.btnSecLock.Text = "Sec Lock";
            this.btnSecLock.UseVisualStyleBackColor = false;
            this.btnSecLock.Click += new System.EventHandler(this.btnSecLock_Click);
            // 
            // btnSecWrite
            // 
            this.btnSecWrite.BackColor = System.Drawing.Color.SlateGray;
            this.btnSecWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSecWrite.ForeColor = System.Drawing.Color.Gold;
            this.btnSecWrite.Location = new System.Drawing.Point(250, 295);
            this.btnSecWrite.Name = "btnSecWrite";
            this.btnSecWrite.Size = new System.Drawing.Size(90, 26);
            this.btnSecWrite.TabIndex = 7;
            this.btnSecWrite.Text = "Sec Write";
            this.btnSecWrite.UseVisualStyleBackColor = false;
            this.btnSecWrite.Click += new System.EventHandler(this.btnSecWrite_Click);
            // 
            // bEpcWrite
            // 
            this.bEpcWrite.BackColor = System.Drawing.Color.SlateGray;
            this.bEpcWrite.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bEpcWrite.ForeColor = System.Drawing.Color.Gold;
            this.bEpcWrite.Location = new System.Drawing.Point(250, 124);
            this.bEpcWrite.Name = "bEpcWrite";
            this.bEpcWrite.Size = new System.Drawing.Size(90, 26);
            this.bEpcWrite.TabIndex = 7;
            this.bEpcWrite.Text = "Write";
            this.bEpcWrite.UseVisualStyleBackColor = false;
            this.bEpcWrite.Click += new System.EventHandler(this.bEpcWrite_Click);
            // 
            // btnSecRead
            // 
            this.btnSecRead.BackColor = System.Drawing.Color.SlateGray;
            this.btnSecRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSecRead.ForeColor = System.Drawing.Color.Gold;
            this.btnSecRead.Location = new System.Drawing.Point(250, 259);
            this.btnSecRead.Name = "btnSecRead";
            this.btnSecRead.Size = new System.Drawing.Size(90, 26);
            this.btnSecRead.TabIndex = 6;
            this.btnSecRead.Text = "Sec Read";
            this.btnSecRead.UseVisualStyleBackColor = false;
            this.btnSecRead.Click += new System.EventHandler(this.btnSecRead_Click);
            // 
            // bEpcRead
            // 
            this.bEpcRead.BackColor = System.Drawing.Color.SlateGray;
            this.bEpcRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bEpcRead.ForeColor = System.Drawing.Color.Gold;
            this.bEpcRead.Location = new System.Drawing.Point(250, 88);
            this.bEpcRead.Name = "bEpcRead";
            this.bEpcRead.Size = new System.Drawing.Size(90, 26);
            this.bEpcRead.TabIndex = 6;
            this.bEpcRead.Text = "Read";
            this.bEpcRead.UseVisualStyleBackColor = false;
            this.bEpcRead.Click += new System.EventHandler(this.bEpcRead_Click);
            // 
            // bEpcId
            // 
            this.bEpcId.BackColor = System.Drawing.Color.SlateGray;
            this.bEpcId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bEpcId.ForeColor = System.Drawing.Color.Gold;
            this.bEpcId.Location = new System.Drawing.Point(250, 25);
            this.bEpcId.Name = "bEpcId";
            this.bEpcId.Size = new System.Drawing.Size(90, 51);
            this.bEpcId.TabIndex = 5;
            this.bEpcId.Text = "Identify";
            this.bEpcId.UseVisualStyleBackColor = false;
            this.bEpcId.Click += new System.EventHandler(this.bEpcId_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.bClear);
            this.groupBox7.Controls.Add(this.lInfo);
            this.groupBox7.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.groupBox7.Location = new System.Drawing.Point(647, 12);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(357, 691);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Opration Info.";
            // 
            // bClear
            // 
            this.bClear.BackColor = System.Drawing.Color.SlateGray;
            this.bClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bClear.ForeColor = System.Drawing.Color.Gold;
            this.bClear.Location = new System.Drawing.Point(264, 657);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(77, 26);
            this.bClear.TabIndex = 11;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = false;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // bReset
            // 
            this.bReset.BackColor = System.Drawing.Color.SlateGray;
            this.bReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.bReset.ForeColor = System.Drawing.Color.Gold;
            this.bReset.Location = new System.Drawing.Point(47, 690);
            this.bReset.Name = "bReset";
            this.bReset.Size = new System.Drawing.Size(169, 26);
            this.bReset.TabIndex = 8;
            this.bReset.Text = "Reset Reader";
            this.bReset.UseVisualStyleBackColor = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnISOReadWithID
            // 
            this.btnISOReadWithID.BackColor = System.Drawing.Color.SlateGray;
            this.btnISOReadWithID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnISOReadWithID.ForeColor = System.Drawing.Color.Gold;
            this.btnISOReadWithID.Location = new System.Drawing.Point(248, 114);
            this.btnISOReadWithID.Name = "btnISOReadWithID";
            this.btnISOReadWithID.Size = new System.Drawing.Size(90, 26);
            this.btnISOReadWithID.TabIndex = 6;
            this.btnISOReadWithID.Text = "Read With ID";
            this.btnISOReadWithID.UseVisualStyleBackColor = false;
            this.btnISOReadWithID.Click += new System.EventHandler(this.btnISOReadWithID_Click);
            // 
            // btnISOWriteWithID
            // 
            this.btnISOWriteWithID.BackColor = System.Drawing.Color.SlateGray;
            this.btnISOWriteWithID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnISOWriteWithID.ForeColor = System.Drawing.Color.Gold;
            this.btnISOWriteWithID.Location = new System.Drawing.Point(248, 177);
            this.btnISOWriteWithID.Name = "btnISOWriteWithID";
            this.btnISOWriteWithID.Size = new System.Drawing.Size(90, 26);
            this.btnISOWriteWithID.TabIndex = 7;
            this.btnISOWriteWithID.Text = "Write With ID";
            this.btnISOWriteWithID.UseVisualStyleBackColor = false;
            this.btnISOWriteWithID.Click += new System.EventHandler(this.btnISOWriteWithID_Click);
            // 
            // txtTagID
            // 
            this.txtTagID.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTagID.Location = new System.Drawing.Point(99, 195);
            this.txtTagID.Name = "txtTagID";
            this.txtTagID.Size = new System.Drawing.Size(125, 21);
            this.txtTagID.TabIndex = 18;
            this.txtTagID.TextChanged += new System.EventHandler(this.tIsoData_TextChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.Location = new System.Drawing.Point(16, 195);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 16);
            this.label21.TabIndex = 21;
            this.label21.Text = "Tag ID";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(1016, 734);
            this.Controls.Add(this.bReset);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox7);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tRfPwr)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tIsoSpeed)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tEpcSpeed)).EndInit();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bRs232Con;
        private System.Windows.Forms.ComboBox cBaudrate;
        private System.Windows.Forms.ComboBox cCommPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bRs232Discon;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tPort;
        private System.Windows.Forms.TextBox tIp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bTcpDiscon;
        private System.Windows.Forms.Button bTcpCon;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bRfQuery;
        private System.Windows.Forms.Button bRfSet;
        private System.Windows.Forms.ComboBox cRfFreq;
        private System.Windows.Forms.TrackBar tRfPwr;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ant4;
        private System.Windows.Forms.CheckBox ant3;
        private System.Windows.Forms.CheckBox ant2;
        private System.Windows.Forms.CheckBox ant1;
        private System.Windows.Forms.Button bAntQuery;
        private System.Windows.Forms.Button bAntSet;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button bIsoQueryLock;
        private System.Windows.Forms.Button bIsoLock;
        private System.Windows.Forms.Button bIsoWrite;
        private System.Windows.Forms.Button bIsoRead;
        private System.Windows.Forms.Button bIsoId;
        private System.Windows.Forms.Button bEpcKill;
        private System.Windows.Forms.Button bEpcWrite;
        private System.Windows.Forms.Button bEpcRead;
        private System.Windows.Forms.Button bEpcId;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.TrackBar tIsoSpeed;
        private System.Windows.Forms.ComboBox cIsoTimes;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tIsoData;
        private System.Windows.Forms.TextBox tIsoCnt;
        private System.Windows.Forms.TextBox tIsoAddr;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar tEpcSpeed;
        private System.Windows.Forms.ComboBox cEpcTimes;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tEpcAccess;
        private System.Windows.Forms.TextBox tEpcKill;
        private System.Windows.Forms.TextBox tEpcData;
        private System.Windows.Forms.ComboBox cEpcWordcnt;
        private System.Windows.Forms.ComboBox cEpcWordptr;
        private System.Windows.Forms.ComboBox cEpcMembank;
        private System.Windows.Forms.Button bReset;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button bEpcInit;
        private System.Windows.Forms.Button btnSetAccessPWD;
        private System.Windows.Forms.Button btnSecLock;
        private System.Windows.Forms.Button btnSecWrite;
        private System.Windows.Forms.Button btnSecRead;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cmbLevel;
        private System.Windows.Forms.Button btnISOWriteWithID;
        private System.Windows.Forms.Button btnISOReadWithID;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtTagID;
    }
}

