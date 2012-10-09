using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Graphics.OpenGL;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;
using Ninject;
using Ninject.Modules;

namespace BlockRTS.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel(new GameModule());
            using (var game = kernel.Get<IGame>())
            {
                game.Start();
                game.Bus.Subscribe(Console.WriteLine);
                while (game.Running) { }
            }
        }
    }

    public class GameModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IGame>().To<Game>().InSingletonScope();
            Bind<IMessageBus>().To<MessageBus>().InSingletonScope();
            Bind<IGraphics>().To<OpenGLGraphics>().InSingletonScope();
            Bind<IObservableTimer>().To<AsyncObservableTimer>().InSingletonScope();
        }
    }
}
