namespace ModuleReaderManager
{
    partial class longtaskfrm
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
            if (disposing && (components != null))
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbexepoweron = new System.Windows.Forms.RadioButton();
            this.rbexeinstant = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbseaandwrite = new System.Windows.Forms.RadioButton();
            this.rbsearandread = new System.Windows.Forms.RadioButton();
            this.rbsearchtag = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.tbuploadip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbuploadport = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rtbwdata = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbpwd = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbuploadintv = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbant4 = new System.Windows.Forms.CheckBox();
            this.cbant3 = new System.Windows.Forms.CheckBox();
            this.cbant2 = new System.Windows.Forms.CheckBox();
            this.cbant1 = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.cbbbank = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tbstartaddr = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tbreadinterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rb180006b = new System.Windows.Forms.RadioButton();
            this.rbgen2 = new System.Windows.Forms.RadioButton();
            this.tbblkcnt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tbreaddur = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnsetlongtask = new System.Windows.Forms.Button();
            this.lvresult = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.btnstoplongtask = new System.Windows.Forms.Button();
            this.btnstopltanddel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tblocalport = new System.Windows.Forms.TextBox();
            this.btnbindrecv = new System.Windows.Forms.Button();
            this.btnstoprecv = new System.Windows.Forms.Button();
            this.btnclearlv = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.labtottagnum = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbexepoweron);
            this.groupBox1.Controls.Add(this.rbexeinstant);
            this.groupBox1.Location = new System.Drawing.Point(207, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(105, 69);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "操作属性";
            // 
            // rbexepoweron
            // 
            this.rbexepoweron.AutoSize = true;
            this.rbexepoweron.Location = new System.Drawing.Point(17, 40);
            this.rbexepoweron.Name = "rbexepoweron";
            this.rbexepoweron.Size = new System.Drawing.Size(71, 16);
            this.rbexepoweron.TabIndex = 1;
            this.rbexepoweron.TabStop = true;
            this.rbexepoweron.Text = "上电执行";
            this.rbexepoweron.UseVisualStyleBackColor = true;
            // 
            // rbexeinstant
            // 
            this.rbexeinstant.AutoSize = true;
            this.rbexeinstant.Location = new System.Drawing.Point(17, 20);
            this.rbexeinstant.Name = "rbexeinstant";
            this.rbexeinstant.Size = new System.Drawing.Size(71, 16);
            this.rbexeinstant.TabIndex = 0;
            this.rbexeinstant.TabStop = true;
            this.rbexeinstant.Text = "立即执行";
            this.rbexeinstant.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbseaandwrite);
            this.groupBox2.Controls.Add(this.rbsearandread);
            this.groupBox2.Controls.Add(this.rbsearchtag);
            this.groupBox2.Location = new System.Drawing.Point(27, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(159, 73);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作类型";
            // 
            // rbseaandwrite
            // 
            this.rbseaandwrite.AutoSize = true;
            this.rbseaandwrite.Location = new System.Drawing.Point(17, 51);
            this.rbseaandwrite.Name = "rbseaandwrite";
            this.rbseaandwrite.Size = new System.Drawing.Size(119, 16);
            this.rbseaandwrite.TabIndex = 3;
            this.rbseaandwrite.TabStop = true;
            this.rbseaandwrite.Text = "搜索标签并写bank";
            this.rbseaandwrite.UseVisualStyleBackColor = true;
            // 
            // rbsearandread
            // 
            this.rbsearandread.AutoSize = true;
            this.rbsearandread.Location = new System.Drawing.Point(17, 32);
            this.rbsearandread.Name = "rbsearandread";
            this.rbsearandread.Size = new System.Drawing.Size(119, 16);
            this.rbsearandread.TabIndex = 2;
            this.rbsearandread.TabStop = true;
            this.rbsearandread.Text = "搜索标签并读bank";
            this.rbsearandread.UseVisualStyleBackColor = true;
            // 
            // rbsearchtag
            // 
            this.rbsearchtag.AutoSize = true;
            this.rbsearchtag.Location = new System.Drawing.Point(17, 16);
            this.rbsearchtag.Name = "rbsearchtag";
            this.rbsearchtag.Size = new System.Drawing.Size(71, 16);
            this.rbsearchtag.TabIndex = 1;
            this.rbsearchtag.TabStop = true;
            this.rbsearchtag.Text = "搜索标签";
            this.rbsearchtag.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 207);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "上传地址";
            // 
            // tbuploadip
            // 
            this.tbuploadip.Location = new System.Drawing.Point(96, 203);
            this.tbuploadip.Name = "tbuploadip";
            this.tbuploadip.Size = new System.Drawing.Size(90, 21);
            this.tbuploadip.TabIndex = 3;
            this.tbuploadip.Text = "192.168.0.122";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 181);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "上传端口";
            // 
            // tbuploadport
            // 
            this.tbuploadport.Location = new System.Drawing.Point(96, 177);
            this.tbuploadport.Name = "tbuploadport";
            this.tbuploadport.Size = new System.Drawing.Size(90, 21);
            this.tbuploadport.TabIndex = 5;
            this.tbuploadport.Text = "12345";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 267);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "写入数据";
            // 
            // rtbwdata
            // 
            this.rtbwdata.Location = new System.Drawing.Point(96, 257);
            this.rtbwdata.Name = "rtbwdata";
            this.rtbwdata.Size = new System.Drawing.Size(90, 75);
            this.rtbwdata.TabIndex = 11;
            this.rtbwdata.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "访问密码";
            // 
            // tbpwd
            // 
            this.tbpwd.Location = new System.Drawing.Point(96, 230);
            this.tbpwd.Name = "tbpwd";
            this.tbpwd.Size = new System.Drawing.Size(90, 21);
            this.tbpwd.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(25, 157);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "上传间隔";
            // 
            // tbuploadintv
            // 
            this.tbuploadintv.Location = new System.Drawing.Point(96, 153);
            this.tbuploadintv.Name = "tbuploadintv";
            this.tbuploadintv.Size = new System.Drawing.Size(90, 21);
            this.tbuploadintv.TabIndex = 15;
            this.tbuploadintv.Text = "0";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbant4);
            this.groupBox3.Controls.Add(this.cbant3);
            this.groupBox3.Controls.Add(this.cbant2);
            this.groupBox3.Controls.Add(this.cbant1);
            this.groupBox3.Location = new System.Drawing.Point(207, 104);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(105, 62);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "天线设置";
            // 
            // cbant4
            // 
            this.cbant4.AutoSize = true;
            this.cbant4.Location = new System.Drawing.Point(55, 38);
            this.cbant4.Name = "cbant4";
            this.cbant4.Size = new System.Drawing.Size(48, 16);
            this.cbant4.TabIndex = 3;
            this.cbant4.Text = "ant4";
            this.cbant4.UseVisualStyleBackColor = true;
            // 
            // cbant3
            // 
            this.cbant3.AutoSize = true;
            this.cbant3.Location = new System.Drawing.Point(8, 38);
            this.cbant3.Name = "cbant3";
            this.cbant3.Size = new System.Drawing.Size(48, 16);
            this.cbant3.TabIndex = 2;
            this.cbant3.Text = "ant3";
            this.cbant3.UseVisualStyleBackColor = true;
            // 
            // cbant2
            // 
            this.cbant2.AutoSize = true;
            this.cbant2.Location = new System.Drawing.Point(55, 16);
            this.cbant2.Name = "cbant2";
            this.cbant2.Size = new System.Drawing.Size(48, 16);
            this.cbant2.TabIndex = 1;
            this.cbant2.Text = "ant2";
            this.cbant2.UseVisualStyleBackColor = true;
            // 
            // cbant1
            // 
            this.cbant1.AutoSize = true;
            this.cbant1.Location = new System.Drawing.Point(8, 16);
            this.cbant1.Name = "cbant1";
            this.cbant1.Size = new System.Drawing.Size(48, 16);
            this.cbant1.TabIndex = 0;
            this.cbant1.Text = "ant1";
            this.cbant1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.cbbbank);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.tbstartaddr);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.tbreadinterval);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.groupBox5);
            this.groupBox4.Controls.Add(this.tbblkcnt);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.tbreaddur);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.btnsetlongtask);
            this.groupBox4.Controls.Add(this.tbuploadintv);
            this.groupBox4.Controls.Add(this.groupBox3);
            this.groupBox4.Controls.Add(this.groupBox1);
            this.groupBox4.Controls.Add(this.groupBox2);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tbpwd);
            this.groupBox4.Controls.Add(this.tbuploadip);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.rtbwdata);
            this.groupBox4.Controls.Add(this.tbuploadport);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(23, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(332, 347);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "长任务配置";
            // 
            // cbbbank
            // 
            this.cbbbank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbbank.FormattingEnabled = true;
            this.cbbbank.Items.AddRange(new object[] {
            "bank0",
            "bank1",
            "bank2",
            "bank3",
            "180006b内存"});
            this.cbbbank.Location = new System.Drawing.Point(252, 278);
            this.cbbbank.Name = "cbbbank";
            this.cbbbank.Size = new System.Drawing.Size(60, 20);
            this.cbbbank.TabIndex = 30;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(198, 281);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 12);
            this.label11.TabIndex = 29;
            this.label11.Text = "bank";
            // 
            // tbstartaddr
            // 
            this.tbstartaddr.Location = new System.Drawing.Point(252, 249);
            this.tbstartaddr.Name = "tbstartaddr";
            this.tbstartaddr.Size = new System.Drawing.Size(60, 21);
            this.tbstartaddr.TabIndex = 28;
            this.tbstartaddr.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(198, 253);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 27;
            this.label10.Text = "起始地址";
            // 
            // tbreadinterval
            // 
            this.tbreadinterval.Location = new System.Drawing.Point(252, 172);
            this.tbreadinterval.Name = "tbreadinterval";
            this.tbreadinterval.Size = new System.Drawing.Size(60, 21);
            this.tbreadinterval.TabIndex = 26;
            this.tbreadinterval.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(198, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 25;
            this.label3.Text = "读间隔";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rb180006b);
            this.groupBox5.Controls.Add(this.rbgen2);
            this.groupBox5.Location = new System.Drawing.Point(27, 104);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(159, 37);
            this.groupBox5.TabIndex = 24;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "协议";
            // 
            // rb180006b
            // 
            this.rb180006b.AutoSize = true;
            this.rb180006b.Location = new System.Drawing.Point(80, 15);
            this.rb180006b.Name = "rb180006b";
            this.rb180006b.Size = new System.Drawing.Size(65, 16);
            this.rb180006b.TabIndex = 26;
            this.rb180006b.TabStop = true;
            this.rb180006b.Text = "180006b";
            this.rb180006b.UseVisualStyleBackColor = true;
            // 
            // rbgen2
            // 
            this.rbgen2.AutoSize = true;
            this.rbgen2.Location = new System.Drawing.Point(17, 15);
            this.rbgen2.Name = "rbgen2";
            this.rbgen2.Size = new System.Drawing.Size(47, 16);
            this.rbgen2.TabIndex = 25;
            this.rbgen2.TabStop = true;
            this.rbgen2.Text = "gen2";
            this.rbgen2.UseVisualStyleBackColor = true;
            // 
            // tbblkcnt
            // 
            this.tbblkcnt.Location = new System.Drawing.Point(252, 223);
            this.tbblkcnt.Name = "tbblkcnt";
            this.tbblkcnt.Size = new System.Drawing.Size(60, 21);
            this.tbblkcnt.TabIndex = 21;
            this.tbblkcnt.Text = "2";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(198, 226);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "读块数";
            // 
            // tbreaddur
            // 
            this.tbreaddur.Location = new System.Drawing.Point(252, 198);
            this.tbreaddur.Name = "tbreaddur";
            this.tbreaddur.Size = new System.Drawing.Size(60, 21);
            this.tbreaddur.TabIndex = 19;
            this.tbreaddur.Text = "70";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(198, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 18;
            this.label8.Text = "读时长";
            // 
            // btnsetlongtask
            // 
            this.btnsetlongtask.Location = new System.Drawing.Point(249, 309);
            this.btnsetlongtask.Name = "btnsetlongtask";
            this.btnsetlongtask.Size = new System.Drawing.Size(63, 23);
            this.btnsetlongtask.TabIndex = 17;
            this.btnsetlongtask.Text = "设置";
            this.btnsetlongtask.UseVisualStyleBackColor = true;
            this.btnsetlongtask.Click += new System.EventHandler(this.btnsetlongtask_Click);
            // 
            // lvresult
            // 
            this.lvresult.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvresult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvresult.Dock = System.Windows.Forms.DockStyle.Right;
            this.lvresult.GridLines = true;
            this.lvresult.Location = new System.Drawing.Point(374, 0);
            this.lvresult.Name = "lvresult";
            this.lvresult.Size = new System.Drawing.Size(626, 442);
            this.lvresult.TabIndex = 18;
            this.lvresult.UseCompatibleStateImageBehavior = false;
            this.lvresult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "EPC";
            this.columnHeader1.Width = 126;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "数据";
            this.columnHeader2.Width = 131;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "EPC读次数";
            this.columnHeader3.Width = 91;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "操作成功次数";
            this.columnHeader4.Width = 92;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "天线";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "协议";
            this.columnHeader6.Width = 40;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "时戳";
            this.columnHeader7.Width = 69;
            // 
            // btnstoplongtask
            // 
            this.btnstoplongtask.Location = new System.Drawing.Point(23, 365);
            this.btnstoplongtask.Name = "btnstoplongtask";
            this.btnstoplongtask.Size = new System.Drawing.Size(88, 23);
            this.btnstoplongtask.TabIndex = 19;
            this.btnstoplongtask.Text = "停止长任务";
            this.btnstoplongtask.UseVisualStyleBackColor = true;
            this.btnstoplongtask.Click += new System.EventHandler(this.btnstoplongtask_Click);
            // 
            // btnstopltanddel
            // 
            this.btnstopltanddel.Location = new System.Drawing.Point(188, 365);
            this.btnstopltanddel.Name = "btnstopltanddel";
            this.btnstopltanddel.Size = new System.Drawing.Size(167, 23);
            this.btnstopltanddel.TabIndex = 20;
            this.btnstopltanddel.Text = "停止长任务并删除上电执行";
            this.btnstopltanddel.UseVisualStyleBackColor = true;
            this.btnstopltanddel.Click += new System.EventHandler(this.btnstopltanddel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 399);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 21;
            this.label4.Text = "本地端口：";
            // 
            // tblocalport
            // 
            this.tblocalport.Location = new System.Drawing.Point(84, 396);
            this.tblocalport.Name = "tblocalport";
            this.tblocalport.Size = new System.Drawing.Size(46, 21);
            this.tblocalport.TabIndex = 22;
            // 
            // btnbindrecv
            // 
            this.btnbindrecv.Location = new System.Drawing.Point(140, 394);
            this.btnbindrecv.Name = "btnbindrecv";
            this.btnbindrecv.Size = new System.Drawing.Size(61, 23);
            this.btnbindrecv.TabIndex = 23;
            this.btnbindrecv.Text = "绑定接收";
            this.btnbindrecv.UseVisualStyleBackColor = true;
            this.btnbindrecv.Click += new System.EventHandler(this.btnbindrecv_Click);
            // 
            // btnstoprecv
            // 
            this.btnstoprecv.Location = new System.Drawing.Point(219, 394);
            this.btnstoprecv.Name = "btnstoprecv";
            this.btnstoprecv.Size = new System.Drawing.Size(62, 23);
            this.btnstoprecv.TabIndex = 24;
            this.btnstoprecv.Text = "停止接收";
            this.btnstoprecv.UseVisualStyleBackColor = true;
            this.btnstoprecv.Click += new System.EventHandler(this.btnstoprecv_Click);
            // 
            // btnclearlv
            // 
            this.btnclearlv.Location = new System.Drawing.Point(302, 394);
            this.btnclearlv.Name = "btnclearlv";
            this.btnclearlv.Size = new System.Drawing.Size(53, 23);
            this.btnclearlv.TabIndex = 25;
            this.btnclearlv.Text = "清空";
            this.btnclearlv.UseVisualStyleBackColor = true;
            this.btnclearlv.Click += new System.EventHandler(this.btnclearlv_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(217, 421);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "标签总数：";
            // 
            // labtottagnum
            // 
            this.labtottagnum.AutoSize = true;
            this.labtottagnum.Location = new System.Drawing.Point(300, 421);
            this.labtottagnum.Name = "labtottagnum";
            this.labtottagnum.Size = new System.Drawing.Size(11, 12);
            this.labtottagnum.TabIndex = 27;
            this.labtottagnum.Text = "0";
            // 
            // longtaskfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 442);
            this.Controls.Add(this.labtottagnum);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnclearlv);
            this.Controls.Add(this.btnstoprecv);
            this.Controls.Add(this.btnbindrecv);
            this.Controls.Add(this.tblocalport);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnstopltanddel);
            this.Controls.Add(this.btnstoplongtask);
            this.Controls.Add(this.lvresult);
            this.Controls.Add(this.groupBox4);
            this.Name = "longtaskfrm";
            this.Text = "长任务";
            this.Load += new System.EventHandler(this.longtaskfrm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.longtaskfrm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbexepoweron;
        private System.Windows.Forms.RadioButton rbexeinstant;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbsearchtag;
        private System.Windows.Forms.RadioButton rbseaandwrite;
        private System.Windows.Forms.RadioButton rbsearandread;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbuploadip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbuploadport;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtbwdata;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbpwd;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbuploadintv;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbant4;
        private System.Windows.Forms.CheckBox cbant3;
        private System.Windows.Forms.CheckBox cbant2;
        private System.Windows.Forms.CheckBox cbant1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbblkcnt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tbreaddur;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnsetlongtask;
        private System.Windows.Forms.ListView lvresult;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Button btnstoplongtask;
        private System.Windows.Forms.Button btnstopltanddel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tblocalport;
        private System.Windows.Forms.Button btnbindrecv;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rb180006b;
        private System.Windows.Forms.RadioButton rbgen2;
        private System.Windows.Forms.Button btnstoprecv;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.Button btnclearlv;
        private System.Windows.Forms.TextBox tbreadinterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbstartaddr;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbbbank;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label labtottagnum;
    }
}