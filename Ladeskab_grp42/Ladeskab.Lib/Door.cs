using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    class Door : IDoor
    {
        #region Variables
        bool doorOpen = false;
        #endregion

        #region Door interface
        public void LockDoor()
        {
            if(doorOpen)
            {
                doorOpen = false;
                Console.WriteLine("Door Locked");
            }
        }
        public void OpenDoor()
        {
            if (!doorOpen)
            {
                doorOpen = true;
                Console.WriteLine("Door Unlocked");
            }
        }
        #endregion

        #region Subject inheritance

        #endregion
    }
}
