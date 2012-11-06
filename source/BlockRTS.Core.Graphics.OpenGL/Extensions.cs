using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BlockRTS.Core.Graphics.Meshing;
using BlockRTS.Core.Graphics.OpenGL.Buffers;
using BlockRTS.Core.Graphics.OpenGL.Vertices;
using BlockRTS.Core.Maths;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL
{
    public static class Extensions
    {
        public static Vector4 ToVector4(this Color col)
        {
            return new Vector4(col.R / 255f, col.G / 255f, col.B / 255f, col.A / 255f);
        }

        public static Vector3 ToVector3(this Vect3 v)
        {
            return new Vector3((float)v.X, (float)v.Y, (float)v.Z);
        }

        public static Vector2 ToVector2(this Vect2 v)
        {
            return new Vector2((float)v.X, (float)v.Y);
        }

        public static Matrix4 ToMatrix4(this Mat4 m)
        {
            var md = m.Transpose();//http://www.opentk.com/node/2771
            return new Matrix4(
                (float)md[1, 1], (float)md[1, 2], (float)md[1, 3], (float)md[1, 4],
                (float)md[2, 1], (float)md[2, 2], (float)md[2, 3], (float)md[2, 4],
                (float)md[3, 1], (float)md[3, 2], (float)md[3, 3], (float)md[3, 4],
                (float)md[4, 1], (float)md[4, 2], (float)md[4, 3], (float)md[4, 4]);
        }

        public static OpenGLVertex ToOpenGLVertex(this Vertex vertex)
        {
            var v = new OpenGLVertex();
            v.Position = vertex.Position.ToVector3();
            if (vertex.Normal != null) v.Normal = vertex.Normal.ToVector3();
            v.Colour = vertex.Color.ToVector4();
            if (vertex.TexCoord != null) v.TexCoord = vertex.TexCoord.ToVector2();
            return v;
        }

        public static IEnumerable<OpenGLVertex> ToOpenGLVertices(this IEnumerable<Vertex> list)
        {
            return list.Select(vertex =>vertex.ToOpenGLVertex());
        }


        public static VBO ToVBO(this Mesh m)
        {
            BeginMode mode;
            switch (m.Type)
            {
                case MeshType.Triangle:
                    mode = BeginMode.Triangles;
                    break;
                case MeshType.Quad:
                    mode = BeginMode.Quads;
                    break;
                default:
                    mode = BeginMode.Points;
                    break;
            }
            return new VBO(m.Vertices.ToOpenGLVertices()) {BeginMode = mode};
        }
    }
}
