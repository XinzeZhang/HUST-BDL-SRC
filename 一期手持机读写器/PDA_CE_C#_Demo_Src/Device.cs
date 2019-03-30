using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PDADemo_CF2._0
{
    /// <summary>
    /// this class is designed to facilitate i60 application software development. 
    /// </summary>
    public class Device
    {
        /// <summary>
        /// This function is used to power on the gsm module
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableGsmModule();


        /// <summary>
        /// This function is used to power off the gsm module
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DisableGsmModule();


        /// <summary>
        /// This function is used to get the power status of the gsm mudule
        /// </summary>
        /// <returns>true when gsm module is power on, false when power off</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetGsmPowerStatus();


        /// <summary>
        /// This function is used to request that the gsm module return the received signal strength indication (rssi)
        /// </summary>
        /// <returns>the rssi value<br/>
        /// 0 -113 dBm or less<br/>
        /// 1 -111 dBm<br/>
        /// 2..30 -109... -53 dBm<br/>
        /// 31 -51 dBm or greater<br/>
        /// 99 not known or not detectable</returns>
        [DllImport("Device.dll")]
        public static extern int GetGsmSignalStrength();


        /// <summary>
        /// This function is used to power on the wlan module
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWlanModule();


        /// <summary>
        /// This function is used to power off the wlan module
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DisableWlanModule();


        /// <summary>
        /// This function is used to get the power status of the wlan module
        /// </summary>
        /// <returns>true when wlan module is power on, false when power off</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWlanPowerStatus();


        /// <summary>
        /// This function is used to request that the wlan driver return the received signal strength indication (RSSI).
        /// </summary>
        /// <returns>the RSSI value. the normal values for the RSSI value are between -10 and -200</returns>
        [DllImport("Device.dll")]
        public static extern int GetWlanSignalStrength();


        /// <summary>
        /// This function is used to Check that the device is connected to the gateway
        /// </summary>
        /// <returns>true when the device has connected to the gateway, false when not connected</returns>
        /// <remarks>this function does not distinguish between network type(such as gprs, wireless lan, usb)</remarks>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CheckNetworkStat();


        /// <summary>
        /// This function is used to establishes a gprs connection
        /// </summary>
        /// <param name="connName">a ras phone book entry name</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ConnectGprs([MarshalAs(UnmanagedType.LPWStr)]string connName);


        /// <summary>
        /// This function is used to establishes a gprs connection
        /// </summary>
        /// <param name="connName">a ras phone book entry name</param>
        /// <param name="errorCode">the Zero indicates success. A nonzero error value, either from the set listed in the RAS header file or ERROR_NOT_ENOUGH_MEMORY, indicates failure.</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ConnectGprsEx([MarshalAs(UnmanagedType.LPWStr)]string connName, ref uint errorCode);


        /// <summary>
        /// This function is used to check the gprs connection status
        /// </summary>
        /// <param name="connName">a ras phone book entry name</param>
        /// <returns>true when active, false when disactive</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetGprsStatus([MarshalAs(UnmanagedType.LPWStr)]string connName);


        /// <summary>
        /// This function is used to terminate the gprs connection
        /// </summary>
        /// <param name="connName">a ras phone book entry name</param>
        [DllImport("Device.dll")]
        public static extern void DisConnectGprs([MarshalAs(UnmanagedType.LPWStr)]string connName);


        /// <summary>
        /// This function is used to force the wifi module reconnect to the ap
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool RebindWlanAdapter();


        /// <summary>
        /// This function is used to set the backlight value
        /// </summary>
        /// <param name="level">the backlight value ,range is 1~99</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetBackLightLevel(int level);


        /// <summary>
        /// This function is used to set the idle timer with the specified time-out value. The backlight will be off after a specified number of seconds has elapsed. the system default value is infinite.
        /// </summary>
        /// <param name="nBatteryTimeout">timeout value when use battery</param>
        /// <param name="nACTimeout">timeout value when use AC</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetBackLightTimeout(int nBatteryTimeout, int nACTimeout);


        /// <summary>
        /// This function is used to  Show or hide the cursor
        /// </summary>
        /// <param name="isEnable">true indicates enable, false indicates disable</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableCursor([MarshalAs(UnmanagedType.Bool)]bool isEnable);


        /// <summary>
        /// This function is used to enable or disable the system sounds(such as keyboard sounds, touch screen sounds)
        /// </summary>
        /// <param name="isEnable">true indicates enable, false indicates disable</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableSystemSound([MarshalAs(UnmanagedType.Bool)]bool isEnable);


        /// <summary>
        /// This function is used to create the device power management events. When user press the power key to suspend or resume the device, these two events will be signaled.
        /// </summary>
        /// <param name="suspendEvent"> suspend event</param>
        /// <param name="resumeEvent">resume event</param>
        /// <returns>true indicates success, false indicates failure</returns>
        /// <remarks>The developer must call the CloseHandle function to close these two handles after use these</remarks>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CreatePowerEvent(out IntPtr suspendEvent, out IntPtr resumeEvent);


        /// <summary>
        /// This function is used to prevent the system to respond to power button
        /// </summary>
        /// <param name="isHold">true to prevent the system to respond to power button, false to not</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SuspendHold([MarshalAs(UnmanagedType.Bool)]bool isHold);


        /// <summary>
        /// This function is used to displays bitmaps that have transparent or semitransparent pixels
        /// </summary>
        /// <param name="dcDest">handle to destination DC</param>
        /// <param name="x">x -coord of upper-left corner</param>
        /// <param name="y">y-coord of upper-left corner</param>
        /// <param name="cx">destination width</param>
        /// <param name="cy">destination height</param>
        /// <param name="dcSrc">handle to source DC</param>
        /// <param name="sx">x-coord of upper-left corner</param>
        /// <param name="sy">y-coord of upper-left corner</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AlphaBlend(IntPtr dcDest, int x, int y, int cx, int cy, IntPtr dcSrc, int sx, int sy);


        /// <summary>
        /// This function is used to get the id of the device
        /// </summary>
        /// <param name="deviceId">the buffer to store the device id</param>
        /// <param name="length">the buffer length</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDeviceID(StringBuilder deviceId, uint length);


        /// <summary>
        /// This function is used to enable the screen auto-lock.  After call this method, the device will auto lockscreen every dwInterval if there is not user inputs. user can unlock by Func+5
        /// </summary>
        /// <param name="idleTime"> inteval time, in seconds</param>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool StartScreenLock(uint idleTime);   //in seconds


        /// <summary>
        /// This function is used to  Reset the screen lock timer.
        /// </summary>
        [DllImport("Device.dll")]
        public static extern void ScreenLockTimerReset();


        /// <summary>
        /// This function is used to disable the auto-screenlock
        /// </summary>
        [DllImport("Device.dll")]
        public static extern void StopScreenLock();

        /// <summary>
        /// This function is used to power on the com2
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Com2PowerOn();


        /// <summary>
        /// This function is used to Power off the com2
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool Com2PowerOff();


        /// <summary>
        /// This function is used to  Power On the bluetooth module(com3)
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BthPowerOn();


        /// <summary>
        /// This function is used to Power off the bluetooth module(com3)
        /// </summary>
        /// <returns>true indicates success, false indicates failure</returns>
        [DllImport("Device.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BthPowerOff();
    }
}
