using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Graphics.OpenGL.Vertices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class VBO : IAsset
    {
        public readonly int Handle;
        public int Count { get; private set; }

        public BeginMode BeginMode { get; set; }

        public IEnumerable<OpenGLVertex> Data;
        public VBO()
        {
            GL.GenBuffers(1, out Handle);
            Count = 0;
            BeginMode = BeginMode.Triangles;
        }

        public VBO(IEnumerable<OpenGLVertex> data):this()
        {
            Buffer(data.ToArray());
            Data = data;
        }

        public void Buffer(IEnumerable<OpenGLVertex> data)
        {
            Buffer(data.ToArray());
        }

        private void Buffer(OpenGLVertex[] data)
        {
            Bind();
            Count = data.Count();
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(data.Length * BlittableValueType.StrideOf(data)), data,
              BufferUsageHint.StaticDraw);
            int size;
            GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out size);
            if (data.Length * BlittableValueType.StrideOf(data) != size)
                throw new ApplicationException("Vertex data not uploaded correctly");
            UnBind();
        }

        public void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        }
        public void UnBind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }
    }
}
