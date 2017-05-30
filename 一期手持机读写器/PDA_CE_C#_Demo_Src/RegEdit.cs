using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace PDADemo_CF2._0
{
    public  class RegEdit
    {
        public static string GetRegistData()
        {
            string registData;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey system = hkml.OpenSubKey("System", true);
            RegistryKey ccontrol = system.OpenSubKey("CurrentControlSet", true);
            RegistryKey control = ccontrol.OpenSubKey("Control", true);
            RegistryKey power = control.OpenSubKey("Power", true);
            RegistryKey timeout = power.OpenSubKey("Timeouts", true);
            registData = timeout.GetValue("BattsuspendTimeout").ToString();
            return registData;
        }
        public static void WTRegedit(string tovalue)
        {
            string registData;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey system = hkml.OpenSubKey("System", true);
            RegistryKey ccontrol = system.OpenSubKey("CurrentControlSet", true);
            RegistryKey control = ccontrol.OpenSubKey("Control", true);
            RegistryKey power = control.OpenSubKey("Power", true);
            RegistryKey timeout = power.OpenSubKey("Timeouts", true);
            timeout.SetValue("BattsuspendTimeout", tovalue);
        }

        public static bool IsRegeditExit(string name)
        {
            bool _exit = false,stl=false;
            string[] subkeyNames,sts;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            sts = software.GetSubKeyNames();
            foreach (string keyName in sts)
            {
               if (keyName=="STL")
               {
                   stl = true;
                   break;
               }
            }
            if (!stl)
            {
                return false;
            }
            RegistryKey aimdir = software.OpenSubKey("STL", true);
            subkeyNames = aimdir.GetSubKeyNames();
            foreach (string keyName in subkeyNames)
            {
                if (keyName == name)
                {
                    _exit = true;
                    return _exit;
                }
            }
            return _exit;
        }
        public static bool AddnewRegeditML(string name, string value)
        {
            try
            {
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
                RegistryKey aimdir = software.CreateSubKey("STL");
                RegistryKey strkey = aimdir.CreateSubKey(name);

                strkey.SetValue("count", value, RegistryValueKind.DWord);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;

        }
        public static string GETCOUNT()
        {
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("STL", true);
            return aimdir.SubKeyCount.ToString();
        }
        public static void DeleteRegist(string name)
        {
            string[] aimnames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("STL", true);
            aimnames = aimdir.GetSubKeyNames();
            foreach (string aimKey in aimnames)
            {
                if (aimKey == name)
                    aimdir.DeleteSubKeyTree(name);
            }
        }
        public static void DeleteRegist()
        {
            string[] aimnames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
            RegistryKey aimdir = software.OpenSubKey("STL", true);
            aimnames = aimdir.GetSubKeyNames();
            foreach (string aimKey in aimnames)
            {
                DeleteRegist(aimKey);
            }

        }
    }
}
