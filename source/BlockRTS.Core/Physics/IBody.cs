using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Physics
{
    public interface IBody
    {
        double Mass { get; }

        Mat4 Transformation { get; }
        Vect3 Velocity { get; }

        void ApplyImpulse();

        void Update(TickTime delta);
    }
}