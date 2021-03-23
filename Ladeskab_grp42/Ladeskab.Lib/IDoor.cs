using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    public interface IDoor
    {
        public void LockDoor();
        public void UnlockDoor();
        public void OnDoorClose();
        public void OnDoorOpen();
    }
}
