using System;

namespace BlockRTS.Core.Graphics.OpenGL.Buffers
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
