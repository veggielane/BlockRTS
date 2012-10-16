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
        private readonly IViewManager _viewManager;
        private readonly BaseBlock _gameObject;

        public IGameObject GameObject { get { return _gameObject; } }
        public bool Loaded { get; private set; }

        public BlockView(IGameObject gameObject,IViewManager viewManager)
        {
            _viewManager = viewManager;
            _gameObject = (BaseBlock)gameObject;
        }

        public void Load()
        {
            _viewManager.Batch<BlockBatchView>().Add(_gameObject);
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

        }
    }
}
