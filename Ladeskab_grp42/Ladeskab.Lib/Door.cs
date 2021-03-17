using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    class Door : Subject, IDoor
    {
        #region Door interface
        public void LockDoor()
        {
            Console.WriteLine("Door Locked");
        }
        public void OpenDoor()
        {
            Console.WriteLine("Door Unlocked");
        }
        #endregion
    }
}
