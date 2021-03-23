using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class Door : Subject, IDoor
    {
        #region Variables

        private bool doorUnlocked = false;

        #endregion

        #region Door interface
        public bool LockDoor()
        {
            if(doorUnlocked)
            {
                doorUnlocked = false;
                Console.WriteLine("Door Locked");
                
            }

            return doorUnlocked;

        }
        public bool UnlockDoor()
        {
            if (!doorUnlocked)
            {
                doorUnlocked = true;
                Console.WriteLine("Door Unlocked");
            }

            return doorUnlocked;
        }

        public void OnDoorOpen()
        {
            Notify("Door opened");
        }

        public void OnDoorClose()
        {
            Notify("Door closed");
        }

        #endregion

        #region Subject inheritance

        #endregion
    }
}
