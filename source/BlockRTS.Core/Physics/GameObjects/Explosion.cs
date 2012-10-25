using System;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Physics.GameObjects
{
    public class Explosion:IGameObject
    {
        public IMessageBus Bus { get; private set; }

        public Vect3 Position { get; private set; }
        public Quat Rotation { get; private set; }

        public Guid Id { get; private set; }
        public void Update(TickTime delta)
        {
            throw new NotImplementedException();
        }
    }
}
