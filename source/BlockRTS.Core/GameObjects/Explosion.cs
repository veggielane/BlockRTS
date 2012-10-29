using System;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Physics;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects
{
    public class Explosion:BaseGameObject,IHasPhysicsEffect
    {
        private readonly ITimer _timer;
        public double Size { get; private set; }
        //public 

        public Explosion(ITimer timer, IMessageBus bus, Vect3 position, Quat rotation) : base(bus, position, rotation)
        {
            _timer = timer;
            Size = 1.0;
        }

        public override void Update(TickTime delta)
        {
            
        }
    }
}
