﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.Meshing;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Graphics.Models
{
    public enum STLFormat
    {
        ASCII,
        Binary
    }


    public class STL:IModel
    {
        private readonly Color _color;
        private List<STLElement> _elements = new List<STLElement>();
        public STL(string filename,Color color, STLFormat format = STLFormat.Binary)
        {
            _color = color;
            switch (format)
            {
                case STLFormat.ASCII:
                    LoadASCII(filename);
                    break;
                case STLFormat.Binary:
                    LoadBinary(filename);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("format");
            }
        }
        private void LoadBinary(string filename)
        {
            using (var br = new BinaryReader(File.Open(filename, FileMode.Open)))
            {
                br.ReadBytes(80); //header
                var count = (int)br.ReadUInt32();
                for (var i = 0; i < count; i++)
                {
                    var normal = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var p1 = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var p2 = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    var p3 = new Vect3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
                    br.ReadUInt16(); //attrib
                    _elements.Add(new STLElement { P1 = p1, P2 = p2, P3 = p3, Normal = normal });
                }
            }
        }
        private void LoadASCII(string filename)
        {
            Vect3 normal = null;
            var points = new Vect3[3];
            int i = 0;
            const NumberStyles style = NumberStyles.AllowExponent | NumberStyles.AllowLeadingSign | NumberStyles.Number;
            foreach (var split in File.ReadLines(filename).Select(line => line.Trim().ToLower().Split(' ')))
            {
                switch (split[0])
                {
                    case "solid":
                        break;
                    case "facet":
                        normal = new Vect3(Double.Parse(split[2], style), Double.Parse(split[3], style),
                                           Double.Parse(split[4], style));
                        break;
                    case "outer":
                        break;
                    case "vertex":
                        points[i++] = new Vect3(Double.Parse(split[1], style), Double.Parse(split[2], style),
                                                Double.Parse(split[3], style));
                        break;
                    case "endloop":
                        break;
                    case "endfacet":
                        _elements.Add(new STLElement { P1 = points[0], P2 = points[1], P3 = points[2], Normal = normal });
                        i = 0;
                        break;
                    case "endsolid":
                        break;
                }
            }
        }

        public Mesh ToMesh()
        {
            var data = new List<Vertex>();
            foreach (var element in _elements)
            {
                data.Add(new Vertex { Position = element.P1, Normal = element.Normal, Color = _color });
                data.Add(new Vertex { Position = element.P2, Normal = element.Normal, Color = _color });
                data.Add(new Vertex { Position = element.P3, Normal = element.Normal, Color = _color });
            }
            return new Mesh(MeshType.Triangle, data);
        }
    }

    public struct STLElement
    {
        public Vect3 P1;
        public Vect3 P2;
        public Vect3 P3;
        public Vect3 Normal;
    }
}
