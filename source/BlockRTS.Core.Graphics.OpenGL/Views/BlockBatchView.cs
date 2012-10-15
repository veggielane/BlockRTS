using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Graphics.Shapes;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Views
{
    class BlockBatchView:IBatchView
    {
        private readonly IAssetManager _assets;


        private IList<IGameObject> _gameObjects = new List<IGameObject>();

        private VAO _cubevao;
        private IShaderProgram _shader;

        public BlockBatchView(IAssetManager assetManager)
        {
            _assets = assetManager;
        }


        public void Add(IGameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public bool Loaded { get; private set; }
        public void Load()
        {
            _shader = _assets.Shader<DefaultShaderProgram>();
            _cubevao = new VAO(_shader, new Cube { Color = Color.FromArgb(255, 94, 169, 198) }.ToMesh().ToVBO());
            Loaded = true;
        }

        public void UnLoad()
        {
            throw new NotImplementedException();
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
                foreach (var gameObject in _gameObjects)
                {
                    _shader.Uniforms["position"].Data = gameObject.Transformation.ToMatrix4();
                    GL.DrawArrays(_cubevao.VBO.BeginMode, 0, _cubevao.VBO.Count);
                }
            }
        }
    }
}
