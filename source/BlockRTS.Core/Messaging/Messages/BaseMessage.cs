using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Messaging.Messages
{
    public abstract class BaseMessage : IMessage
    {
        public IGameTime TimeSent { get; private set; }
        public BaseMessage(IGameTime timeSent)
        {
            TimeSent = timeSent;
        }
        public override string ToString()
        {
            return "<{0}>".Fmt(GetType().Name);
        }
    }
}
