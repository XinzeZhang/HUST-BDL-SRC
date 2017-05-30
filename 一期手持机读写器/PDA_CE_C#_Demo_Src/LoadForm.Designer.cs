namespace PDADemo_CF2._0
{
    partial class LoadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoadForm));
            this.Canclebutton = new System.Windows.Forms.Button();
            this.AboutpictureBox = new System.Windows.Forms.PictureBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.Loadlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Canclebutton
            // 
            this.Canclebutton.Location = new System.Drawing.Point(157, 170);
            this.Canclebutton.Name = "Canclebutton";
            this.Canclebutton.Size = new System.Drawing.Size(72, 20);
            this.Canclebutton.TabIndex = 0;
            this.Canclebutton.Text = "取消";
            this.Canclebutton.Click += new System.EventHandler(this.Canclebutton_Click);
            // 
            // AboutpictureBox
            // 
            //this.AboutpictureBox.Image = ((System.Drawing.Image)(resources.GetObject("AboutpictureBox.Image")));
            //this.AboutpictureBox.Location = new System.Drawing.Point(0, 0);
            //this.AboutpictureBox.Name = "AboutpictureBox";
            //this.AboutpictureBox.Size = new System.Drawing.Size(237, 87);
            //this.AboutpictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            //this.AboutpictureBox.Visible = false;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(0, 93);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(237, 20);
            // 
            // Loadlabel
            // 
            this.Loadlabel.Location = new System.Drawing.Point(3, 119);
            this.Loadlabel.Name = "Loadlabel";
            this.Loadlabel.Size = new System.Drawing.Size(226, 48);
            // 
            // LoadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(237, 193);
            this.ControlBox = false;
            this.Controls.Add(this.Loadlabel);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.AboutpictureBox);
            this.Controls.Add(this.Canclebutton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoadForm";
            this.Text = "LoadForm";
            this.Load += new System.EventHandler(this.LoadForm_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.LoadForm_Closing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Canclebutton;
        private System.Windows.Forms.PictureBox AboutpictureBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label Loadlabel;
    }
}