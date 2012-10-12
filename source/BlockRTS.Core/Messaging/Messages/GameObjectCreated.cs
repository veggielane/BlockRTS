using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Messaging.Messages
{

    public class GameObjectCreated : BaseMessage
    {
        public IGameObject GameObject { get; private set; }

        public GameObjectCreated(IGameTime timeSent, IGameObject gameObject)
            : base(timeSent)
        {
            GameObject = gameObject;
        }

        public override string ToString()
        {
            return "<{0}> {1} ({2})".Fmt(GetType().Name, GameObject,GameObject.Id);
        }
    }
}
