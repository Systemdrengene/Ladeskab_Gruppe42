using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{


    public interface IDoor
    {

	    public bool LockDoor();
        public bool UnlockDoor();
        public bool OnDoorClose();
        public bool OnDoorOpen();
    }
}
