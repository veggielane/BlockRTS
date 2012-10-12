using System;
using System.Collections.Concurrent;
using BlockRTS.Core.Messaging;

namespace BlockRTS.Core.GameObjects
{
    public interface IGameObjectFactory : IHasMessageBus
    {
        ConcurrentDictionary<Guid, IGameObject> GameObjects { get; }
    }
}
