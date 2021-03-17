using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class RFIDReader : Subject, IRFIDReader
    {
        int id = -1;
        public void OnRfidRead(int id)
        {
            Console.WriteLine("Read ID: " + id);
            this.id = id;
        }
        public int GetID()
        {
            if (id != -1)
            {
                return id;
            }
            throw new Exception("RFID hasn't been read!");
        }
    }
}
