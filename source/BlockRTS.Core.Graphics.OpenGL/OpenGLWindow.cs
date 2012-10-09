using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Graphics.OpenGL.Vertices;
using BlockRTS.Core.Graphics.Shapes;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Messaging.Messages;
using BlockRTS.Core.Timing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class OpenGLWindow : GameWindow
    {
        public IMessageBus Bus { get; set; }
        public IObservableTimer Timer { get; set; }
        private readonly RTSCamera _camera;

        public AssetManager Assets = new AssetManager();

        public OpenGLWindow(IMessageBus bus, IObservableTimer timer)
            : base(1280, 720, new GraphicsMode(32, 0, 0, 4), "Test")
        {
            Bus = bus;
            Timer = timer;
            _camera= new RTSCamera();
            _camera.Model = Mat4.Translate(0f, 0f, 0.0f);
            _camera.Eye = new Vect3(0.0f, 0.0f, 5.0f);

        }


        private IShaderProgram _test;
  


        private VBO _vbo;
        private VAO _vao;

        private VBO _cubevbo;
        private VAO _cubevao;


        private Cube _cube = new Cube();

        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Assets.Load();


            _test = new TestShaderProgram();
            _test.Link();
            _test.AddUniform("mvp");
            _test.AddUniform("position");

  

            _vbo = new VBO();
            var data = new List<OpenGLVertex>();
            float asize = 0.5f;
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, 0), Colour = Color.Blue.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(asize, 0, 0), Colour = Color.Blue.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, 0), Colour = Color.Red.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, asize, 0), Colour = Color.Red.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, 0), Colour = Color.Green.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, asize), Colour = Color.Green.ToVector4() });
            _vbo.Buffer(data);

            _vao = new VAO(_test, _vbo);
            

            //_cubevbo = new VBO();
            var cubedata = new List<OpenGLVertex>();
            float size = 0.5f;


            var col = Color.FromArgb(255, 94, 169, 198).ToVector4();

            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, -size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, -size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, size, size), Colour = col });

            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, -size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, -size, -size), Colour = col });

            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, size, -size), Colour = col });

            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, -size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, -size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, -size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, -size, size), Colour = col });

            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, -size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(size, -size, size), Colour = col });

            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, -size, -size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, -size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, size, size), Colour = col });
            cubedata.Add(new OpenGLVertex { Position = new Vector3(-size, size, -size), Colour = col });


           // _cubevbo.Buffer(cubedata);

            _cubevbo = _cube.ToMesh().ToVBO();
            _cubevao = new VAO(_test, _cubevbo);



            Bus.Add(new DebugMessage(Timer.LastTickTime, "Loaded OpenGL Window"));
            var err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at OnLoad: " + err);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            if (Keyboard[Key.P])
            {
                ToggleWireFrame();
            }
            using (new Bind(_test))
            {
                _camera.Model *= Mat4.RotateZ(Angle.FromDegrees(1)) * Mat4.RotateY(Angle.FromDegrees(1)) * Mat4.RotateX(Angle.FromDegrees(1));
                _test.UpdateUniform("mvp", _camera.MVP.ToMatrix4());
            }
        }
        private bool _wireframe;

        private void ToggleWireFrame()
        {
            _wireframe = !_wireframe;
            GL.PolygonMode(MaterialFace.FrontAndBack, _wireframe ? PolygonMode.Line : PolygonMode.Fill);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(new Color4(0.137f, 0.121f, 0.125f, 0f));


            using (Bind.Asset(_test))
            using (Bind.Asset(Assets.Texture<DefaultTexture>()))
            using (new Bind(_vao))
            {
                _test.UpdateUniform("position", Matrix4.Identity);
                GL.DrawArrays(BeginMode.Lines, 0, _vbo.Count);
            }

            using (Bind.Asset(_test))
            using (Bind.Asset(Assets.Texture<DefaultTexture>()))
            using (new Bind(_cubevao))
            {
                _test.UpdateUniform("position", Matrix4.Identity);
                GL.DrawArrays(_cubevao.VBO.BeginMode, 0, _cubevao.VBO.Count);
            }



            SwapBuffers();
            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Swapbuffers: " + err);

        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            Bus.Add(new DebugMessage(Timer.LastTickTime, "Window Resize"));
            _camera.Projection = Mat4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width / (float)Height, 0.1f, 512.0f);
        }
    }

    public class RTSCamera
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
            Projection = Mat4.Identity;
        }
    }
}
