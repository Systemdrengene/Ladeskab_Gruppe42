using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{

	public class DoorEventArgs : EventArgs
	{
        //Door state anvendes i Station control
        public bool DoorState { set; get; }
	}

    public interface IDoor
    {
	    event EventHandler<DoorEventArgs> DoorEvent;

	    public bool LockDoor();
        public bool UnlockDoor();
        public bool OnDoorClose();
        public bool OnDoorOpen();
    }
}
