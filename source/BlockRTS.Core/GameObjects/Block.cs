using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects
{
    public class Block:BaseGameObject
    {
        public override Mat4 Transformation { get; set; }

        public Block(IMessageBus bus) : base(bus)
        {
            Transformation = Mat4.Identity;
        }

        public override void Update(TickTime delta)
        {

        }
    }
}
