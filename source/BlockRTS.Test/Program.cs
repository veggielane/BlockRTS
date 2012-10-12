using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Graphics.OpenGL;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;
using Ninject;
using Ninject.Modules;
using Ninject.Parameters;

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
            Bind<IGameObjectFactory>().To<GameObjectFactory>().InSingletonScope();
            Bind<IGameObjectCreator>().To<NinjectGameObjectCreator>().InSingletonScope();

            Bind<ICamera>().To<RTSCamera>().InSingletonScope();
        }
    }

    public class NinjectGameObjectCreator:IGameObjectCreator
    {
        private readonly IKernel _kernal;

        public NinjectGameObjectCreator(IKernel kernal)
        {
            _kernal = kernal;
        }

        public IGameObject Create(Type gameObjectType, Mat4 transformation)
        {
            return (IGameObject)_kernal.Get(gameObjectType, new ConstructorArgument("transformation", transformation));
        }
    }
}
