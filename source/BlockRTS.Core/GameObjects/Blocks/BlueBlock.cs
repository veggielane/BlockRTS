using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;

namespace BlockRTS.Core.GameObjects.Blocks
{
    public class BlueBlock:BaseBlock
    {
        public BlueBlock(IMessageBus bus, Vect3 position, Quat rotation)
            : base(bus, position,rotation, Color.FromArgb(255, 94, 169, 198))//5EA9C6
        {

        }
    }
}
