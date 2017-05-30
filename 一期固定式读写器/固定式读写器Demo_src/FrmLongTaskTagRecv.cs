#define TimeStampFormat_Short
#define UploadMsg_ReaderAlarm
#define UploadMsg_Notag
#define Xdiot_App
//#define SaveTagLog

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using ModuleTech;
using System.IO;
using System.Diagnostics;


namespace ModuleReaderManager
{
    public partial class FrmLongTaskTagRecv : Form
    {
        public FrmLongTaskTagRecv()
        {
            InitializeComponent();
            recvfrm = this;
        }

        delegate void UpdateUi(UploadTag[] tags);


        void addrecord(UploadTag[] tags)
        {
            if (this.lvtags.Items.Count > 100)
                this.lvtags.Items.Clear();

            foreach (UploadTag tag in tags)
            {
                bool isnew = true;

                foreach (ListViewItem item in this.lvtags.Items)
                {
                    if (item.SubItems[0].Text == tag.epcstr)
                    {
                        isnew = false;
                        item.SubItems[2].Text = (int.Parse(item.SubItems[2].Text) + tag.readcnt).ToString();
                        if (tag.bankstr != null)
                        {
                            if (item.SubItems[1].Text != tag.bankstr)
                                item.SubItems[1].Text = tag.bankstr;
                        }

                        item.SubItems[3].Text = tag.ant.ToString();
                        item.SubItems[5].Text = tag.rssi.ToString();
                        item.SubItems[6].Text = tag.tsstr;
                        break;
                    }
                }

                if (isnew)
                {
                    ListViewItem neit = new ListViewItem(tag.epcstr);
                    if (tag.bankstr != null)
                    {
                        neit.SubItems.Add(tag.bankstr);
                    }
                    else
                        neit.SubItems.Add("");
                    neit.SubItems.Add(tag.readcnt.ToString());
                    neit.SubItems.Add(tag.ant.ToString());

                    if (tag.potl == 5)
                        neit.SubItems.Add("Gen2");
                    else if (tag.potl == 3)
                        neit.SubItems.Add("180006b");
                    neit.SubItems.Add(tag.rssi.ToString());
                    neit.SubItems.Add(tag.tsstr);
                    lvtags.Items.Add(neit);
                }
            }
  
            this.labtotrttagnum.Text = lvtags.Items.Count.ToString();
        }

        void addrecord_offline(UploadTag[] tags)
        {
            foreach (UploadTag tag in tags)
            {
                bool isnew = true;

                foreach (ListViewItem item in this.lvoltags.Items)
                {
                    if (item.SubItems[0].Text == tag.epcstr)
                    {
                        isnew = false;
                        item.SubItems[2].Text = (int.Parse(item.SubItems[2].Text) + tag.readcnt).ToString();
                        if (tag.bankstr != null)
                        {
                            if (item.SubItems[1].Text != tag.bankstr)
                                item.SubItems[1].Text = tag.bankstr;
                        }

                        item.SubItems[3].Text = tag.ant.ToString();
                        item.SubItems[5].Text = tag.rssi.ToString();
                        item.SubItems[6].Text = tag.tsstr;
                        break;
                    }
                }

                if (isnew)
                {
                    ListViewItem neit = new ListViewItem(tag.epcstr);
                    if (tag.bankstr != null)
                    {
                        neit.SubItems.Add(tag.bankstr);
                    }
                    else
                        neit.SubItems.Add("");
                    neit.SubItems.Add(tag.readcnt.ToString());
                    neit.SubItems.Add(tag.ant.ToString());

                    if (tag.potl == 5)
                        neit.SubItems.Add("Gen2");
                    else if (tag.potl == 3)
                        neit.SubItems.Add("180006b");
                    neit.SubItems.Add(tag.rssi.ToString());
                    neit.SubItems.Add(tag.tsstr);
                    lvoltags.Items.Add(neit);
                }
            }

            this.labtotoltagnum.Text = lvoltags.Items.Count.ToString();
        }

        public static FrmLongTaskTagRecv recvfrm = null;

        private void FrmLongTaskTagRecv_FormClosing(object sender, FormClosingEventArgs e)
        {
            recvfrm = null;

#if SaveTagLog
            smwter.Flush();
            smwter.Close();
            smrawwter.Flush();
            smrawwter.Close();           
#endif
        }
        Thread thlisten = null;
        int localport;
        bool isaccept = false;
        TcpListener tcpListener = null;
        bool isStopListen = false;
        bool isRecvTags = false;

