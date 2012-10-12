using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public abstract class BaseFragmentBaseShader : BaseShader
    {

        public BaseFragmentBaseShader()
            : base(ShaderType.FragmentShader)
        {

        }
    }
}