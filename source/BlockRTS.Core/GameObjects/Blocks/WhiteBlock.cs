﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;

namespace BlockRTS.Core.GameObjects.Blocks
{

    public class WhiteBlock : BaseBlock
    {
        public WhiteBlock(IMessageBus bus, Vect3 position, Quat rotation)
            : base(bus, position, rotation, Color.WhiteSmoke)
        {

        }
    }
}
