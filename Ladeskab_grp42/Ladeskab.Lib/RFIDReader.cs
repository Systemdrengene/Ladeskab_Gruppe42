using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class RFIDReader :  IRFIDReader
    {
	    public event EventHandler<RfidEventArgs> RfidEvent;

        public void ScanRFfidTag(int ID)
        {
	        if (ID > 0)
	        {
				RfidEvent?.Invoke(this,new RfidEventArgs{Id= ID});
	        }
	        else
	        {
		        Console.WriteLine("Error RFID");
	        }

        }
    }
}
