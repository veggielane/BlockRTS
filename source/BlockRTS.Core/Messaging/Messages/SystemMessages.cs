using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Messaging.Messages
{
    public class RequestCloseMessage : BaseMessage
    {
        public RequestCloseMessage(IGameTime timeSent) : base(timeSent)
        {
        }
    }
}
