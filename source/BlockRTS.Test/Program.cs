using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Graphics.OpenGL;
using BlockRTS.Core.Graphics.OpenGL.Assets;
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
            Bind<ITimer>().To<AsyncTimer>().InSingletonScope();
            Bind<IGameObjectFactory>().To<GameObjectFactory>().InSingletonScope();
            Bind<IObjectCreator>().To<NinjectObjectCreator>().InSingletonScope();

            Bind<ICamera>().To<RTSCamera>().InSingletonScope();

            Bind<IViewManager>().To<ViewManager>().InSingletonScope();
            Bind<IAssetManager>().To<AssetManager>().InSingletonScope();
        }
    }

    public class NinjectObjectCreator:IObjectCreator
    {
        private readonly IKernel _kernal;

        public NinjectObjectCreator(IKernel kernal)
        {
            _kernal = kernal;
        }

        public IGameObject CreateGameObject(Type gameObjectType, Vect3 position, Quat rotation)
        {
            return (IGameObject)_kernal.Get(gameObjectType, new ConstructorArgument("position", position), new ConstructorArgument("rotation", rotation));
        }

        public IView CreateView(Type viewType,IGameObject gameObject)
        {
            return (IView)_kernal.Get(viewType, new ConstructorArgument("gameObject", gameObject));
        }

        public IBatchView CreateBatchView(Type viewType)
        {
            return (IBatchView)_kernal.Get(viewType);
        }

        public T Create<T>()
        {
            return _kernal.Get<T>();
        }

        public T Create<T>(Type type)
        {
            return (T)_kernal.Get(type);
        }

        public object Create(Type type)
        {
            return _kernal.Get(type);
        }
    }
}
