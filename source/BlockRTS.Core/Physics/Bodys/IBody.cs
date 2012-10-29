using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Physics.Bodys
{
    public interface IBody
    {
        double Mass { get; }
        BodyState State { get; }

        Vect3 Position { get; set; }
        Quat Rotation { get; set; }
        Vect3 Velocity { get; }
        Quat AngularVelocity { get; }

        //void ApplyImpulse(Vect3 impulse);

        void Update(TickTime delta);
    }
}