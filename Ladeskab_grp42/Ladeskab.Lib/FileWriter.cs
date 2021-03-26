using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ladeskab.Lib
{
    public class FileWriter
    {
        public void WriteFile(string path, string logmsg)
        {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine("New log " + DateTime.Now + " : " + logmsg);
            sw.Close();
        }
    }
}
