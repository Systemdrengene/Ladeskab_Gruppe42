using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ladeskab.Lib
{
    public class FileLogger
    {
        public void LogFile(string logmsg)
        {
            string path = "C:/Users/1chri/Documents/GitHub/Ladeskab_Gruppe42/Ladeskab_grp42";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("New log " + DateTime.Now + " : " + logmsg);
            }
        }
    }
}
