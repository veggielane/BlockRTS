using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public interface IShader
    {
        int Handle { get; }
        ShaderType type { get; }
        string Source { get; }
        void Compile();
    }
}
