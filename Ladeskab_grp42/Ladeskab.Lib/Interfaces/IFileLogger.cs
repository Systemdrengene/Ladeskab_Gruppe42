using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ladeskab.Lib.Interfaces
{
	public interface IFileLogger
	{
		public void LogFile(string logmsg);
		public string ReadFile();
	}
}
