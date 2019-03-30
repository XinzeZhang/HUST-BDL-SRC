using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PositionManage
{
    public partial class FormDefinePoint : Form
    {
        public FormDefinePoint()
        {
            InitializeComponent();
        }

        public int PointX
        {
            get;
            set;
        }

        public int PointY
        {
            get;
            set;
        }

        public Color PointColor
        {
            get;
            set;
        }

        public string PointName
        {
            get;
            set;
        }

        private void FormDefinePoint_Load(object sender, EventArgs e)
        {
            this.tbPtX.Text = this.PointX.ToString();
            this.tbPtY.Text = this.PointY.ToString();
            this.tbPtColor.Text = System.Drawing.ColorTranslator.ToHtml(PointColor);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                this.PointX = Convert.ToInt32(this.tbPtX.Text);
                this.PointY = Convert.ToInt32(this.tbPtY.Text);
            }
            catch
            {
                MessageBox.Show("请正确填写坐标值!");
                return;
            }
            this.PointName = this.tbPtName.Text.Trim();
            if (string.IsNullOrEmpty(this.PointName) || this.PointName.Length < 4)
            {
                MessageBox.Show("请填写4位参考点名称!");
                return;
            }

            if (this.panColor.BackColor.Name == "Control")
            {
                MessageBox.Show("请选择正确颜色!");
                return;
            }

            this.PointColor = this.panColor.BackColor;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tbPtColor_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.tbPtColor.Text.StartsWith("#"))
                {
                    this.panColor.BackColor = System.Drawing.ColorTranslator.FromHtml(this.tbPtColor.Text);
                }
                else
                {
                    this.panColor.BackColor = System.Drawing.Color.FromKnownColor((System.Drawing.KnownColor)Enum.Parse(typeof(System.Drawing.KnownColor), this.tbPtColor.Text));
                }
            }
            catch
            {

            }
        }
    }
}
