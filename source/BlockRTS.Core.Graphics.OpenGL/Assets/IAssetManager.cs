using BlockRTS.Core.Graphics.OpenGL.Shaders;

namespace BlockRTS.Core.Graphics.OpenGL.Assets
{
    public interface IAssetManager
    {
        void Load();
        ITexture Texture<T>() where T : ITexture;
        T Shader<T>() where T : IShaderProgram;
        T UBO<T>() where T: IUBO;
    }
}