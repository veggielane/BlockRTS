using System.Drawing;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.GameObjects.Blocks;
using BlockRTS.Core.Graphics.Meshing;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Buffers;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Shapes;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Views
{
    [BindView(typeof(Explosion))]
    public class ExplosionView : IView
    {
        public IGameObject GameObject { get; set; }
        private readonly IAssetManager _assets;

        public bool Loaded { get; private set; }

        public ExplosionView(IAssetManager assets, IGameObject gameObject)
        {
            GameObject = gameObject;
            _assets = assets;
        }

        private VAO _vao;
        private IShaderProgram _shader;

        public void Load()
        {
            _shader = _assets.Shader<DefaultShaderProgram>();
            _vao = new VAO(_shader, new Sphere(Vect3.Zero, Quat.Identity, ((Explosion)GameObject).Size).ToMesh(5, Color.Yellow).ToVBO());
            Loaded = true;
        }

        public void UnLoad()
        {

        }

        public void Update(double delta)
        {

        }

        public void Render()
        {
            using (Bind.Asset(_shader))
            using (Bind.Asset(_assets.Texture<DefaultTexture>()))
            using (new Bind(_vao))
            {
                _shader.Uniforms["position"].Data = Mat4.Translate(GameObject.Position).ToMatrix4();
                GL.DrawArrays(_vao.VBO.BeginMode, 0, _vao.VBO.Count);
            }
        }
    }
}