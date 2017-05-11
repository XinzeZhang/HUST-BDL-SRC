namespace ModuleReaderManager
{
    partial class CountTagsFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbcountdur = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cbant4 = new System.Windows.Forms.CheckBox();
            this.cbant1 = new System.Windows.Forms.CheckBox();
            this.cbant3 = new System.Windows.Forms.CheckBox();
            this.cbant2 = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(58, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "时长：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "重置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(187, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "清点";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(89, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 29);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // tbcountdur
            // 
            this.tbcountdur.Location = new System.Drawing.Point(114, 66);
            this.tbcountdur.Name = "tbcountdur";
            this.tbcountdur.Size = new System.Drawing.Size(100, 21);
            this.tbcountdur.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cbant4);
            this.groupBox3.Controls.Add(this.cbant1);
            this.groupBox3.Controls.Add(this.cbant3);
            this.groupBox3.Controls.Add(this.cbant2);
            this.groupBox3.Location = new System.Drawing.Point(24, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(238, 46);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "天线选择";
            // 
            // cbant4
            // 
            this.cbant4.AutoSize = true;
            this.cbant4.Location = new System.Drawing.Point(176, 20);
            this.cbant4.Name = "cbant4";
            this.cbant4.Size = new System.Drawing.Size(48, 16);
            this.cbant4.TabIndex = 11;
            this.cbant4.Text = "ant4";
            this.cbant4.UseVisualStyleBackColor = true;
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
            // cbant3
            // 
            this.cbant3.AutoSize = true;
            this.cbant3.Location = new System.Drawing.Point(123, 20);
            this.cbant3.Name = "cbant3";
            this.cbant3.Size = new System.Drawing.Size(48, 16);
            this.cbant3.TabIndex = 10;
            this.cbant3.Text = "ant3";
            this.cbant3.UseVisualStyleBackColor = true;
            // 
            // cbant2
            // 
            this.cbant2.AutoSize = true;
            this.cbant2.Location = new System.Drawing.Point(70, 20);
            this.cbant2.Name = "cbant2";
            this.cbant2.Size = new System.Drawing.Size(48, 16);
            this.cbant2.TabIndex = 9;
            this.cbant2.Text = "ant2";
            this.cbant2.UseVisualStyleBackColor = true;
            // 
            // CountTagsFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 207);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.tbcountdur);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CountTagsFrm";
            this.Text = "标签计数";
            this.Load += new System.EventHandler(this.CountTagsFrm_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbcountdur;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox cbant4;
        private System.Windows.Forms.CheckBox cbant1;
        private System.Windows.Forms.CheckBox cbant3;
        private System.Windows.Forms.CheckBox cbant2;
    }
}