namespace PDADemo_CF2._0
{
    partial class UpdateForm
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
            this.label4 = new System.Windows.Forms.Label();
            this.UpdatetypecomboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filetextBox = new System.Windows.Forms.TextBox();
            this.Updatebutton = new System.Windows.Forms.Button();
            this.Viewbutton = new System.Windows.Forms.Button();
            this.UpdateprogressBar = new System.Windows.Forms.ProgressBar();
            this.Leavebutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.addrtextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(3, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 17);
            this.label4.Text = "读写器类型";
            // 
            // UpdatetypecomboBox
            // 
            this.UpdatetypecomboBox.Items.Add("TM系列");
            this.UpdatetypecomboBox.Items.Add("M5E_A7");
            this.UpdatetypecomboBox.Items.Add("M6E_A7");
            this.UpdatetypecomboBox.Items.Add("PR系列");
            this.UpdatetypecomboBox.Items.Add("SL系列");
            this.UpdatetypecomboBox.Items.Add("M1S");
            this.UpdatetypecomboBox.Location = new System.Drawing.Point(84, 13);
            this.UpdatetypecomboBox.Name = "UpdatetypecomboBox";
            this.UpdatetypecomboBox.Size = new System.Drawing.Size(83, 23);
            this.UpdatetypecomboBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 20);
            this.label1.Text = "固件";
            // 
            // filetextBox
            // 
            this.filetextBox.Location = new System.Drawing.Point(35, 79);
            this.filetextBox.Name = "filetextBox";
            this.filetextBox.Size = new System.Drawing.Size(132, 23);
            this.filetextBox.TabIndex = 4;
            // 
            // Updatebutton
            // 
            this.Updatebutton.Location = new System.Drawing.Point(54, 147);
            this.Updatebutton.Name = "Updatebutton";
            this.Updatebutton.Size = new System.Drawing.Size(58, 20);
            this.Updatebutton.TabIndex = 5;
            this.Updatebutton.Text = "升级";
            this.Updatebutton.Click += new System.EventHandler(this.Updatebutton_Click);
            // 
            // Viewbutton
            // 
            this.Viewbutton.Location = new System.Drawing.Point(3, 147);
            this.Viewbutton.Name = "Viewbutton";
            this.Viewbutton.Size = new System.Drawing.Size(45, 20);
            this.Viewbutton.TabIndex = 6;
            this.Viewbutton.Text = "浏览";
            this.Viewbutton.Click += new System.EventHandler(this.Viewbutton_Click);
            // 
            // UpdateprogressBar
            // 
            this.UpdateprogressBar.Location = new System.Drawing.Point(0, 121);
            this.UpdateprogressBar.Name = "UpdateprogressBar";
            this.UpdateprogressBar.Size = new System.Drawing.Size(183, 20);
            // 
            // Leavebutton
            // 
            this.Leavebutton.Location = new System.Drawing.Point(118, 147);
            this.Leavebutton.Name = "Leavebutton";
            this.Leavebutton.Size = new System.Drawing.Size(58, 20);
            this.Leavebutton.TabIndex = 8;
            this.Leavebutton.Text = "离开";
            this.Leavebutton.Click += new System.EventHandler(this.Leavebutton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.Text = "地址";
            // 
            // addrtextBox
            // 
            this.addrtextBox.Location = new System.Drawing.Point(38, 44);
            this.addrtextBox.Name = "addrtextBox";
            this.addrtextBox.Size = new System.Drawing.Size(51, 23);
            this.addrtextBox.TabIndex = 13;
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(183, 183);
            this.ControlBox = false;
            this.Controls.Add(this.addrtextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Leavebutton);
            this.Controls.Add(this.UpdateprogressBar);
            this.Controls.Add(this.Viewbutton);
            this.Controls.Add(this.Updatebutton);
            this.Controls.Add(this.filetextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UpdatetypecomboBox);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.Text = "升级";
            this.Load += new System.EventHandler(this.UpdateForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox UpdatetypecomboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox filetextBox;
        private System.Windows.Forms.Button Updatebutton;
        private System.Windows.Forms.Button Viewbutton;
        private System.Windows.Forms.ProgressBar UpdateprogressBar;
        private System.Windows.Forms.Button Leavebutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox addrtextBox;
    }
}