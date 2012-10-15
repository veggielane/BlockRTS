using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Physics
{
    public interface IHasPhysics
    {
        IBody Body { get; }

    }
}
