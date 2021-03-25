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
        FileWriter _filewriter;
        FileReader _filereader;

        public FileLogger(FileWriter filewriter, FileReader filereader)
        {
            _filewriter = filewriter;
            _filereader = filereader;
        }

        public void LogFile(string logmsg)
        {
            var path = "./log.txt";

            _filewriter.WriteFile(path, logmsg);
        }

        public string ReadFile()
        {
            string path = "./log.txt";

            return _filereader.ReadFile(path);
        }
    }
}
