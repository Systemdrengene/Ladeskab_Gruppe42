﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladeskab.Lib
{
    interface IDoor
    {
        public void OnDoorOpen();
        public void OnDoorClose();
    }
}
