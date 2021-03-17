using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class RFIDReader : Subject, IRFIDReader
    {
        Random rnd = new Random();
        public int OnRfidRead(int id)
        {



            Console.WriteLine("Read ID: " + id);
            return rnd.Next(50);
        }
    }
}
