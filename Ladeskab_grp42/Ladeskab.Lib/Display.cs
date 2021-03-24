using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class Display : IDisplay
    {
        private string _stationMsg = "";
        private string _chargeMsg = "";
        public string Separator { get; private set; }

        public Display()
        {
            Separator = "============";
        }

        public void UpdateUserMsg(string m)
        {
            _stationMsg = m;
            DisplayMessage();
        }

        public void UpdateChargeMsg(string m)
        {
            _chargeMsg = m;
            DisplayMessage();
        }

        public void DisplayMessage()
        {
            Console.WriteLine("============");
            Console.WriteLine(_stationMsg);
            Console.WriteLine(_chargeMsg);
            Console.WriteLine("============");
        }
    }
}
