namespace ModuleReaderManager
{
    partial class MultiBankWriteFrm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckant4 = new System.Windows.Forms.CheckBox();
            this.ckant3 = new System.Windows.Forms.CheckBox();
            this.ckant2 = new System.Windows.Forms.CheckBox();
            this.ckant1 = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btndel = new System.Windows.Forms.Button();
            this.cbbwbank = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lvwriteitems = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.btnadd = new System.Windows.Forms.Button();
            this.rtbwdata = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbwaddr = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnwrite = new System.Windows.Forms.Button();
            this.btnstop = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbflen = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbfaddr = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbbfbank = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbaccesspwd = new System.Windows.Forms.TextBox();
            this.ckispwd = new System.Windows.Forms.CheckBox();
            this.labtip = new System.Windows.Forms.Label();
            this.ckisrepeat = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ckisnofilter = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckant4);
            this.groupBox1.Controls.Add(this.ckant3);
            this.groupBox1.Controls.Add(this.ckant2);
            this.groupBox1.Controls.Add(this.ckant1);
            this.groupBox1.Location = new System.Drawing.Point(12, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(307, 39);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "天线选择";
            // 
            // ckant4
            // 
            this.ckant4.AutoSize = true;
            this.ckant4.Location = new System.Drawing.Point(250, 14);
            this.ckant4.Name = "ckant4";
            this.ckant4.Size = new System.Drawing.Size(48, 16);
            this.ckant4.TabIndex = 4;
            this.ckant4.Text = "ant4";
            this.ckant4.UseVisualStyleBackColor = true;
            // 
            // ckant3
            // 
            this.ckant3.AutoSize = true;
            this.ckant3.Location = new System.Drawing.Point(173, 14);
            this.ckant3.Name = "ckant3";
            this.ckant3.Size = new System.Drawing.Size(48, 16);
            this.ckant3.TabIndex = 3;
            this.ckant3.Text = "ant3";
            this.ckant3.UseVisualStyleBackColor = true;
            // 
            // ckant2
            // 
            this.ckant2.AutoSize = true;
            this.ckant2.Location = new System.Drawing.Point(90, 14);
            this.ckant2.Name = "ckant2";
            this.ckant2.Size = new System.Drawing.Size(48, 16);
            this.ckant2.TabIndex = 2;
            this.ckant2.Text = "ant2";
            this.ckant2.UseVisualStyleBackColor = true;
            // 
            // ckant1
            // 
            this.ckant1.AutoSize = true;
            this.ckant1.Location = new System.Drawing.Point(13, 14);
            this.ckant1.Name = "ckant1";
            this.ckant1.Size = new System.Drawing.Size(48, 16);
            this.ckant1.TabIndex = 1;
            this.ckant1.Text = "ant1";
            this.ckant1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btndel);
            this.groupBox2.Controls.Add(this.cbbwbank);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lvwriteitems);
            this.groupBox2.Controls.Add(this.btnadd);
            this.groupBox2.Controls.Add(this.rtbwdata);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.tbwaddr);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(307, 242);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "写内容";
            // 
            // btndel
            // 
            this.btndel.Location = new System.Drawing.Point(243, 121);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(55, 23);
            this.btndel.TabIndex = 8;
            this.btndel.Text = "移除";
            this.btndel.UseVisualStyleBackColor = true;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            // 
            // cbbwbank
            // 
            this.cbbwbank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbwbank.FormattingEnabled = true;
            this.cbbwbank.Items.AddRange(new object[] {
            "bank0",
            "bank1",
            "bank3"});
            this.cbbwbank.Location = new System.Drawing.Point(40, 17);
            this.cbbwbank.Name = "cbbwbank";
            this.cbbwbank.Size = new System.Drawing.Size(77, 20);
            this.cbbwbank.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "bank";
            // 
            // lvwriteitems
            // 
            this.lvwriteitems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4});
            this.lvwriteitems.FullRowSelect = true;
            this.lvwriteitems.Location = new System.Drawing.Point(13, 150);
            this.lvwriteitems.Name = "lvwriteitems";
            this.lvwriteitems.Size = new System.Drawing.Size(285, 80);
            this.lvwriteitems.TabIndex = 7;
            this.lvwriteitems.UseCompatibleStateImageBehavior = false;
            this.lvwriteitems.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "bank";
            this.columnHeader1.Width = 48;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "起始地址";
            this.columnHeader2.Width = 68;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "数据";
            this.columnHeader4.Width = 157;
            // 
            // btnadd
            // 
            this.btnadd.Location = new System.Drawing.Point(37, 121);
            this.btnadd.Name = "btnadd";
            this.btnadd.Size = new System.Drawing.Size(55, 23);
            this.btnadd.TabIndex = 6;
            this.btnadd.Text = "添加";
            this.btnadd.UseVisualStyleBackColor = true;
            this.btnadd.Click += new System.EventHandler(this.btnadd_Click);
            // 
            // rtbwdata
            // 
            this.rtbwdata.Location = new System.Drawing.Point(40, 44);
            this.rtbwdata.Name = "rtbwdata";
            this.rtbwdata.Size = new System.Drawing.Size(258, 71);
            this.rtbwdata.TabIndex = 5;
            this.rtbwdata.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据";
            // 
            // tbwaddr
            // 
            this.tbwaddr.Location = new System.Drawing.Point(238, 16);
            this.tbwaddr.Name = "tbwaddr";
            this.tbwaddr.Size = new System.Drawing.Size(60, 21);
            this.tbwaddr.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(179, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "起始地址";
            // 
            // btnwrite
            // 
            this.btnwrite.Location = new System.Drawing.Point(12, 397);
            this.btnwrite.Name = "btnwrite";
            this.btnwrite.Size = new System.Drawing.Size(61, 23);
            this.btnwrite.TabIndex = 2;
            this.btnwrite.Text = "写入";
            this.btnwrite.UseVisualStyleBackColor = true;
            this.btnwrite.Click += new System.EventHandler(this.btnwrite_Click);
            // 
            // btnstop
            // 
            this.btnstop.Location = new System.Drawing.Point(254, 397);
            this.btnstop.Name = "btnstop";
            this.btnstop.Size = new System.Drawing.Size(65, 23);
            this.btnstop.TabIndex = 3;
            this.btnstop.Text = "停止";
            this.btnstop.UseVisualStyleBackColor = true;
            this.btnstop.Click += new System.EventHandler(this.btnstop_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbflen);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tbfaddr);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.cbbfbank);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(12, 299);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(307, 43);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "过滤选择";
            // 
            // tbflen
            // 
            this.tbflen.Location = new System.Drawing.Point(262, 14);
            this.tbflen.Name = "tbflen";
            this.tbflen.Size = new System.Drawing.Size(36, 21);
            this.tbflen.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(207, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "数据长度";
            // 
            // tbfaddr
            // 
            this.tbfaddr.Location = new System.Drawing.Point(169, 14);
            this.tbfaddr.Name = "tbfaddr";
            this.tbfaddr.Size = new System.Drawing.Size(36, 21);
            this.tbfaddr.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(113, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "起始地址";
            // 
            // cbbfbank
            // 
            this.cbbfbank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbfbank.FormattingEnabled = true;
            this.cbbfbank.Items.AddRange(new object[] {
            "bank0",
            "bank1",
            "bank2",
            "bank3"});
            this.cbbfbank.Location = new System.Drawing.Point(42, 15);
            this.cbbfbank.Name = "cbbfbank";
            this.cbbfbank.Size = new System.Drawing.Size(67, 20);
            this.cbbfbank.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "bank";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "访问密码";
            // 
            // tbaccesspwd
            // 
            this.tbaccesspwd.Location = new System.Drawing.Point(60, 15);
            this.tbaccesspwd.Name = "tbaccesspwd";
            this.tbaccesspwd.Size = new System.Drawing.Size(56, 21);
            this.tbaccesspwd.TabIndex = 9;
            // 
            // ckispwd
            // 
            this.ckispwd.AutoSize = true;
            this.ckispwd.Location = new System.Drawing.Point(121, 18);
            this.ckispwd.Name = "ckispwd";
            this.ckispwd.Size = new System.Drawing.Size(48, 16);
            this.ckispwd.TabIndex = 10;
            this.ckispwd.Text = "密码";
            this.ckispwd.UseVisualStyleBackColor = true;
            // 
            // labtip
            // 
            this.labtip.AutoSize = true;
            this.labtip.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labtip.ForeColor = System.Drawing.Color.Red;
            this.labtip.Location = new System.Drawing.Point(83, 399);
            this.labtip.Name = "labtip";
            this.labtip.Size = new System.Drawing.Size(42, 16);
            this.labtip.TabIndex = 6;
            this.labtip.Text = "提示";
            // 
            // ckisrepeat
            // 
            this.ckisrepeat.AutoSize = true;
            this.ckisrepeat.Location = new System.Drawing.Point(175, 18);
            this.ckisrepeat.Name = "ckisrepeat";
            this.ckisrepeat.Size = new System.Drawing.Size(60, 16);
            this.ckisrepeat.TabIndex = 7;
            this.ckisrepeat.Text = "重复写";
            this.ckisrepeat.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ckisnofilter);
            this.groupBox4.Controls.Add(this.ckispwd);
            this.groupBox4.Controls.Add(this.ckisrepeat);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tbaccesspwd);
            this.groupBox4.Location = new System.Drawing.Point(12, 344);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(307, 42);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "杂项";
            // 
            // ckisnofilter
            // 
            this.ckisnofilter.AutoSize = true;
            this.ckisnofilter.Location = new System.Drawing.Point(242, 17);
            this.ckisnofilter.Name = "ckisnofilter";
            this.ckisnofilter.Size = new System.Drawing.Size(60, 16);
            this.ckisnofilter.TabIndex = 11;
            this.ckisnofilter.Text = "不过滤";
            this.ckisnofilter.UseVisualStyleBackColor = true;
            // 
            // MultiBankWriteFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 429);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labtip);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnstop);
            this.Controls.Add(this.btnwrite);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MultiBankWriteFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gen2多bank写";
            this.Load += new System.EventHandler(this.MultiBankWriteFrm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MultiBankWriteFrm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckant4;
        private System.Windows.Forms.CheckBox ckant3;
        private System.Windows.Forms.CheckBox ckant2;
        private System.Windows.Forms.CheckBox ckant1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtbwdata;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbwaddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvwriteitems;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Button btnadd;
        private System.Windows.Forms.Button btndel;
        private System.Windows.Forms.ComboBox cbbwbank;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnwrite;
        private System.Windows.Forms.Button btnstop;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbbfbank;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox ckispwd;
        private System.Windows.Forms.TextBox tbaccesspwd;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbflen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbfaddr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labtip;
        private System.Windows.Forms.CheckBox ckisrepeat;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox ckisnofilter;
    }
}