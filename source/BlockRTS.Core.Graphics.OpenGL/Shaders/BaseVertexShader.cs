using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public abstract class BaseVertexBaseShader : BaseShader
    {
        public BaseVertexBaseShader()
            : base(ShaderType.VertexShader)
        {

        }
    }
}