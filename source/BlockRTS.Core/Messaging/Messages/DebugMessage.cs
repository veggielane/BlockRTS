using System;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Messaging.Messages
{
    public class DebugMessage : BaseMessage
    {
        public String Message { get; private set; }

        public DebugMessage(IGameTime timeSent, String message)
            : base(timeSent)
        {
            Message = message;
        }

        public override string ToString()
        {
            return "<{0}> {1}".Fmt(GetType().Name, Message);
        }
    }
}
