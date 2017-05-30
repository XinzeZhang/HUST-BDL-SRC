namespace DesktopUHF
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
            this.txbEpc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnReadEpc = new System.Windows.Forms.Button();
            this.btnWriteEpc = new System.Windows.Forms.Button();
            this.txbTID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnReadTID = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbUser = new System.Windows.Forms.RichTextBox();
            this.btnReadUser = new System.Windows.Forms.Button();
            this.btnWriteUser = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txbNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txbStart = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txbEpc
            // 
            this.txbEpc.Location = new System.Drawing.Point(107, 39);
            this.txbEpc.Name = "txbEpc";
            this.txbEpc.Size = new System.Drawing.Size(248, 21);
            this.txbEpc.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "ECP:";
            // 
            // btnReadEpc
            // 
            this.btnReadEpc.Location = new System.Drawing.Point(387, 37);
            this.btnReadEpc.Name = "btnReadEpc";
            this.btnReadEpc.Size = new System.Drawing.Size(75, 23);
            this.btnReadEpc.TabIndex = 4;
            this.btnReadEpc.Text = "读取EPC";
            this.btnReadEpc.UseVisualStyleBackColor = true;
            this.btnReadEpc.Click += new System.EventHandler(this.btnReadEpc_Click);
            // 
            // btnWriteEpc
            // 
            this.btnWriteEpc.Location = new System.Drawing.Point(509, 37);
            this.btnWriteEpc.Name = "btnWriteEpc";
            this.btnWriteEpc.Size = new System.Drawing.Size(75, 23);
            this.btnWriteEpc.TabIndex = 4;
            this.btnWriteEpc.Text = "写EPC";
            this.btnWriteEpc.UseVisualStyleBackColor = true;
            this.btnWriteEpc.Click += new System.EventHandler(this.btnWriteEpc_Click);
            // 
            // txbTID
            // 
            this.txbTID.Location = new System.Drawing.Point(107, 90);
            this.txbTID.Name = "txbTID";
            this.txbTID.Size = new System.Drawing.Size(248, 21);
            this.txbTID.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "TID:";
            // 
            // btnReadTID
            // 
            this.btnReadTID.Location = new System.Drawing.Point(387, 90);
            this.btnReadTID.Name = "btnReadTID";
            this.btnReadTID.Size = new System.Drawing.Size(75, 23);
            this.btnReadTID.TabIndex = 4;
            this.btnReadTID.Text = "读取TID";
            this.btnReadTID.UseVisualStyleBackColor = true;
            this.btnReadTID.Click += new System.EventHandler(this.btnReadTID_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(68, 184);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "用户数据:";
            // 
            // rtbUser
            // 
            this.rtbUser.Location = new System.Drawing.Point(121, 184);
            this.rtbUser.Name = "rtbUser";
            this.rtbUser.Size = new System.Drawing.Size(230, 82);
            this.rtbUser.TabIndex = 5;
            this.rtbUser.Text = "";
            // 
            // btnReadUser
            // 
            this.btnReadUser.Location = new System.Drawing.Point(387, 162);
            this.btnReadUser.Name = "btnReadUser";
            this.btnReadUser.Size = new System.Drawing.Size(75, 23);
            this.btnReadUser.TabIndex = 4;
            this.btnReadUser.Text = "读用户区";
            this.btnReadUser.UseVisualStyleBackColor = true;
            this.btnReadUser.Click += new System.EventHandler(this.btnReadUser_Click);
            // 
            // btnWriteUser
            // 
            this.btnWriteUser.Location = new System.Drawing.Point(509, 162);
            this.btnWriteUser.Name = "btnWriteUser";
            this.btnWriteUser.Size = new System.Drawing.Size(75, 23);
            this.btnWriteUser.TabIndex = 4;
            this.btnWriteUser.Text = "写用户区";
            this.btnWriteUser.UseVisualStyleBackColor = true;
            this.btnWriteUser.Click += new System.EventHandler(this.btnWriteUser_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txbNum);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txbStart);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(61, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 145);
            this.panel1.TabIndex = 7;
            // 
            // txbNum
            // 
            this.txbNum.Location = new System.Drawing.Point(226, 9);
            this.txbNum.Name = "txbNum";
            this.txbNum.Size = new System.Drawing.Size(75, 21);
            this.txbNum.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(190, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "字数:";
            // 
            // txbStart
            // 
            this.txbStart.Location = new System.Drawing.Point(63, 10);
            this.txbStart.Name = "txbStart";
            this.txbStart.Size = new System.Drawing.Size(100, 21);
            this.txbStart.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "起始地址:";
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(61, 81);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(541, 38);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Location = new System.Drawing.Point(62, 29);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(541, 38);
            this.panel3.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 367);
            this.Controls.Add(this.rtbUser);
            this.Controls.Add(this.btnReadTID);
            this.Controls.Add(this.btnWriteEpc);
            this.Controls.Add(this.btnWriteUser);
            this.Controls.Add(this.btnReadUser);
            this.Controls.Add(this.btnReadEpc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txbTID);
            this.Controls.Add(this.txbEpc);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbEpc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReadEpc;
        private System.Windows.Forms.Button btnWriteEpc;
        private System.Windows.Forms.TextBox txbTID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnReadTID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbUser;
        private System.Windows.Forms.Button btnReadUser;
        private System.Windows.Forms.Button btnWriteUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txbNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txbStart;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}

