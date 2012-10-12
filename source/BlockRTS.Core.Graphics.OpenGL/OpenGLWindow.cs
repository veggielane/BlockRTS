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
        private readonly ICamera _camera;

        public AssetManager Assets = new AssetManager();

        public OpenGLWindow(IMessageBus bus, IObservableTimer timer, ICamera camera)
            : base(1280, 720, new GraphicsMode(32, 0, 0, 4), "Test")
        {
            Bus = bus;
            Timer = timer;
            _camera = camera;
            _camera.Model = Mat4.Translate(0f, 0f, 0.0f);
            _camera.Eye = new Vect3(0.0f, 0.0f, 100.0f);

        }


        //private IShaderProgram _test;
  


        private VBO _vbo;
        private VAO _vao;

        private VAO _cubevao;

        private IShaderProgram _shader;


        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            Assets.Load();
            _shader = Assets.Shader<DefaultShaderProgram>();

           // _test = new DefaultShaderProgram();
            //_test.Link();
            //_test.AddUniform("mvp");
            //_test.AddUniform("position");

  

            _vbo = new VBO();
            var data = new List<OpenGLVertex>();
            float asize = 10f;
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, 0), Colour = Color.Blue.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(asize, 0, 0), Colour = Color.Blue.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, 0), Colour = Color.Red.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, asize, 0), Colour = Color.Red.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, 0), Colour = Color.Green.ToVector4() });
            data.Add(new OpenGLVertex { Position = new Vector3(0, 0, asize), Colour = Color.Green.ToVector4() });
            _vbo.Buffer(data);
            _vao = new VAO(_shader, _vbo);



            _cubevao = new VAO(_shader, new Cube { Color = Color.FromArgb(255, 94, 169, 198) }.ToMesh().ToVBO());




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


            using (new Bind(_shader))
            {
                _camera.Model *= Mat4.RotateZ(Angle.FromDegrees(1)) * Mat4.RotateY(Angle.FromDegrees(1)) * Mat4.RotateX(Angle.FromDegrees(1));
                _shader.Uniforms["mvp"].Data =  _camera.MVP.ToMatrix4();
                _shader.Uniforms["position"].Data = Matrix4.Identity;
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


            using (Bind.Asset(_shader))
            using (Bind.Asset(Assets.Texture<DefaultTexture>()))
            using (new Bind(_vao))
            {

                GL.DrawArrays(BeginMode.Lines, 0, _vbo.Count);
            }

            using (Bind.Asset(_shader))
            using (Bind.Asset(Assets.Texture<DefaultTexture>()))
            using (new Bind(_cubevao))
            {
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
}
