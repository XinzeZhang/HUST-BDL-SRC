using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Drawing.Drawing2D;
namespace CarManager
{
    class Draw2
    {
        //DataAccess DataAccess1 = new DataAccess();
        //Functions functions1 = new Functions();
        //ToolTip toolTip1 = new ToolTip();

        //public static int borderx;
        //public static int bordery;
        //public static int[] portnum = new int[3];
        //public static int[] numx = new int[3];
        //public static int[] numy = new int[3];
        //public static int[] cwidth = new int[3];
        //public static int[] cheight = new int[3];
        //public static int[] linespace = new int[3];
        //public static int[] cellx = new int[3];
        //public static int[] celly = new int[3];
        //public static int[] carwidth = new int[3];
        //public static int[] carheight = new int[3];
        //public static int[] sign = new int[3];

        //public static int[] arrayno = new int[3];








        ////public static int NumxA, NumxB, NumxC;


        //public int NumxA
        //{
        //    get { return numx[0]; }
        //    set { numx[0] = value; }
        //}
        //public int NumxB
        //{
        //    get { return numx[1]; }
        //    set { numx[1] = value; }
        //}
        //public int NumxC
        //{
        //    get { return numx[2]; }
        //    set { numx[2] = value; }
        //}
        //public int BorderX
        //{
        //    get { return borderx; }
        //    set { borderx = value; }
        //}
        //public int BorderY
        //{
        //    get { return bordery; }
        //    set { bordery = value; }
        //}
        //public int LineSpaceA
        //{
        //    get { return linespace[0]; }
        //    set { linespace[0] = value; }
        //}
        //public int LineSpaceB
        //{
        //    get { return linespace[1]; }
        //    set { linespace[1] = value; }
        //}
        //public int LineSpaceC
        //{
        //    get { return linespace[2]; }
        //    set { linespace[2] = value; }
        //}

        //public void GetPortData(Control Control0,Control Control1,Control Control2)
        //{
        //    portnum[0] = DataAccess1.getPortnum("PortA");
        //    portnum[1] = DataAccess1.getPortnum("PortB");
        //    portnum[2] = DataAccess1.getPortnum("PortC");

        //    DataAccess1.getPortstate("PortA", out arrayno[0]);
        //    DataAccess1.getPortstate("PortB", out arrayno[1]);
        //    DataAccess1.getPortstate("PortC", out arrayno[2]);         

        //    cwidth[0] = Control0.Size.Width;
        //    cwidth[1] = Control1.Size.Width;
        //    cwidth[2] = Control2.Size.Width;

        //    cheight[0] = Control0.Size.Height;
        //    cheight[1] = Control1.Size.Height;
        //    cheight[2] = Control2.Size.Height;

        //    //for (int i = 0; i < 3; i++)
        //    //{
        //    //    if (portnum[i] % numx[i] == 0)
        //    //    {
        //    //        sign[i] = 1;
        //    //        numy[i] = portnum[i] / numx[i];
        //    //    }
        //    //    else
        //    //    {
        //    //        sign[i] = 0;
        //    //        numy[i] = portnum[i] / numx[i] + 1;
        //    //    }

        //    //    cellx[i] = Convert.ToInt32(Convert.ToDouble((cwidth[i] - (numx[i] - 1) * linespace[i] - borderx[i] * 2)) / numx[i]);
        //    //    celly[i] = Convert.ToInt32(Convert.ToDouble((cheight[i] - bordery[i] * 2)) / numy[i]);

        //    //    carwidth[i] = Convert.ToInt32(cellx[i] * 0.6);
        //    //    carheight[i] = Convert.ToInt32(celly[i] * 0.6);                          
        //    //}



        //    if (portnum[0] % numx[0] == 0)
        //    {
        //        sign[0] = 1;
        //        numy[0] = portnum[0] / numx[0];
        //    }
        //    else
        //    {
        //        sign[0] = 0;
        //        numy[0] = portnum[0] / numx[0] + 1;
        //    }

        //    cellx[0] = Convert.ToInt32(Convert.ToDouble((cwidth[0] - (numx[0] - 1) * linespace[0] - borderx[0] * 2)) / numx[0]);
        //    celly[0] = Convert.ToInt32(Convert.ToDouble((cheight[0] - bordery[0] * 2)) / numy[0]);

        //    carwidth[0] = Convert.ToInt32(cellx[0] * 0.6);
        //    carheight[0] = Convert.ToInt32(celly[0] * 0.6);     









        //}



        //public void drawport(Control Drawcontrol, string PortName)
        //{

        //    int no;

        //    if (PortName == "PortA") { no = 0; }

        //    else if (PortName == "PortB") { no = 1; }

        //    else { no = 2; }


        //    //--------------------------------------------------------数据---------------------------------------------------------------------


        //    Graphics Graphics1 = Drawcontrol.CreateGraphics();

        //    Pen pen1 = new Pen(Color.Black, 1);

        //    //----------------------------------------------------------画车位----------------------------------------------------------------

        //    for (int i = 0; i < numx[no]; i++)
        //    {
        //        if (sign[no] == 0)
        //        {
        //            if (i == numx[no] - 1)
        //            {
        //                Point p1k0 = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no]);
        //                Point p1k1 = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no] + celly[no] * numy[no] - (numx[no] * numy[no] - portnum[no]) * celly[no]);
        //                Point p2k0 = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no]);
        //                Point p2k1 = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no] + celly[no] * numy[no] - (numx[no] * numy[no] - portnum[no]) * celly[no]);
        //                Graphics1.DrawLine(pen1, p1k0, p1k1);
        //                Graphics1.DrawLine(pen1, p2k0, p2k1);

        //                for (int j = 0; j < numy[no] - (numx[no] * numy[no] - portnum[no]) + 1; j++)
        //                {
        //                    Point p10j = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no] + j * celly[no]);
        //                    Point p11j = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no] + j * celly[no]);
        //                    Graphics1.DrawLine(pen1, p10j, p11j);
        //                }
        //            }
        //            else
        //            {
        //                Point p1i0 = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no]);
        //                Point p1i1 = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no] + celly[no] * numy[no]);
        //                Point p2i0 = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no]);
        //                Point p2i1 = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no] + celly[no] * numy[no]);
        //                Graphics1.DrawLine(pen1, p1i0, p1i1);
        //                Graphics1.DrawLine(pen1, p2i0, p2i1);

        //                for (int j = 0; j < numy[no] + 1; j++)
        //                {
        //                    Point p10j = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no] + j * celly[no]);
        //                    Point p11j = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no] + j * celly[no]);
        //                    Graphics1.DrawLine(pen1, p10j, p11j);
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Point p1i0 = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no]);
        //            Point p1i1 = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no] + celly[no] * numy[no]);
        //            Point p2i0 = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no]);
        //            Point p2i1 = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no] + celly[no] * numy[no]);
        //            Graphics1.DrawLine(pen1, p1i0, p1i1);
        //            Graphics1.DrawLine(pen1, p2i0, p2i1);

        //            for (int j = 0; j < numy[no] + 1; j++)
        //            {
        //                Point p10j = new Point(borderx[no] + i * (cellx[no] + linespace[no]), bordery[no] + j * celly[no]);
        //                Point p11j = new Point(borderx[no] + i * (cellx[no] + linespace[no]) + cellx[no], bordery[no] + j * celly[no]);
        //                Graphics1.DrawLine(pen1, p10j, p11j);
        //            }
        //        }

        //    }

        //    //----------------------------------------------------------画车位----------------------------------------------------------------        

        //}






    }
}
