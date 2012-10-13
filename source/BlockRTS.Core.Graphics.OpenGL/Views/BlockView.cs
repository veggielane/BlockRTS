using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.GameObjects.Blocks;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Graphics.Shapes;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Views
{
    [BindView(typeof(WhiteBlock))]
    [BindView(typeof(BlueBlock))]
    public class BlockView : IView
    {
        private readonly IAssetManager _assets;
        private readonly BaseBlock _gameObject;

        public IGameObject GameObject { get { return _gameObject; } }
        public bool Loaded { get; private set; }

        private VAO _cubevao;
        private IShaderProgram _shader;

        public BlockView(IAssetManager assets, IGameObject gameObject)
        {
            _assets = assets;
            _gameObject = (BaseBlock)gameObject;
        }

        public void Load()
        {
            _shader = _assets.Shader<DefaultShaderProgram>();
            _cubevao = new VAO(_shader, new Cube { Color =_gameObject.BlockColor }.ToMesh().ToVBO());
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
            using (new Bind(_cubevao))
            {
                _shader.Uniforms["position"].Data = GameObject.Transformation.ToMatrix4();
                GL.DrawArrays(_cubevao.VBO.BeginMode, 0, _cubevao.VBO.Count);
            }
        }
    }
}
