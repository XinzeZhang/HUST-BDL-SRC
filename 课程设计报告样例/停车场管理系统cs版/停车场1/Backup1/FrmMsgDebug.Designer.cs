namespace ModuleReaderManager
{
    partial class FrmMsgDebug
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
            this.btngetdebugop = new System.Windows.Forms.Button();
            this.cbbdebugop = new System.Windows.Forms.ComboBox();
            this.btnsetdebugop = new System.Windows.Forms.Button();
            this.rtbmsglog = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnupdmsglog = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btngetdebugop
            // 
            this.btngetdebugop.Location = new System.Drawing.Point(125, 13);
            this.btngetdebugop.Name = "btngetdebugop";
            this.btngetdebugop.Size = new System.Drawing.Size(56, 23);
            this.btngetdebugop.TabIndex = 0;
            this.btngetdebugop.Text = "获取";
            this.btngetdebugop.UseVisualStyleBackColor = true;
            this.btngetdebugop.Click += new System.EventHandler(this.btngetdebugop_Click);
            // 
            // cbbdebugop
            // 
            this.cbbdebugop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbdebugop.FormattingEnabled = true;
            this.cbbdebugop.Items.AddRange(new object[] {
            "禁用信令调试",
            "启用信令调试"});
            this.cbbdebugop.Location = new System.Drawing.Point(17, 15);
            this.cbbdebugop.Name = "cbbdebugop";
            this.cbbdebugop.Size = new System.Drawing.Size(91, 20);
            this.cbbdebugop.TabIndex = 1;
            // 
            // btnsetdebugop
            // 
            this.btnsetdebugop.Location = new System.Drawing.Point(204, 13);
            this.btnsetdebugop.Name = "btnsetdebugop";
            this.btnsetdebugop.Size = new System.Drawing.Size(56, 23);
            this.btnsetdebugop.TabIndex = 3;
            this.btnsetdebugop.Text = "设置";
            this.btnsetdebugop.UseVisualStyleBackColor = true;
            this.btnsetdebugop.Click += new System.EventHandler(this.btnsetdebugop_Click);
            // 
            // rtbmsglog
            // 
            this.rtbmsglog.Location = new System.Drawing.Point(18, 54);
            this.rtbmsglog.Name = "rtbmsglog";
            this.rtbmsglog.Size = new System.Drawing.Size(616, 164);
            this.rtbmsglog.TabIndex = 4;
            this.rtbmsglog.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btngetdebugop);
            this.groupBox1.Controls.Add(this.btnsetdebugop);
            this.groupBox1.Controls.Add(this.cbbdebugop);
            this.groupBox1.Location = new System.Drawing.Point(18, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 45);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "调试选项";
            // 
            // btnupdmsglog
            // 
            this.btnupdmsglog.Location = new System.Drawing.Point(424, 16);
            this.btnupdmsglog.Name = "btnupdmsglog";
            this.btnupdmsglog.Size = new System.Drawing.Size(93, 23);
            this.btnupdmsglog.TabIndex = 6;
            this.btnupdmsglog.Text = "更新信令日志";
            this.btnupdmsglog.UseVisualStyleBackColor = true;
            this.btnupdmsglog.Click += new System.EventHandler(this.btnupdmsglog_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(541, 16);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "清空信令日志";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // FrmMsgDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 246);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.btnupdmsglog);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.rtbmsglog);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmMsgDebug";
            this.Text = "信令调试";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btngetdebugop;
        private System.Windows.Forms.ComboBox cbbdebugop;
        private System.Windows.Forms.Button btnsetdebugop;
        private System.Windows.Forms.RichTextBox rtbmsglog;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnupdmsglog;
        private System.Windows.Forms.Button button4;
    }
}