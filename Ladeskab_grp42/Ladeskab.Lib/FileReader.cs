using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ladeskab.Lib
{
    public class FileReader: IFileReader
    {
        public string ReadFile(string path)
        {
            var output = File.ReadLines(path).Last();
            return output;
        }
    }
}
