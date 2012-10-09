using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

namespace BlockRTS.Core.Graphics.OpenGL.Vertices
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct OpenGLVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector4 Colour;
        public Vector2 TexCoord;
    }
}