        Thread thlistenol = null;
        int localportol;
        bool isacceptol = false;
        TcpListener tcpListenerol = null;
        bool isStopListenol = false;
        bool isRecvTagsol = false;

        int getmsg(NetworkStream stream, byte[] buf, int len)
        {
            int nleft = len;
            int pos = 0;
            int nread = 0;
            while (nleft > 0)
            {
                try
                {
                    nread = stream.Read(buf, pos, nleft);
                }
                catch (System.Exception eff)
                {
                //    Debug.WriteLine("getmsg:" + eff.ToString());
                    return -2;
                }
                
                if (nread == 0)
                    return -1;
                nleft -= nread;
                pos += nread;
            }
            return 0;
        }
        class UploadTag
        {
            public string epcstr = null;
            public string bankstr = null;
            public int ant;
            public int rssi;
            public int potl;
            public int readcnt;
            public string tsstr = null;
        }

        string ParseShortTimeStamp(byte[] recvbuf, int offset)
        {
            int pos = offset;
            string tmstr = null;
            UInt32 firstint = (uint)((recvbuf[pos] << 24) | (recvbuf[pos + 1] << 16) |
                           (recvbuf[pos + 2] << 8) | recvbuf[pos + 3]);
            pos += 4;
            tmstr = ((firstint >> 20) & 0xfff).ToString();
            tmstr += ((firstint >> 16) & 0xf).ToString().PadLeft(2, '0');
            tmstr += ((firstint >> 11) & 0x1f).ToString().PadLeft(2, '0');
            tmstr += ((firstint >> 6) & 0x1f).ToString().PadLeft(2, '0');
            tmstr += (firstint & 0x3f).ToString().PadLeft(2, '0');
            tmstr += ((recvbuf[pos] >> 2) & 0x3f).ToString().PadLeft(2, '0');
            tmstr += ((recvbuf[pos] & 3) * 250).ToString().PadLeft(3, '0');
            return tmstr;
        }

 //       Dictionary<String, int> dicepcs = new Dictionary<String, int>();


