using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL.Shaders;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class Bind : IDisposable
    {
        private readonly IAsset _asset;
        public Bind(IAsset asset)
        {
            _asset = asset;
            _asset.Bind();
        }

        public void Dispose()
        {
            _asset.UnBind();
        }
        public static Bind Asset(IAsset asset)
        {
            return new Bind(asset);
        }
    }
}
