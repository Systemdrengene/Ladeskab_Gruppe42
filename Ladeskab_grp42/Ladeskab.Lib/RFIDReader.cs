using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class RFIDReader : Subject, IRFIDReader
    {
        private int id = -1;
        public int Id
        {
            get { return GetID(); }
            set { id = value; }
        }

        public void OnRfidRead(int id)
        {
            Id = id;
            Notify("RFID");
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
