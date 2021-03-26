using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
	public interface IFileWriter
	{
		public void WriteFile(string path, string logmsg);
	}
}
