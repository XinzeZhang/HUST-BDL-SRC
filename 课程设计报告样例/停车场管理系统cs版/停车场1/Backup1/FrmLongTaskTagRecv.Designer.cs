namespace ModuleReaderManager
{
    partial class FrmLongTaskTagRecv
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
            this.lvtags = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.lvoltags = new System.Windows.Forms.ListView();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
            this.labtotrttagnum = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnclearlv = new System.Windows.Forms.Button();
            this.btnstoprecv = new System.Windows.Forms.Button();
            this.btnbindrecv = new System.Windows.Forms.Button();
            this.tblocalport = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.labtotoltagnum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnclearlvol = new System.Windows.Forms.Button();
            this.tblocalportol = new System.Windows.Forms.TextBox();
            this.btnstoprecvol = new System.Windows.Forms.Button();
            this.btnbindrecvol = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvtags
            // 
            this.lvtags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7});
            this.lvtags.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvtags.GridLines = true;
            this.lvtags.Location = new System.Drawing.Point(0, 0);
            this.lvtags.Name = "lvtags";
            this.lvtags.Size = new System.Drawing.Size(779, 237);
            this.lvtags.TabIndex = 36;
            this.lvtags.UseCompatibleStateImageBehavior = false;
            this.lvtags.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "EPC（实时）";
            this.columnHeader1.Width = 153;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "数据";
            this.columnHeader2.Width = 210;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "读次数";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "天线";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "协议";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "RSSI";
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "时间戳";
            this.columnHeader7.Width = 231;
            // 
            // lvoltags
            // 
            this.lvoltags.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.lvoltags.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvoltags.GridLines = true;
            this.lvoltags.Location = new System.Drawing.Point(0, 297);
            this.lvoltags.Name = "lvoltags";
            this.lvoltags.Size = new System.Drawing.Size(779, 251);
            this.lvoltags.TabIndex = 40;
            this.lvoltags.UseCompatibleStateImageBehavior = false;
            this.lvoltags.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "EPC（离线）";
            this.columnHeader8.Width = 153;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "数据";
            this.columnHeader9.Width = 210;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "读次数";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "天线";
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "协议";
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "RSSI";
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "时间戳";
            this.columnHeader14.Width = 231;
            // 
            // labtotrttagnum
            // 
            this.labtotrttagnum.AutoSize = true;
            this.labtotrttagnum.Location = new System.Drawing.Point(62, 21);
            this.labtotrttagnum.Name = "labtotrttagnum";
            this.labtotrttagnum.Size = new System.Drawing.Size(17, 12);
            this.labtotrttagnum.TabIndex = 47;
            this.labtotrttagnum.Text = "la";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 46;
            this.label1.Text = "标签总数:";
            // 
            // btnclearlv
            // 
            this.btnclearlv.Location = new System.Drawing.Point(306, 16);
            this.btnclearlv.Name = "btnclearlv";
            this.btnclearlv.Size = new System.Drawing.Size(43, 23);
            this.btnclearlv.TabIndex = 45;
            this.btnclearlv.Text = "清空";
            this.btnclearlv.UseVisualStyleBackColor = true;
            this.btnclearlv.Click += new System.EventHandler(this.btnclearlv_Click);
            // 
            // btnstoprecv
            // 
            this.btnstoprecv.Location = new System.Drawing.Point(256, 16);
            this.btnstoprecv.Name = "btnstoprecv";
            this.btnstoprecv.Size = new System.Drawing.Size(44, 23);
            this.btnstoprecv.TabIndex = 44;
            this.btnstoprecv.Text = "停止";
            this.btnstoprecv.UseVisualStyleBackColor = true;
            this.btnstoprecv.Click += new System.EventHandler(this.btnstoprecv_Click);
            // 
            // btnbindrecv
            // 
            this.btnbindrecv.Location = new System.Drawing.Point(189, 17);
            this.btnbindrecv.Name = "btnbindrecv";
            this.btnbindrecv.Size = new System.Drawing.Size(61, 23);
            this.btnbindrecv.TabIndex = 43;
            this.btnbindrecv.Text = "绑定接收";
            this.btnbindrecv.UseVisualStyleBackColor = true;
            this.btnbindrecv.Click += new System.EventHandler(this.btnbindrecv_Click);
            // 
            // tblocalport
            // 
            this.tblocalport.Location = new System.Drawing.Point(137, 17);
            this.tblocalport.Name = "tblocalport";
            this.tblocalport.Size = new System.Drawing.Size(46, 21);
            this.tblocalport.TabIndex = 42;
            this.tblocalport.Text = "12345";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(104, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 12);
            this.label20.TabIndex = 41;
            this.label20.Text = "端口";
            // 
            // labtotoltagnum
            // 
            this.labtotoltagnum.AutoSize = true;
            this.labtotoltagnum.Location = new System.Drawing.Point(62, 22);
            this.labtotoltagnum.Name = "labtotoltagnum";
            this.labtotoltagnum.Size = new System.Drawing.Size(17, 12);
            this.labtotoltagnum.TabIndex = 49;
            this.labtotoltagnum.Text = "la";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labtotrttagnum);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.btnclearlv);
            this.groupBox1.Controls.Add(this.tblocalport);
            this.groupBox1.Controls.Add(this.btnstoprecv);
            this.groupBox1.Controls.Add(this.btnbindrecv);
            this.groupBox1.Location = new System.Drawing.Point(11, 242);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(357, 48);
            this.groupBox1.TabIndex = 50;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "实时数据接收设置";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.labtotoltagnum);
            this.groupBox2.Controls.Add(this.btnclearlvol);
            this.groupBox2.Controls.Add(this.tblocalportol);
            this.groupBox2.Controls.Add(this.btnstoprecvol);
            this.groupBox2.Controls.Add(this.btnbindrecvol);
            this.groupBox2.Location = new System.Drawing.Point(389, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(369, 48);
            this.groupBox2.TabIndex = 51;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "离线数据接收设置";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 46;
            this.label3.Text = "标签总数:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(114, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "端口";
            // 
            // btnclearlvol
            // 
            this.btnclearlvol.Location = new System.Drawing.Point(316, 16);
            this.btnclearlvol.Name = "btnclearlvol";
            this.btnclearlvol.Size = new System.Drawing.Size(43, 23);
            this.btnclearlvol.TabIndex = 45;
            this.btnclearlvol.Text = "清空";
            this.btnclearlvol.UseVisualStyleBackColor = true;
            this.btnclearlvol.Click += new System.EventHandler(this.btnclearlvol_Click);
            // 
            // tblocalportol
            // 
            this.tblocalportol.Location = new System.Drawing.Point(147, 17);
            this.tblocalportol.Name = "tblocalportol";
            this.tblocalportol.Size = new System.Drawing.Size(46, 21);
            this.tblocalportol.TabIndex = 42;
            this.tblocalportol.Text = "12346";
            // 
            // btnstoprecvol
            // 
            this.btnstoprecvol.Location = new System.Drawing.Point(266, 16);
            this.btnstoprecvol.Name = "btnstoprecvol";
            this.btnstoprecvol.Size = new System.Drawing.Size(44, 23);
            this.btnstoprecvol.TabIndex = 44;
            this.btnstoprecvol.Text = "停止";
            this.btnstoprecvol.UseVisualStyleBackColor = true;
            this.btnstoprecvol.Click += new System.EventHandler(this.btnstoprecvol_Click);
            // 
            // btnbindrecvol
            // 
            this.btnbindrecvol.Location = new System.Drawing.Point(199, 17);
            this.btnbindrecvol.Name = "btnbindrecvol";
            this.btnbindrecvol.Size = new System.Drawing.Size(61, 23);
            this.btnbindrecvol.TabIndex = 43;
            this.btnbindrecvol.Text = "绑定接收";
            this.btnbindrecvol.UseVisualStyleBackColor = true;
            this.btnbindrecvol.Click += new System.EventHandler(this.btnbindrecv0l_Click);
            // 
            // FrmLongTaskTagRecv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 548);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lvoltags);
            this.Controls.Add(this.lvtags);
            this.Name = "FrmLongTaskTagRecv";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "长任务标签接收";
            this.Load += new System.EventHandler(this.FrmLongTaskTagRecv_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmLongTaskTagRecv_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvtags;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ListView lvoltags;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Label labtotrttagnum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnclearlv;
        private System.Windows.Forms.Button btnstoprecv;
        private System.Windows.Forms.Button btnbindrecv;
        private System.Windows.Forms.TextBox tblocalport;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label labtotoltagnum;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnclearlvol;
        private System.Windows.Forms.TextBox tblocalportol;
        private System.Windows.Forms.Button btnstoprecvol;
        private System.Windows.Forms.Button btnbindrecvol;
    }
}