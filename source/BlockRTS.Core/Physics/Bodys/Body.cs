using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Physics.Bodys
{
    public class Body:IBody
    {
        public double Mass { get { return 1.0; } }
        public BodyState State { get; private set; }
        public Vect3 Position { get; set; }
        public Quat Rotation { get; set; }

        public Vect3 Velocity { get; private set; }
        public Quat AngularVelocity { get; private set; }

        public Body(Vect3 position, Quat rotation)
        {
            Position = position;
            Rotation = rotation;
            Velocity = new Vect3(1,1,1);//Vect3.Zero);
            AngularVelocity = Quat.Identity;
            State = BodyState.Moving;
        }

        public void Update(TickTime delta)
        {
            if (State != BodyState.Moving) return;
            Position = Position + (Velocity * delta.GameTimeDelta.TotalSeconds);
            Rotation *= (AngularVelocity*delta.GameTimeDelta.TotalSeconds);
        }
    }
}
