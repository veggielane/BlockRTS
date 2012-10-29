using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics.Models;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Maths;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Views
{
    class BlockBatchView : IBatchView
    {
        private readonly IAssetManager _assets;


        private IList<IGameObject> _gameObjects = new List<IGameObject>();

        //private VAO _cubevao;
        private IShaderProgram _shader;

        public BlockBatchView(IAssetManager assetManager)
        {
            _assets = assetManager;
        }


        public void Add(IGameObject gameObject)
        {

            _gameObjects.Add(gameObject);
        }

        public bool Loaded { get; private set; }

        //private VBO _vbo;
        //private VAO _vao;
        private int _squareVao, _squareVbo;
        private const float Size = 0.5f;
        public void Load()
        {
            _shader = _assets.Shader<BlockShaderProgram>();


            STL stl = new STL("chamfer_cube.stl", Color.Yellow);
            var verts = new List<float>();
            var mesh = stl.ToMesh();

            foreach (var vertex in mesh.Vertices)
            {
                verts.Add((float)vertex.Position.X);
                verts.Add((float)vertex.Position.Y);
                verts.Add((float)vertex.Position.Z);


            }

            _squareVertices = verts.ToArray();
            /*
            _squareVertices = new[]{
                //Front Face
                -Size, -Size, Size,
                Size, -Size, Size,
                Size, Size, Size,
                -Size, Size, Size,
                //Back Face
                -Size, -Size, -Size,
                -Size, Size, -Size,
                Size, Size, -Size,
                Size, -Size, -Size,
                //Top Face
                -Size, Size, -Size,
                -Size, Size, Size,
                Size, Size, Size,
                Size, Size, -Size,
                //Bottom Face
                -Size, -Size, -Size,
                Size, -Size, -Size,
                Size, -Size, Size,
                -Size, -Size, Size,
                //Right Face
                Size, -Size, -Size,
                Size, Size, -Size,
                Size, Size, Size,
                Size, -Size, Size,
                //Left Face
                -Size, -Size, -Size,
                -Size, -Size, Size,
                -Size, Size, Size,
                -Size, Size, -Size
            };*/
            GL.GenVertexArrays(1, out _squareVao);
            GL.GenBuffers(1, out _squareVbo);
            GL.BindVertexArray(_squareVao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _squareVbo);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);

            GL.Arb.VertexAttribDivisor(1, 1);
            
            GL.Arb.VertexAttribDivisor(2, 1);
            GL.Arb.VertexAttribDivisor(3, 1);

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 11 * sizeof(float), _squareVertices.Length * sizeof(float));
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 11 * sizeof(float), (_squareVertices.Length + 3) * sizeof(float));
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 11 * sizeof(float), (_squareVertices.Length + 3 + 4) * sizeof(float));

            Loaded = true;
        }

        public void UnLoad()
        {
            throw new NotImplementedException();
        }

        private float[] _squareVertices;

        private int _count = 0;

        public void Update(double delta)
        {
            //var objects = _gameObjects.ToArray();
            _count = _gameObjects.Count();
            GL.BindVertexArray(_squareVao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _squareVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr((_squareVertices.Length + (_count * (3 + 4 + 4))) * sizeof(float)), IntPtr.Zero, BufferUsageHint.StreamDraw);
            var ptr = GL.MapBuffer(BufferTarget.ArrayBuffer, BufferAccess.WriteOnly); 
            unsafe
            {
                var videoMemory = (float*) ptr.ToPointer();
                var i = 0;
                foreach (var squareVertex in _squareVertices)
                {
                    videoMemory[i++] = squareVertex;
                }
                foreach (var gameObject in _gameObjects)
                {
                    //position
                    videoMemory[i++] = (float)gameObject.Position.X;
                    videoMemory[i++] = (float)gameObject.Position.Y;
                    videoMemory[i++] = (float)gameObject.Position.Z;
                    //rotation
                    videoMemory[i++] = (float)gameObject.Rotation.X;
                    videoMemory[i++] = (float)gameObject.Rotation.Y;
                    videoMemory[i++] = (float)gameObject.Rotation.Z;
                    videoMemory[i++] = (float)gameObject.Rotation.W;
                    //color
                    videoMemory[i++] = 1.0f;
                    videoMemory[i++] = 1.0f;
                    videoMemory[i++] = 1.0f;
                    videoMemory[i++] = 1.0f;
                }
            }
            GL.UnmapBuffer(BufferTarget.ArrayBuffer);
        }

        public void Render()
        {
            using (Bind.Asset(_shader))
            {
                GL.BindVertexArray(_squareVao);
                GL.DrawArraysInstanced(BeginMode.Triangles, 0, _squareVertices.Count()/3, _count);
            }
        }
    }
}
