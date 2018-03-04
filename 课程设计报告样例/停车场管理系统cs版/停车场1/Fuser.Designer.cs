namespace CarManager
{
    partial class Fuser
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Uname = new System.Windows.Forms.TextBox();
            this.CarNo = new System.Windows.Forms.TextBox();
            this.PortNo = new System.Windows.Forms.TextBox();
            this.CarClass = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "姓名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "车牌号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(59, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "停车位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 168);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "车辆类型";
            // 
            // Uname
            // 
            this.Uname.Location = new System.Drawing.Point(106, 53);
            this.Uname.Name = "Uname";
            this.Uname.Size = new System.Drawing.Size(100, 21);
            this.Uname.TabIndex = 4;
            // 
            // CarNo
            // 
            this.CarNo.Location = new System.Drawing.Point(106, 90);
            this.CarNo.Name = "CarNo";
            this.CarNo.Size = new System.Drawing.Size(100, 21);
            this.CarNo.TabIndex = 5;
            // 
            // PortNo
            // 
            this.PortNo.Location = new System.Drawing.Point(106, 128);
            this.PortNo.Name = "PortNo";
            this.PortNo.Size = new System.Drawing.Size(100, 21);
            this.PortNo.TabIndex = 6;
            // 
            // CarClass
            // 
            this.CarClass.FormattingEnabled = true;
            this.CarClass.Items.AddRange(new object[] {
            "固定用户",
            "临时用户"});
            this.CarClass.Location = new System.Drawing.Point(106, 165);
            this.CarClass.Name = "CarClass";
            this.CarClass.Size = new System.Drawing.Size(99, 20);
            this.CarClass.TabIndex = 7;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(93, 212);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Fuser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.CarClass);
            this.Controls.Add(this.PortNo);
            this.Controls.Add(this.CarNo);
            this.Controls.Add(this.Uname);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Fuser";
            this.Text = "Fuser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox Uname;
        private System.Windows.Forms.TextBox CarNo;
        private System.Windows.Forms.TextBox PortNo;
        private System.Windows.Forms.ComboBox CarClass;
        private System.Windows.Forms.Button button1;
    }
}