        void ParseTagData(object state)
        {
            byte[] recvbuf = new byte[1500];
            TcpClient client = null;
            NetworkStream stream = null;
            UInt16 msgver = 0;
            byte msgtype = 0;

            UInt32 serialnum = 0;
            try
            {
                client = (TcpClient)state;
                
                stream = client.GetStream();
                stream.ReadTimeout = 2000;
                
                while (true)
                {
                    int pos = 0;
                    if (!isRecvTags)
                        break;
                    client.Client.Blocking = true;
                    int recret = getmsg(stream, recvbuf, 9);
                    if (recret < 0)
                    {
                        if (!isRecvTags)
                            break;
                        else
                        {
                            Debug.WriteLine("ParseTagData ffffffffffffffffffffffffffff");
                            continue;
                        }
                    }

                    msgver = (UInt16)((recvbuf[pos] << 8) | recvbuf[1 + pos]);
                    pos += 2;
                    msgtype = recvbuf[pos++];

                    serialnum = (UInt32)((recvbuf[pos] << 24) | (recvbuf[pos+1] << 16) |
                        (recvbuf[pos + 2] << 8) | recvbuf[pos+3]);
                    pos += 4;
  //                  Debug.WriteLine("serialnum:" + serialnum.ToString());
                    int datalen = (recvbuf[pos] << 8) | recvbuf[1+pos];
                    pos += 2;
                    //Debug.WriteLine("datalen:" + datalen.ToString());
#if SaveTagLog
                    smrawwter.WriteLine("----------------------------------------------------------------- 接收时间："+
                        DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff"));
                    smwter.WriteLine("----------------------------------------------------------------- 接收时间：" +
                        DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss fff"));

                    string rawdata = "";
                    for (int n = 0; n < 9; ++n)
                        rawdata += " " + recvbuf[n].ToString("X2");
#endif
                    recret = getmsg(stream, recvbuf, datalen);
                    if (recret < 0)
                        return;

#if SaveTagLog

                    for (int d = 0; d < datalen; ++d)
                        rawdata += " " + recvbuf[d].ToString("X2");
                    smrawwter.WriteLine(rawdata);
                    string msginfostr = "消息版本：";
#endif

#if UploadMsg_Notag
                    if (msgtype == 21)
                    {
 //                       Console.WriteLine("notag -----------------");
                        pos = 0;
                        int rdrnamelen = recvbuf[pos++];
                        pos += rdrnamelen;
                        string tailtm = ParseShortTimeStamp(recvbuf, pos);
                        Console.WriteLine("notag rttailtm:" + tailtm);
#if SaveTagLog
                        
                        msginfostr += msgver.ToString();
                        msginfostr += " 序列号：";
                        msginfostr += serialnum.ToString();
                        msginfostr += " 消息类型：";
                        msginfostr += msgtype.ToString();
                        msginfostr += " 消息时间戳：";
                        msginfostr += tailtm;
                        smwter.WriteLine(msginfostr);

#endif
                        continue;
                    }
#endif
#if UploadMsg_ReaderAlarm
                    if (msgtype == 22)
                    {
                        pos = 0;
                        UInt16 ecode = (UInt16)((recvbuf[pos] << 8) | recvbuf[pos + 1]);
 //                       Console.WriteLine("ecode:" + ecode.ToString());
                        pos += 2;
                        byte[] einfo = new byte[8];
                        Array.Copy(recvbuf, 2, einfo, 0, 8);
                        pos += 8;
 //                       Console.WriteLine("einfo:");
 //                       for (int d = 0; d < 8; ++d)
 //                           Console.Write(" " + einfo[d].ToString());
 //                       Console.WriteLine("");
                        int rdrnamelen = recvbuf[pos++];
                        pos += rdrnamelen;
                        string tailtm3 = ParseShortTimeStamp(recvbuf, pos);
 //                       Console.WriteLine("alarm rttailtm:" + tailtm3);
                        continue;
                    }
#endif
                    //
  //                  for (int n = 0; n < datalen; ++n)
   //                     Debug.Write(" " + recvbuf[n].ToString("X2"));
   //                 Debug.WriteLine("");
                    //

                    pos = 0;
                    int tagcnt = recvbuf[pos++];
                    UploadTag[] tags = new UploadTag[tagcnt];
//                    dicepcs.Clear();
#if SaveTagLog
                    msginfostr += msgver.ToString();
                    msginfostr += " 序列号：";
                    msginfostr += serialnum.ToString();
                    msginfostr += " 消息类型：";
                    msginfostr += msgtype.ToString();
                    string taginfostr = "";
                    
#endif
                    for (int i = 0; i < tagcnt; ++i)
                    {
                        tags[i] = new UploadTag();
                        int epclen = recvbuf[pos++];
                        Debug.WriteLine("epclen:" + epclen.ToString());
                        byte[] epc = new byte[epclen];
                        Array.Copy(recvbuf, pos, epc, 0, epclen);
                        tags[i].epcstr = ByteFormat.ToHex(epc);

  //                      if (dicepcs.ContainsKey(tags[i].epcstr))
  //                          MessageBox.Show("发现重复标签");
  //                      else
  //                      {
  //                          dicepcs.Add(tags[i].epcstr, 1);
  //                      }
                        Debug.WriteLine("epcstr:" + tags[i].epcstr);
                        pos += epclen;
                        int banklen = recvbuf[pos++];
                        Debug.WriteLine("banklen:" + banklen.ToString());
                        if (banklen > 0)
                        {
                            byte[] bank = new byte[banklen];
                            Array.Copy(recvbuf, pos, bank, 0, banklen);
                            pos += banklen;
                            tags[i].bankstr = ByteFormat.ToHex(bank);
                            Debug.WriteLine("bankstr:" + tags[i].bankstr);
                        }
                        tags[i].ant = recvbuf[pos++];
                        tags[i].readcnt = recvbuf[pos++];
                        Debug.WriteLine("readcnt:" + tags[i].readcnt.ToString());
                        tags[i].potl = recvbuf[pos++];
                        Debug.WriteLine("potl:" + tags[i].potl.ToString());
                        tags[i].rssi = (sbyte)recvbuf[pos++];
                        Debug.WriteLine("rssi:" + tags[i].rssi.ToString());
#if TimeStampFormat_Short
                        tags[i].tsstr = ParseShortTimeStamp(recvbuf, pos);
                        pos += 5;
 //                       Console.WriteLine("tsstr:" + tags[i].tsstr);
#else
                        tags[i].tsstr = Encoding.ASCII.GetString(recvbuf, pos, 17);
    //                    Debug.WriteLine("tsstr:" + tags[i].tsstr);
                        pos += 17;
#endif

#if SaveTagLog
                        taginfostr += tags[i].epcstr;
                        taginfostr += ",";
                        taginfostr += tags[i].ant.ToString();
                        taginfostr += ",";
                        taginfostr += tags[i].readcnt.ToString();
                        taginfostr += ",";
                        taginfostr += tags[i].rssi.ToString();
                        taginfostr += ",";
                        taginfostr += tags[i].potl.ToString();
                        taginfostr += ",";
                        taginfostr += tags[i].tsstr;
                        taginfostr += "|";
#endif
                    }

                    int readernamelen = recvbuf[pos++];
   //                 Debug.WriteLine("readernamelen:" + readernamelen.ToString());
   //                 Debug.WriteLine("readername:" +
    //                    Encoding.ASCII.GetString(recvbuf, pos, readernamelen));
                    pos += readernamelen;
#if Xdiot_App
                    string tailtm2 = ParseShortTimeStamp(recvbuf, pos);
 //                   Console.WriteLine("tag rttailtm:" + tailtm2);
#if SaveTagLog
                    msginfostr += " 消息时间戳：";
                    msginfostr += tailtm2;
                    smwter.WriteLine(msginfostr);
                    if (tagcnt > 0)
                        smwter.WriteLine(taginfostr);
#endif

#endif
                    if (msgtype == 1)
                        this.BeginInvoke(new UpdateUi(addrecord), new object[] { tags });
                    else if (msgtype == 2)
                        this.BeginInvoke(new UpdateUi(addrecord_offline), new object[] { tags });
#if Xdiot_App
                    else if (msgtype == 3)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 50 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord), new object[] { tags });
                    }
                    else if (msgtype == 4)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 50 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord_offline), new object[] { tags });
                    }
                    else if (msgtype == 5)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 60 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord), new object[] { tags });
                    }
                    else if (msgtype == 6)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 60 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord_offline), new object[] { tags });
                    }
