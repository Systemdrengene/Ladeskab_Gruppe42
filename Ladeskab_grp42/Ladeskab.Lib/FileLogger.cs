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
            var path = "./log.txt";

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine("New log " + DateTime.Now + " : " + logmsg);
            }
        }
    }
}
