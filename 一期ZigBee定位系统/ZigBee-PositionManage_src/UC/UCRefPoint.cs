using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PositionManage.UC
{
    public partial class UCRefPoint : Label
    {
        public UCRefPoint()
        {
            InitializeComponent();

            this.AutoSize = true;
            this.Padding = new Padding(10);
            this.Font = new System.Drawing.Font("宋体", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }

        public Color PointColor
        {
            get
            {
                return this.BackColor;             
            }
            set
            {
                this.BackColor = value;

                if (value.GetBrightness() < 0.3)
                {
                    this.ForeColor = Color.White;
                }
                else
                {
                    this.ForeColor = Color.Black;
                }
            }
        }

        public string PointName
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }

        public bool IsSelected
        {
            get
            {
                return this.BorderStyle == BorderStyle.FixedSingle;
            }
            set
            {
                if (value)
                {
                    this.BorderStyle = BorderStyle.FixedSingle;
                }
                else
                {
                    this.BorderStyle = BorderStyle.None;
                }
            }
        }

        private void UCRefPoint_Paint(object sender, PaintEventArgs e)
        {
            if (this.IsSelected)
            {
                ControlPaint.DrawBorder(e.Graphics,
                                    this.ClientRectangle,
                                    Color.LightSeaGreen,//7f9db9
                                    1,
                                    ButtonBorderStyle.Solid,
                                    Color.LightSeaGreen,
                                    1,
                                    ButtonBorderStyle.Solid,
                                    Color.LightSeaGreen,
                                    1,
                                    ButtonBorderStyle.Solid,
                                    Color.LightSeaGreen,
                                    1,
                                    ButtonBorderStyle.Solid);
            }
            else
            {

            }
        }
    }
}
