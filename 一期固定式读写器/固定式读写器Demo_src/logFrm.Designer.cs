namespace ModuleReaderManager
{
    partial class logFrm
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
            this.rtblogstr = new System.Windows.Forms.RichTextBox();
            this.tbrdraddr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btngetlog = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtblogstr
            // 
            this.rtblogstr.Location = new System.Drawing.Point(38, 60);
            this.rtblogstr.Name = "rtblogstr";
            this.rtblogstr.Size = new System.Drawing.Size(686, 253);
            this.rtblogstr.TabIndex = 0;
            this.rtblogstr.Text = "";
            // 
            // tbrdraddr
            // 
            this.tbrdraddr.AcceptsReturn = true;
            this.tbrdraddr.Location = new System.Drawing.Point(121, 24);
            this.tbrdraddr.Name = "tbrdraddr";
            this.tbrdraddr.Size = new System.Drawing.Size(100, 21);
            this.tbrdraddr.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "读写器地址：";
            // 
            // btngetlog
            // 
            this.btngetlog.Location = new System.Drawing.Point(649, 24);
            this.btngetlog.Name = "btngetlog";
            this.btngetlog.Size = new System.Drawing.Size(75, 23);
            this.btngetlog.TabIndex = 3;
            this.btngetlog.Text = "获取";
            this.btngetlog.UseVisualStyleBackColor = true;
            this.btngetlog.Click += new System.EventHandler(this.btngetlog_Click);
            // 
            // logFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 353);
            this.Controls.Add(this.btngetlog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbrdraddr);
            this.Controls.Add(this.rtblogstr);
            this.Name = "logFrm";
            this.Text = "日志";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtblogstr;
        private System.Windows.Forms.TextBox tbrdraddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btngetlog;
    }
}