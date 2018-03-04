namespace ModuleReaderManager
{
    partial class FrmPsam
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
            this.btnexec = new System.Windows.Forms.Button();
            this.cbbslot = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbsend = new System.Windows.Forms.TextBox();
            this.tbrecv = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnexec
            // 
            this.btnexec.Location = new System.Drawing.Point(254, 26);
            this.btnexec.Name = "btnexec";
            this.btnexec.Size = new System.Drawing.Size(75, 23);
            this.btnexec.TabIndex = 0;
            this.btnexec.Text = "执行";
            this.btnexec.UseVisualStyleBackColor = true;
            this.btnexec.Click += new System.EventHandler(this.btnexec_Click);
            // 
            // cbbslot
            // 
            this.cbbslot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbslot.FormattingEnabled = true;
            this.cbbslot.Items.AddRange(new object[] {
            "PSAM卡槽1",
            "PSAM卡槽2"});
            this.cbbslot.Location = new System.Drawing.Point(107, 26);
            this.cbbslot.Name = "cbbslot";
            this.cbbslot.Size = new System.Drawing.Size(121, 20);
            this.cbbslot.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(46, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "PSAM卡槽";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(46, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "发送";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(46, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "接收";
            // 
            // tbsend
            // 
            this.tbsend.Location = new System.Drawing.Point(107, 74);
            this.tbsend.Name = "tbsend";
            this.tbsend.Size = new System.Drawing.Size(222, 21);
            this.tbsend.TabIndex = 5;
            // 
            // tbrecv
            // 
            this.tbrecv.Location = new System.Drawing.Point(107, 115);
            this.tbrecv.Name = "tbrecv";
            this.tbrecv.Size = new System.Drawing.Size(222, 21);
            this.tbrecv.TabIndex = 6;
            // 
            // FrmPsam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 181);
            this.Controls.Add(this.tbrecv);
            this.Controls.Add(this.tbsend);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbbslot);
            this.Controls.Add(this.btnexec);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPsam";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Psam测试";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnexec;
        private System.Windows.Forms.ComboBox cbbslot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbsend;
        private System.Windows.Forms.TextBox tbrecv;
    }
}