#endif
 //                   this.BeginInvoke(new UpdateUiThreadCnt(updateThreadcnt), new object[] { nThreadRun });
                }
            ;
            }
            catch (System.Exception exx)
            {
                Debug.WriteLine("ParseTagData aaaaaaaaaaaaa exception:" + exx.ToString());
                if (client != null)
                    client.Close();
                if (stream != null)
                    stream.Close();
            }

            Debug.WriteLine("ParseTagData bbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
            if (client != null)
                client.Close();
            if (stream != null)
                stream.Close();

        }

        void ParseTagDataol(object state)
        {
            byte[] recvbuf = new byte[1500];
            TcpClient client = null;
            NetworkStream stream = null;
            UInt16 msgver = 0;
            byte msgtype = 0;
            UInt32 serialnum = 0;
            try
            {
                client = (TcpClient)state;               
                stream = client.GetStream();
                stream.ReadTimeout = 2000;
                
                while (true)
                {
                    int pos = 0;
                    if (!isRecvTagsol)
                        break;
                    client.Client.Blocking = true;
  //                  Console.WriteLine("before ol getmsg");
                    if (getmsg(stream, recvbuf, 9) < 0)
                    {
                     //   return;
                        
                        if (!isRecvTagsol)
                            break;
                        else
                        {
                            Debug.WriteLine("ParseTagDataol ffffffffffffffffffffffffffff");
                            continue;
                        }
                    }
//                    Console.WriteLine("after ol getmsg");

                    msgver = (UInt16)((recvbuf[pos] << 8) | recvbuf[1 + pos]);
                    pos += 2;
                    msgtype = recvbuf[pos++];

                    serialnum = (UInt32)((recvbuf[pos] << 24) | (recvbuf[pos + 1] << 16) |
                        (recvbuf[pos + 2] << 8) | recvbuf[pos + 3]);
                    pos += 4;
                    //                  Debug.WriteLine("serialnum:" + serialnum.ToString());
                    int datalen = (recvbuf[pos] << 8) | recvbuf[1 + pos];
                    pos += 2;
                    //                  Debug.WriteLine("datalen:" + datalen.ToString());
                    if (getmsg(stream, recvbuf, datalen) < 0)
                        return;
                    //
                    //                  for (int n = 0; n < datalen; ++n)
                    //                     Debug.Write(" " + recvbuf[n].ToString("X2"));
                    //                 Debug.WriteLine("");
                    //
                    pos = 0;
                    int tagcnt = recvbuf[pos++];
                    UploadTag[] tags = new UploadTag[tagcnt];
                    for (int i = 0; i < tagcnt; ++i)
                    {
                        tags[i] = new UploadTag();

                        int epclen = recvbuf[pos++];
                        //                     Debug.WriteLine("epclen:" + epclen.ToString());
                        byte[] epc = new byte[epclen];
                        Array.Copy(recvbuf, pos, epc, 0, epclen);
                        tags[i].epcstr = ByteFormat.ToHex(epc);
                        //                     Debug.WriteLine("epcstr:" + tags[i].epcstr);
                        pos += epclen;
                        int banklen = recvbuf[pos++];
                        //                     Debug.WriteLine("banklen:" + banklen.ToString());
                        if (banklen > 0)
                        {
                            byte[] bank = new byte[banklen];
                            Array.Copy(recvbuf, pos, bank, 0, banklen);
                            pos += banklen;
                            tags[i].bankstr = ByteFormat.ToHex(bank);
                            //                       Debug.WriteLine("bankstr:" + tags[i].bankstr);
                        }
                        tags[i].ant = recvbuf[pos++];
                        //                    Debug.WriteLine("ant:" + tags[i].ant.ToString());
                        tags[i].readcnt = recvbuf[pos++];
                        //                   Debug.WriteLine("readcnt:" + tags[i].readcnt.ToString());
                        tags[i].potl = recvbuf[pos++];
                        //                   Debug.WriteLine("potl:" + tags[i].potl.ToString());
                        tags[i].rssi = (sbyte)recvbuf[pos++];
                        //                     Debug.WriteLine("rssi:" + tags[i].rssi.ToString());
#if TimeStampFormat_Short
                        tags[i].tsstr = ParseShortTimeStamp(recvbuf, pos);
                        pos += 5;
                        //                       Console.WriteLine("tsstr:" + tags[i].tsstr);
#else
                        tags[i].tsstr = Encoding.ASCII.GetString(recvbuf, pos, 17);
    //                    Debug.WriteLine("tsstr:" + tags[i].tsstr);
                        pos += 17;
#endif

                    }

                    int readernamelen = recvbuf[pos++];
                    //                 Debug.WriteLine("readernamelen:" + readernamelen.ToString());
                    //                 Debug.WriteLine("readername:" +
                    //                    Encoding.ASCII.GetString(recvbuf, pos, readernamelen));
                    pos += readernamelen;
#if Xdiot_App
                    string tailtm = ParseShortTimeStamp(recvbuf, pos);
//                    Console.WriteLine("tag oltailtm:" + tailtm);
#endif
                    if (msgtype == 1)
                        this.BeginInvoke(new UpdateUi(addrecord), new object[] { tags });
                    else if (msgtype == 2)
                        this.BeginInvoke(new UpdateUi(addrecord_offline), new object[] { tags });
#if Xdiot_App
                    else if (msgtype == 3)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 50 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord), new object[] { tags });
                    }
                    else if (msgtype == 4)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 50 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord_offline), new object[] { tags });
                    }
                    else if (msgtype == 5)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 60 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord), new object[] { tags });
                    }
                    else if (msgtype == 6)
                    {
                        foreach (UploadTag tg in tags)
                        {
                            tg.ant = 60 + tg.ant;
                        }
                        this.BeginInvoke(new UpdateUi(addrecord_offline), new object[] { tags });
                    }
