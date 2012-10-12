using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects
{
    public abstract class BaseGameObject:IGameObject
    {
        public Guid Id { get; private set; }
        public abstract Mat4 Transformation { get; set; }
        public abstract void Update(TickTime delta);

        public BaseGameObject(IMessageBus bus)
        {
            Id = Guid.NewGuid();
        }

        public IMessageBus Bus { get; private set; }
    }
}
