using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
	public class RfidEventArgs : EventArgs
	{
		//Rfid id tag
		public int Id { get; set; }
	}
	public interface IRFIDReader
	{
		event EventHandler<RfidEventArgs> RfidEvent;
	}
}
