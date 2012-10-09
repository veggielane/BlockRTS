using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Messaging.Messages;
using BlockRTS.Core.Reactive;

namespace BlockRTS.Core.Messaging
{
    public class MessageBus : ConcurrentObservable<IMessage>, IMessageBus
    {
        ConcurrentQueue<IMessage> PendingMessages;
        //private IBuffer<IMessage> PendingMessages { get;  set; }
        public MessageBus()
        {
            //PendingMessages = new Buffer<IMessage>();

            PendingMessages = new ConcurrentQueue<IMessage>();
        }

        public void Add(IMessage message)
        {
            //PendingMessages.Add(message);
            PendingMessages.Enqueue(message);
        }

        public void SendAll()
        {
            for (int i = 0; i < PendingMessages.Count; i++)
            {
                IMessage message;
                PendingMessages.TryDequeue(out message);
                OnNext(message);
            }
        }

        public override void Dispose()
        {
            PendingMessages = null;
            base.Dispose();
        }
    }
}
