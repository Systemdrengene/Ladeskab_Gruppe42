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
        private bool doorOpen = false;
        #endregion

        #region Door interface
        public bool LockDoor()
        {
            if(doorUnlocked)  //Muligvis også tilføj om Door er open, så vi kan have flere test og mere validation
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

        public bool OnDoorOpen()
        {
	        if (!doorOpen)
	        {
		        doorOpen = true;
				 Notify("Door opened");
	        }

	        return doorOpen;

        }

        public bool OnDoorClose()
        {
	        if (doorOpen)
	        {
		        doorOpen = false;
		        Notify("Door closed");
            }

	        return doorOpen;
        }

        #endregion

        #region Subject inheritance

        #endregion
    }
}
