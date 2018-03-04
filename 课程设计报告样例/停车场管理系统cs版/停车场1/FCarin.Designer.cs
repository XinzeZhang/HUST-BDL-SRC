namespace CarManager
{
    partial class FCarin
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
            this.label19 = new System.Windows.Forms.Label();
            this.Minute = new System.Windows.Forms.ComboBox();
            this.Hour = new System.Windows.Forms.ComboBox();
            this.B_in = new System.Windows.Forms.Button();
            this.CarClass = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CarNo = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(136, 44);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(11, 12);
            this.label19.TabIndex = 10;
            this.label19.Text = ":";
            // 
            // Minute
            // 
            this.Minute.FormattingEnabled = true;
            this.Minute.Location = new System.Drawing.Point(153, 41);
            this.Minute.Name = "Minute";
            this.Minute.Size = new System.Drawing.Size(48, 20);
            this.Minute.TabIndex = 9;
            this.Minute.Text = "MM";
            // 
            // Hour
            // 
            this.Hour.FormattingEnabled = true;
            this.Hour.Location = new System.Drawing.Point(86, 41);
            this.Hour.Name = "Hour";
            this.Hour.Size = new System.Drawing.Size(44, 20);
            this.Hour.TabIndex = 8;
            this.Hour.Text = "HH";
            // 
            // B_in
            // 
            this.B_in.Location = new System.Drawing.Point(86, 105);
            this.B_in.Name = "B_in";
            this.B_in.Size = new System.Drawing.Size(75, 23);
            this.B_in.TabIndex = 7;
            this.B_in.Text = "确定入库";
            this.B_in.UseVisualStyleBackColor = true;
            this.B_in.Click += new System.EventHandler(this.B_in_Click);
            // 
            // CarClass
            // 
            this.CarClass.FormattingEnabled = true;
            this.CarClass.Location = new System.Drawing.Point(86, 70);
            this.CarClass.Name = "CarClass";
            this.CarClass.Size = new System.Drawing.Size(75, 20);
            this.CarClass.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "车辆型号：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "入库时间：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "车牌号码：";
            // 
            // CarNo
            // 
            this.CarNo.Location = new System.Drawing.Point(86, 12);
            this.CarNo.Name = "CarNo";
            this.CarNo.Size = new System.Drawing.Size(75, 21);
            this.CarNo.TabIndex = 0;
            this.CarNo.Text = "京A-12345";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.CarNo);
            this.panel1.Controls.Add(this.Minute);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Hour);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.B_in);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.CarClass);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 140);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(167, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 23);
            this.button1.TabIndex = 11;
            this.button1.Text = "手气不错";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FCarin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 164);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(280, 200);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(280, 200);
            this.Name = "FCarin";
            this.Text = "车辆入库：自动车位";
            this.Activated += new System.EventHandler(this.FCarin_Activated);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox Minute;
        private System.Windows.Forms.ComboBox Hour;
        private System.Windows.Forms.Button B_in;
        private System.Windows.Forms.ComboBox CarClass;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CarNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}