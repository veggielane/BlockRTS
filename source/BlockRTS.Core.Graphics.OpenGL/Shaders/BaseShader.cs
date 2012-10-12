using System;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
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
}