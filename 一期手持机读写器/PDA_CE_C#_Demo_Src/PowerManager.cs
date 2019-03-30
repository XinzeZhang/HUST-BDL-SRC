/*   
 * 用途:电源管理类 
 * 
 * Date: 2007/08/28
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;

namespace PDADemo_CF2._0
{

    #region PowerNotify Event
    /// <summary>
    /// PowerNotifyEventArgs
    /// </summary>
    public class PowerNotifyEventArgs : EventArgs
    {
        private uint acLineStatus;

        private uint batteryLifePercent;

        public uint ACLineStatus
        {
            get { return acLineStatus; }
        }

        public uint BatteryLifePercent
        {
            get { return batteryLifePercent; }
        }

        public PowerNotifyEventArgs(uint acLineStatus, uint batteryLifePercent)
        {
            this.acLineStatus = acLineStatus;
            this.batteryLifePercent = batteryLifePercent;
        }
    }
    public class SleepEventArgs:EventArgs
    {
        public SleepEventArgs()
        {

        }
    }
    public class ResumeEventArgs:EventArgs
    {
        public ResumeEventArgs()
        {

        }
    }
    /// <summary>
    ///  PowerNotifyEvent Delegate
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PowerNotifyEventHandler(object sender, PowerNotifyEventArgs e);

    public delegate void SleepEventHandler(SleepEventArgs e);
    public delegate void ResumeEventHandler(ResumeEventArgs e);
    #endregion
    
    public class PowerManager
    {
        log dlog;
        public log Dlog
        {
            get { return dlog; }
            set
            {
                if (value != null)
                    dlog = value;
                else
                    dlog = null;
            }
        }
        #region Event
        public event PowerNotifyEventHandler PowerNotify = null;

        public event ResumeEventHandler ResumeNotify=null;
        public event SleepEventHandler SleepNotify = null;

        protected virtual void OnPowerNotify(PowerNotifyEventArgs e)
        {
            if (PowerNotify != null)
            {
                PowerNotify(this, e);
            }
        }

        
       
        #endregion

        #region Variable
        IntPtr hMsgQ;

        IntPtr[] hEvent = new IntPtr[4];
        #endregion
       
        #region Structure

        public PowerManager()
        {
            hEvent[0] = Win32.CreateEvent(IntPtr.Zero, false, false, null);
            Device.CreatePowerEvent(out hEvent[1], out hEvent[2]);
        }

        //~PowerManager()
        //{

         
        //}

        #endregion

        #region Method
        /// <summary>
        /// Start  PowerManagement Thread
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            Win32.MSGQUEUEOPTIONS options = new Win32.MSGQUEUEOPTIONS();
            options.dwSize = 20;
            options.dwFlags = 2;
            options.dwMaxMessages = 1;
            options.cbMaxMessage = 64;
            options.bReadAccess = true;

            hMsgQ = Win32.CreateMsgQueue(null, options);
            if (hMsgQ == IntPtr.Zero)
            {
                return false;
            }

            IntPtr hNotifi = Win32.RequestPowerNotifications(hMsgQ, Win32.PBT_POWERINFOCHANGE);
            if (hNotifi == IntPtr.Zero)
            {
                return false;
            }

            hEvent[3] = hMsgQ;

            
            Thread  t = new Thread(new ThreadStart(this.PowerThreadPorc));
            t.Start();
            //if (dlog != null)
            //    dlog.WirteLog("sleep name:"+t.Name+" "+t.ManagedThreadId);
            return true;
        }

        /// <summary>
        /// Stop PowerManagement Thread
        /// </summary>
        public void Stop()
        {
            Win32.EventModify(hEvent[0], Win32.EVENT_SET);
            Win32.StopPowerNotifications(hMsgQ);
            Win32.CloseMsgQueue(hMsgQ);
            Win32.CloseHandle(hEvent[0]);
            Win32.CloseHandle(hEvent[1]);
            Win32.CloseHandle(hEvent[2]);
            Win32.CloseHandle(hEvent[3]);
        }

        /// <summary>
        /// PowerThreadPorc
        /// </summary>
        private void PowerThreadPorc()
        {
           Device.SuspendHold(true);//阻止系统响应按钮

            while (true)
            {
                uint evt = Win32.WaitForMultipleObjects(4, hEvent, false, Win32.INFINITE);
               
                switch (evt)
                {
                    case 0://return thread
                        if (dlog != null)
                            dlog.WirteLog("evt0000000:" + evt.ToString()+"\n");
                        if (!Device.SuspendHold(false))
                            dlog.WirteLog("devce:false fail!!\n");
                        else
                            dlog.WirteLog("devce:false sucessful!!\n");
                        return;
                    case 1://disable network 
                        if (dlog != null)
                            dlog.WirteLog("evt111111:" + evt.ToString()+"\n");
                        SleepEventArgs sea2 = new SleepEventArgs();
                        SleepNotify.Invoke(sea2);
                        if (!Device.SuspendHold(false))
                            dlog.WirteLog("devce:false fail!!\n");
                        else
                            dlog.WirteLog("devce:false sucessful!!\n");
                        break;
                    case 2:
                        if (dlog != null)
                            dlog.WirteLog("evt2222222:" + evt.ToString()+"\n");
                        
                        
                        Win32.SYSTEM_POWER_STATUS_EX2 status = new Win32.SYSTEM_POWER_STATUS_EX2();
                        if (Win32.GetSystemPowerStatusEx2(status, (uint)Marshal.SizeOf(typeof(Win32.SYSTEM_POWER_STATUS_EX2)), true) > 0)
                        {
                            
                            if (status.ACLineStatus != 0x01 && status.BatteryLifePercent <= 7)
                            {
                                Win32.SetSystemPowerState(null, Win32.POWER_STATE_SUSPEND, Win32.POWER_FORCE);
                                break;
                            }
                        }
                        //Device.Com2PowerOn();
                        Thread.Sleep(300);
                        ResumeEventArgs rea = new ResumeEventArgs();
                        ResumeNotify.Invoke(rea);

                        //if (!Device.SuspendHold(true))
                        //    dlog.WirteLog("device:true fail!!!\n");
                        //else
                        //    dlog.WirteLog("device:true succeful!!\n");

                        break;
                    case 3:
                        if (dlog != null)
                            dlog.WirteLog("evt333333:" + evt.ToString() + "\n");
                        uint bytesRead;
                        uint flags;
                        Win32.POWER_BROADCAST pB = new Win32.POWER_BROADCAST();
                        if (Win32.ReadMsgQueue(hMsgQ, out pB, (uint)Marshal.SizeOf(typeof(Win32.POWER_BROADCAST)), out bytesRead, Win32.INFINITE, out flags))
                        {
                            if (pB.Message == Win32.PBT_POWERINFOCHANGE && pB.PI.bACLineStatus != 0x01 && pB.PI.bBatteryLifePercent <= 5)
                            {
                                Win32.SetSystemPowerState(null, Win32.POWER_STATE_SUSPEND, Win32.POWER_FORCE);
                                break;
                            }
                           // OnPowerNotify(new PowerNotifyEventArgs(pB.PI.bACLineStatus, pB.PI.bBatteryLifePercent));
                        }
                        break;
                    default:
                        if (dlog != null)
                            dlog.WirteLog("evt:" + evt.ToString());
                        break;
             
                }
                Thread.Sleep(100);
            }
        }

       
       
        #endregion
    }
}
