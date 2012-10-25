using System;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects
{
    public interface IGameObject:IHasMessageBus
    {
        Guid Id { get; }
        void Update(TickTime delta);
        Vect3 Position { get; }
        Quat Rotation { get; }
    }
}