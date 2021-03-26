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

        //Brug for fake FileLogger - til test, da substitute ikke kan med parameter?

        public FileLogger()
        {
	        _filereader = new();
	        _filewriter = new();
        }

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

            string read = _filereader.ReadFile(path);
            

            return read;
        }
    }
}
