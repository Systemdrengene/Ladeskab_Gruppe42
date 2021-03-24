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
	        // Locked or open = Kan ikke Lock
	        if (!doorUnlocked || doorOpen) return doorUnlocked;  // return false;

	        doorUnlocked = false; 
            Console.WriteLine("Door Locked");
            return doorUnlocked;

        }
        public bool UnlockDoor()
        {
            // Unlocked = kan ikke unlock
	        if (doorUnlocked) return doorUnlocked;
            
	        doorUnlocked = true;
            Console.WriteLine("Door Unlocked");
            return doorUnlocked;
        }

        public bool OnDoorOpen()
        {
            // Already Open or locked = kan ikke doorOpen
            if (doorOpen || !doorUnlocked) return doorOpen;
	        
	        doorOpen = true;
			Notify("Door opened");
			return doorOpen;
        }

        public bool OnDoorClose()
        {
            // Door closed or locked = kan ikke close  
            if (!doorOpen ) return doorOpen;
	        
	        doorOpen = false;
	        Notify("Door closed");
	        return doorOpen;
        }

        #endregion

        #region Subject inheritance

        #endregion
    }
}
