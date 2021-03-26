using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
	public interface IFileReader
	{
		public string ReadFile(string path);
	}
}
