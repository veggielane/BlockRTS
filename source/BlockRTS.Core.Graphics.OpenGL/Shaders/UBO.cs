using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Shaders
{
     public interface IUBO:IAsset
     {
         int Handle { get; }
         //void Update();
         void BindToShaderProgram(IShaderProgram program);
     }

     public abstract class BaseUBO<T> : IUBO where T:struct
     {
         private readonly string _blockName;
         private readonly int _location;
         private readonly int _size;
         private int _handle;

         protected T Data;

         public int Handle
         {
             get { return _handle; }
             private set { _handle = value; }
         }



         protected BaseUBO(string blockName, int location, int size)
         {
             _blockName = blockName;
             _location = location;
             _size = size;
             GL.GenBuffers(1, out _handle);
             using (new Bind(this))
             {
                 GL.BufferData(BufferTarget.UniformBuffer, (IntPtr)(_size), (IntPtr)(null), BufferUsageHint.StreamDraw);
                 GL.BindBufferRange(BufferTarget.UniformBuffer, _location, _handle, (IntPtr)0, (IntPtr)_size);
             }
         }

         public void BindToShaderProgram(IShaderProgram program)
         {
             GL.UniformBlockBinding(program.Handle, GL.GetUniformBlockIndex(program.Handle, _blockName), _location);
         }

         protected void Update()
         {
             using (new Bind(this))
             {
                 GL.BufferSubData(BufferTarget.UniformBuffer, (IntPtr)0, (IntPtr)_size, ref Data);
             }
         }

         public void Bind()
         {
             GL.BindBuffer(BufferTarget.UniformBuffer, _handle);
         }

         public void UnBind()
         {
             GL.BindBuffer(BufferTarget.UniformBuffer, 0);
         }
     }

    public class CameraUBO : BaseUBO<CameraUBO.CameraData>
    {

        public struct CameraData
        {
            public Matrix4 MVP;
        }

        public CameraUBO()
            : base("Camera", 0, sizeof(float) * 4 * 4)
        {

        }

        public void Update(ICamera camera)
        {
            Data = new CameraData { MVP = camera.MVP.ToMatrix4() };
            Update();
        }
    }
}
