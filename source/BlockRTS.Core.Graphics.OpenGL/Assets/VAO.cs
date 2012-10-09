﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public class VAO : IAsset
    {
        private int _handle;
        public int Handle
        {
            get { return _handle; }
            private set { _handle = value; }
        }

        public VBO VBO { get; private set; }

        public VAO(IShaderProgram program, VBO vbo)
        {
            VBO = vbo;
            GL.GenVertexArrays(1, out _handle);
            using (new Bind(program))
            using (new Bind(this))
            using (new Bind(vbo))
            {
                var stride = Vector3.SizeInBytes * 2 + Vector4.SizeInBytes + Vector2.SizeInBytes;

                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, stride, 0);
                GL.BindAttribLocation(program.Handle, 0, "vert_position");

                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, stride, Vector3.SizeInBytes);
                GL.BindAttribLocation(program.Handle, 1, "vert_normal");

                GL.EnableVertexAttribArray(2);
                GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, stride, Vector3.SizeInBytes * 2);
                GL.BindAttribLocation(program.Handle, 2, "vert_colour");

                GL.EnableVertexAttribArray(3);
                GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, stride, Vector3.SizeInBytes * 2 + Vector4.SizeInBytes);
                GL.BindAttribLocation(program.Handle, 3, "vert_texture");
            }
        }

        public void Bind()
        {
            GL.BindVertexArray(Handle);
        }

        public void UnBind()
        {
            GL.BindVertexArray(0);
        }
    }
}
