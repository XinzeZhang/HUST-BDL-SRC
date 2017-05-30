using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Reflection;

namespace PDADemo_CF2._0
{
   public class log
    {

        private bool isexist;
        public bool IsExist
        {
            get { return isexist; }
            set { isexist = value; }
        }
        //private string logfilename;
        private FileStream filestream;
        public log()
        {

        }
        public bool CreatLogFile(string filename)
        {
            if (filename == string.Empty)
            {
                filename = "log.txt";
            }
          //  string currentpath = Directory.GetCurrentDirectory();
            string currentpath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
            string file = currentpath + "//" + filename;
            try
            {
                filestream = File.Open(file, FileMode.Create,FileAccess.ReadWrite);
                IsExist = true;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                IsExist = false;
                return false;
            }
            return true;


        }
        public bool WirteLog(string writestring)
        {
            if (!IsExist)
                return false;
            byte[] bytes = new UTF8Encoding(true).GetBytes(writestring);
            try
            {
                filestream.Write(bytes, 0, bytes.Length);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public void CloseLogFile()
        {
            if (filestream != null)
                filestream.Close();

        }
    }
    
}
