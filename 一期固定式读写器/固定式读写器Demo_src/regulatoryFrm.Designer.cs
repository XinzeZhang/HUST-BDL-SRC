namespace ModuleReaderManager
{
    partial class regulatoryFrm
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
            this.btnsetopfre = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbopfre = new System.Windows.Forms.TextBox();
            this.btntransCW = new System.Windows.Forms.Button();
            this.btnstopCW = new System.Windows.Forms.Button();
            this.btnPRBSOn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tbdur = new System.Windows.Forms.TextBox();
            this.tbopant = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnsetopant = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnsetopfre
            // 
            this.btnsetopfre.Location = new System.Drawing.Point(217, 69);
            this.btnsetopfre.Name = "btnsetopfre";
            this.btnsetopfre.Size = new System.Drawing.Size(108, 23);
            this.btnsetopfre.TabIndex = 0;
            this.btnsetopfre.Text = "定频";
            this.btnsetopfre.UseVisualStyleBackColor = true;
            this.btnsetopfre.Click += new System.EventHandler(this.btnsetopfre_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "频点";
            // 
            // tbopfre
            // 
            this.tbopfre.Location = new System.Drawing.Point(91, 69);
            this.tbopfre.Name = "tbopfre";
            this.tbopfre.Size = new System.Drawing.Size(100, 21);
            this.tbopfre.TabIndex = 2;
            // 
            // btntransCW
            // 
            this.btntransCW.Location = new System.Drawing.Point(43, 114);
            this.btntransCW.Name = "btntransCW";
            this.btntransCW.Size = new System.Drawing.Size(108, 23);
            this.btntransCW.TabIndex = 3;
            this.btntransCW.Text = "发射载波";
            this.btntransCW.UseVisualStyleBackColor = true;
            this.btntransCW.Click += new System.EventHandler(this.btntransCW_Click);
            // 
            // btnstopCW
            // 
            this.btnstopCW.Location = new System.Drawing.Point(217, 114);
            this.btnstopCW.Name = "btnstopCW";
            this.btnstopCW.Size = new System.Drawing.Size(108, 23);
            this.btnstopCW.TabIndex = 4;
            this.btnstopCW.Text = "停止发射载波";
            this.btnstopCW.UseVisualStyleBackColor = true;
            this.btnstopCW.Click += new System.EventHandler(this.btnstopCW_Click);
            // 
            // btnPRBSOn
            // 
            this.btnPRBSOn.Location = new System.Drawing.Point(217, 164);
            this.btnPRBSOn.Name = "btnPRBSOn";
            this.btnPRBSOn.Size = new System.Drawing.Size(108, 23);
            this.btnPRBSOn.TabIndex = 5;
            this.btnPRBSOn.Text = "发射调制数据";
            this.btnPRBSOn.UseVisualStyleBackColor = true;
            this.btnPRBSOn.Click += new System.EventHandler(this.btnPRBSOn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "时长";
            // 
            // tbdur
            // 
            this.tbdur.Location = new System.Drawing.Point(91, 164);
            this.tbdur.Name = "tbdur";
            this.tbdur.Size = new System.Drawing.Size(100, 21);
            this.tbdur.TabIndex = 7;
            // 
            // tbopant
            // 
            this.tbopant.Location = new System.Drawing.Point(91, 17);
            this.tbopant.Name = "tbopant";
            this.tbopant.Size = new System.Drawing.Size(100, 21);
            this.tbopant.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "天线：";
            // 
            // btnsetopant
            // 
            this.btnsetopant.Location = new System.Drawing.Point(217, 16);
            this.btnsetopant.Name = "btnsetopant";
            this.btnsetopant.Size = new System.Drawing.Size(108, 23);
            this.btnsetopant.TabIndex = 10;
            this.btnsetopant.Text = "设置";
            this.btnsetopant.UseVisualStyleBackColor = true;
            this.btnsetopant.Click += new System.EventHandler(this.btnsetopant_Click);
            // 
            // regulatoryFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 218);
            this.Controls.Add(this.btnsetopant);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbopant);
            this.Controls.Add(this.tbdur);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnPRBSOn);
            this.Controls.Add(this.btnstopCW);
            this.Controls.Add(this.btntransCW);
            this.Controls.Add(this.tbopfre);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnsetopfre);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "regulatoryFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "频率管制规则测试";
            this.Load += new System.EventHandler(this.regulatoryFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnsetopfre;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbopfre;
        private System.Windows.Forms.Button btntransCW;
        private System.Windows.Forms.Button btnstopCW;
        private System.Windows.Forms.Button btnPRBSOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbdur;
        private System.Windows.Forms.TextBox tbopant;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnsetopant;
    }
}