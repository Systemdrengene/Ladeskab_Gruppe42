using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public interface IDisplay
    {
        public void UpdateUserMsg(string m);
        public void UpdateChargeMsg(string m);
        public void DisplayMessage();
    }
}
