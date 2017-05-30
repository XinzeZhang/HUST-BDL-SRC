using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;

namespace PositionManage
{
    public partial class Form1 : Form
    {
        #region //图形显示参数变量

        /// <summary>
        /// 选中点
        /// </summary>
        private UC.UCRefPoint ucPointSelected
        {
            get
            {
                return _ucPointSelected;
            }
            set
            {
                if (_ucPointSelected != null)
                {
                    _ucPointSelected.IsSelected = false;
                }
                _ucPointSelected = value;
                _ucPointSelected.IsSelected = true;
            }
        }

        private UC.UCRefPoint _ucPointSelected = null;

        /// <summary>
        /// 是否在拖动
        /// </summary>
        private bool _IsPointDrag = false;

        /// <summary>
        /// 初始拖动控件坐标点
        /// </summary>
        private Point _ptDragOrigin = new Point();

        /// <summary>
        /// 初始拖动时鼠标坐标点
        /// </summary>
        private Point _ptDragCursorOrigin = new Point();

        /// <summary>
        /// 地图路径
        /// </summary>
        private string _sMapPath = null;

        #endregion

        #region //支线程变量
        /// <summary>
        /// 支线程
        /// </summary>
        private System.Threading.Thread _th = null;

        /// <summary>
        /// udp监听
        /// </summary>
        private volatile System.Net.Sockets.UdpClient _udp = null;

        /// <summary>
        /// 监听端口
        /// </summary>
        private volatile int _portListen = -1;

        /// <summary>
        /// 监听目的ip及端口
        /// </summary>
        private volatile System.Net.IPEndPoint _ipaddrDes = null;

        #endregion

        #region //其他全局变量

        /// <summary>
        /// 是否初始化模式
        /// </summary>
        private volatile bool _IsInit = true;

        /// <summary>
        /// 存储接受数据表格
        /// </summary>
        private volatile DataTable dtData = null;

        /// <summary>
        /// 定义空参数返回委托
        /// </summary>
        private delegate void VoidDelegate();

        /// <summary>
        /// 定位点列表
        /// </summary>
        private Dictionary<string, Point> _dictReaderLoc = new Dictionary<string, Point>();

        /// <summary>
        /// 采样点列表
        /// </summary>
        private Dictionary<string, Point> _dictSampleLoc = new Dictionary<string, Point>();

        /// <summary>
        /// 定位标签列表
        /// </summary>
        private List<string> _TagNo = new List<string>();

        private Dictionary<int, int> _dictTag = new Dictionary<int,int>();

        #endregion
        
        public Form1()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);  
            SetStyle(ControlStyles.DoubleBuffer, true); 
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //初始化地图
            _sMapPath = System.IO.Path.GetFullPath(Program.GetAppSettingValue("MapPath", "./Map/Map.jpg"));

            string sDir = System.IO.Path.GetDirectoryName(_sMapPath);
            if (!System.IO.Directory.Exists(sDir))
            {
                System.IO.Directory.CreateDirectory(sDir);
            }

            if (System.IO.File.Exists(_sMapPath))
            {
                System.IO.FileStream fs = new System.IO.FileStream(_sMapPath, System.IO.FileMode.Open);
                try
                {
                    Image img = Image.FromStream(fs);
                    this.panMap.BackgroundImage = img;
                }
                catch
                {
                    MessageBox.Show("加载图片失败!");
                    return;
                }
                finally
                {
                    fs.Close();
                    fs.Dispose();
                }
            }

            //初始化参考点坐标
            string msg;
            var dict = XMLDal.GetXMLDatas("reader", "data", "readerID", out msg);
            if (dict != null && dict.Count > 0)
            {
                foreach (KeyValuePair<string, string> kv in dict)
                {
                    string[] sLoc = kv.Value.Split('-');
                    if (sLoc.Length != 2)
                    {
                        continue;
                    }
                    else
                    {
                        AddNewReaderLoc(kv.Key, Color.Red, new Point(Convert.ToInt32(sLoc[0]), Convert.ToInt32(sLoc[1])));
                    }
                }
            }


            //初始化IP和端口信息
            this.tbIP.Text = Program.GetAppSettingValue("DefaultIP", "");
            this.tbPort.Text = Program.GetAppSettingValue("DefaultPortNo", "");

