using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.GameObjects.Blocks;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Messaging.Messages;
using BlockRTS.Core.Physics;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core
{
    public class Game:IGame
    {
        public IMessageBus Bus { get; private set; }
        public IObservableTimer Timer { get; private set; }
 

        public IGraphics Graphics { get; private set; }
        public IGameObjectFactory Factory { get; private set; }

        public bool Running { get; private set; }

        public Game(IObservableTimer timer, IGraphics graphics, IMessageBus bus, IGameObjectFactory factory)
        {
            Timer = timer;
            Timer.SubSample(5).Subscribe(t => Bus.SendAll());
            Graphics = graphics;
            Bus = bus;
            Factory = factory;
            Timer.Subscribe(Update);


            Bus.OfType<RequestCloseMessage>().Subscribe(m => Stop());
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
            var _rand = new Random();
            var results = Enumerable.Range(0, 5000)
                        .Select(r => new { x = _rand.NextDouble(-20.0, 20.0), y = _rand.NextDouble(-20.0, 20.0), z = _rand.NextDouble(-20.0, 20.0) })
                        .ToList();
            
            foreach (var result in results)
            {
                if(_rand.Next(3) == 2)
                {
                    Bus.Add(GameObjectRequest.Create<WhiteBlock>(Timer.LastTickTime, new Vect3(result.x, result.y, result.z), Quat.Identity));
                }else
                {
                    Bus.Add(GameObjectRequest.Create<BlueBlock>(Timer.LastTickTime,  new Vect3(result.x, result.y, result.z), Quat.Identity));
                }
            }

            Bus.Add(GameObjectRequest.Create<WhiteBlock>(Timer.LastTickTime,  Vect3.Zero, Quat.Identity));
        }

        public void Stop()
        {
            Running = false;
        }

        private void Update(TickTime tickTime)
        {
            Parallel.ForEach(Factory.GameObjects.Values, gameObject =>
                {
                    var hasPhysics = gameObject as IHasPhysics;
                    if (hasPhysics != null)
                    {
                        hasPhysics.Body.Update(tickTime);
                    }
                    gameObject.Update(tickTime);
                });

            /*
            foreach (var gameObject in Factory.GameObjects.Values)
            {
                var hasPhysics = gameObject as IHasPhysics;
                if (hasPhysics != null)
                {
                    hasPhysics.Body.Update(tickTime);
                }
                gameObject.Update(tickTime);
            }*/
        }
    }
}
