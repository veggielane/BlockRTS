using System;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
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
            _camera.Model = Mat4.Translate(0f, 0f, 0.0f);
            _camera.Eye = new Vect3(0.0f, 0.0f, 80.0f);
            _camera.Projection = Mat4.CreatePerspectiveFieldOfView(Math.PI / 4.0, Width / (float)Height, 1, 512);
            VSync = VSyncMode.On;
            
        }

        private IShaderProgram _shader;


        protected override void OnLoad(EventArgs e)
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            _assetManager.Load();
            _shader = _assetManager.Shader<DefaultShaderProgram>();

            _viewManager.Load();

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

            _viewManager.Update(e.Time);
            using (new Bind(_shader))
            {
                _shader.Uniforms["mvp"].Data = _camera.MVP.ToMatrix4();
                _shader.Uniforms["position"].Data = Matrix4.Identity;
            }

            using (new Bind(_assetManager.Shader<BlockShaderProgram>()))
            {
                _assetManager.Shader<BlockShaderProgram>().Uniforms["mvp"].Data = _camera.MVP.ToMatrix4();
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

            SwapBuffers();
            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Swapbuffers: " + err.ToString());
            //Title =" FPS: " + string.Format("{0:F}", 1.0 / e.Time);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            Bus.Add(new DebugMessage(Timer.LastTickTime, "Window Resize"));
            _camera.Projection = Mat4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, Width/(float) Height, 0.1f, 512.0f);
        }

        protected override void OnClosed(EventArgs e)
        {
            Bus.Add(new RequestCloseMessage(Timer.LastTickTime));
            base.OnClosed(e);
        }
    }
}
