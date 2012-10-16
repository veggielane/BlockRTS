using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Physics
{
    public interface IBody
    {
        double Mass { get; }

        Vect3 Position { get; set; }
        Quat Rotation { get; set; }

        Vect3 Velocity { get; }

        void ApplyImpulse();

        void Update(TickTime delta);
    }
}