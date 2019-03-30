namespace PositionManage
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnPosition = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPosition = new System.Windows.Forms.TabPage();
            this.panMap = new System.Windows.Forms.Panel();
            this.contextMenuStripPic = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsMenuItemVaryImg = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemAddLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMenuItemImport = new System.Windows.Forms.ToolStripMenuItem();
            this.lbLocation = new System.Windows.Forms.Label();
            this.tabData = new System.Windows.Forms.TabPage();
            this.dgData = new System.Windows.Forms.DataGridView();
            this.colDataNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTagNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReferNo1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSignalStrength1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReferNo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSignalStrength2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colReferNo3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSignalStrength3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDataTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPosition.SuspendLayout();
            this.panMap.SuspendLayout();
            this.contextMenuStripPic.SuspendLayout();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(833, 489);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnPosition);
            this.groupBox3.Controls.Add(this.btnInit);
            this.groupBox3.Location = new System.Drawing.Point(16, 220);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(262, 249);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "工作模式";
            // 
            // btnPosition
            // 
            this.btnPosition.Location = new System.Drawing.Point(26, 142);
            this.btnPosition.Name = "btnPosition";
            this.btnPosition.Size = new System.Drawing.Size(210, 27);
            this.btnPosition.TabIndex = 6;
            this.btnPosition.Text = "定位模式";
            this.btnPosition.UseVisualStyleBackColor = true;
            this.btnPosition.Click += new System.EventHandler(this.btnPosition_Click);
            // 
            // btnInit
            // 
            this.btnInit.Location = new System.Drawing.Point(26, 45);
            this.btnInit.Name = "btnInit";
            this.btnInit.Size = new System.Drawing.Size(210, 31);
            this.btnInit.TabIndex = 5;
            this.btnInit.Text = "初始化模式";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnDisconnect);
            this.groupBox2.Controls.Add(this.btnConnect);
            this.groupBox2.Controls.Add(this.tbPort);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tbIP);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(16, 20);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(262, 160);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "连接";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(161, 118);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 4;
            this.btnDisconnect.Text = "断开";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(26, 118);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.Text = "连接";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(83, 77);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(64, 21);
            this.tbPort.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口号:";
            // 
            // tbIP
            // 
            this.tbIP.Location = new System.Drawing.Point(83, 39);
            this.tbIP.Name = "tbIP";
            this.tbIP.Size = new System.Drawing.Size(173, 21);
            this.tbIP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "接收器IP:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPosition);
            this.tabControl1.Controls.Add(this.tabData);
            this.tabControl1.Location = new System.Drawing.Point(284, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(533, 453);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyDown);
            // 
            // tabPosition
            // 
            this.tabPosition.Controls.Add(this.panMap);
            this.tabPosition.Location = new System.Drawing.Point(4, 21);
            this.tabPosition.Name = "tabPosition";
            this.tabPosition.Padding = new System.Windows.Forms.Padding(3);
            this.tabPosition.Size = new System.Drawing.Size(525, 428);
            this.tabPosition.TabIndex = 0;
            this.tabPosition.Text = "定位地图";
            this.tabPosition.UseVisualStyleBackColor = true;
            // 
            // panMap
            // 
            this.panMap.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMap.ContextMenuStrip = this.contextMenuStripPic;
            this.panMap.Controls.Add(this.lbLocation);
            this.panMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMap.Location = new System.Drawing.Point(3, 3);
            this.panMap.Name = "panMap";
            this.panMap.Size = new System.Drawing.Size(519, 422);
            this.panMap.TabIndex = 0;
            this.panMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panMap_MouseMove);
            // 
            // contextMenuStripPic
            // 
            this.contextMenuStripPic.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsMenuItemVaryImg,
            this.tsMenuItemAddLocation,
            this.tsMenuItemImport});
            this.contextMenuStripPic.Name = "contextMenuStripPic";
            this.contextMenuStripPic.Size = new System.Drawing.Size(143, 70);
            // 
            // tsMenuItemVaryImg
            // 
            this.tsMenuItemVaryImg.Name = "tsMenuItemVaryImg";
            this.tsMenuItemVaryImg.Size = new System.Drawing.Size(142, 22);
            this.tsMenuItemVaryImg.Text = "变更地图";
            this.tsMenuItemVaryImg.Click += new System.EventHandler(this.tsMenuItemVaryImg_Click);
            // 
            // tsMenuItemAddLocation
            // 
            this.tsMenuItemAddLocation.Name = "tsMenuItemAddLocation";
            this.tsMenuItemAddLocation.Size = new System.Drawing.Size(142, 22);
            this.tsMenuItemAddLocation.Text = "添加参考点";
            this.tsMenuItemAddLocation.Click += new System.EventHandler(this.tsMenuItemAddLocation_Click);
            // 
            // tsMenuItemImport
            // 
            this.tsMenuItemImport.Name = "tsMenuItemImport";
            this.tsMenuItemImport.Size = new System.Drawing.Size(142, 22);
            this.tsMenuItemImport.Text = "导入样点数据";
            this.tsMenuItemImport.Click += new System.EventHandler(this.tsMenuItemImport_Click);
            // 
            // lbLocation
            // 
            this.lbLocation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLocation.AutoSize = true;
            this.lbLocation.Location = new System.Drawing.Point(408, 408);
            this.lbLocation.Name = "lbLocation";
            this.lbLocation.Size = new System.Drawing.Size(41, 12);
            this.lbLocation.TabIndex = 4;
            this.lbLocation.Text = "坐标：";
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.dgData);
            this.tabData.Location = new System.Drawing.Point(4, 21);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(525, 428);
            this.tabData.TabIndex = 1;
            this.tabData.Text = "后台数据";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // dgData
            // 
            this.dgData.AllowUserToAddRows = false;
            this.dgData.AllowUserToDeleteRows = false;
            this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colDataNo,
            this.colTagNo,
            this.colReferNo1,
            this.colSignalStrength1,
            this.colReferNo2,
            this.colSignalStrength2,
            this.colReferNo3,
            this.colSignalStrength3,
            this.colDataTime});
            this.dgData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgData.Location = new System.Drawing.Point(3, 3);
            this.dgData.Name = "dgData";
            this.dgData.ReadOnly = true;
            this.dgData.RowHeadersVisible = false;
            this.dgData.RowTemplate.Height = 23;
            this.dgData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgData.Size = new System.Drawing.Size(519, 422);
            this.dgData.TabIndex = 0;
            // 
            // colDataNo
            // 
            this.colDataNo.DataPropertyName = "RowNo";
            this.colDataNo.FillWeight = 60F;
            this.colDataNo.HeaderText = "序号";
            this.colDataNo.Name = "colDataNo";
            this.colDataNo.ReadOnly = true;
            this.colDataNo.Width = 60;
            // 
            // colTagNo
            // 
            this.colTagNo.DataPropertyName = "TagNo";
            this.colTagNo.FillWeight = 80F;
            this.colTagNo.HeaderText = "标签编号";
            this.colTagNo.Name = "colTagNo";
            this.colTagNo.ReadOnly = true;
            this.colTagNo.Width = 80;
            // 
            // colReferNo1
            // 
            this.colReferNo1.DataPropertyName = "ReferNo1";
            this.colReferNo1.FillWeight = 80F;
            this.colReferNo1.HeaderText = "参考点1";
            this.colReferNo1.Name = "colReferNo1";
            this.colReferNo1.ReadOnly = true;
            this.colReferNo1.Width = 80;
            // 
            // colSignalStrength1
            // 
            this.colSignalStrength1.DataPropertyName = "Distance1";
            this.colSignalStrength1.FillWeight = 60F;
            this.colSignalStrength1.HeaderText = "强度1";
            this.colSignalStrength1.Name = "colSignalStrength1";
            this.colSignalStrength1.ReadOnly = true;
            this.colSignalStrength1.Width = 60;
            // 
            // colReferNo2
            // 
            this.colReferNo2.DataPropertyName = "ReferNo2";
            this.colReferNo2.FillWeight = 80F;
            this.colReferNo2.HeaderText = "参考点2";
            this.colReferNo2.Name = "colReferNo2";
            this.colReferNo2.ReadOnly = true;
            this.colReferNo2.Width = 80;
            // 
            // colSignalStrength2
            // 
            this.colSignalStrength2.DataPropertyName = "Distance2";
            this.colSignalStrength2.FillWeight = 60F;
            this.colSignalStrength2.HeaderText = "强度2";
            this.colSignalStrength2.Name = "colSignalStrength2";
            this.colSignalStrength2.ReadOnly = true;
            this.colSignalStrength2.Width = 60;
            // 
            // colReferNo3
            // 
            this.colReferNo3.DataPropertyName = "ReferNo3";
            this.colReferNo3.FillWeight = 80F;
            this.colReferNo3.HeaderText = "参考点3";
            this.colReferNo3.Name = "colReferNo3";
            this.colReferNo3.ReadOnly = true;
            this.colReferNo3.Width = 80;
            // 
            // colSignalStrength3
            // 
            this.colSignalStrength3.DataPropertyName = "Distance3";
            this.colSignalStrength3.FillWeight = 60F;
            this.colSignalStrength3.HeaderText = "强度3";
            this.colSignalStrength3.Name = "colSignalStrength3";
            this.colSignalStrength3.ReadOnly = true;
            this.colSignalStrength3.Width = 60;
            // 
            // colDataTime
            // 
            this.colDataTime.DataPropertyName = "ReadDate";
            dataGridViewCellStyle1.Format = "T";
            dataGridViewCellStyle1.NullValue = null;
            this.colDataTime.DefaultCellStyle = dataGridViewCellStyle1;
            this.colDataTime.HeaderText = "数据时间";
            this.colDataTime.Name = "colDataTime";
            this.colDataTime.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 513);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "定位程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPosition.ResumeLayout(false);
            this.panMap.ResumeLayout(false);
            this.panMap.PerformLayout();
            this.contextMenuStripPic.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnPosition;
        private System.Windows.Forms.Button btnInit;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPosition;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.Panel panMap;
        private System.Windows.Forms.Label lbLocation;
        private System.Windows.Forms.DataGridView dgData;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripPic;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemVaryImg;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemAddLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTagNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReferNo1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSignalStrength1;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReferNo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSignalStrength2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colReferNo3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSignalStrength3;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDataTime;
        private System.Windows.Forms.ToolStripMenuItem tsMenuItemImport;

    }
}

