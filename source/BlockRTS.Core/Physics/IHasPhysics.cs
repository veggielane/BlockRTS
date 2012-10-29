﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Physics.Bodys;

namespace BlockRTS.Core.Physics
{

    public interface IHasPhysics
    {
        IBody Body { get; }

    }
}