#endif
                    //                   this.BeginInvoke(new UpdateUiThreadCnt(updateThreadcnt), new object[] { nThreadRun });
                }
                ;
            }
            catch (System.Exception exx)
            {
                Debug.WriteLine("ParseTagDataol aaaaaaaaaa exception:" + exx.ToString());
                if (client != null)
                    client.Close();
                if (stream != null)
                    stream.Close();
            }

            Debug.WriteLine("ParseTagDataol bbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");
            if (client != null)
                client.Close();
            if (stream != null)
                stream.Close();

        }

        void FuncListen()
        {
            
            TcpClient client = null;

            try
            {
                tcpListener = new TcpListener(IPAddress.Any, localport);
                tcpListener.Start();
            }
            catch
            {
          //      MessageBox.Show("listen err");
                if (!isStopListen)
                    tcpListener.Stop();
                tcpListener = null;
                Debug.WriteLine("FuncListen cccccccccccccc");
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
                    Debug.WriteLine("FuncListen yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
           //         MessageBox.Show("accept err");
                    tcpListener.Stop();
                    tcpListener = null;
                    return;
                }
                Debug.WriteLine("ThreadPool.QueueUserWorkItem(ParseTagData, client)");
                ThreadPool.QueueUserWorkItem(ParseTagData, client);
            }
        }

        void FuncListenol()
        {

            TcpClient client = null;

            try
            {
                tcpListenerol = new TcpListener(IPAddress.Any, localportol);
                tcpListenerol.Start();
            }
            catch
            {
           //     MessageBox.Show("listen err");
                if (!isStopListenol)
                    tcpListenerol.Stop();
                tcpListenerol = null;
                Debug.WriteLine("FuncListenol cccccccccccccc");
                return;
            }
            while (isacceptol)
            {
                try
                {
                    client = tcpListenerol.AcceptTcpClient();
                }
                catch (SocketException exp)
                {
                    Debug.WriteLine("FuncListenol yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy");
          //          MessageBox.Show("accept err");
                    tcpListenerol.Stop();
                    tcpListenerol = null;
                    return;
                }
                Debug.WriteLine("ThreadPool.QueueUserWorkItem(ParseTagDataol, client)");
                ThreadPool.QueueUserWorkItem(ParseTagDataol, client);
            }
        }

        private void btnbindrecv_Click(object sender, EventArgs e)
        {
            if (this.tblocalport.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入实时数据绑定端口");
                return;
            }
            localport = int.Parse(this.tblocalport.Text.Trim());
            isaccept = true;
            isStopListen = false;
            isRecvTags = true;
            thlisten = new Thread(FuncListen);
            thlisten.Start();
            btnbindrecv.Enabled = false;
            btnstoprecv.Enabled = true;
        }

        private void btnstoprecv_Click(object sender, EventArgs e)
        {
            isStopListen = true;
            isRecvTags = false;
            tcpListener.Stop();
            thlisten.Join();
            btnbindrecv.Enabled = true;
            btnstoprecv.Enabled = false;
        }

