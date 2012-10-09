using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Graphics.Meshing
{
    public enum MeshType {Triangle, Quad}
    public class Mesh
    {
        public MeshType Type { get; private set; }
        public List<Vertex> Vertices { get; private set; }

        public Mesh(MeshType type, List<Vertex> vertices)
        {
            Type = type;
            Vertices = vertices;
        }
    }
}
