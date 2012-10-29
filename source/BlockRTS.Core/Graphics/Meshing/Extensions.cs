using System;
using System.Collections.Generic;
using System.Drawing;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Shapes;

namespace BlockRTS.Core.Graphics.Meshing
{
    public static class Extensions
    {
        public static Mesh ToMesh(this Cube e, Color color)
        {
            var data = new List<Vertex>
                {
                    new Vertex {Position = new Vect3(-e.Size, -e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, -e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, -e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, -e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, -e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, -e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, -e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, -e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, -e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(e.Size, -e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, -e.Size, -e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, -e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, e.Size, e.Size), Color = color},
                    new Vertex {Position = new Vect3(-e.Size, e.Size, -e.Size), Color = color}
                };
            return new Mesh(MeshType.Quad, data);
        }

        public static Mesh ToMesh(this Sphere sphere, int subdivisions, Color colour)
        {
            const float x = 0.525731112119133696f;
            const float z = 0.850650808352039932f;
            var vdata = new[]
            {
                new Vect3(-x, 0.0f, z), new Vect3(x, 0.0f, z), new Vect3(-x, 0.0f, -z), new Vect3(x, 0.0f, -z),
                new Vect3(0.0f, z, x), new Vect3(0.0f, z, -x), new Vect3(0.0f, -z, x), new Vect3(0.0f, -z, -x),
                new Vect3(z, x, 0.0f), new Vect3(-z, x, 0.0f), new Vect3(z, -x, 0.0f), new Vect3(-z, -x, 0.0f)
            };
            var tindices = new[]
            {
                new[] {1, 4, 0}, new[] {4, 9, 0}, new[] {4, 5, 9}, new[] {8, 5, 4},new[] {1, 8, 4},
                new[] {1, 10, 8}, new[] {10, 3, 8}, new[] {8, 3, 5}, new[] {3, 2, 5},new[] {3, 7, 2},
                new[] {3, 10, 7}, new[] {10, 6, 7}, new[] {6, 11, 7}, new[] {6, 0, 11},new[] {6, 1, 0},
                new[] {10, 1, 6}, new[] {11, 0, 9}, new[] {2, 11, 9}, new[] {5, 2, 9},new[] {11, 2, 7}
            };
            var vertices = new List<Vertex>();
            for (int i = 0; i < 20; i++)
                SubdivideSphere(vertices, vdata[tindices[i][0]], vdata[tindices[i][1]], vdata[tindices[i][2]], subdivisions, sphere.Radius, colour);
            return new Mesh(MeshType.Triangle, vertices);
        }

        private static void SubdivideSphere(ICollection<Vertex> vertices, Vect3 v1, Vect3 v2, Vect3 v3, int div, Double r, Color color)
        {
            if (div <= 0)
            {
                vertices.Add(new Vertex {Position = (v1*(float) r),Normal = v1, Color = color});
                vertices.Add(new Vertex {Position = (v2*(float) r),Normal = v2, Color = color});
                vertices.Add(new Vertex {Position = (v3*(float) r),Normal = v3, Color = color});
            }
            else
            {
                var v12 = new Vect3((v1.X + v2.X) / 2.0f, (v1.Y + v2.Y) / 2.0f, (v1.Z + v2.Z) / 2.0f).Normalize();
                var v23 = new Vect3((v2.X + v3.X) / 2.0f, (v2.Y + v3.Y) / 2.0f, (v2.Z + v3.Z) / 2.0f).Normalize();
                var v31 = new Vect3((v3.X + v1.X) / 2.0f, (v3.Y + v1.Y) / 2.0f, (v3.Z + v1.Z) / 2.0f).Normalize();
                SubdivideSphere(vertices, v1, v12, v31, div - 1, r, color);
                SubdivideSphere(vertices, v2, v23, v12, div - 1, r, color);
                SubdivideSphere(vertices, v3, v31, v23, div - 1, r, color);
                SubdivideSphere(vertices, v12, v23, v31, div - 1, r, color);
            }
        }
    }


}
