using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    class RFIDReader : Subject, IRFIDReader
    {
        Random rnd = new Random();
        public int GetID()
        {
            return rnd.Next(50);
        }
    }
}
