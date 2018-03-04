using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Hit.Common;
using Hit.RFID;

namespace DesktopUHF
{
    public partial class Form1 : Form
    {

        private static ReaderHandle _reader;
        private static bool _isConnected = false;
        private static string _comPort;

        public static ReaderHandle Reader
        {
            set { _reader = value; }
            get { return _reader; }
        }

        public static bool IsConnected
        {
            set { _isConnected = value; }
            get { return _isConnected; }
        }

        public static string ComPort
        {
            set { _comPort = value; }
            get { return _comPort; }
        }

        public Form1()
        {
            InitializeComponent();


        }


        // 连接读写器
        private void Connect()
        {
            if (_reader == null)
            {
                _reader = new ReaderHandle();

                _isConnected = _reader.Connect();
            }

            if (!_isConnected)
            {
                MessageBox.Show("读写器连接失败");
            }
            else
            {
                MessageBox.Show("读写器连接成功");
            }

        }

        // 关闭读写器
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (_reader != null)
            {
                _reader.DisConnect();
                _isConnected = false;
                _reader = null;
            }
        }

        // 读取epc编号
        private void btnReadEpc_Click(object sender, EventArgs e)
        {
           txbEpc.Text = _reader.ReadEpc();
        }

        // 写入EPC编号
        private void btnWriteEpc_Click(object sender, EventArgs e)
        {
            bool writeStatus = _reader.WriteEpc(txbEpc.Text.Trim());
            if(writeStatus)
            {
                MessageBox.Show("EPC写入成功");
            }

        }

        //读TID
        private void btnReadTID_Click(object sender, EventArgs e)
        {
            txbTID.Text = _reader.ReadTID();
        }

        // 写用户区
        private void btnWriteUser_Click(object sender, EventArgs e)
        {
            string hexecp = _reader.ReadEpc();

            int writeNum = 0;
            try
            {
                writeNum = Int32.Parse(txbStart.Text);
            }
            catch
            {
                MessageBox.Show("起始地址不正确");
            }


            if (_reader.WriteUserData(hexecp, writeNum, rtbUser.Text))
            {
                MessageBox.Show("用户区写入成功");
            }
            else
            {
                MessageBox.Show("用户区写入失败");
            }
        }

        // 读用户区
        private void btnReadUser_Click(object sender, EventArgs e)
        {

            try
            {
                Int32.Parse(txbStart.Text);
            }
            catch
            {
                MessageBox.Show("起始地址不正确");
            }

            try
            {
                Int32.Parse(txbNum.Text);
            }
            catch
            {
                MessageBox.Show("字数不正确");
            }

            string hexecp = _reader.ReadEpc();
            this.rtbUser.Text = _reader.ReadUserData(hexecp, txbStart.Text, txbNum.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _reader = null;
            txbEpc.Text = "";
            Connect();
        }


    }
}
