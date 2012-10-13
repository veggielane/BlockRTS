using System;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects
{
    public abstract class BaseGameObject:IGameObject
    {
        public Guid Id { get; private set; }
        public Mat4 Transformation { get; set; }
        public abstract void Update(TickTime delta);
        public IMessageBus Bus { get; private set; }
        protected BaseGameObject(IMessageBus bus, Mat4 transformation)
        {
            Bus = bus;
            Id = Guid.NewGuid();
            Transformation = transformation;
        }
    }
}
