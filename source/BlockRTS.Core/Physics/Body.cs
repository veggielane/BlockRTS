using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Physics
{
    public class Body:IBody
    {
        public double Mass { get; private set; }
        public Mat4 Transformation { get; private set; }
        public Vect3 Velocity { get; private set; }

        public Body(Mat4 transformation)
        {
            Transformation = transformation;

            var rnd = new Random();
            Velocity = new Vect3(rnd.NextDouble(-5, 5), rnd.NextDouble(-5, 5), rnd.NextDouble(-5, 5));// *Mat4.RotateX(Angle.FromDegrees(rnd.NextDouble(-0.1, 0.1)));
        }

 
        public void ApplyImpulse()
        {

        }

        public void Update(TickTime delta)
        {
            Transformation = (Transformation.ToVect3() + Velocity * delta.GameTimeDelta.TotalSeconds).ToMat4();
        }
    }
}
