using System;
using System.Collections.Generic;
using System.Drawing;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Buffers;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Graphics.OpenGL.Vertices;
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
        public ITimer Timer { get; set; }
        private readonly ICamera _camera;
        private readonly IViewManager _viewManager;
        private readonly IAssetManager _assetManager;

        public OpenGLWindow(IMessageBus bus, ITimer timer, ICamera camera, IViewManager viewManager,
                            IAssetManager assetManager)
            : base(1280, 720, new GraphicsMode(32, 0, 0, 4), "Test")
        {
            Bus = bus;
            Timer = timer;
            _camera = camera;
            _viewManager = viewManager;
            _assetManager = assetManager;
    
            _camera.Eye = new Vect3(0.0f, 0.0f, 80.0f);
            _camera.Projection = Mat4.CreatePerspectiveFieldOfView(Math.PI / 2.0, Width / (float)Height, 1, 100);
            VSync = VSyncMode.On;

            Mouse.WheelChanged += (sender, args) =>
                {
                    _camera.Eye += new Vect3(0, 0, args.DeltaPrecise *-2.0);
                };
        }

        private IShaderProgram _shader;

        private FBO _fbo;
        private VAO _vao;
        private VBO _vbo;

        private VAO _dotsvao;
        private VBO _dotsvbo;


        protected override void OnLoad(EventArgs e)
        {

            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            _fbo = new FBO(Width,Height);
            _assetManager.Load();
            _shader = _assetManager.Shader<DefaultShaderProgram>();

            var data = new List<OpenGLVertex>
            {
                new OpenGLVertex {Position = new Vector3(-1, -1, 0),TexCoord = new Vector2(0,0)},
                new OpenGLVertex {Position = new Vector3(1, -1, 0),TexCoord = new Vector2(1,0)},
                new OpenGLVertex {Position = new Vector3(1, 1, 0),TexCoord = new Vector2(1,1)},
                new OpenGLVertex {Position = new Vector3(-1, 1, 0),TexCoord = new Vector2(0,1)}
            };

            _vbo = new VBO(data){BeginMode = BeginMode.Quads};
            _vao = new VAO(_assetManager.Shader<FlatShaderProgram>(), _vbo);


            var dotdata = new List<OpenGLVertex>
            {
                new OpenGLVertex {Position = new Vector3(0, 0, 0)},
                new OpenGLVertex {Position = new Vector3(10, 10, 0)},
            };
            _dotsvbo = new VBO(dotdata) { BeginMode = BeginMode.Lines };
            _dotsvao = new VAO(_assetManager.Shader<BasicShaderProgram>(), _dotsvbo);

            _viewManager.Load();

            Bus.Add(new DebugMessage(Timer.LastTickTime, "Loaded OpenGL Window"));
            var err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at OnLoad: " + err);
        }
        Random rnd = new Random();
        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            if (Keyboard[Key.P])
            {
                ToggleWireFrame();
            }

            if(Keyboard[Key.Left])
            {
                _camera.Eye += new Vect3(-0.1,0,0);
                _camera.Target += new Vect3(-0.1, 0, 0);
            }

            if (Keyboard[Key.Right])
            {
                _camera.Eye += new Vect3(0.1, 0, 0);
                _camera.Target += new Vect3(0.1, 0, 0);
            }

            if (Keyboard[Key.Up])
            {
                _camera.Eye += new Vect3(0, 0.1, 0);
                _camera.Target += new Vect3(0, 0.1, 0);
            }

            if (Keyboard[Key.Down])
            {
                _camera.Eye += new Vect3(0, -0.1, 0);
                _camera.Target += new Vect3(0, -0.1, 0);
            }

            int x = Mouse.X;
            int y = Height - Mouse.Y;

            int windowY = y - Height / 2;
            double normY = windowY / (Height / 2.0);
            int windowX = x - Width / 2;
            double normX = windowX / (Width / 2.0);


            var unview = _camera.MVP.Inverse();

            var near = unview * new Vect4(normX, normY, -1, 1); //* Vec(normalised_x, normalised_y, 0, 1)
            

            _viewManager.Update(e.Time);


            _camera.Update(e.Time);
            _camera.Pick(Mouse.X, Height - Mouse.Y);

            _dotsvbo.Buffer(new[] { new OpenGLVertex { Position = new Vector3(0, 0, 0) }, new OpenGLVertex { Position = new Vector3((float)near.X, (float)near.Y, (float)near.Z) }, });

            _assetManager.UBO<CameraUBO>().Update(_camera);


            using (new Bind(_shader))
            {
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
            _viewManager.Render();

            _dotsvao.Draw();

            //using (Bind.Asset(_fbo))
            //{
            //    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //    //GL.ClearColor(new Color4(1, 1f, 0.125f, 0f));
            //    GL.ClearColor(new Color4(0.137f, 0.121f, 0.125f, 0f));
            //    _viewManager.Render();
            //}

            //GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //GL.ClearColor(new Color4(0.137f, 0.121f, 0.125f, 0f));

            //GL.BindTexture(TextureTarget.Texture2D, _fbo.ColorTexture);
            //using (Bind.Asset(_assetManager.Shader<FlatShaderProgram>()))
            //using (new Bind(_vao))
            //{
            //    GL.DrawArrays(_vao.VBO.BeginMode, 0, _vao.VBO.Count);
            //}

            SwapBuffers();
            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Swapbuffers: " + err.ToString());
            Title = " FPS: " + string.Format("{0:F}", 1.0 / e.Time) + "Mouse<{0},{1}>".Fmt(Mouse.X, Height - Mouse.Y);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            Bus.Add(new DebugMessage(Timer.LastTickTime, "Window Resize"));

            _camera.Resize(Width,Height);
            _fbo.Resize(Width, Height);
   

        }

        protected override void OnClosed(EventArgs e)
        {
            Bus.Add(new RequestCloseMessage(Timer.LastTickTime));
            base.OnClosed(e);
        }
    }
}
