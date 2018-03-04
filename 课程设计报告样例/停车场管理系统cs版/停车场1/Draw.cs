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
    class Draw
    {
        DataAccess DataAccess1 = new DataAccess();
        Functions functions1 = new Functions();
        ToolTip toolTip1 = new ToolTip();//鼠标移动到车位时，出现对入库车辆的介绍信息
        public static string portnotoadd;
        public static string detailCarno;

        public string myportno//获取停车位
        {
            get
            {
                return portnotoadd;           
            }
            set
            {
                portnotoadd = value;
            }
        }

        public string mycarno//获取车牌号
        {
            get
            {
                return detailCarno;
            }
            set
            {
                detailCarno = value;
            }
        }


//================================================================================================================================================

        public void drawdetail(Control Drawcontrol, string CarNo)//绘制右下角方框，每辆车具体停车信息
        {
            int time1, time2, time3, intime, outtime;
            double Rate1, Rate2, Rate3;
            string CarCla, PortNo, sintime, souttime;

            int Cwidth, Cheight, left, down, cellx, celly, infox, infoy, infow, infoh;

            detailCarno = CarNo;
//-------------------------------------------------------------------------数据---------------------------------------------------------

            time1 = time2 = time3 = intime = outtime = 0;
            Rate1 = Rate2 = Rate3 = 0;
            CarCla = PortNo = sintime = souttime = "";
            
            infox = infoy = infow = infoh = 0;

            left = 10;
            down = 55;

            Cheight = Drawcontrol.Size.Height;
            Cwidth = Drawcontrol.Size.Width;
            cellx = Convert.ToInt32(Convert.ToDouble((Cwidth - left)) / 25);
            celly = Convert.ToInt32(Convert.ToDouble((Cheight - down)) / 18);

            infow = 150;
            infoh = 85;
            infox = Cwidth - infow - left;
            infoy = 10;

//-------------------------------------------------------------------------数据---------------------------------------------------------   
 
            Graphics Graphics1 = Drawcontrol.CreateGraphics();
            Font myFont1 = new Font("Arial", 10, GraphicsUnit.Pixel);
            Font myFont2 = new Font("微软雅黑", 9, FontStyle.Bold);
            Font myFont3 = new Font("微软雅黑", 9, FontStyle.Regular);

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

            if (CarNo == "axis")
            {
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            //-----------------------------------------------------------------------画坐标轴------------------------------------------------------------------------------------

                for (int i = 0; i < 25; i++)                                                                         //横轴刻度
                {
                    if (i % 5 == 0)
                    {
                        Point P1 = new Point(cellx * i + left, Cheight - down - 7);
                        Point Q1 = new Point(cellx * i + left, Cheight - down);
                        Pen Ptemp = new Pen(Color.Red);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                        Graphics1.DrawString(i.ToString(), new Font("Tahoma", 6), Brushes.Black, new PointF(cellx * i + left - 3, Cheight - down));
                    }
                    else
                    {
                        Point P1 = new Point(cellx * i + left, Cheight - down - 5);
                        Point Q1 = new Point(cellx * i + left, Cheight - down);
                        Pen Ptemp = new Pen(Color.Black);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                    }
                    Graphics1.DrawString(24.ToString(), new Font("Tahoma", 6), Brushes.Black, new PointF(cellx * 24 + left - 3, Cheight - down));
                }

                for (int i = 1; i < 16; i++)                                                                         //纵轴刻度
                {
                    if (i % 5 == 0)
                    {
                        Point P1 = new Point(1 + left, Cheight - down - i * celly);
                        Point Q1 = new Point(6 + left, Cheight - down - i * celly);
                        Pen Ptemp = new Pen(Color.Red);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                        Graphics1.DrawString((i / 5).ToString(), new Font("Tahoma", 6), Brushes.Black, new PointF(left - 8, Cheight - down - i * celly - 4));
                    }
                    else
                    {
                        Point P1 = new Point(1 + left, Cheight - down - i * celly);
                        Point Q1 = new Point(4 + left, Cheight - down - i * celly);
                        Pen Ptemp = new Pen(Color.Black);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                    }
                }

                Pen Penaxis = new Pen(Color.Black, 2);                                                               //轴       
                Point p0 = new Point(left, 0);
                Point p1 = new Point(left, Cheight - down);
                Point p2 = new Point(Cwidth, Cheight - down);
                Point[] parray1 = new Point[3];
                parray1[0] = p0;
                parray1[1] = p1;
                parray1[2] = p2;
                Graphics1.DrawLines(Penaxis, parray1);

            //-----------------------------------------------------------------------画坐标轴------------------------------------------------------------------------------------
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ 
            }
            else
            {
                DataAccess1.getCardetail(CarNo, out CarCla, out sintime, out PortNo);
                DataAccess1.getRate(CarCla, out time1, out time2, out time3, out Rate1, out Rate2, out Rate3);
                functions1.Orderit(ref time1, ref time2, ref time3, ref Rate1, ref Rate2, ref Rate3);
                souttime = functions1.getOuttime();
                intime = functions1.getHtime(sintime);
                outtime = functions1.getHtime(souttime);

//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                //-------------------------------------------------------------------------填充----------------------------------------------------------------------------------------

                if (intime > outtime)
                {
                    if (outtime < time1 + 1)
                    {
                        if (intime < time1 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (time1 - intime) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + intime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + intime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (time2 - time1) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + time2)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + time2)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, (time3 - time2) * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);

                            Pen pfill4 = new Pen(Color.LawnGreen, (24 - time3) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, outtime * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);


                        }
                        else if (intime < time2 + 1 && intime > time1)
                        {
                            Pen pfill2 = new Pen(Color.LawnGreen, (time2 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, (time3 - time2) * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);

                            Pen pfill4 = new Pen(Color.LawnGreen, (24 - time3) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, outtime * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);

                        }
                        else if (intime > time2 && intime < time3 + 1)
                        {
                            Pen pfill2 = new Pen(Color.LawnGreen, (time3 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill4 = new Pen(Color.LawnGreen, (24 - time3) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, outtime * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);


                        }
                        else
                        {
                            Pen pfill4 = new Pen(Color.LawnGreen, (24 - intime) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, outtime * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(outtime) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);


                        }
                    }
                    else if (outtime > time1 && outtime < time2 + 1)
                    {

                        if (intime < time2 + 1)
                        {
                            Pen pfill2 = new Pen(Color.LawnGreen, (time2 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, (time3 - time2) * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);

                            Pen pfill4 = new Pen(Color.LawnGreen, (24 - time3) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, time1 * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);

                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time1) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);


                        }
                        else if (intime < time3 + 1 && intime > time2)
                        {
                            Pen pfill3 = new Pen(Color.LawnGreen, (time3 - intime) * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);

                            Pen pfill4 = new Pen(Color.LawnGreen, (24 - time3) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, time1 * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);

                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time1) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);


                        }
                        else
                        {
                            Pen pfill4 = new Pen(Color.LawnGreen, (24 - intime) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, time1 * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);

                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time1) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                        }
                    }
                    else if (outtime > time2 && outtime < time3 + 1)
                    {
                        if (intime < time3 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (time3 - intime) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (24 - time3) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, time1 * cellx);
                            Point pof31 = new Point(Convert.ToInt32(Convert.ToDouble((time1 / 2) * cellx)) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32(Convert.ToDouble((time1 / 2) * cellx)) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);

                            Pen pfill4 = new Pen(Color.LawnGreen, (time2 - time1) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time1)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time1)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, (outtime - time2) * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);
                        }
                        else
                        {
                            Pen pfill2 = new Pen(Color.LawnGreen, (24 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, time1 * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble(time1 / 2) * cellx)) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble(time1 / 2) * cellx)) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);

                            Pen pfill4 = new Pen(Color.LawnGreen, (time2 - time1) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time1)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time1)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);

                            Pen pfill5 = new Pen(Color.LawnGreen, (outtime - time2) * cellx);
                            Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill5, pof51, pof52);
                        }


                    }


                    else
                    {
                        Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time3) * cellx);
                        Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                        Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down);
                        Graphics1.DrawLine(pfill1, pof11, pof12);

                        Pen pfill2 = new Pen(Color.LawnGreen, (time2 - time1) * cellx);
                        Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + time2)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                        Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + time2)) / 2) * cellx) + left, Cheight - down);
                        Graphics1.DrawLine(pfill2, pof21, pof22);

                        Pen pfill3 = new Pen(Color.LawnGreen, (time3 - time2) * cellx);
                        Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                        Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down);
                        Graphics1.DrawLine(pfill3, pof31, pof32);

                        Pen pfill4 = new Pen(Color.LawnGreen, (24 - intime) * cellx);
                        Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                        Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((intime + 24)) / 2) * cellx) + left, Cheight - down);
                        Graphics1.DrawLine(pfill4, pof41, pof42);

                        Pen pfill5 = new Pen(Color.LawnGreen, time1 * cellx);
                        Point pof51 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                        Point pof52 = new Point(Convert.ToInt32((Convert.ToDouble(time1) / 2) * cellx) + left, Cheight - down);
                        Graphics1.DrawLine(pfill5, pof51, pof52);
                    }
                }
                else
                {
                    if (intime < time1 + 1)
                    {
                        if (outtime < time1 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - intime) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);


                        }
                        else if (outtime > time1 && outtime < time2 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time1) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (time1 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time1)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time1)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);


                        }
                        else if (outtime > time2 && outtime < time3 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time2) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (time1 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time1)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time1)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, (time2 - time1) * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time1)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time1)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);


                        }
                        else
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time3) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (time2 - time1) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + time2)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((time1 + time2)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, (time3 - time2) * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);

                            Pen pfill4 = new Pen(Color.LawnGreen, (time1 - intime) * cellx);
                            Point pof41 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time1)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof42 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time1)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill4, pof41, pof42);


                        }
                    }
                    else if (intime > time1 && intime < time2 + 1)
                    {
                        if (outtime < time2 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - intime) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);
                        }
                        else if (outtime > time2 && outtime < time3 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time2) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (time2 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);


                        }
                        else
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time3) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (time2 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time2)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);

                            Pen pfill3 = new Pen(Color.LawnGreen, (time3 - time2) * cellx);
                            Point pof31 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof32 = new Point(Convert.ToInt32((Convert.ToDouble((time2 + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill3, pof31, pof32);


                        }
                    }
                    else if (intime > time2 && intime < time3 + 1)
                    {
                        if (outtime < time3 + 1)
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - intime) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);


                        }
                        else
                        {
                            Pen pfill1 = new Pen(Color.LawnGreen, (outtime - time3) * cellx);
                            Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                            Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((time3 + outtime)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill1, pof11, pof12);

                            Pen pfill2 = new Pen(Color.LawnGreen, (time3 - intime) * cellx);
                            Point pof21 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                            Point pof22 = new Point(Convert.ToInt32((Convert.ToDouble((intime + time3)) / 2) * cellx) + left, Cheight - down);
                            Graphics1.DrawLine(pfill2, pof21, pof22);


                        }
                    }
                    else
                    {
                        Pen pfill1 = new Pen(Color.LawnGreen, (outtime - intime) * cellx);
                        Point pof11 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                        Point pof12 = new Point(Convert.ToInt32((Convert.ToDouble((intime + outtime)) / 2) * cellx) + left, Cheight - down);
                        Graphics1.DrawLine(pfill1, pof11, pof12);
                    }
                }
                //-------------------------------------------------------------------------填充----------------------------------------------------------------------------------------




                //-----------------------------------------------------------------------画坐标轴------------------------------------------------------------------------------------

                for (int i = 0; i < 24; i++)                                                                         //横轴刻度
                {
                    if (i % 5 == 0)
                    {
                        Point P1 = new Point(cellx * i + left, Cheight - down - 7);
                        Point Q1 = new Point(cellx * i + left, Cheight - down);
                        Pen Ptemp = new Pen(Color.Red);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                        Graphics1.DrawString(i.ToString(), new Font("Tahoma", 6), Brushes.Black, new PointF(cellx * i + left - 3, Cheight - down));
                    }
                    else
                    {
                        Point P1 = new Point(cellx * i + left, Cheight - down - 5);
                        Point Q1 = new Point(cellx * i + left, Cheight - down);
                        Pen Ptemp = new Pen(Color.Black);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                    }
                    Graphics1.DrawString(24.ToString(), new Font("Tahoma", 6), Brushes.Black, new PointF(cellx * 24 + left - 3, Cheight - down));
                }

                for (int i = 1; i < 16; i++)                                                                         //纵轴刻度
                {
                    if (i % 5 == 0)
                    {
                        Point P1 = new Point(1 + left, Cheight - down - i * celly);
                        Point Q1 = new Point(6 + left, Cheight - down - i * celly);
                        Pen Ptemp = new Pen(Color.Red);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                        Graphics1.DrawString((i / 5).ToString(), new Font("Tahoma", 6), Brushes.Black, new PointF(left - 8, Cheight - down - i * celly - 4));
                    }
                    else
                    {
                        Point P1 = new Point(1 + left, Cheight - down - i * celly);
                        Point Q1 = new Point(4 + left, Cheight - down - i * celly);
                        Pen Ptemp = new Pen(Color.Black);
                        Graphics1.DrawLine(Ptemp, P1, Q1);
                    }
                }

                Pen Penaxis = new Pen(Color.Black, 2);                                                               //轴       
                Point p0 = new Point(left, 0);
                Point p1 = new Point(left, Cheight - down);
                Point p2 = new Point(Cwidth, Cheight - down);
                Point[] parray1 = new Point[3];
                parray1[0] = p0;
                parray1[1] = p1;
                parray1[2] = p2;
                Graphics1.DrawLines(Penaxis, parray1);

                //-----------------------------------------------------------------------画坐标轴------------------------------------------------------------------------------------



                //-----------------------------------------------------------------------画收费函数图形------------------------------------------------------------------------------------

                Pen Penfare = new Pen(Color.Black, 1);
                Point Pt0 = new Point(left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                Point Pt1 = new Point(time1 * cellx + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                Point Pt2 = new Point(time1 * cellx + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                Point Pt3 = new Point(time2 * cellx + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                Point Pt4 = new Point(time2 * cellx + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                Point Pt5 = new Point(time3 * cellx + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                Point Pt6 = new Point(time3 * cellx + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                Point Pt7 = new Point(24 * cellx + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                Point Pt8 = new Point(24 * cellx + left, Cheight - down);
                Point[] parray2 = new Point[9];
                parray2[0] = Pt0;
                parray2[1] = Pt1;
                parray2[2] = Pt2;
                parray2[3] = Pt3;
                parray2[4] = Pt4;
                parray2[5] = Pt5;
                parray2[6] = Pt6;
                parray2[7] = Pt7;
                parray2[8] = Pt8;
                Graphics1.DrawLines(Penfare, parray2);

                //-----------------------------------------------------------------------画收费函数图形------------------------------------------------------------------------------------




                //-----------------------------------------------------------------------画进出时间线------------------------------------------------------------------------------------

                Point Pin1, Pin2, Pout1, Pout2;
                Pin2 = new Point(intime * cellx + left, Cheight - down);
                Pout2 = new Point(outtime * cellx + left, Cheight - down);
                Pen Penin = new Pen(Color.Green, 2);
                Pen Penout = new Pen(Color.Red, 2);

                if (intime > time1 - 1 && intime < time2)
                {
                    Pin1 = new Point(intime * cellx + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                }
                else if (intime > time2 - 1 && intime < time3)
                {
                    Pin1 = new Point(intime * cellx + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                }
                else
                {
                    Pin1 = new Point(intime * cellx + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                }

                if (outtime > time1 && outtime < time2 + 1)
                {
                    Pout1 = new Point(outtime * cellx + left, Cheight - down - Convert.ToInt32(Rate1 * celly * 5));
                }
                else if (outtime > time2 && outtime < time3 + 1)
                {
                    Pout1 = new Point(outtime * cellx + left, Cheight - down - Convert.ToInt32(Rate2 * celly * 5));
                }
                else
                {
                    Pout1 = new Point(outtime * cellx + left, Cheight - down - Convert.ToInt32(Rate3 * celly * 5));
                }

                Graphics1.DrawLine(Penin, Pin1, Pin2);
                Graphics1.DrawLine(Penout, Pout1, Pout2);

                //-----------------------------------------------------------------------画进出时间线------------------------------------------------------------------------------------


                //-----------------------------------------------------------------------画详细信息------------------------------------------------------------------------------------

                Pen Pendetail = new Pen(Color.Gray, 1);

                Point Pd0 = new Point(infox, infoy);
                Point Pd1 = new Point(infox, infoy + infoh);
                Point Pd2 = new Point(infox + infow, infoy + infoh);
                Point Pd3 = new Point(infox + infow, infoy);

                Point[] parray3 = new Point[4];
                parray3[0] = Pd0;
                parray3[1] = Pd1;
                parray3[2] = Pd2;
                parray3[3] = Pd3;

                Graphics1.DrawPolygon(Pendetail, parray3);

                Graphics1.DrawString("车牌号码：", myFont2, Brushes.DarkBlue, new PointF(infox + 5, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2)));
                Graphics1.DrawString("入库时间：", myFont2, Brushes.DarkBlue, new PointF(infox + 5, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 13 + 3));
                Graphics1.DrawString("出库时间：", myFont2, Brushes.DarkBlue, new PointF(infox + 5, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 26 + 6));
                Graphics1.DrawString("车辆型号：", myFont2, Brushes.DarkBlue, new PointF(infox + 5, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 39 + 9));
                Graphics1.DrawString("停泊车位：", myFont2, Brushes.DarkBlue, new PointF(infox + 5, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 52 + 12));


                Graphics1.DrawString(CarNo, myFont3, Brushes.Black, new PointF(infox + 65, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2)));
                Graphics1.DrawString(sintime.Substring(0, 2) + "时" + sintime.Substring(3, 2) + "分", myFont3, Brushes.Black, new PointF(infox + 65, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 13 + 3));
                Graphics1.DrawString(souttime.Substring(0, 2) + "时" + souttime.Substring(3, 2) + "分", myFont3, Brushes.Black, new PointF(infox + 65, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 26 + 6));
                Graphics1.DrawString(CarCla, myFont3, Brushes.Black, new PointF(infox + 65, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 39 + 9));
                Graphics1.DrawString(PortNo, myFont3, Brushes.Black, new PointF(infox + 65, infoy + Convert.ToInt32((infoh - 5 * 13 - 4 * 3) / 2) + 52 + 12));


                Graphics1.DrawString("消费：", new Font("微软雅黑", 12, FontStyle.Bold), Brushes.DarkBlue, new PointF(Convert.ToInt32(Cwidth / 2) - 95, Cheight - 40));
                Graphics1.DrawString(functions1.getCharge(CarNo).ToString() + "元", new Font("微软雅黑", 25, FontStyle.Bold), Brushes.Black, new PointF(Convert.ToInt32(Cwidth / 2) - 40, Cheight - 50));


                //-----------------------------------------------------------------------画详细信息------------------------------------------------------------------------------------
//++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


        
            }
        }

        public void drawstate(Control Drawcontrol)//绘制右上角方框，车库具体信息
        {
            int Anum, Bnum, Aused, Bused,UsedlenA, UsedlenB,  FullLen;
            int borderx, bordery, cwidth, cheight, paddingleft, linespace, lineheight;



//-----------------------------------------------------数据--------------------------------------------------------------

            cwidth = Drawcontrol.Size.Width;
            cheight = Drawcontrol.Size.Height;
            borderx = 10;
            bordery = 10;
            paddingleft = 45;
            linespace = 10;
            lineheight = Convert.ToInt32(Convert.ToDouble((cheight - bordery * 2 - linespace * 2)) / 3);

            DataAccess1.getPortused("PortA", out Anum, out Aused);
            DataAccess1.getPortused("PortB", out Bnum, out Bused);
          

            FullLen = cwidth - borderx * 2 - paddingleft;

            UsedlenA = Convert.ToInt32((Convert.ToDouble(Aused) / Anum) * FullLen);
            UsedlenB = Convert.ToInt32((Convert.ToDouble(Bused) / Bnum) * FullLen);
          



//-----------------------------------------------------数据--------------------------------------------------------------

            Graphics Graphics1 = Drawcontrol.CreateGraphics();
            Pen myPen1 = new Pen(Color.Green, lineheight);
            Pen myPen2 = new Pen(Color.Green, lineheight);

            Pen myPen4 = new Pen(Color.Black, 1);
            Font myFont1 = new Font("Arial", Convert.ToInt32(lineheight * 0.6), GraphicsUnit.Pixel);
            Font myFont2 = new Font("微软雅黑", Convert.ToInt32(lineheight * 0.6),FontStyle.Bold);

            if (UsedlenA < Convert.ToInt32(FullLen * 0.5) + 1)
            {
                myPen1 = new Pen(Color.Green, lineheight);
            }
            else if (UsedlenA < Convert.ToInt32(FullLen * 0.8) && UsedlenA > Convert.ToInt32(FullLen * 0.5))
            {
                myPen1 = new Pen(Color.Yellow, lineheight);
            }
            else
            {
                myPen1 = new Pen(Color.Red, lineheight);
            }

            if (UsedlenB < Convert.ToInt32(FullLen * 0.5) + 1)
            {
                myPen2 = new Pen(Color.Green, lineheight);
            }
            else if (UsedlenB < Convert.ToInt32(FullLen * 0.8) && UsedlenB > Convert.ToInt32(FullLen * 0.5))
            {
                myPen2 = new Pen(Color.Yellow, lineheight);
            }
            else
            {
                myPen2 = new Pen(Color.Red, lineheight);
            }

            

//-----------------------------------------------------填充--------------------------------------------------------------

            Point A1 = new Point(paddingleft + borderx, bordery + Convert.ToInt32(Convert.ToDouble(lineheight) / 2));
            Point A2 = new Point(paddingleft + borderx + UsedlenA, bordery + Convert.ToInt32(Convert.ToDouble(lineheight) / 2));
            Point B1 = new Point(paddingleft + borderx, bordery + Convert.ToInt32(Convert.ToDouble(lineheight) / 2) + lineheight + linespace);
            Point B2 = new Point(paddingleft + borderx + UsedlenB, bordery + Convert.ToInt32(Convert.ToDouble(lineheight) / 2) + lineheight + linespace);
           
           

            Graphics1.DrawLine(myPen1, A1, A2);
            Graphics1.DrawLine(myPen2, B1, B2);
            

//-----------------------------------------------------填充--------------------------------------------------------------



//-----------------------------------------------------方框线--------------------------------------------------------------

            Point[] parray1 = new Point[4];
            Point[] parray2 = new Point[4];
           
            Point p11 = new Point(borderx + paddingleft, bordery);
            Point p12 = new Point(borderx + paddingleft + FullLen, bordery);
            Point p13 = new Point(borderx + paddingleft, bordery+lineheight);
            Point p14 = new Point(borderx + paddingleft + FullLen, bordery + lineheight);
            Point p21 = new Point(borderx + paddingleft, bordery + lineheight + linespace);
            Point p22 = new Point(borderx + paddingleft + FullLen, bordery + lineheight + linespace);
            Point p23 = new Point(borderx + paddingleft, bordery + lineheight * 2 + linespace);
            Point p24 = new Point(borderx + paddingleft + FullLen, bordery + lineheight * 2 + linespace);
            
            parray1[0] = p11;
            parray1[1] = p13;
            parray1[2] = p14;
            parray1[3] = p12;
            parray2[0] = p21;
            parray2[1] = p23;
            parray2[2] = p24;
            parray2[3] = p22;
          
            Graphics1.DrawPolygon(myPen4, parray1);
            Graphics1.DrawPolygon(myPen4, parray2);
          

//-----------------------------------------------------方框线--------------------------------------------------------------


//-----------------------------------------------------刻度--------------------------------------------------------------

            for (int i = 0; i < 11; i++)
            {
                Point P1 = new Point(Convert.ToInt32((Convert.ToDouble(FullLen) / 10) * i) + borderx + paddingleft, bordery + lineheight - 3);
                Point Q1 = new Point(Convert.ToInt32((Convert.ToDouble(FullLen) / 10) * i) + borderx + paddingleft, bordery + lineheight);

                Point P2 = new Point(Convert.ToInt32((Convert.ToDouble(FullLen) / 10) * i) + borderx + paddingleft, bordery + lineheight * 2 + linespace - 3);
                Point Q2 = new Point(Convert.ToInt32((Convert.ToDouble(FullLen) / 10) * i) + borderx + paddingleft, bordery + lineheight * 2 + linespace);

                
                Graphics1.DrawLine(myPen4, P1, Q1);
                Graphics1.DrawLine(myPen4, P2, Q2);
               
            }

//-----------------------------------------------------刻度--------------------------------------------------------------


//-----------------------------------------------------字符串--------------------------------------------------------------

            Graphics1.DrawString("固定：", myFont2, Brushes.DarkBlue, new PointF(borderx, bordery));
            Graphics1.DrawString("临时：", myFont2, Brushes.DarkBlue, new PointF(borderx, bordery + lineheight + linespace));
           
            Graphics1.DrawString(Aused + " used of " + Anum, myFont1, Brushes.Olive   , new PointF(Convert.ToInt32(Convert.ToDouble(FullLen) / 2) + paddingleft + borderx - Convert.ToInt32(lineheight * 2), bordery + 2));
            Graphics1.DrawString(Bused + " used of " + Bnum, myFont1, Brushes.Olive, new PointF(Convert.ToInt32(Convert.ToDouble(FullLen) / 2) + paddingleft + borderx - Convert.ToInt32(lineheight * 2), bordery + lineheight + linespace + 2));
            
//-----------------------------------------------------字符串--------------------------------------------------------------

        
        }

        public void drawport(Control Drawcontrol, string PortName)
        {

            int PortNum;
            int cellx, celly, borderx, bordery, numx, numy, cwidth, cheight, linespace, carwidth, carheight;
            int arrayno, Uarrayno;
            int tag1 = 1;//判断车库是否能够填满某行，1为能够，0为不能够


//--------------------------------------------------------数据---------------------------------------------------------------------

            DataAccess1.getPortstate(PortName, out arrayno);//获取某停车场入库车辆的停车位集合
            int[] Pnoarray = new int[arrayno];
            Pnoarray = DataAccess1.getPortstate(PortName, out arrayno);

            PortNum = DataAccess1.getPortnum(PortName);//获取某停车场的车位总数

            DataAccess1.getUserstate(PortName, out Uarrayno);//获取某停车场已有主人的停车位集合
            int[] PUnoarray = new int[Uarrayno];
            PUnoarray = DataAccess1.getUserstate(PortName, out Uarrayno);

            borderx = 10;
            bordery = 10;

            cwidth = Drawcontrol.Size.Width;
            cheight = Drawcontrol.Size.Height;

            if (PortName == "PortA")
            {
                numx = 5;
                linespace = 15;
            }
            else
            {
                numx = 6;
                linespace = 10;
            }




            if (PortNum % numx == 0)//确定停车场列数
            {
                numy = PortNum / numx;
            }
            else
            {
                tag1 = 0;
                numy = PortNum / numx + 1;         
            }

            cellx = Convert.ToInt32(Convert.ToDouble((cwidth - (numx - 1) * linespace - borderx * 2)) / numx);//每个车位的宽度
            celly = Convert.ToInt32(Convert.ToDouble((cheight - bordery * 2)) / numy);//每个车位的长度

            carwidth = Convert.ToInt32(cellx * 0.6);//确定车宽
            carheight = Convert.ToInt32(celly * 0.6);//确定车长

//--------------------------------------------------------数据---------------------------------------------------------------------


            Graphics Graphics1 = Drawcontrol.CreateGraphics();

            Pen pen1 = new Pen(Color.Black,1);
            SolidBrush s = new SolidBrush(Color.Yellow);//给有车主的车位填充黄色
            SolidBrush s1 = new SolidBrush(Color.Black);//用于标车位号码
            Font Font1 = new Font("宋体", 12);

//----------------------------------------------------------画车位----------------------------------------------------------------

            for (int i = 0; i < numx; i++)
            {
                if (tag1 == 0)//如果车位数没有铺满列
                {
                    if (i == numx - 1)//如果是最后一列
                    {
                        Point p1k0 = new Point(borderx + i * (cellx + linespace), bordery);
                        Point p1k1 = new Point(borderx + i * (cellx + linespace), bordery + celly * numy - (numx * numy - PortNum) * celly);
                        Point p2k0 = new Point(borderx + i * (cellx + linespace) + cellx, bordery);
                        Point p2k1 = new Point(borderx + i * (cellx + linespace) + cellx, bordery + celly * numy - (numx * numy - PortNum) * celly);
                        Graphics1.DrawLine(pen1, p1k0, p1k1);
                        Graphics1.DrawLine(pen1, p2k0, p2k1);

                        for (int j = 0; j < numy - (numx * numy - PortNum) + 1; j++)
                        {
                            Point p10j = new Point(borderx + i * (cellx + linespace), bordery + j * celly);
                            Point p11j = new Point(borderx + i * (cellx + linespace) + cellx, bordery + j * celly);
                            Graphics1.DrawLine(pen1, p10j, p11j);


                            for (int k = 0; k < Uarrayno; k++)//将有车主的车位设为黄色
                            {
                                if (PUnoarray[k] == (i * numy + j + 1) && j < numy)
                                {
                                    Graphics1.FillRectangle(s, borderx + i * (cellx + linespace) + Convert.ToInt32((cellx - carwidth) / 2), bordery + j * celly + Convert.ToInt32((celly - carheight) / 2), carwidth, carheight);
                                }
                            }

                            if (j < numy - (numx * numy - PortNum))//给最后一列标上车位号
                            {
                                string fs;
                                fs = Convert.ToString(i * numy + j + 1);
                                Graphics1.DrawString(fs, Font1, s1, p10j);
                            }


                            for (int k = 0; k < arrayno; k++)//将已经停了车的车位设为绿色
                            {
                                if (Pnoarray[k] == (i * numy + j + 1))
                                {
                                    string CarNo, InTime, CarCla, PortNo;
                                    if (PortName == "PortA")
                                    {
                                        PortNo = "A" + Pnoarray[k];
                                    }
                                    else
                                    {
                                        PortNo = "B" + Pnoarray[k];
                                    }
                                   
                                    DataAccess1.getCardetail2(PortNo, out CarNo, out InTime, out CarCla);
                                    Panel Controltoadd = new Panel();
                                    Controltoadd.Name = CarNo;
                                    Controltoadd.Location = new Point(borderx + i * (cellx + linespace) + Convert.ToInt32((cellx - carwidth) / 2), bordery + j * celly + Convert.ToInt32((celly - carheight) / 2));
                                    Controltoadd.Size = new Size(carwidth, carheight);
                                    Controltoadd.BackColor = Color.Green;
                                    string tooltip = "车牌号码：" + CarNo + "\n车辆类型：" + CarCla + "\n入库时间：" + InTime.Substring(0, 2) + "时" + InTime.Substring(2, 2) + "分\n停放车位：" + PortNo;
                                    toolTip1.SetToolTip(Controltoadd, tooltip);
                                    Controltoadd.MouseDoubleClick += new MouseEventHandler(delcar);
                                    Controltoadd.MouseClick += new MouseEventHandler(showdetail);
                                    Drawcontrol.Controls.Add(Controltoadd);
                                }
                            }
                        }
                    }
                    else//如果i != numx - 1，是前面几列
                    {
                        Point p1i0 = new Point(borderx + i * (cellx + linespace), bordery);//整个一条车位的左上点
                        Point p1i1 = new Point(borderx + i * (cellx + linespace), bordery + celly * numy);//左下点
                        Point p2i0 = new Point(borderx + i * (cellx + linespace) + cellx, bordery);//右上点
                        Point p2i1 = new Point(borderx + i * (cellx + linespace) + cellx, bordery + celly * numy);//右下点
                        Graphics1.DrawLine(pen1, p1i0, p1i1);//画一条车位的左长竖线
                        Graphics1.DrawLine(pen1, p2i0, p2i1);//画一条车位的右长竖线

                        for (int j = 0; j < numy + 1; j++)
                        {
                            Point p10j = new Point(borderx + i * (cellx + linespace), bordery + j * celly);//某车库的左上点
                            Point p11j = new Point(borderx + i * (cellx + linespace) + cellx, bordery + j * celly);//某车库的右上点
                            Graphics1.DrawLine(pen1, p10j, p11j);//画某车位的宽，横线

                            for (int k = 0; k < Uarrayno; k++)//有车主的车位，黄色
                            {
                                if (PUnoarray[k] == (i * numy + j + 1) && j < numy)
                                {
                                    Graphics1.FillRectangle(s, borderx + i * (cellx + linespace) + Convert.ToInt32((cellx - carwidth) / 2), bordery + j * celly + Convert.ToInt32((celly - carheight) / 2), carwidth, carheight);
                                }
                            }

                            if (j < numy)//给每一个车位标上号码
                            {
                                string fs;
                                fs = Convert.ToString(i * numy + j + 1);
                                Graphics1.DrawString(fs, Font1, s1, p10j);
                            }

                            for (int k = 0; k < arrayno; k++)
                            {
                                if (Pnoarray[k] == (i * numy + j + 1) && j < numy)
                                {
                                    string CarNo, InTime, CarCla, PortNo;
                                    if (PortName == "PortA")
                                    {
                                        PortNo = "A" + Pnoarray[k];
                                    }
                                    else 
                                    {
                                        PortNo = "B" + Pnoarray[k];
                                    }
                                    
                                    DataAccess1.getCardetail2(PortNo, out CarNo, out InTime, out CarCla);
                                    Panel Controltoadd = new Panel();
                                    Controltoadd.Name = CarNo;
                                    Controltoadd.Location = new Point(borderx + i * (cellx + linespace) + Convert.ToInt32((cellx - carwidth) / 2), bordery + j * celly + Convert.ToInt32((celly - carheight) / 2));
                                    Controltoadd.Size = new Size(carwidth, carheight);
                                    Controltoadd.BackColor = Color.Green;
                                    string tooltip = "车牌号码：" + CarNo + "\n车辆类型：" + CarCla + "\n入库时间：" + InTime.Substring(0, 2) + "时" + InTime.Substring(2, 2) + "分\n停放车位：" + PortNo;
                                    toolTip1.SetToolTip(Controltoadd, tooltip);
                                    Controltoadd.MouseDoubleClick += new MouseEventHandler(delcar);
                                    Controltoadd.MouseClick += new MouseEventHandler(showdetail);
                                    Drawcontrol.Controls.Add(Controltoadd);
                                }
                            }
                        }                                                          
                    }
                }
                else//如果车辆数铺满列
                { 
                    Point p1i0 = new Point(borderx + i * (cellx + linespace), bordery);
                    Point p1i1 = new Point(borderx + i * (cellx + linespace), bordery + celly * numy);
                    Point p2i0 = new Point(borderx + i * (cellx + linespace) + cellx, bordery);
                    Point p2i1 = new Point(borderx + i * (cellx + linespace) + cellx, bordery + celly * numy);
                    Graphics1.DrawLine(pen1, p1i0, p1i1);
                    Graphics1.DrawLine(pen1, p2i0, p2i1);

                    for (int j = 0; j < numy + 1; j++)
                    {
                        Point p10j = new Point(borderx + i * (cellx + linespace), bordery + j * celly);
                        Point p11j = new Point(borderx + i * (cellx + linespace) + cellx, bordery + j * celly);
                        Graphics1.DrawLine(pen1, p10j, p11j);

                        for (int k = 0; k < Uarrayno; k++)//有车主的车位，黄色
                        {
                            if (PUnoarray[k] == (i * numy + j + 1) && j < numy)
                            {
                                Graphics1.FillRectangle(s, borderx + i * (cellx + linespace) + Convert.ToInt32((cellx - carwidth) / 2), bordery + j * celly + Convert.ToInt32((celly - carheight) / 2), carwidth, carheight);
                            }
                        }

                        if (j < numy)//给每一个车位标上号码
                        {
                            string fs;
                            fs = Convert.ToString(i * numy + j + 1);
                            Graphics1.DrawString(fs, Font1, s1, p10j);
                        }

                        for (int k = 0; k < arrayno; k++)
                        {
                            if (Pnoarray[k] == (i * numy + j + 1) && j < numy)
                            {
                                string CarNo, InTime, CarCla, PortNo;
                                if (PortName == "PortA")
                                {
                                    PortNo = "A" + Pnoarray[k];
                                }
                                else 
                                {
                                    PortNo = "B" + Pnoarray[k];
                                }
                                
                                DataAccess1.getCardetail2(PortNo, out CarNo, out InTime, out CarCla);
                                Panel Controltoadd = new Panel();
                                Controltoadd.Name = CarNo;
                                Controltoadd.Location = new Point(borderx + i * (cellx + linespace) +Convert.ToInt32((cellx - carwidth) / 2), bordery + j * celly + Convert.ToInt32((celly -carheight) / 2) );
                                Controltoadd.Size = new Size(carwidth, carheight);
                                Controltoadd.BackColor = Color.Green;
                                string tooltip = "车牌号码：" + CarNo + "\n车辆类型：" + CarCla + "\n入库时间：" + InTime.Substring(0, 2) + "时" + InTime.Substring(2, 2) + "分\n停放车位：" + PortNo;
                                toolTip1.SetToolTip(Controltoadd, tooltip);
                                Controltoadd.MouseDoubleClick += new MouseEventHandler(delcar);//双击车辆出库
                                Controltoadd.MouseClick += new MouseEventHandler(showdetail);//单击，detail框中出现入库车辆信息
                                Drawcontrol.Controls.Add(Controltoadd);
                            }
                        }
                    } 
                }

            }

//----------------------------------------------------------画车位----------------------------------------------------------------        
          
        }

        public void drawpic(Control Drawcontrol)
        {

            Image xxx = new Bitmap(CarManager.Properties.Resources.down);
            Graphics Graphics1 = Drawcontrol.CreateGraphics();
            Graphics1.DrawImage(xxx,70, 0);

        
        }

//=================================================================================================================================================

        public void showdetail(object sender, EventArgs e)
        {

            Panel panel1 = (Panel)sender;

            int arrayno = DataAccess1.getDataNum("CarIn");
            string[] Cararray = new string[arrayno];
            Cararray = DataAccess1.getCarNo();

            Panel[] panelarray = new Panel[arrayno];

            for (int i = 0; i < arrayno; i++)
            {
                panelarray[i] = (Panel)(panel1.FindForm().Controls.Find(Cararray[i], true)[0]);

                if (panelarray[i].Name != panel1.Name)
                {
                    panelarray[i].BackColor = Color.Green;
                }
            }
            panel1.BackColor = Color.Red;

            Panel Pdrawdetail = (Panel)(panel1.FindForm().Controls.Find("Pdrawdetail", true)[0]);

            Pdrawdetail.Refresh();
            drawdetail(Pdrawdetail, panel1.Name);
        }

        public void delcar(object sender, EventArgs e)
        {
            Panel panel1 = (Panel)sender;
            DataAccess1.delCar(panel1.Name,functions1.getCharge(panel1.Name).ToString());

            Panel PportA = (Panel)(panel1.FindForm().Controls.Find("PportA", true)[0]);
            Panel PportB = (Panel)(panel1.FindForm().Controls.Find("PportB", true)[0]);
           
            Panel Pdrawstate = (Panel)(panel1.FindForm().Controls.Find("Pdrawstate", true)[0]);
            Panel Pdrawdetail = (Panel)(panel1.FindForm().Controls.Find("Pdrawdetail", true)[0]);
            Panel panel2 = (Panel)(panel1.FindForm().Controls.Find("panel1", true)[0]);

            PportA.Refresh();
            PportB.Refresh();
            
            Pdrawstate.Refresh();
            panel2.Refresh();
            Pdrawdetail.Refresh();

            PportA.Controls.Clear();
            PportB.Controls.Clear();
            
            drawport(PportA, "PortA");
            drawport(PportB, "PortB");
           
            drawstate(Pdrawstate);
            drawpic(panel2);
            //drawdetail(Pdrawdetail, "axis");

        }

        public void addcar(object sender, MouseEventArgs e)
        {

            double pX =e.Location.X;
            double pY = e.Location.Y;
            string lei = "";
            string carclass = "";
            int linespace, PortNum, numx, numy;
            linespace = PortNum = numx = numy = 0;


            Panel panel1 = (Panel)sender;

            if (panel1.Name == "PportA")
            {             
                linespace = 15;
                PortNum = DataAccess1.getPortnum("PortA");
                numx = 5;
                numy = 0;
                lei = "A";
                carclass = "固定用户";
            }
            else 
            {
                linespace = 10;
                PortNum = DataAccess1.getPortnum("PortB");
                numx = 6;
                numy = 0;
                lei = "B";
                carclass = "临时用户";
            }
           
            int borderx = 10;
            int bordery = 10;
            int cwidth = panel1.Size.Width;
            int cheight = panel1.Size.Height;

            if (PortNum % numx == 0)
            {
                numy = PortNum / numx;
            }
            else
            {
                numy = PortNum / numx + 1;
            }

            int cellx = Convert.ToInt32(Convert.ToDouble((cwidth - (numx - 1) * linespace - borderx * 2)) / numx);
            int celly = Convert.ToInt32(Convert.ToDouble((cheight - bordery * 2)) / numy);

            for (int i = 0; i < numx; i++)
            {
                for (int j = 0; j < numy; j++)
                {
                    if (pX > borderx + i * (cellx + linespace) && pX < borderx + i * (cellx + linespace) + cellx && pY > bordery + j * celly && pY < bordery + (j + 1) * celly)
                    {
                        string portno = lei + (i * numy + j + 1).ToString();
                        portnotoadd = portno;
                        int carportnum = DataAccess1.getCarportnum(carclass);
                        int tagsame = 0;
                        string[] carportno = new string[carportnum];
                        carportno = DataAccess1.getCarportno(carclass);

                        for (int k = 0; k < carportnum; k++)
                        {
                            if (carportno[k].Trim() == portno.Trim())
                            {
                                tagsame = 1;
                            }                       
                        }
                        if (i * numy + j < PortNum && tagsame == 0)
                        { 
                            Form FCarin1 = new FCarin();
                            FCarin1.Show();                        
                        }
                    }
                }
            }       
        }

    }
}
