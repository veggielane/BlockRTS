using System;
using BlockRTS.Core.Messaging.Messages;

namespace BlockRTS.Core.Messaging
{
    public interface IMessageBus : IObservable<IMessage>
    {

        void Add(IMessage message);
        void SendAll();
    }
}