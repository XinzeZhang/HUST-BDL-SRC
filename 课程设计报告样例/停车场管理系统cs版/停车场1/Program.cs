using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CarManager
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
            Login formin = new Login();
           // Application.Run(formin);
            formin.ShowDialog();

            if (formin.openport)
            {
                formin.Close();
                Fportstate fpoert = new Fportstate();
                fpoert.label6.Text = formin.mcla;
                Application.Run(fpoert);
                //fpoert.ShowDialog();
               
            }
        }
    }
}