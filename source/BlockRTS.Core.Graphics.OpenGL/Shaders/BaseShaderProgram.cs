using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public abstract class BaseShaderProgram:IShaderProgram
    {
        public int Handle { get; private set; }

        public IDictionary<string, IUniform> Uniforms { get; private set; }

        public BaseShaderProgram()
        {
            Handle = GL.CreateProgram();
            Uniforms = new Dictionary<string, IUniform>();
        }


        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }


        public virtual void Link()
        {
            string info;
            GL.LinkProgram(Handle);
            GL.GetProgramInfoLog(Handle, out info);
            Console.WriteLine(info);

            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Shader: " + err);
        }

        public void AddShader(IShader shader)
        {
            shader.Compile();
            GL.AttachShader(Handle, shader.Handle);
        }

        public virtual void AddUniforms()
        {

        }
    }
}