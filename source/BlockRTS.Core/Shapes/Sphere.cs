using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Shapes
{
    public class Sphere : IShape
    {
        public double Radius { get; set; }
        public Vect3 Position { get; set; }
        public Quat Rotation { get; set; }
        public Sphere(Vect3 position, Quat rotation, double radius)
        {
            Position = position;
            Rotation = rotation;
            Radius = radius;
        }
    }
}