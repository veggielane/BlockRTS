using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.Meshing;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Graphics.Shapes
{
    public class Cube
    {
        public double Size { get; set; }
        public Vect3 Position { get; set; }
        public Quat Rotation { get; set; }
        public Color Color { get;  set; } 

        public Cube()
        {
            Size = 1.0;
            Position = Vect3.Zero;
            Rotation = Quat.Identity;
            Color = Color.Red;
        }

        public Mesh ToMesh()
        {

            var data = new List<Vertex>();
            data.Add(new Vertex { Position = new Vect3(-Size, -Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, -Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(-Size, Size, Size), Color = Color });

            data.Add(new Vertex { Position = new Vect3(-Size, -Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(-Size, Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, -Size, -Size), Color = Color });

            data.Add(new Vertex { Position = new Vect3(-Size, Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(-Size, Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, Size, -Size), Color = Color });

            data.Add(new Vertex { Position = new Vect3(-Size, -Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, -Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, -Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(-Size, -Size, Size), Color = Color });

            data.Add(new Vertex { Position = new Vect3(Size, -Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(Size, -Size, Size), Color = Color });

            data.Add(new Vertex { Position = new Vect3(-Size, -Size, -Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(-Size, -Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(-Size, Size, Size), Color = Color });
            data.Add(new Vertex { Position = new Vect3(-Size, Size, -Size), Color = Color });
            return new Mesh(MeshType.Quad, data);
        }
    }
}
