using System;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Graphics
{
    public class RTSCamera:ICamera
    {
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

        public RTSCamera()
        {
            Model = Mat4.Identity;


            Eye = Vect3.Zero;
            Target = Vect3.Zero;
            Up = Vect3.UnitY;


            Eye = new Vect3(0.0f, 0.0f, 80.0f);


        }
    }
}