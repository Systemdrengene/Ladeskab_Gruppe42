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
            using (StreamWriter sw = File.AppendText(path))
            {

                sw.WriteLine("New log " + DateTime.Now + " : " + logmsg);
            }
        }
    }
}
