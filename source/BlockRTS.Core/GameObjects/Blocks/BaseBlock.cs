using System;
using System.Drawing;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Physics;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects.Blocks
{
    public abstract class BaseBlock:IGameObject,IHasPhysics 
    {
        public Color BlockColor { get; private set; }

        public Guid Id { get; private set; }
        public Mat4 Transformation { get { return Body.Transformation; } }
        public IMessageBus Bus { get; private set; }

        protected BaseBlock(IMessageBus bus,Mat4 transformation,Color blockColor)
        {
            Bus = bus;
            Id = Guid.NewGuid();
            BlockColor = blockColor;
            Body = new Body(transformation);
        }

        public void Update(TickTime delta)
        {

        }

        public IBody Body { get; private set; }
        public Mat4 Velocity { get; private set; }
    }
}
