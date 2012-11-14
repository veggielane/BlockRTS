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
        private readonly IGameObjectFactory _factory;
        private readonly IMessageBus _bus;
        private readonly ITimer _timer;


        private readonly float _nearPlane;
        private readonly float _farPlane;

        public RTSCamera(IGameObjectFactory factory, IMessageBus bus, ITimer timer)
        {
            _factory = factory;
            _bus = bus;
            _timer = timer;

            _nearPlane = 0.001f;
            _farPlane = 512.0f;

            Model = Mat4.Identity;

            Eye = Vect3.Zero;
            Target = Vect3.Zero;
            Up = Vect3.UnitY;
            Eye = new Vect3(0.0f, 0.0f, 80.0f);

        }


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
            Projection = Mat4.CreatePerspectiveFieldOfView(Math.PI / 4, _width / (float)_height, _nearPlane,_farPlane);
        }

        public void Pick(int x, int y)
        {
            
            int windowY = y - _height/2;
            double normY = windowY/(_height/2.0);
            int windowX = x - _width/2;
            double normX = windowX/(_width/2.0);

            _bus.Add(new DebugMessage(_timer.LastTickTime, "{0} - {1}".Fmt(normX, normY)));

            //Mat4 inv = (Model*View).Inverse();




            foreach (var gameObject in _factory.GameObjects.Values.OfType<ICanBeSelected>())
            {
                //_bus.Add(new DebugMessage(_timer.LastTickTime,"{0}".Fmt(gameObject.Id)));
            }
        }

        private int _width;
        private int _height;


    }
}