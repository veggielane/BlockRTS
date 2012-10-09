using System;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core
{
    public interface IGame : IDisposable, IHasMessageBus
    {
        bool Running { get; }
        void Start();

        IGraphics Graphics { get; }
        IObservableTimer Timer { get; }
    }
}