using System;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Maths;
using System.Linq;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Messaging.Messages;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Graphics
{
    public class RTSCamera:ICamera
    {
        private readonly IMessageBus _bus;
        private readonly ITimer _timer;
        private int _width;
        private int _height;


        public RTSCamera(IMessageBus bus, ITimer timer)
        {
            _bus = bus;
            _timer = timer;
            Near = 1f;
            Far = 512.0f;
            Model = Mat4.Identity;
            Eye = Vect3.Zero;
            Target = Vect3.Zero;
            Up = Vect3.UnitY;
            Eye = new Vect3(0.0f, 0.0f, 80.0f);
        }

        public double Near { get; private set; }
        public double Far { get; private set; }
        public Mat4 Model { get; set; }

        
        public Mat4 View
        {
            get { return Mat4.LookAt(Eye, Target, Up); }
        }

        public Mat4 Projection { get; set; }

        public Mat4 MVP
        {
            get { return Projection * View * Model; }
        }

        public Vect3 Eye { get; set; }

        public Vect3 Target { get; set; }

        public Vect3 Up { get; set; }

        public void Update(double delta)
        {
        
        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
            Projection = Mat4.CreatePerspectiveFieldOfView(Math.PI / 4, _width / (float)_height, Near,Far);
        }

        public Ray Pick(int x, int y)
        {
            return new Ray( UnProject(new Vect3(x,y,Near)), UnProject(new Vect3(x,y,Far)));
        }

        private Vect3 UnProject(Vect3 win)
        {
            var o = MVP.Inverse() * new Vect4((win.X - _width / 2.0) / (_width / 2.0), (win.Y - _height / 2.0) / (_height / 2.0), 2.0 * win.Z - 1.0, 1); 
            return Math.Abs(o.A - 0.0) < double.Epsilon ? Vect3.Zero : new Vect3(o.X/o.A,o.Y/o.A,o.Z/o.A);
        }
    }
}