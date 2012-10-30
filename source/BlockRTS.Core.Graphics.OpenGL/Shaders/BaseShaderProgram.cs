using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public abstract class BaseShaderProgram : IShaderProgram
    {
        public int Handle { get; private set; }
        public IDictionary<string, IUniform> Uniforms { get; private set; }
        public BaseShaderProgram()
        {
            Handle = GL.CreateProgram();
            Uniforms = new Dictionary<string, IUniform>();
        }

        public void CompileShader(ShaderType type, string source)
        {
            source = PreProcess(source);
            int shaderHandle = GL.CreateShader(type);
            GL.ShaderSource(shaderHandle, source);
            GL.CompileShader(shaderHandle);
            Console.WriteLine(GL.GetShaderInfoLog(shaderHandle));
            int compileResult;
            GL.GetShader(shaderHandle, ShaderParameter.CompileStatus, out compileResult);
            if (compileResult != 1)
            {
                Console.WriteLine("Compile Error:" + type);
            }
            GL.AttachShader(Handle, shaderHandle);
        }

        public string PreProcess(string source)
        {

            return source;
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

            //GL.BindAttribLocation(Handle, 0, "position");
            //GL.BindAttribLocation(Handle, 1, "instance_position");
            //GL.BindAttribLocation(Handle, 2, "instance_rotation");

            string info;
            GL.LinkProgram(Handle);
            GL.GetProgramInfoLog(Handle, out info);
            Console.WriteLine(info);

            ErrorCode err = GL.GetError();
            if (err != ErrorCode.NoError)
                Console.WriteLine("Error at Shader: " + err);
        }
    }

    /*
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
     
         public abstract class BaseShader:IShader
    {
        public abstract string Source { get; }
        public int Handle { get; private set; }
        public ShaderType type { get; private set; }

        public BaseShader(ShaderType type)
        {
            Handle = GL.CreateShader(type);

        }

        public void Compile()
        {
            GL.ShaderSource(Handle, Source);
            GL.CompileShader(Handle);
            Console.WriteLine(GL.GetShaderInfoLog(Handle));
            int compileResult;
            GL.GetShader(Handle, ShaderParameter.CompileStatus, out compileResult);
            if (compileResult != 1)
            {
                Console.WriteLine("Compile Error:" + type);
            }
        }
    }
     */
}