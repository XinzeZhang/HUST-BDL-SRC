using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace PositionManage
{
    /// <summary>
    /// 异步Socket通信，实际用隐藏线程实现异步效果。
    /// 需提交回调函数。
    /// </summary>
    public class SocketSync
    {
        public event SocketCallBack ConnectSuccess;

        public event SocketCallBack DataReceive;

        private Thread _th = null;

        private volatile Socket soc = null;

        private bool _bIsConnect = false;

        private System.Net.IPAddress _ipAddr = null;

        private int _iPort = 0;

        public SocketSync(System.Net.IPAddress ipaddr, int iPort)
        {
            _ipAddr = ipaddr;
            _iPort = iPort;
        }

        public void Connect()
        {
            _th = new Thread(new ThreadStart(ConnectAsyn));
            _th.Start();
        }

        public void Close()
        {
            if (_th != null && _th.IsAlive)
            {
                _th.Abort();
            }

            if (soc != null)
            {
                soc.Disconnect(false);
                soc = null;
            }
        }

        private void ConnectAsyn()
        {
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                soc.Connect(_ipAddr, _iPort);
            }
            catch(Exception ex)
            {
                ConnectSuccess(false, ex);
                return;
            }
            ConnectSuccess(true, null);
        }


    }

    public delegate void SocketCallBack(bool IsSuccess, Exception ex);
}
