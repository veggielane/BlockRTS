using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
    public enum ShaderType { Vertex, Fragment, Geometry, TessellationControl, TessellationEvaluation }
    public abstract class GLSLProgram : IAsset
    {
        public int Handle { get; private set; }

        public GLSLProgram()
        {
                        Handle = GL.CreateProgram();
        }

        public void CompileShader(string source, ShaderType type)
        {
            source = PreProcess(source);


        }

        public string PreProcess(string source)
        {

            return source;
        }

        public void PrintActiveUniforms()
        {
            
        }

        public void Bind()
        {
            GL.UseProgram(Handle);
        }

        public void UnBind()
        {
            GL.UseProgram(0);
        }
    }
}
