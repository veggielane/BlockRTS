using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class OpenGLGraphics:IGraphics
    {
        private readonly IMessageBus _bus;
        private readonly ITimer _timer;
        private readonly ICamera _camera;
        private readonly IViewManager _viewManager;
        private readonly IAssetManager _assetManager;
        private readonly IGameObjectFactory _factory;

        public OpenGLGraphics(IMessageBus bus, ITimer timer, ICamera camera, IViewManager viewManager, IAssetManager assetManager, IGameObjectFactory factory)
        {
            _bus = bus;
            _timer = timer;
            _camera = camera;
            _viewManager = viewManager;
            _assetManager = assetManager;
            _factory = factory;
        }

        public void Start()
        {
            new Task(() => new OpenGLWindow(_bus, _timer, _camera, _viewManager, _assetManager, _factory).Run()).Start();
        }
    }
}
