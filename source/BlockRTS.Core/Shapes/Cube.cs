using System.Drawing;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Shapes
{
    public class Cube:IShape
    {
        public double Size { get; set; }
        public Vect3 Position { get; set; }
        public Quat Rotation { get; set; }
        public Cube()
        {
            Size = 1.0;
            Position = Vect3.Zero;
            Rotation = Quat.Identity;
        }
    }
}
