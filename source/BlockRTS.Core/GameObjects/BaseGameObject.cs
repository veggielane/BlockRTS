using System;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects
{
    public abstract class BaseGameObject : IGameObject
    {
        public Guid Id { get; private set; }
        public Vect3 Position { get; set; }
        public Quat Rotation { get; set; }
        public abstract void Update(TickTime delta);
        public IMessageBus Bus { get; private set; }
        protected BaseGameObject(IMessageBus bus, Vect3 position, Quat rotation)
        {
            Bus = bus;
            Position = position;
            Rotation = rotation;
            Id = Guid.NewGuid();
        }
    }
}
