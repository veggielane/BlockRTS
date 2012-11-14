using System;
using System.Drawing;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Physics;
using BlockRTS.Core.Physics.Bodys;
using BlockRTS.Core.Shapes;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects.Blocks
{
    public abstract class BaseBlock:IGameObject,IHasPhysics,ICanBeSelected
    {
        public Color BlockColor { get; private set; }

        public Guid Id { get; private set; }

        public Vect3 Position { get { return Body.Position; } }
        public Quat Rotation { get { return Body.Rotation; } }

        //public Mat4 Transformation { get { return Body.Transformation; } }
        public IMessageBus Bus { get; private set; }

        protected BaseBlock(IMessageBus bus, Vect3 position, Quat rotation, Color blockColor)
        {
            Bus = bus;
            Id = Guid.NewGuid();
            BlockColor = blockColor;
            Body = new Body(position,rotation);
            BoundingSphere = new Sphere(Body.Position, Quat.Identity, 10);
        }

        public void Update(TickTime delta)
        {

        }

        public IBody Body { get; private set; }
        public Mat4 Velocity { get; private set; }

        public Sphere BoundingSphere { get; private set; }
    }
}
