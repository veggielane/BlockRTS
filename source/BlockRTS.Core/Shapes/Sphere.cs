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

        public bool Intersects(Ray ray)
        {
            var dir = (ray.End - ray.Start).Normalize();
            var w = Position - ray.Start;
            var wsq = w.DotProduct(w);
            var proj = w.DotProduct(dir);
            var rsq = Radius*Radius;
            if (proj < 0.0f && wsq > rsq) return false;
            var vsq = dir.DotProduct(dir);
            return (vsq*wsq - proj*proj <= vsq*rsq);
        }
    }
}