using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class OpenGLGraphics:IGraphics
    {
        private readonly IMessageBus _bus;
        private readonly IObservableTimer _timer;
        private readonly ICamera _camera;
        private readonly IViewManager _viewManager;
        private readonly IAssetManager _assetManager;
        private readonly OpenGLWindow _window;
        public OpenGLGraphics(IMessageBus bus, IObservableTimer timer, ICamera camera, IViewManager viewManager, IAssetManager assetManager)
        {
            _bus = bus;
            _timer = timer;
            _camera = camera;
            _viewManager = viewManager;
            _assetManager = assetManager;
        }

        public void Start()
        {
            new Task(() => new OpenGLWindow(_bus, _timer, _camera, _viewManager, _assetManager).Run()).Start();
        }
    }
}
