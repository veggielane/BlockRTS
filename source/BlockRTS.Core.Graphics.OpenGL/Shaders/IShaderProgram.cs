using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public interface IShaderProgram:IAsset
    {
        int Handle { get; }
        void Link();
        void AddShader(IShader shader);
        IDictionary<string,IUniform> Uniforms { get; }
        void AddUniforms();
    }

    public interface IUniform
    {
        int Handle { get; }
        string Name { get; }
        object Data { set; }
    }

    public interface IUniform<T> : IUniform
    {
        new T Data { set; }
    }


    public class UniformMatrix4:IUniform<Matrix4>

    {
        public int Handle { get; private set; }
        public string Name { get; private set; }

        public Matrix4 Data
        {
            set
            {
                GL.UniformMatrix4(Handle, false, ref value);
            }
        }

        object IUniform.Data
        {
            set { Data = (Matrix4)value; }
        }

        public UniformMatrix4(string name, IShaderProgram program)
        {
            Name = name;
            Handle = GL.GetUniformLocation(program.Handle, name);
        }
    }
}

