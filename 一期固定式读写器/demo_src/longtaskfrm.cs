using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ModuleTech;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace ModuleReaderManager
{
    public partial class longtaskfrm : Form
    {
        public longtaskfrm(Reader rdr_)
        {
            InitializeComponent();
            rdr = rdr_;
        }
        Reader rdr = null;

        private void btnsetlongtask_Click(object sender, EventArgs e)
        {
            
            if ((!this.rbexeinstant.Checked) && (!this.rbexepoweron.Checked))
            {
                MessageBox.Show("请选择操作属性");
                return;
            }
            if ((!this.rbsearchtag.Checked) && (!this.rbsearandread.Checked) && (!this.rbseaandwrite.Checked))
            {
                MessageBox.Show("请选择操作类型");
                return;
            }
            if ((!this.cbant1.Checked) && (!this.cbant2.Checked)
                && (!this.cbant3.Checked) && (!this.cbant4.Checked))
            {
                MessageBox.Show("请选择天线");
                return;
            }
            if ((!this.rbgen2.Checked) && (!this.rb180006b.Checked))
            {
                MessageBox.Show("请选择协议");
                return;
            }
            if (this.tbuploadintv.Text.Trim() == string.Empty)
            {
                MessageBox.Show("输入上传间隔");
                return;
            }
            if (this.tbuploadip.Text.Trim() == string.Empty)
            {
                MessageBox.Show("输入上传地址");
                return;
            }
            if (this.tbuploadport.Text.Trim() == string.Empty)
            {
                MessageBox.Show("输入上传端口");
                return;
            }
            if (this.tbreaddur.Text.Trim() == string.Empty)
            {
                MessageBox.Show("输入读时长");
                return;
            }
            if (this.tbreadinterval.Text.Trim() == string.Empty)
            {
                MessageBox.Show("输入读间隔");
                return;
            }
            if (this.rbsearandread.Checked)
            {
                if (this.tbblkcnt.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("输入读块数");
                    return;
                }
            }
            if (this.rbseaandwrite.Checked)
            {
                if (this.rtbwdata.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("输入写入数据");
                    return;
                }
            }

            if (this.rbsearandread.Checked || this.rbseaandwrite.Checked)
            {
                if (this.cbbbank.SelectedIndex == -1)
                {
                    MessageBox.Show("请选择bank");
                    return;
                }
                if (this.tbstartaddr.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("输入起始地址");
                    return;
                }
            }

            LongTaskInfo ltinfo = new LongTaskInfo();
            if (this.tbpwd.Text.Trim() == string.Empty)
            {
                ltinfo.AccessPassword = (uint)0;
            }
            else
            {
                byte[] tmp = ByteFormat.FromHex(this.tbpwd.Text.Trim());
                ltinfo.AccessPassword = (uint)((tmp[0] << 24) | (tmp[1] << 16) | (tmp[2] << 8) | tmp[3]);
            }
            ltinfo.InvDur = int.Parse(this.tbreaddur.Text.Trim());
            ltinfo.InvInterval = int.Parse(this.tbreadinterval.Text.Trim());
            ltinfo.UploadPort = int.Parse(this.tbuploadport.Text.Trim());
            ltinfo.UploadIp = this.tbuploadip.Text.Trim();
            ltinfo.UploadInterval = int.Parse(this.tbuploadintv.Text.Trim());
            List<int> ants = new List<int>();
            if (this.cbant1.Checked)
                ants.Add(1);
            if (this.cbant2.Checked)
                ants.Add(2);
            if (this.cbant3.Checked)
                ants.Add(3);
            if (this.cbant4.Checked)
                ants.Add(4);
            ltinfo.OpAnts = ants.ToArray();
            if (this.rb180006b.Checked)
                ltinfo.Potl = TagProtocol.ISO180006B;
            else
                ltinfo.Potl = TagProtocol.GEN2;
            if (this.rbsearchtag.Checked)
                ltinfo.OpType = LongTaskInfo.TagOpType.TagOp_SearchTag;
            else if (this.rbsearandread.Checked)
                ltinfo.OpType = LongTaskInfo.TagOpType.TagOp_SearchTagAndReadBank;
            else
                ltinfo.OpType = LongTaskInfo.TagOpType.TagOp_SearchTagAndWriteBank;
            if (this.rbexeinstant.Checked)
                ltinfo.Action = LongTaskInfo.LongTaskAction.LTA_Start_Instant;
            else
                ltinfo.Action = LongTaskInfo.LongTaskAction.LTA_Start_PowerOn;

            if (this.rbsearandread.Checked || this.rbseaandwrite.Checked)
            {
                ltinfo.OpBank = this.cbbbank.SelectedIndex;
                ltinfo.StartAddr = int.Parse(this.tbstartaddr.Text.Trim());
            }

            if (this.rbsearandread.Checked)
            {
                ltinfo.BlkCnt = int.Parse(this.tbblkcnt.Text.Trim());
            }
            if (this.rbseaandwrite.Checked)
            {
                ltinfo.Wdata = ByteFormat.FromHex(this.rtbwdata.Text.Trim());
            }

            try
            {
         //       ltinfo.IsTriggerByGpi = true;
         //       ltinfo.GpiBitMap = 0xf;
                ltinfo.IsDriveGpo = false;
                ltinfo.IsTriggerByGpi = false;
                //ltinfo.OpGpiBitMap = 0x7;
                //ltinfo.SuccesGpoBitMap = 0xf;
                //ltinfo.DefaultGpoBitMap = 0x01;
                //ltinfo.SucGpoBitMapDur = 2;
                rdr.ParamSet("LongTaskSetting", ltinfo);
                this.tblocalport.Text = this.tbuploadport.Text;
                MessageBox.Show("设置成功");
            }
            catch
            {
                MessageBox.Show("设置失败");
            }
        }

        private void btnstoplongtask_Click(object sender, EventArgs e)
        {
            try
            {
                rdr.ParamSet("StopLongTask", LongTaskInfo.StopLongTaskAction.SLA_Stop_LongTask);
                MessageBox.Show("操作成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作作败:"+ex.ToString());
            }
        }

        private void btnstopltanddel_Click(object sender, EventArgs e)
        {
            try
            {
                rdr.ParamSet("StopLongTask", LongTaskInfo.StopLongTaskAction.SLA_Stop_LongTask_And_Modify_Conf);
                MessageBox.Show("操作成功");
            }
            catch
            {
                MessageBox.Show("操作作败");
            }
        }

        int localport = 0;
        bool isaccept = false;
        TcpListener tcpListener = null;
        TcpClient client = null;
        NetworkStream stream = null;

        int getmsg(byte[] buf, int len)
        {
            int nleft = len;
            int pos = 0;
            int nread = 0;
            while (nleft > 0)
            {
                nread = stream.Read(buf, pos, nleft);
                if (nread == 0)
                    return -1;
                nleft -= nread;
                pos += nread;
            }
            return 0;
        }

        BackgroundWorker WaitTags;

        delegate void UpdateUi(ResultInfo relt);

        //delegate void StopRecv();
        //void AutoStopRecv()
        //{
        //    this.btnbindrecv.Enabled = true;
        //    this.btnstoprecv.Enabled = false;
        //    isaccept = false;
        //}

        void addrecord__(ResultInfo relt)
        {
            if (this.lvresult.InvokeRequired)
            {
                this.BeginInvoke(new UpdateUi(addrecord__), new object[] { relt });
            }
            else
            {
                addrecord(relt);
            }
        }
        void addrecord(ResultInfo relt)
        {
            Console.WriteLine("enter addrecord");
            string epcstr = ByteFormat.ToHex(relt.epc);
            string rdatastr = null;
            if (relt.rdata != null)
                rdatastr = ByteFormat.ToHex(relt.rdata);

            bool isnew = true;

            foreach (ListViewItem item in this.lvresult.Items)
            {
                if (item.SubItems[0].Text == epcstr)
                {
                    isnew = false;
                    item.SubItems[2].Text = (int.Parse(item.SubItems[2].Text) + relt.epcrcnt).ToString();
                    if (rdatastr != null)
                    {
                        if (item.SubItems[1].Text != rdatastr)
                            item.SubItems[1].Text = rdatastr;
                    }
                    if (relt.optype != 1)
                    {
                        if (item.SubItems[3].Text == "")
                            item.SubItems[3].Text = relt.opsuccnt.ToString();
                        else
                            item.SubItems[3].Text = (int.Parse(item.SubItems[3].Text) + relt.opsuccnt).ToString();
                    }

                    item.SubItems[4].Text = relt.antid.ToString();
                    item.SubItems[6].Text = relt.timestamp;
                    break;
                }
            }

            if (isnew)
            {
                ListViewItem neit = new ListViewItem(epcstr);
                if (rdatastr != null)
                {
                    neit.SubItems.Add(rdatastr);
                }
                else
                    neit.SubItems.Add("");
                neit.SubItems.Add(relt.epcrcnt.ToString());
                if (relt.optype != 1)
                    neit.SubItems.Add(relt.opsuccnt.ToString());
                else
                    neit.SubItems.Add("");
                neit.SubItems.Add(relt.antid.ToString());

                if (relt.potl == 0)
                    neit.SubItems.Add("Gen2");
                else
                    neit.SubItems.Add("180006b");

                neit.SubItems.Add(relt.timestamp);
                lvresult.Items.Add(neit);
            }
            this.labtottagnum.Text = lvresult.Items.Count.ToString();
        }
        /*
        void WaitResult()
        {
            byte[] buf = new byte[152];

            try
            {
                tcpListener = new TcpListener(IPAddress.Any, localport);
                tcpListener.Start();
            }
            catch
            {
                Debug.WriteLine("服务器线程创建失败");
                return;
            }
            while (isaccept)
            {
                try
                {
                    client = tcpListener.AcceptTcpClient();
                }
                catch (SocketException exp)
                {
                    Console.WriteLine("999999999999");
                    return;
                }

                stream = client.GetStream();
                try
                {
                    if (getmsg(buf, 152) < 0)
                        break;
                }
                catch
                {
                    continue;
                }

                //Console.WriteLine("");
                //for (int j = 0; j < 152; ++j)
                //{
                //    Console.Write(" " + buf[j].ToString());
                //}
                //Console.WriteLine("");

                if (buf[0] != 0 || buf[1] != 0)
                {
                    if (buf[0] == 0xff && buf[1] == 0xff)
                    {
                        stream.Close();
                        stream = null;
                        client.Close();
                        client = null;
                        tcpListener.Stop();
                        tcpListener = null;
                        return;
                    }
                    MessageBox.Show("读写器错误：" + ((buf[0] << 8) | buf[1]).ToString());
                    stream.Close();
                    stream = null;
                    client.Close();
                    client = null;
                    tcpListener.Stop();
                    tcpListener = null;
                    this.Invoke(new StopRecv(AutoStopRecv));
                    return;
                }

                byte optype = buf[2];
                byte[] epc = new byte[buf[3]];
                Array.Copy(buf, 4, epc, 0, epc.Length);
                byte opret = buf[68];
                byte rdatalen = 0;
                byte[] rdata = null;
                if (optype == 2)
                {
                    rdatalen = buf[69];
                    if (rdatalen > 0)
                    {
                        rdata = new byte[rdatalen];
                        Array.Copy(buf, 70, rdata, 0, rdata.Length);
                    }
                }
                byte antid = buf[134];
                byte potl = buf[135];
                byte epcrcnt = buf[136];
                byte opsuccnt = buf[137];
                string timestamp = Encoding.ASCII.GetString(buf, 138, 14);
                //Console.WriteLine("epc.len:" + epc.Length.ToString());
                //Console.WriteLine("optype:" + optype.ToString());
                //Console.WriteLine("opret:" + opret.ToString());
                //if (rdata != null)
                //{
                //    Console.WriteLine("rdata.len:" + rdata.Length.ToString());
                //}
                //Console.WriteLine("antid:" + antid.ToString());
                //Console.WriteLine("potl:" + potl.ToString());
                //Console.WriteLine("epcrcnt:" + epcrcnt.ToString());
                //Console.WriteLine("opsuccnt:" + opsuccnt.ToString());
                //Console.WriteLine("timestamp:" + timestamp.ToString());
                //Console.WriteLine("00000000000000000000000000000000000000");
                ResultInfo relt = new ResultInfo();
                relt.epc = epc;
                relt.optype = optype;
                relt.opret = opret;
                relt.rdata = rdata;
                relt.antid = antid;
                relt.potl = potl;
                relt.epcrcnt = epcrcnt;
                relt.opsuccnt = opsuccnt;
                relt.timestamp = timestamp;
                addrecord__(relt);
                //try
                //{
                //    this.BeginInvoke(new UpdateUi(addrecord), new object[] {relt});
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.ToString());
                //}
                stream.Close();
                stream = null;
                client.Close();
                client = null;
            }
        }
        */

        private void backgroundWorker1_ProgressChanged(object sender,
         ProgressChangedEventArgs e)
        {
            ResultInfo ret = (ResultInfo)e.UserState;
            addrecord(ret);
        }
        private void backgroundWorker1_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            this.btnbindrecv.Enabled = true;
            this.btnstoprecv.Enabled = false;
            isaccept = false;
        }



        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] buf = new byte[152];

            try
            {
                tcpListener = new TcpListener(IPAddress.Any, localport);
                tcpListener.Start();
            }
            catch
            {
                MessageBox.Show("listen err");
                tcpListener.Stop();
                tcpListener = null;
                return;
            }
            while (isaccept)
            {
                try
                {
                    client = tcpListener.AcceptTcpClient();
                }
                catch (SocketException exp)
                {
                    MessageBox.Show("accept err");
                    tcpListener.Stop();
                    tcpListener = null;
                    return;
                }

                stream = client.GetStream();
                try
                {
                    if (getmsg(buf, 152) < 0)
                    {
                        stream.Close();
                        stream = null;
                        client.Close();
                        client = null;
                        continue;
                    }
                }
                catch
                {
                    stream.Close();
                    stream = null;
                    client.Close();
                    client = null;
                    continue;
                }

                //Console.WriteLine("");
                //for (int j = 0; j < 152; ++j)
                //{
                //    Console.Write(" " + buf[j].ToString());
                //}
                //Console.WriteLine("");

                if (buf[0] != 0 || buf[1] != 0)
                {
                    if (buf[0] == 0xff && buf[1] == 0xff)
                    {
                        stream.Close();
                        stream = null;
                        client.Close();
                        client = null;
                        tcpListener.Stop();
                        tcpListener = null;
                        return;
                    }
                    MessageBox.Show("读写器错误：" + ((buf[0] << 8) | buf[1]).ToString());
                    stream.Close();
                    stream = null;
                    client.Close();
                    client = null;
                    tcpListener.Stop();
                    tcpListener = null;
       //             this.Invoke(new StopRecv(AutoStopRecv));
                    return;
                }

                byte optype = buf[2];
                byte[] epc = new byte[buf[3]];
                Array.Copy(buf, 4, epc, 0, epc.Length);
                byte opret = buf[68];
                byte rdatalen = 0;
                byte[] rdata = null;
                if (optype == 2)
                {
                    rdatalen = buf[69];
                    if (rdatalen > 0)
                    {
                        rdata = new byte[rdatalen];
                        Array.Copy(buf, 70, rdata, 0, rdata.Length);
                    }
                }
                byte antid = buf[134];
                byte potl = buf[135];
                byte epcrcnt = buf[136];
                byte opsuccnt = buf[137];
                string timestamp = Encoding.ASCII.GetString(buf, 138, 14);
                //Console.WriteLine("epc.len:" + epc.Length.ToString());
                //Console.WriteLine("optype:" + optype.ToString());
                //Console.WriteLine("opret:" + opret.ToString());
                //if (rdata != null)
                //{
                //    Console.WriteLine("rdata.len:" + rdata.Length.ToString());
                //}
                //Console.WriteLine("antid:" + antid.ToString());
                //Console.WriteLine("potl:" + potl.ToString());
                //Console.WriteLine("epcrcnt:" + epcrcnt.ToString());
                //Console.WriteLine("opsuccnt:" + opsuccnt.ToString());
                //Console.WriteLine("timestamp:" + timestamp.ToString());
                //Console.WriteLine("00000000000000000000000000000000000000");
                ResultInfo relt = new ResultInfo();
                relt.epc = epc;
                relt.optype = optype;
                relt.opret = opret;
                relt.rdata = rdata;
                relt.antid = antid;
                relt.potl = potl;
                relt.epcrcnt = epcrcnt;
                relt.opsuccnt = opsuccnt;
                relt.timestamp = timestamp;
             
                WaitTags.ReportProgress(0, relt);
          
                //try
                //{
                //    this.BeginInvoke(new UpdateUi(addrecord), new object[] {relt});
                //}
                //catch (Exception ex)
                //{
                //    Console.WriteLine(ex.ToString());
                //}
                stream.Close();
                stream = null;
                client.Close();
                client = null;
            }
        }

        private void btnbindrecv_Click(object sender, EventArgs e)
        {
            if (this.tblocalport.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入绑定的本地端口");
                return;
            }
            localport = int.Parse(this.tblocalport.Text.Trim());

            WaitTags = new BackgroundWorker();
            WaitTags.WorkerReportsProgress = true;
            WaitTags.DoWork +=
                new DoWorkEventHandler(backgroundWorker1_DoWork);
            WaitTags.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorker1_RunWorkerCompleted);
            WaitTags.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorker1_ProgressChanged);
            isaccept = true;
            WaitTags.RunWorkerAsync();
            this.btnbindrecv.Enabled = false;
            this.btnstoprecv.Enabled = true;
          //  WaitTags
            /*
            if (this.tblocalport.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入绑定的本地端口");
                return;
            }
            localport = int.Parse(this.tblocalport.Text.Trim());

            Listenth = new Thread(WaitResult);
            isaccept = true;
            Listenth.Start();
            this.btnbindrecv.Enabled = false;
            this.btnstoprecv.Enabled = true;
             * */
        }

        private void btnstoprecv_Click(object sender, EventArgs e)
        {
            byte[] quit = new byte[152];
            quit[0] = 0xff;
            quit[1] = 0xff;
            TcpClient client = new TcpClient();
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint destpoint = new IPEndPoint(ip, localport);
            client.Connect(destpoint);
            NetworkStream tcpstream = client.GetStream();
            tcpstream.Write(quit, 0, quit.Length);
            tcpstream.Close();
            client.Close();

            /*
            byte[] quit = new byte[152];
            quit[0] = 0xff;
            quit[1] = 0xff;
            TcpClient client = new TcpClient();
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint destpoint = new IPEndPoint(ip, localport);
            client.Connect(destpoint);
            NetworkStream tcpstream = client.GetStream();
            tcpstream.Write(quit, 0, quit.Length);
            Listenth.Join();
            Listenth = null;
            this.btnbindrecv.Enabled = true;
            this.btnstoprecv.Enabled = false;
            isaccept = false;
             * */
        }

        private void btnclearlv_Click(object sender, EventArgs e)
        {
            this.lvresult.Items.Clear();
            this.labtottagnum.Text = "0";
        }

        private void longtaskfrm_Load(object sender, EventArgs e)
        {
            this.btnbindrecv.Enabled = true;
            this.btnstoprecv.Enabled = false;
        }

        private void longtaskfrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isaccept)
                btnstoprecv_Click(null, null);
        }



    }
    public class ResultInfo
    {
        public byte[] epc = null;
        public byte optype;
        public byte opret;
        public byte[] rdata = null;
        public byte antid;
        public byte potl;
        public byte epcrcnt;
        public byte opsuccnt;
        public string timestamp;
    }
}
