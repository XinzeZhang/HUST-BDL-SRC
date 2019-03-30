using System;
//using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace PDADemo_CF2._0
{
    public class Battery
    {
        [DllImport("coredll.dll")]
        public static extern uint GetSystemPowerStatusEx2(ref SYSTEM_POWER_STATUS_EX2 pSystemPowerStatusEx2, int dwLen, int fUpdate);
        public struct SYSTEM_POWER_STATUS_EX2
        {  //c# Windows CE读取电池电量的实现 
            public byte ACLineStatus;
            public byte BatteryFlag;
            public byte BatteryLifePercent;
            public byte Reserved1;
            public uint BatteryLifeTime;
            public uint BatteryFullLifeTime;
            public byte Reserved2;
            public byte BackupBatteryFlag;
            public byte BackupBatteryLifePercent;
            public byte Reserved3;
            public uint BackupBatteryLifeTime;
            public uint BackupBatteryFullLifeTime;
            public uint BatteryVoltage;
            public uint BatteryCurrent;
            public uint BatteryAverageCurrent;
            public uint BatteryAverageInterval;
            public uint BatterymAHourConsumed;
            public uint BatteryTemperature;
            public uint BackupBatteryVoltage;
            public byte BatteryChemistry;
        }
        static public int GetBatteryLifePercent()
        {
            SYSTEM_POWER_STATUS_EX2 status = new SYSTEM_POWER_STATUS_EX2();
            GetSystemPowerStatusEx2(ref status, System.Runtime.InteropServices.Marshal.SizeOf(status), 1);
            if (status.ACLineStatus == 0)
            {
                return status.BatteryLifePercent;
            }
            else
                return -1;
        }
    }
}
