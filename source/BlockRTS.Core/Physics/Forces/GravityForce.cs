using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Physics.Bodys;
using BlockRTS.Core.Physics.BoundingBoxes;

namespace BlockRTS.Core.Physics.Forces
{
    public class GravityForce:IForce
    {
        private readonly IBody _b1;

        private IBoundingBox _boundingBox;

        private const double G = 6.67384E-11;

        public GravityForce(IBody b1)
        {
            _b1 = b1;
        }

        public Vect3 CalculateForce(IBody b2)
        {
            var unit = (b2.Position - _b1.Position)/(b2.Position - _b1.Position).Length;
            return -G*((_b1.Mass*b2.Mass)/(b2.Position - _b1.Position).LengthSquared)*unit;
        }
    }
}