#if SaveTagLog
        StreamWriter smwter = null;
        StreamWriter smrawwter = null;
#endif
        private void FrmLongTaskTagRecv_Load(object sender, EventArgs e)
        {
            btnbindrecv.Enabled = true;
            btnstoprecv.Enabled = false;

            btnbindrecvol.Enabled = true;
            btnstoprecvol.Enabled = false;
#if SaveTagLog
            FileInfo fileInfo = new FileInfo("tagslog.txt");
            smwter = fileInfo.CreateText();
            FileInfo fileInforaw = new FileInfo("rawlog.txt");
            smrawwter = fileInforaw.CreateText();
            smwter.WriteLine("标签表示格式：epc,ant,readcnt,rssi,potl,timestamp; 用|分隔多个标签数据");
#endif

            ThreadPool.SetMaxThreads(100, 50);
        }

        private void btnclearlv_Click(object sender, EventArgs e)
        {
            this.lvtags.Items.Clear();
        }

        private void btnclearlvol_Click(object sender, EventArgs e)
        {
            this.lvoltags.Items.Clear();
        }

        private void btnbindrecv0l_Click(object sender, EventArgs e)
        {
            if (this.tblocalportol.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请输入离线数据绑定端口");
                return;
            }
            localportol = int.Parse(this.tblocalportol.Text.Trim());
            isacceptol = true;
            isStopListenol = false;
            isRecvTagsol = true;
            thlistenol = new Thread(FuncListenol);
            thlistenol.Start();
            btnbindrecvol.Enabled = false;
            btnstoprecvol.Enabled = true;
        }

        private void btnstoprecvol_Click(object sender, EventArgs e)
        {
            isStopListenol = true;
            isRecvTagsol = false;
            tcpListenerol.Stop();
            thlistenol.Join();
            btnbindrecvol.Enabled = true;
            btnstoprecvol.Enabled = false;
        }

    }
}
