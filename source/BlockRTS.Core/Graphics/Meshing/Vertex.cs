using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Graphics.Meshing
{
    public struct Vertex
    {
        public Vect3 Position;
        public Vect3 Normal;
        public Color Color;
        public Vect2 TexCoord;
    }
}