            //初始化udp监听端口
            _portListen = Convert.ToInt32(Program.GetAppSettingValue("ListenPort", "8567"));

            //初始化表格
            dtData = new DataTable();
            dtData.Columns.AddRange( new DataColumn[]{
                new DataColumn("RowNo",typeof(int)),
                new DataColumn("TagNo", typeof(string)),
                new DataColumn("ReferNo1", typeof(string)),
                new DataColumn("Distance1", typeof(int)),
                new DataColumn("ReferNo2", typeof(string)),
                new DataColumn("Distance2", typeof(int)),
                new DataColumn("ReferNo3", typeof(string)),
                new DataColumn("Distance3", typeof(int)),
                new DataColumn("ReadDate",typeof(DateTime))
            });

            this.dgData.AutoGenerateColumns = false;
            this.dgData.DataSource = dtData;

            //初始化定位网格大小
            CaculateLocation.SetGridSize(3, 6);
            ImportSampleData();
        }

        #region 连接&断开

        //打开socket通信
        private void btnConnect_Click(object sender, EventArgs e)
        {
            System.Net.IPAddress ipaddr = null;
            if(!System.Net.IPAddress.TryParse(this.tbIP.Text, out ipaddr))
            {
                MessageBox.Show("请正确填写IP地址!");
                return;
            }

            int iPort = 0;
            if(!int.TryParse(this.tbPort.Text, out iPort) || iPort <= 0)
            {
                MessageBox.Show("请正确填写端口!");
                return;
            }

            _ipaddrDes = new System.Net.IPEndPoint(ipaddr, iPort);

            if (_udp != null)
            {
                _udp.Close();
                _udp = null;
            }

            _udp = new UdpClient(_portListen);

            try
            {
                _udp.Connect(_ipaddrDes);
            }
            catch (System.ObjectDisposedException)
            {
                MessageBox.Show("System.Net.Sockets.UdpClient 已关闭");
                return;
            }
            catch (System.ArgumentOutOfRangeException)
            {
                MessageBox.Show("port 不在 System.Net.IPEndPoint.MinPort 和 System.Net.IPEndPoint.MaxPort 之间");
                return;
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                MessageBox.Show("试图访问套接字时发生错误:" + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("未知类型错误:" + ex.Message);
                return;
            }
            Program.SetAppSettingValue("DefaultIP", this.tbIP.Text);
            Program.SetAppSettingValue("DefaultPortNo", this.tbPort.Text);

            this.btnDisconnect.Enabled = true;
            this.btnConnect.Enabled = false;
        }

        //关闭socket通信
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (_udp != null)
            {
                _udp.Close();
                _udp = null;
            }

            this.btnDisconnect.Enabled = false;
            this.btnConnect.Enabled = true;
        }
        #endregion

        private void btnInit_Click(object sender, EventArgs e)
        {
            if (!_IsInit)
            {
                this.panMap.ContextMenuStrip = this.contextMenuStripPic;
                _IsInit = true;

                this.btnInit.Enabled = false;
                this.btnPosition.Enabled = true;

                this.tabControl1.Focus();

                if (_th != null && _th.IsAlive)
                {
                    _th.Abort();
                }
                _th = null;
            }
        }

        private void btnPosition_Click(object sender, EventArgs e)
        {
            if (_IsInit)
            {
                this.panMap.ContextMenuStrip = null;
                _IsInit = false;

                //初始化支线程并开启
                _th = new System.Threading.Thread(new System.Threading.ThreadStart(LoopReceiveData));
                _th.Start();

                this.btnPosition.Enabled = false;
                this.btnInit.Enabled = true;
            }
        }


        /// <summary>
        /// 监听支线程，循环模式。
        /// </summary>
        private void LoopReceiveData()
        {
            byte[] buff = null;
            System.Net.IPEndPoint ipDes = _ipaddrDes;
            while (!_IsInit)
            {
                try
                {
                    buff = _udp.Receive(ref ipDes);
                    if (buff != null && buff.Length >= 8)
                    {
                        if (buff[0] != 0xFA || buff[8] != 0xFB)   //FA开头有效，中间为FB有效
                        {
                            continue;
                        }

                        //校验校验和是否正确
                        byte iSum = 0;
                        bool bVaild = true;
                        for (int ii = 0; ii < 16; ii++)
                        {
                            if(ii == 7)
                            {
                                if (iSum != buff[7])
                                {
                                    bVaild = false;
                                    break;
                                }
                                else
                                {
                                    iSum += buff[ii];
                                }
                            }
                            else if (ii == 15)
                            {
                                if (iSum != buff[15])
                                {
                                    bVaild = false;
                                    break;
                                } 
                            }
                            else
                            {
                                iSum += buff[ii];
                            }
                        }
                        if (!bVaild)
                        {
                            continue;
                        }

                        //解析数据，并处理
                        UDPData data = new UDPData();
                        data.TagNo = Convert.ToString(((buff[1] << 16) + (buff[2] << 8) + buff[3]), 16).PadLeft(6,'0').ToUpper();
                        data.Loc[0] = new Location()
                        {
                            ReferNo = Convert.ToString(((buff[5] << 8) + buff[6]), 16).PadLeft(4,'0').ToUpper(),
                            Destance = buff[4]
                        };

                        data.Loc[1] = new Location()
                        {
                            ReferNo = Convert.ToString(((buff[10] << 8) + buff[11]), 16).PadLeft(4, '0').ToUpper(),
                            Destance = buff[9]
                        };

                        data.Loc[2] = new Location()
                        {
                            ReferNo = Convert.ToString(((buff[13] << 8) + buff[14]), 16).PadLeft(4, '0').ToUpper(),
                            Destance = buff[12]
                        };

                        DealData(data);
                    }
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
        }

        private void DealData(UDPData data)
        {
            //纳入表格
            DataRow dr = dtData.NewRow();
            dr["RowNo"] = dtData.Rows.Count + 1;
            dr["TagNo"] = data.TagNo;
            dr["ReferNo1"] = data.Loc[0].ReferNo;
            dr["Distance1"] = data.Loc[0].Destance;
            dr["ReferNo2"] = data.Loc[1].ReferNo;
            dr["Distance2"] = data.Loc[1].Destance;
            dr["ReferNo3"] = data.Loc[2].ReferNo;
            dr["Distance3"] = data.Loc[2].Destance;

            dr["ReadDate"] = DateTime.Now;

            dtData.Rows.Add(dr);

            VoidDelegate dRefresh = new VoidDelegate(delegate
            {
                //this.dgData
                this.dgData.Refresh();
            });

            if (this.dgData.InvokeRequired)
            {
                this.Invoke(dRefresh);
            }
            else
            {
                dRefresh();
            }

            if (string.IsNullOrEmpty(data.Loc[2].ReferNo))
            {
                return;
            }

            //计算网格坐标
            int x,y;
            CalulateLocation(data, out x, out y);

            //显示
            VoidDelegate dShow = new VoidDelegate(delegate
            {
                UC.UCRefPoint ucPoint = null;
                if (!this.panMap.Controls.ContainsKey("uc" + data.TagNo))
                {
                    ucPoint = new PositionManage.UC.UCRefPoint();
                    ucPoint.Name = "uc" + data.TagNo;
                    ucPoint.PointName = data.TagNo;
                    ucPoint.PointColor = Color.Blue;
                    this.panMap.Controls.Add(ucPoint);
                }
                else
                {
                    var ctrls = this.panMap.Controls.Find("uc" + data.TagNo, false);
                    if (ctrls.Length > 0)
                    {
                        ucPoint = ctrls[0] as UC.UCRefPoint;
                    }
                    else
                    {
                        return;
                    }
                }

                int iLocX = 0, iLocY = 0;
                //四顶点  x + y * 3, x + y * 3 + 1, x + y * 3 + 3, x + y * 3 + 4
                if (x == 0)
                {
                    iLocY = 300;
                }
                else
                {
                    iLocY = 100;
                }
                if(y == 0)
                {
                    iLocX = 350;
                }
                else if (y == 1)
                {
                    iLocX = 290;
                }
                else if(y == 2)
                {
                    iLocX = 220;
                }
                else if (y == 3)
                {
                    iLocX = 150;
                }
                else
                {
                    iLocX = 100;
                }

                ucPoint.Location = new Point(iLocX, iLocY);
            });

            if (this.dgData.InvokeRequired)
            {
                this.Invoke(dShow);
            }
            else
            {
                dShow();
            }
        }

        private void CalulateLocation(UDPData data , out int x, out int y)
        {
            if (string.IsNullOrEmpty(data.Loc[0].ReferNo))
            {
                x = y = -1;
                return;
            }
            else
            {
                for (int ii = 0; ii < 3; ii++)
                {
                    for (int jj = 0; jj < _dictReaderLoc.Count; jj++)
                    {
                        if (data.Loc[ii].ReferNo == _dictReaderLoc.Keys.ElementAt(jj))
                        {
                            if (_dictTag.ContainsKey(jj))
                            {
                                _dictTag[jj] = data.Loc[ii].Destance;
                            }
                            else
                            {
                                _dictTag.Add(jj, data.Loc[ii].Destance);
                            }
                            break;
                        }
                    }
                }

                Point pt = CaculateLocation.OutputResultLocation(_dictTag);
                x = pt.X;
                y = pt.Y;
            }
        }

        private void panMap_MouseMove(object sender, MouseEventArgs e)
        {
            this.lbLocation.Text = "X:" + e.X + ",Y:" + e.Y; 
        }

        private void tsMenuItemVaryImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "图片文件jpg(*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                System.IO.Stream stm = ofd.OpenFile();
                Image img = Image.FromStream(stm);

                this.panMap.BackgroundImage = img;

                System.IO.FileStream fs = new System.IO.FileStream(_sMapPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                try
                {
                    img.Save(fs, img.RawFormat);
                    fs.Close();
                    fs.Dispose();
                }
                catch
                {
                    fs.Close();
                    fs.Dispose();
                }
            }
        }

        private void tsMenuItemAddLocation_Click(object sender, EventArgs e)
        {
            string[] sLocs = this.lbLocation.Text.Split(new char[] { ':', ',' });
            int iPtX, iPtY; 
            int.TryParse(sLocs[1], out iPtX);
            int.TryParse(sLocs[3], out iPtY);
            FormDefinePoint fdp = new FormDefinePoint();
            fdp.PointX = iPtX;
            fdp.PointY = iPtY;
            fdp.PointColor = Color.Red;
            if (fdp.ShowDialog() == DialogResult.OK)
            {
                AddNewReaderLoc(fdp.PointName, fdp.PointColor, new Point(iPtX, iPtY));
            }
        }

        private void AddNewReaderLoc(string pointname, System.Drawing.Color color, Point location)
        {
            PositionManage.UC.UCRefPoint uc = new PositionManage.UC.UCRefPoint();

            uc.PointName = pointname;
            uc.PointColor = color;
            uc.Location = location;

            //添加单击选中事件
            uc.Click += new EventHandler(uc_Click);

            //添加拖动事件
            uc.MouseDown += new MouseEventHandler(uc_MouseDown);
            uc.MouseMove += new MouseEventHandler(uc_MouseMove);
            uc.MouseUp += new MouseEventHandler(uc_MouseUp);
            uc.LocationChanged += new EventHandler(uc_LocationChanged);

            this.panMap.Controls.Add(uc);

            _dictReaderLoc.Add(uc.PointName, uc.Location);
            ucPointSelected = uc;
        }

        private void uc_LocationChanged(object sender, EventArgs e)
        {
            UC.UCRefPoint uc = sender as UC.UCRefPoint;
            if (uc != null)
            {
                if (_dictReaderLoc.ContainsKey(uc.PointName))
                {
                    _dictReaderLoc[uc.PointName] = uc.Location;
                }
                else
                {
                    _dictReaderLoc.Add(uc.PointName, uc.Location);
                }
            }
        }

        private void uc_MouseUp(object sender, MouseEventArgs e)
        {
            var ucSender = sender as UC.UCRefPoint;
            if (ucPointSelected == ucSender && _IsInit)
            {
                //清空
                _IsPointDrag = false;
                _ptDragOrigin = new Point();
                _ptDragCursorOrigin = new Point();
            }
        }

        private void uc_MouseMove(object sender, MouseEventArgs e)
        {
            var ucSender = sender as UC.UCRefPoint;
            if (ucPointSelected == ucSender && _IsPointDrag && _IsInit)
            {
                ucSender.Location = new Point(ucSender.Location.X + e.Location.X - _ptDragCursorOrigin.X + _ptDragOrigin.X, ucSender.Location.Y + e.Location.Y - _ptDragCursorOrigin.Y + _ptDragOrigin.Y);
            }
        }

        private void uc_MouseDown(object sender, MouseEventArgs e)
        {
            var ucSender = sender as UC.UCRefPoint;
            if (ucPointSelected == ucSender && _IsInit)
            {
                _IsPointDrag = true;
                _ptDragOrigin = ucSender.Location;
                _ptDragCursorOrigin = new Point(ucSender.Location.X + e.Location.X, ucSender.Location.Y + e.Location.Y);
            }
        }

        public void uc_Click(object sender, EventArgs e)
        {
            if (_IsInit)
            {
                ucPointSelected = sender as UC.UCRefPoint;
            }
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (ucPointSelected != null && _IsInit)
            {
                e.Handled = true;
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        ucPointSelected.Location = new Point(ucPointSelected.Location.X, ucPointSelected.Location.Y - 1);
                        break;
                    case Keys.Down:
                        ucPointSelected.Location = new Point(ucPointSelected.Location.X, ucPointSelected.Location.Y + 1);
                        break;
                    case Keys.Left:
                        ucPointSelected.Location = new Point(ucPointSelected.Location.X - 1, ucPointSelected.Location.Y);
                        break;
                    case Keys.Right:
                        ucPointSelected.Location = new Point(ucPointSelected.Location.X + 1, ucPointSelected.Location.Y);
                        break;
                    case Keys.Back:
                        this.panMap.Controls.Remove(ucPointSelected);
                        _dictReaderLoc.Remove(ucPointSelected.PointName);
                        break;
                    case Keys.Delete:
                        this.panMap.Controls.Remove(ucPointSelected);
                        _dictReaderLoc.Remove(ucPointSelected.PointName);
                        break;
                    default:
                        e.Handled = false;
                        return;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //存储历史记录
            //保存定位器数据
            string sErrorMsg = "";

            foreach(KeyValuePair<string, Point> kv in this._dictReaderLoc)
            {
                string msg;
                if (!XMLDal.SaveDataToXML("reader", "data", "readerID", kv.Key, kv.Value.X + "-" + kv.Value.Y, out msg))
                {
                    sErrorMsg += msg + "\r\n";
                }
            }

            if (!string.IsNullOrEmpty(sErrorMsg))
            {
                MessageBox.Show("保存定位器坐标失败!错误详细：\r\n" + sErrorMsg);
                sErrorMsg = "";
            }
        }

        private void tsMenuItemImport_Click(object sender, EventArgs e)
        {
            ImportSampleData();
        }

        private void ImportSampleData()
        {
            int[][] iSamples = new int[][] { 
                new int[]{45,18,29,28,16,17,16,0},
                new int[]{40,31,33,29,29,0,0,0},
                new int[]{35,42,27,39,21,16,0,0},
                new int[]{33,0,45,20,26,0,27,0},
                new int[]{32,0,38,23,23,0,0,0},
                new int[]{0,32,28,46,0,25,0,0},
                new int[]{37,0,30,0,32,0,25,0},
                new int[]{0,19,34,28,0,0,0,0},
                new int[]{29,19,16,49,0,28,0,18},
                new int[]{0,0,40,0,36,0,35,21},
                new int[]{0,0,24,0,32,38,23,0},
                new int[]{0,0,0,23,26,42,0,37},
                new int[]{0,0,26,0,0,29,33,28},
                new int[]{0,0,0,0,44,30,25,29},
                new int[]{21,0,0,28,21,49,0,43},
                new int[]{0,0,32,0,39,37,35,32},
                new int[]{0,0,0,0,22,23,25,23},
                new int[]{0,0,0,18,24,39,0,38}
            };

            for (int ii = 0; ii < iSamples.Length; ii++)
            {
                int x = ii % 3, y = ii / 3;
                for (int jj = 0; jj < 8; jj++)
                {
                    CaculateLocation.InputStudyLocation(jj, iSamples[ii][jj], new Point(x, y));
                }
            }
        }
    }
}
