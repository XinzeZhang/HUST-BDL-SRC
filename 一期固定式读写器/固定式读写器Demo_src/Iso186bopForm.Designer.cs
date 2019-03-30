namespace ModuleReaderManager
{
    partial class Iso186bopForm
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnclear = new System.Windows.Forms.Button();
            this.btnInvStart = new System.Windows.Forms.Button();
            this.btnInvStop = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.cbant4 = new System.Windows.Forms.CheckBox();
            this.cbant3 = new System.Windows.Forms.CheckBox();
            this.cbant2 = new System.Windows.Forms.CheckBox();
            this.cbant1 = new System.Windows.Forms.CheckBox();
            this.groupbox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnlock = new System.Windows.Forms.Button();
            this.tbBlkCnt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbStartAddr = new System.Windows.Forms.TextBox();
            this.rtbdata = new System.Windows.Forms.RichTextBox();
            this.btnwrite = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupbox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnclear);
            this.groupBox1.Controls.Add(this.btnInvStart);
            this.groupBox1.Controls.Add(this.btnInvStop);
            this.groupBox1.Controls.Add(this.listBox2);
            this.groupBox1.Location = new System.Drawing.Point(22, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(184, 195);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "清点操作";
            // 
            // btnclear
            // 
            this.btnclear.Location = new System.Drawing.Point(128, 21);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(40, 23);
            this.btnclear.TabIndex = 8;
            this.btnclear.Text = "清空";
            this.btnclear.UseVisualStyleBackColor = true;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // btnInvStart
            // 
            this.btnInvStart.Location = new System.Drawing.Point(12, 20);
            this.btnInvStart.Name = "btnInvStart";
            this.btnInvStart.Size = new System.Drawing.Size(42, 23);
            this.btnInvStart.TabIndex = 5;
            this.btnInvStart.Text = "开始";
            this.btnInvStart.UseVisualStyleBackColor = true;
            this.btnInvStart.Click += new System.EventHandler(this.btnInvStart_Click);
            // 
            // btnInvStop
            // 
            this.btnInvStop.Location = new System.Drawing.Point(70, 21);
            this.btnInvStop.Name = "btnInvStop";
            this.btnInvStop.Size = new System.Drawing.Size(40, 23);
            this.btnInvStop.TabIndex = 7;
            this.btnInvStop.Text = "停止";
            this.btnInvStop.UseVisualStyleBackColor = true;
            this.btnInvStop.Click += new System.EventHandler(this.btnInvStop_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(15, 50);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(153, 136);
            this.listBox2.TabIndex = 6;
            // 
            // cbant4
            // 
            this.cbant4.AutoSize = true;
            this.cbant4.Location = new System.Drawing.Point(104, 46);
            this.cbant4.Name = "cbant4";
            this.cbant4.Size = new System.Drawing.Size(48, 16);
            this.cbant4.TabIndex = 11;
            this.cbant4.Text = "ant4";
            this.cbant4.UseVisualStyleBackColor = true;
            // 
            // cbant3
            // 
            this.cbant3.AutoSize = true;
            this.cbant3.Location = new System.Drawing.Point(21, 46);
            this.cbant3.Name = "cbant3";
            this.cbant3.Size = new System.Drawing.Size(48, 16);
            this.cbant3.TabIndex = 10;
            this.cbant3.Text = "ant3";
            this.cbant3.UseVisualStyleBackColor = true;
            // 
            // cbant2
            // 
            this.cbant2.AutoSize = true;
            this.cbant2.Location = new System.Drawing.Point(104, 20);
            this.cbant2.Name = "cbant2";
            this.cbant2.Size = new System.Drawing.Size(48, 16);
            this.cbant2.TabIndex = 9;
            this.cbant2.Text = "ant2";
            this.cbant2.UseVisualStyleBackColor = true;
            // 
            // cbant1
            // 
            this.cbant1.AutoSize = true;
            this.cbant1.Location = new System.Drawing.Point(21, 20);
            this.cbant1.Name = "cbant1";
            this.cbant1.Size = new System.Drawing.Size(48, 16);
            this.cbant1.TabIndex = 8;
            this.cbant1.Text = "ant1";
            this.cbant1.UseVisualStyleBackColor = true;
            // 
            // groupbox2
            // 
            this.groupbox2.Controls.Add(this.label3);
            this.groupbox2.Controls.Add(this.btnlock);
            this.groupbox2.Controls.Add(this.tbBlkCnt);
            this.groupbox2.Controls.Add(this.label2);
            this.groupbox2.Controls.Add(this.label1);
            this.groupbox2.Controls.Add(this.tbStartAddr);
            this.groupbox2.Controls.Add(this.rtbdata);
            this.groupbox2.Controls.Add(this.btnwrite);
            this.groupbox2.Controls.Add(this.btnRead);
            this.groupbox2.Location = new System.Drawing.Point(230, 21);
            this.groupbox2.Name = "groupbox2";
            this.groupbox2.Size = new System.Drawing.Size(307, 276);
            this.groupbox2.TabIndex = 13;
            this.groupbox2.TabStop = false;
            this.groupbox2.Text = "标签操作";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = " ";
            // 
            // btnlock
            // 
            this.btnlock.Location = new System.Drawing.Point(207, 29);
            this.btnlock.Name = "btnlock";
            this.btnlock.Size = new System.Drawing.Size(75, 23);
            this.btnlock.TabIndex = 7;
            this.btnlock.Text = "锁";
            this.btnlock.UseVisualStyleBackColor = true;
            this.btnlock.Click += new System.EventHandler(this.btnlock_Click);
            // 
            // tbBlkCnt
            // 
            this.tbBlkCnt.Location = new System.Drawing.Point(231, 76);
            this.tbBlkCnt.Name = "tbBlkCnt";
            this.tbBlkCnt.Size = new System.Drawing.Size(48, 21);
            this.tbBlkCnt.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(183, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "块数：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "起始地址：";
            // 
            // tbStartAddr
            // 
            this.tbStartAddr.Location = new System.Drawing.Point(87, 76);
            this.tbStartAddr.Name = "tbStartAddr";
            this.tbStartAddr.Size = new System.Drawing.Size(47, 21);
            this.tbStartAddr.TabIndex = 3;
            // 
            // rtbdata
            // 
            this.rtbdata.Location = new System.Drawing.Point(16, 122);
            this.rtbdata.Name = "rtbdata";
            this.rtbdata.Size = new System.Drawing.Size(277, 133);
            this.rtbdata.TabIndex = 2;
            this.rtbdata.Text = "";
            // 
            // btnwrite
            // 
            this.btnwrite.Location = new System.Drawing.Point(109, 29);
            this.btnwrite.Name = "btnwrite";
            this.btnwrite.Size = new System.Drawing.Size(75, 23);
            this.btnwrite.TabIndex = 1;
            this.btnwrite.Text = "写";
            this.btnwrite.UseVisualStyleBackColor = true;
            this.btnwrite.Click += new System.EventHandler(this.btnwrite_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(16, 29);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 0;
            this.btnRead.Text = "读";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbant4);
            this.groupBox3.Controls.Add(this.cbant1);
            this.groupBox3.Controls.Add(this.cbant3);
            this.groupBox3.Controls.Add(this.cbant2);
            this.groupBox3.Location = new System.Drawing.Point(24, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 73);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "天线选择";
            // 
            // Iso186bopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(562, 322);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupbox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Iso186bopForm";
            this.Text = "Iso180006b标签操作";
            this.Load += new System.EventHandler(this.Iso186bopForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Iso186bopForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupbox2.ResumeLayout(false);
            this.groupbox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnInvStart;
        private System.Windows.Forms.Button btnInvStop;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.GroupBox groupbox2;
        private System.Windows.Forms.TextBox tbBlkCnt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbStartAddr;
        private System.Windows.Forms.RichTextBox rtbdata;
        private System.Windows.Forms.Button btnwrite;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnlock;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox cbant1;
        private System.Windows.Forms.CheckBox cbant4;
        private System.Windows.Forms.CheckBox cbant3;
        private System.Windows.Forms.CheckBox cbant2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Label label3;
    }
}