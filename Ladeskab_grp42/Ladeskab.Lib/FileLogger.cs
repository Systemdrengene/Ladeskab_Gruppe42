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
        IFileWriter _filewriter;
        IFileReader _filereader;

        public string Path { get; set; }

        public FileLogger(IFileWriter filewriter, IFileReader filereader, string path)
        {
	        _filewriter = filewriter;
	        _filereader = filereader;

	        Path = path;
        }

        public void LogFile(string logmsg)
        {
	        _filewriter.WriteFile(Path, logmsg);
        }

		public string ReadFile()
		{
			string read = _filereader.ReadFile(Path);

			return read;
		}
	}
}
