using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class Display : IDisplay
    {
        public void DisplayMessage(string msg)
        {
            Console.WriteLine("New Message: " + msg);
        }
    }
}
