using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class OpenGLGraphics:IGraphics
    {
        private readonly IMessageBus _bus;
        private readonly IObservableTimer _timer;
        private readonly OpenGLWindow _window;
        public OpenGLGraphics(IMessageBus bus, IObservableTimer timer)
        {
            _bus = bus;
            _timer = timer;
        }

        public void Start()
        {
            new Task(() => new OpenGLWindow(_bus,_timer).Run()).Start();
        }
    }
}
