using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public class Door : IDoor
    {
        #region Variables

        public EventHandler<DoorEventArgs> DoorStateEvent;

        private DoorEventArgs State = new DoorEventArgs
        {
	        DoorState = false
        };


        private bool _doorUnlocked = true;
        private bool _doorOpen = false;
        #endregion

        #region Door interface

        public event EventHandler<DoorEventArgs> DoorEvent;

        public bool LockDoor()
        {
	        // Locked or open = Kan ikke Lock
	        if (!_doorUnlocked || _doorOpen) return _doorUnlocked;  // return false;

	        _doorUnlocked = false; 
            Console.WriteLine("Door Locked");
            return _doorUnlocked;

        }
        public bool UnlockDoor()
        {
            // Unlocked = kan ikke unlock
	        if (_doorUnlocked) return _doorUnlocked;
            
	        _doorUnlocked = true;
            Console.WriteLine("Door Unlocked");
            return _doorUnlocked;
        }

        public bool OnDoorOpen() // Åben door
        {
            // Already Open or locked = kan ikke doorOpen
            if (_doorOpen || !_doorUnlocked) return _doorOpen;
            _doorOpen = true;
            State.DoorState = true;
			//Event til Station control
            DoorEvent?.Invoke(this,State);  
            return _doorOpen;
        }

        public bool OnDoorClose()  // Close door
        {
            // Door closed or locked = kan ikke close  
            if (!_doorOpen || !_doorUnlocked) return _doorOpen;
            _doorOpen = false;
            State.DoorState = false;
            DoorEvent?.Invoke(this,State);  
	        return _doorOpen;
        }

        #endregion

        #region Subject inheritance

        #endregion
    }
}
