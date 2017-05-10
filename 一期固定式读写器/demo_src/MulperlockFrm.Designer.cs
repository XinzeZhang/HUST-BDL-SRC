namespace ModuleReaderManager
{
    partial class MulperlockFrm
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
            this.btnlock = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnlock
            // 
            this.btnlock.Location = new System.Drawing.Point(30, 171);
            this.btnlock.Name = "btnlock";
            this.btnlock.Size = new System.Drawing.Size(75, 23);
            this.btnlock.TabIndex = 0;
            this.btnlock.Text = "锁定";
            this.btnlock.UseVisualStyleBackColor = true;
            this.btnlock.Click += new System.EventHandler(this.btnlock_Click);
            // 
            // MulperlockFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 224);
            this.Controls.Add(this.btnlock);
            this.Name = "MulperlockFrm";
            this.Text = "MulperlockFrm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnlock;
    }
}