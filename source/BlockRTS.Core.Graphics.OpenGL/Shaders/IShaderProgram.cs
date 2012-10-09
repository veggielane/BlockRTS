using System;
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
        void Add(IShader shader);
        void AddUniform(string name);
        void UpdateUniform(string name, Matrix4 matrix);
    }

    public abstract class BaseShaderProgram:IShaderProgram
    {
        public int Handle { get; private set; }
        public readonly Dictionary<string, int> Uniforms = new Dictionary<string, int>();

        public BaseShaderProgram()
        {
            Handle = GL.CreateProgram();
        }


        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }


        public void Link()
        {
            string info;
            GL.LinkProgram(Handle);
            GL.GetProgramInfoLog(Handle, out info);
            Console.WriteLine(info);

            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Shader: " + err.ToString());
        }

        public void Add(IShader shader)
        {
            shader.Compile();
            GL.AttachShader(Handle, shader.Handle);
        }
        public void AddUniform(string name)
        {
            Uniforms.Add(name, GL.GetUniformLocation(Handle, name));
        }
        public void UpdateUniform(string name, Matrix4 matrix)
        {
            GL.UniformMatrix4(Uniforms[name], false, ref matrix);
        }
    }
}
