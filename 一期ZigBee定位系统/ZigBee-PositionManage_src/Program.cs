using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PositionManage
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static string GetAppSettingValue(string AppName, string defaulvalue)
        {
            return System.Configuration.ConfigurationManager.AppSettings[AppName] == null ?
                defaulvalue : System.Configuration.ConfigurationManager.AppSettings[AppName].ToString();
        }

        public static void SetAppSettingValue(string AppName, string value)
        {
            if (value == GetAppSettingValue(AppName, ""))
            {
                return;
            }

            if (System.Configuration.ConfigurationManager.AppSettings[AppName] == null)
            {
                System.Configuration.ConfigurationManager.AppSettings.Add(AppName, value);
            }
            else
            {
                System.Configuration.ConfigurationManager.AppSettings.Set(AppName, value);
            }
        }
    }
}
