using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Messaging.Messages;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core
{
    public class Game:IGame
    {
        public IMessageBus Bus { get; private set; }
        public IObservableTimer Timer { get; private set; }
        public IGraphics Graphics { get; private set; }

        public bool Running { get; private set; }

        public Game(IObservableTimer timer, IGraphics graphics, IMessageBus bus)
        {
            Timer = timer;
            Timer.SubSample(5).Subscribe(t => Bus.SendAll());
            Graphics = graphics;
            Bus = bus;
        }

        public void Dispose()
        {
            
        }

        public void Start()
        {
            Bus.Add(new DebugMessage(Timer.LastTickTime, "Starting Game Engine"));
            Running = true;
            Timer.Start();
            Graphics.Start();
            Bus.Add(new DebugMessage(Timer.LastTickTime, "Started Game Engine"));
        }
    }
}
