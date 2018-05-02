
namespace Kinect_PPT
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using Microsoft.Kinect;
    using System.Windows.Forms;
    //for SendKeys and DoEvents

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// PPT 控制属性声明
        /// </summary>
        bool COLORFRAME = false;
        const float LIFT = 0.15f;
        const int LOCKED = 0;
        const int CONTROLLING = 1;
        const int SELECTING = 2;
        const int TOESC = 3;
        const int TOPLAY = 4;
        const int TOSTART = 5;
        const int TOHIDE = 6;
        string[] mode = { "锁", "得到控制", "selecting", "退出全屏", "向右翻", "全屏", "向左翻" };//定义状态
        bool nextpage = false;
        bool prevpage = false;
        bool hiding = false;
        bool outside = false;
        int State = LOCKED;
        int itab = 0;
        #region joints definition
        public const int Jointnum = 25;
        public const int Spinebase = 0;
        public const int Spinemid = 1;
        public const int Neck = 2;
        public const int Head = 3;
        public const int Shoulderleft = 4;
        public const int Elbowleft = 5;
        public const int Wristleft = 6;
        public const int Handleft = 7;
        public const int Shoulderright = 8;
        public const int Elbowright = 9;
        public const int Wristright = 10;
        public const int Handright = 11;
        public const int Hipleft = 12;
        public const int Kneeleft = 13;
        public const int Ankleleft = 14;
        public const int Footleft = 15;
        public const int Hipright = 16;
        public const int Kneeright = 17;
        public const int Ankleright = 18;
        public const int Footright = 19;
        public const int Spineshoulder = 20;
        public const int Handtipleft = 21;
        public const int Thumbleft = 22;
        public const int Handtipright = 23;
        public const int Thumbright = 24;
        #endregion

        #region original varies
        /// <summary>
        /// 手画圆的半径
        /// </summary>
        private const double HandSize = 15;
        private const double HeadSize = 20;

        /// <summary>
        /// 厚度接缝
        /// </summary>
        private const double JointThickness = 3;

        /// <summary>
        /// 厚度剪辑矩形边缘
        /// </summary>
        private const double ClipBoundsThickness = 10;

        /// <summary>
        /// 常数夹紧摄像机空间点的Z值负
        /// </summary>
        private const float InferredZPositionClamp = 0.1f;

        /// <summary>
        /// 刷用于绘图的手,正在跟踪关闭
        /// </summary>
        private readonly Brush handClosedBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));

        /// <summary>
        /// 刷用于当前跟踪画的手打开
        /// </summary>
        private readonly Brush handOpenBrush = new SolidColorBrush(Color.FromArgb(128, 0, 255, 0));

        /// <summary>
        /// 刷用于绘图的手,目前跟踪如套索(指针)的位置
        /// </summary>
        private readonly Brush handLassoBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));

        /// <summary>
        /// 刷用于关节目前跟踪
        /// </summary>
        private readonly Brush trackedJointBrush = new SolidColorBrush(Color.FromArgb(255, 68, 192, 68));

        /// <summary>
        /// 刷用于关节目前推断
        /// </summary>        
        private readonly Brush inferredJointBrush = Brushes.Yellow;

        /// <summary>
        /// 笔用于绘图的骨头,目前推断
        /// </summary>        
        private readonly Pen inferredBonePen = new Pen(Brushes.Gray, 1);

        /// <summary>
        /// 画组身体呈现输出
        /// </summary>
        private DrawingGroup drawingGroup;

        /// <summary>
        /// 用图像显示
        /// </summary>
        private DrawingImage imageSource;

        /// <summary>
        /// 活跃的Kinect传感器
        /// </summary>
        private KinectSensor kinectSensor = null;

        /// <summary>
        /// 坐标映射器将一种类型的点映射到另一个地方
        /// </summary>
        private CoordinateMapper coordinateMapper = null;

        /// <summary>
        /// 读取的身体帧数
        /// </summary>
        private BodyFrameReader bodyFrameReader = null;
        //读取颜色的帧数
        private ColorFrameReader colorFrameReader = null;///////////////////////////////////////////////////////////////////////////////        

        /// <summary>
        /// 数组的身体
        /// </summary>
        private Body[] bodies = null;

        /// <summary>
        /// 定义骨骼
        /// </summary>
        private List<Tuple<JointType, JointType>> bones;

        /// <summary>
        /// 显示宽度(深度空间)
        /// </summary>
        private int displayWidth;

        /// <summary>
        /// 显示高度
        /// </summary>
        private int displayHeight;

        /// <summary>
        /// 每个身体的颜色跟踪列表
        /// </summary>
        private List<Pen> bodyColors;
        #endregion

        /// <summary>
        /// 初始化主窗口类的一个新实例.
        /// </summary>
        public MainWindow()
        {
            // 目前支持的一个传感器
            this.kinectSensor = KinectSensor.GetDefault();

            // 得到坐标映射器
            this.coordinateMapper = this.kinectSensor.CoordinateMapper;

            // 得到的深度(显示)区段
            FrameDescription frameDescription = this.kinectSensor.DepthFrameSource.FrameDescription;

            // 得到关节空间的大小
            this.displayWidth = frameDescription.Width;
            this.displayHeight = frameDescription.Height;

            // 打开读取的帧
            this.bodyFrameReader = this.kinectSensor.BodyFrameSource.OpenReader();
            //打开颜色读取帧
            if(COLORFRAME)this.colorFrameReader = this.kinectSensor.ColorFrameSource.OpenReader();///////////////////////////////////////////////////

            // 骨头被定义为两个关节之间的一条线
            this.bones = new List<Tuple<JointType, JointType>>();

            // 躯干
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Head, JointType.Neck));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft));

            //右臂
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight));

            // 左臂
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft));

            // 右腿
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight));

            // 左腿
            this.bones.Add(new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft));
            this.bones.Add(new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft));

            // 身体填充颜色,每个体形指数一个
            this.bodyColors = new List<Pen>();

            this.bodyColors.Add(new Pen(Brushes.Red, 6));
            this.bodyColors.Add(new Pen(Brushes.Orange, 6));
            this.bodyColors.Add(new Pen(Brushes.Green, 6));
            this.bodyColors.Add(new Pen(Brushes.Blue, 6));
            this.bodyColors.Add(new Pen(Brushes.Indigo, 6));
            this.bodyColors.Add(new Pen(Brushes.Violet, 6));

            //打开传感器
            this.kinectSensor.Open();

            // 创建绘图组我们将使用绘图
            this.drawingGroup = new DrawingGroup();

            // 创建一个图像源图像中我们可以使用控制
            this.imageSource = new DrawingImage(this.drawingGroup);

            // 使用窗口对象作为视图模型在这个简单的例子
            this.DataContext = this;

            // 初始化
            InitializeComponent();
        }

        /// <summary>
        /// 获取位图显示
        /// </summary>
        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
        }

        /// <summary>
        /// 执行启动任务
        /// </summary>
        /// <param name="sender">对象发送事件</param>
        /// <param name="e">说明</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                this.bodyFrameReader.FrameArrived += this.Reader_FrameArrived;
            }
            if (COLORFRAME)if (this.colorFrameReader != null)
            {
                colorFrameReader.FrameArrived += colorFrameReader_FrameArrived;///////////////////////////////////////////////////////
            }
        }

        /// <summary>
        /// 执行关闭任务
        /// </summary>
      
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            if (this.bodyFrameReader != null)
            {
                // 骨架接口读取
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }
            if (COLORFRAME) if (this.colorFrameReader != null)/////////////////////////////////////////////////////////////////////////////////////
            {
                // 色帧接口读取
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }
            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
        }

        /// <summary>
        /// 大骨架处理来自传感器的数据
        /// </summary>

        private void Reader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            bool dataReceived = false;
            using (BodyFrame bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame != null)
                {
                    if (this.bodies == null) this.bodies = new Body[bodyFrame.BodyCount];
                    // Kinect GetAndRefreshBodyData第一次被调用时,将分配数组中的每个身体。
                    // 只要这些身体对象不处理而不是数组中设置为null,这些身体的对象将被重用。
                    bodyFrame.GetAndRefreshBodyData(this.bodies);
                    dataReceived = true;
                }
            }

            if (dataReceived)
            {
                try
                {
                    using (DrawingContext dc = this.drawingGroup.Open())
                    {
                        // 画一个透明背景设置渲染的大小
                        dc.DrawRectangle(Brushes.Transparent, null, new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));

                        int penIndex = 0;
                        var body = bodies[0];
                        foreach (Body thebody in this.bodies)
                        {
                            Pen drawPen = this.bodyColors[penIndex++];
                            if (thebody.IsTracked)
                            {
                                this.DrawBody(thebody, dc, drawPen);
                                if (body.Joints[JointType.SpineBase].Position.Z == 0) body = thebody;
                                else if (body.Joints[JointType.SpineBase].Position.Z > thebody.Joints[JointType.SpineBase].Position.Z) body = thebody;
                            }
                        }

                        Pen drawPenInside = new Pen(Brushes.LightBlue, 11);
                        Pen drawPenOutside = new Pen(Brushes.DarkBlue, 11);
                        this.DrawClippedEdges(body, dc);
                        if (outside) this.DrawBody(body, dc, drawPenOutside);
                        else
                        {
                            this.DrawBody(body, dc, drawPenInside);
                            //PPT控制
                            #region PPT control
                            //头
                            CameraSpacePoint head = getCameraSpacePoint(body, JointType.Head);
                            //左手
                            CameraSpacePoint handL = getCameraSpacePoint(body, JointType.HandLeft);
                            //右手
                            CameraSpacePoint handR = getCameraSpacePoint(body, JointType.HandRight);

                            CameraSpacePoint spineBase = getCameraSpacePoint(body, JointType.SpineBase);
                            //右臂
                            CameraSpacePoint shoulderR = getCameraSpacePoint(body, JointType.ShoulderRight);
                            //左臂
                            CameraSpacePoint shoulderL = getCameraSpacePoint(body, JointType.ShoulderLeft);

                            if (hiding)
                            {
                                state.Content = "";
                                state.Background = Brushes.Transparent;
                            }
                            else
                            {
                                state.Content = mode[State];
                                state.Background = Brushes.Black;
                            }
                            if (handL.Y < head.Y) State = LOCKED;
                            if (State == LOCKED) { }
                            //如果髋骨中心Z轴-左手Z >0.15f 与髋骨中心Z轴-右手Z轴<0.15f 与 右手Z-髋骨中心Z轴>150  退出程序
                            else if (spineBase.Z - handL.Z > LIFT && spineBase.Z - handR.Z < LIFT && handR.X - spineBase.X > 150) exit();
                            else if (spineBase.Z - handL.Z > LIFT && spineBase.Z - handR.Z > LIFT)
                            {
                                //将TOESC值赋给右手张开 左手张开 
                                if (body.HandRightState == HandState.Open && body.HandLeftState == HandState.Open) State = TOESC;
                                //将SELECTING值赋给右手闭合 左手张开 
                                if (body.HandRightState == HandState.Open && body.HandLeftState == HandState.Closed) { State = SELECTING; itab = 0; }

                                if (body.HandRightState == HandState.Closed && body.HandLeftState == HandState.Open) State = TOPLAY;
                                if (body.HandRightState == HandState.Closed && body.HandLeftState == HandState.Closed) State = TOSTART;
                                //将SELECTING值赋给右手闭合 左手做个手势
                                if (body.HandRightState == HandState.Open && body.HandLeftState == HandState.Lasso) State = TOHIDE;
                            }
                            switch (State)
                            {
                                case LOCKED:
                                    if (handR.Y < head.Y) State = CONTROLLING;
                                    break;
                                case TOHIDE:
                                    if (spineBase.Z - handL.Z < LIFT && spineBase.Z - handR.Z < LIFT)
                                    {
                                        //hiding = !hiding;
                                        //State = CONTROLLING;
                                      
                                        SendKeys.SendWait("{Left}");
                                        State = CONTROLLING;
                                    }
                                    break;
                                case TOSTART:
                                    if (spineBase.Z - handL.Z < LIFT && spineBase.Z - handR.Z < LIFT)
                                    {
                                        SendKeys.SendWait("{F5}");
                                        State = CONTROLLING;
                                    }
                                    break;
                                case TOPLAY:
                                    if (spineBase.Z - handL.Z < LIFT && spineBase.Z - handR.Z < LIFT)
                                    {
                                        SendKeys.SendWait("{Tab}");
                                        SendKeys.SendWait("{Enter}");
                                        State = LOCKED;
                                    }
                                    break;
                                case TOESC:
                                    if (spineBase.Z - handL.Z < LIFT && spineBase.Z - handR.Z < LIFT)
                                    {
                                        SendKeys.SendWait("{Esc}");
                                        State = LOCKED;
                                    }
                                    break;
                                case CONTROLLING:
                                    if (spineBase.Z - handL.Z > LIFT && spineBase.Z - handR.Z < LIFT) //如果左手抬起向前   按左键
                                    {
                                        if (handL.X < shoulderL.X) prevpage = true;
                                        else
                                        {
                                            if (prevpage)
                                            {
                                                SendKeys.SendWait("{Left}");
                                                prevpage = false;
                                            }
                                        }
                                    }
                                    else if (spineBase.Z - handR.Z > LIFT && spineBase.Z - handL.Z < LIFT) // 如果右手只向前推进
                                    {
                                        if (handR.X > shoulderR.X) nextpage = true;
                                        else
                                        {
                                            if (nextpage)
                                            {
                                                SendKeys.SendWait("{Right}");
                                                nextpage = false;
                                            }
                                        }
                                    }
                                    break;
                                //case SELECTING:
                                  
                                //    break;
                                default: break;
                            }
                            #endregion
                        }

                        // 防止我们的渲染区域以外的图纸
                        this.drawingGroup.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, this.displayWidth, this.displayHeight));
                    }
                }
                catch { return; }
            }
        }
        private CameraSpacePoint getCameraSpacePoint(Body body, JointType jointType)
        {
            CameraSpacePoint cameraSpacePoint;
            CameraSpacePoint position = body.Joints[jointType].Position;
            if (position.Z < 0) position.Z = InferredZPositionClamp;
            cameraSpacePoint.Z = position.Z;
            DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
            cameraSpacePoint.X = depthSpacePoint.X;
            cameraSpacePoint.Y = depthSpacePoint.Y;
            return cameraSpacePoint;
        }

        /// <summary>
        /// 画了一个身体
        /// </summary>
        /// <param name="joints">关节画</param>
        /// <param name="jointPoints">关节位置</param>
        /// <param name="drawingContext">画背景画</param>
        /// <param name="drawingPen">指定颜色画一个特定的身体</param>
        private void DrawBody(Body thebody, DrawingContext drawingContext, Pen drawingPen)
        {
            if (hiding) return;
            IReadOnlyDictionary<JointType, Joint> joints = thebody.Joints;
            // 联合点转换为深度(显示)空间
            Dictionary<JointType, Point> jointPoints = new Dictionary<JointType, Point>();
            foreach (JointType jointType in joints.Keys)
            {
                // 有时的深度(Z)推断联合可能显示为负
                // 夹到0.1度,以防止coordinatemapper返回(无穷小,无穷小)
                CameraSpacePoint position = joints[jointType].Position;
                if (position.Z < 0) position.Z = InferredZPositionClamp;
                DepthSpacePoint depthSpacePoint = this.coordinateMapper.MapCameraPointToDepthSpace(position);
                jointPoints[jointType] = new Point(depthSpacePoint.X, depthSpacePoint.Y);
            }

            // 画出的骨头
            foreach (var bone in this.bones)
            {
                this.DrawBone(joints, jointPoints, bone.Item1, bone.Item2, drawingContext, drawingPen);
            }

            // 画出关节
            foreach (JointType jointType in joints.Keys)
            {
                Brush drawBrush = null;

                TrackingState trackingState = joints[jointType].TrackingState;

                if (trackingState == TrackingState.Tracked)
                {
                    drawBrush = this.trackedJointBrush;
                }
                else if (trackingState == TrackingState.Inferred)
                {
                    drawBrush = this.inferredJointBrush;
                }

                if (drawBrush != null)
                {
                    drawingContext.DrawEllipse(drawBrush, null, jointPoints[jointType], JointThickness, JointThickness);
                }
            }

            // 画出的手和脑袋
            Brush drawbrush = Brushes.LightBlue;
            drawingContext.DrawEllipse(drawbrush, null, jointPoints[JointType.Head], HeadSize, HeadSize);
            switch (thebody.HandLeftState)
            {
                case HandState.Closed:drawbrush = this.handClosedBrush; break;
                case HandState.Open:drawbrush = this.handOpenBrush; break;
                case HandState.Lasso:drawbrush = this.handLassoBrush; break;
                case HandState.Unknown:drawbrush = Brushes.Black; break;
                case HandState.NotTracked: drawbrush = Brushes.Yellow; break;
            }
            drawingContext.DrawEllipse(drawbrush, null, jointPoints[JointType.HandLeft], HandSize, HandSize);
            switch (thebody.HandRightState)
            {
                case HandState.Closed: drawbrush = this.handClosedBrush; break;
                case HandState.Open: drawbrush = this.handOpenBrush; break;
                case HandState.Lasso: drawbrush = this.handLassoBrush; break;
                case HandState.Unknown: drawbrush = Brushes.Black; break;
                case HandState.NotTracked: drawbrush = Brushes.Yellow; break;
            }
            drawingContext.DrawEllipse(drawbrush, null, jointPoints[JointType.HandRight], HandSize, HandSize);
        }

        /// <summary>
        ///画一个身体的骨骼(关节,关节)
        /// </summary>
        /// <param name="joints">关节图</param>
        /// <param name="jointPoints">关节位置</param>
        /// <param name="jointType0">第一个关节的骨头</param>
        /// <param name="jointType1">第二个关节的骨头</param>
        /// <param name="drawingContext">画背景画</param>
        /// /// <param name="drawingPen">指定颜色画一个特定的骨骼</param>
        private void DrawBone(IReadOnlyDictionary<JointType, Joint> joints, IDictionary<JointType, Point> jointPoints, JointType jointType0, JointType jointType1, DrawingContext drawingContext, Pen drawingPen)
        {
            Joint joint0 = joints[jointType0];
            Joint joint1 = joints[jointType1];

            // 如果我们找不到这两个关节,退出
            if (joint0.TrackingState == TrackingState.NotTracked ||
                joint1.TrackingState == TrackingState.NotTracked)
            {
                return;
            }

            // 我们假设所有跟踪骨头都推断除非关节
            Pen drawPen = this.inferredBonePen;
            if ((joint0.TrackingState == TrackingState.Tracked) && (joint1.TrackingState == TrackingState.Tracked))
            {
                drawPen = drawingPen;
            }

            drawingContext.DrawLine(drawPen, jointPoints[jointType0], jointPoints[jointType1]);
        }

        /// <summary>
        ///绘制指标显示哪些边缘裁剪的身体数据
        /// </summary>
        /// <param name="body">身体画剪裁信息</param>
        /// <param name="drawingContext">画背景画</param>
        private void DrawClippedEdges(Body body, DrawingContext drawingContext)
        {
            outside = false;
            FrameEdges clippedEdges = body.ClippedEdges;

            if (clippedEdges.HasFlag(FrameEdges.Bottom))
            {
                if (!hiding) drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, this.displayHeight - ClipBoundsThickness, this.displayWidth, ClipBoundsThickness));
            }

            if (clippedEdges.HasFlag(FrameEdges.Top))
            {
                if (!hiding) drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, this.displayWidth, ClipBoundsThickness));
                outside = true;
            }

            if (clippedEdges.HasFlag(FrameEdges.Left))
            {
                if (!hiding) drawingContext.DrawRectangle(Brushes.Red, null, new Rect(0, 0, ClipBoundsThickness, this.displayHeight));
                outside = true;
            }

            if (clippedEdges.HasFlag(FrameEdges.Right))
            {
                if (!hiding) drawingContext.DrawRectangle(Brushes.Red, null, new Rect(this.displayWidth - ClipBoundsThickness, 0, ClipBoundsThickness, this.displayHeight));
                outside = true;
            }
        }

        private void colorFrameReader_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
        {
            using (ColorFrame colorFrame = e.FrameReference.AcquireFrame())
            {
                // 防御性编程:以防传感器跳过一个框架,退出功能
                if (colorFrame == null) return;

                // 设置一个数组,可以容纳所有的字节的图像
                var colorFrameDescription = colorFrame.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
                var frameSize = colorFrameDescription.Width * colorFrameDescription.Height * colorFrameDescription.BytesPerPixel;
                var colorData = new byte[frameSize];

                //填写的数组数据从相机
                colorFrame.CopyConvertedFrameDataToArray(colorData, ColorImageFormat.Bgra);

                // 使用字节数组,使图像在屏幕上
                CameraImage.Source = BitmapSource.Create(
                    colorFrame.ColorFrameSource.FrameDescription.Width,
                    colorFrame.ColorFrameSource.FrameDescription.Height,
                    96, 96, PixelFormats.Bgr32, null, colorData, colorFrameDescription.Width * 4);
            }
        }

        private void exit()
        {
            if (this.bodyFrameReader != null)
            {
                // 身体骨架接口读取
                this.bodyFrameReader.Dispose();
                this.bodyFrameReader = null;
            }
            if (COLORFRAME) if (this.colorFrameReader != null)///////////////////////////////////////////////////////////////////////////////////////
            {
                // 色帧接口读取
                this.colorFrameReader.Dispose();
                this.colorFrameReader = null;
            }
            if (this.kinectSensor != null)
            {
                this.kinectSensor.Close();
                this.kinectSensor = null;
            }
            for (int i = 0; i < 3; i++)
            {
                state.Content = "Kinect_PPT is closing......(" + (3 - i).ToString() + ")";
                System.Threading.Thread t = new System.Threading.Thread(o => System.Threading.Thread.Sleep(1000));
                t.Start(this);
                while (t.IsAlive) System.Windows.Forms.Application.DoEvents();
            }
            //System.Threading.Thread.Sleep(3000);
            System.Environment.Exit(System.Environment.ExitCode);
        }
    }
}
