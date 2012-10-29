using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Shapes;

namespace BlockRTS.Core.Physics.BoundingBoxes
{
    public class SphereBoundingBox:Sphere,IBoundingBox
    {
        public SphereBoundingBox(Vect3 position, Quat rotation, double size) : base(position, rotation, size)
        {

        }

        public bool Contains(Vect3 pos)
        {
            return (pos - Position).Length < Radius;
        }
    }
}
