using System.Drawing;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects.Blocks
{
    public abstract class BaseBlock:BaseGameObject
    {
        public Color BlockColor { get; private set; }
        protected BaseBlock(IMessageBus bus,Mat4 transformation,Color blockColor)
            : base(bus, transformation)
        {
            BlockColor = blockColor;
        }

        public override void Update(TickTime delta)
        {

        }
    }
}
