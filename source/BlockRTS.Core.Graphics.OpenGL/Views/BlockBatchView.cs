using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.GameObjects.Blocks;
using BlockRTS.Core.Graphics.Models;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Buffers;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Maths;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Views
{
    class BlockBatchView : IBatchView
    {
        private readonly IAssetManager _assets;
        public bool Loaded { get; private set; }

        private readonly IList<BaseBlock> _gameObjects = new List<BaseBlock>();

        private IShaderProgram _shader;

        private int _squareVao, _squareVbo;
        private const float Size = 0.5f;
        private float[] _squareVertices;

        private int _count;

        public BlockBatchView(IAssetManager assetManager)
        {
            _assets = assetManager;
        }


        public void Add(IGameObject gameObject)
        {

            _gameObjects.Add((BaseBlock)gameObject);
        }

        public void Load()
        {
            _shader = _assets.Shader<BlockShaderProgram>();


            var stl = new STL("chamfer_cube.stl", Color.Yellow);
            var cubedata = new List<float>();

            foreach (var vertex in stl.ToMesh().Vertices)
            {
                cubedata.Add((float)vertex.Position.X);
                cubedata.Add((float)vertex.Position.Y);
                cubedata.Add((float)vertex.Position.Z);
            }
            foreach (var vertex in stl.ToMesh().Vertices)
            {
                cubedata.Add((float)vertex.Normal.X);
                cubedata.Add((float)vertex.Normal.Y);
                cubedata.Add((float)vertex.Normal.Z);
            }



            _squareVertices = cubedata.ToArray();

            GL.GenVertexArrays(1, out _squareVao);
            GL.GenBuffers(1, out _squareVbo);
            GL.BindVertexArray(_squareVao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _squareVbo);

            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);
            GL.EnableVertexAttribArray(3);
            GL.EnableVertexAttribArray(4);


            GL.Arb.VertexAttribDivisor(2, 1);//position
            GL.Arb.VertexAttribDivisor(3, 1);//rotation
            GL.Arb.VertexAttribDivisor(4, 1);//color

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 11 * sizeof(float), _squareVertices.Length * sizeof(float));
            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, 11 * sizeof(float), (_squareVertices.Length + 3) * sizeof(float));
            GL.VertexAttribPointer(4, 4, VertexAttribPointerType.Float, false, 11 * sizeof(float), (_squareVertices.Length + 3 + 4) * sizeof(float));

            Loaded = true;
        }

        public void UnLoad()
        {
            throw new NotImplementedException();
        }



        public void Update(double delta)
        {
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
                    //color 0.37,0.66,0.78,0.9
                    
                    //videoMemory[i++] = 0.37f;
                    //videoMemory[i++] = 0.66f;
                    //videoMemory[i++] = 0.78f;
                    //videoMemory[i++] = 1.0f;

                    videoMemory[i++] = (float)MathsHelper.Map(gameObject.BlockColor.R, 0, 255, 0, 1);
                    videoMemory[i++] = (float)MathsHelper.Map(gameObject.BlockColor.G, 0, 255, 0, 1);
                    videoMemory[i++] = (float)MathsHelper.Map(gameObject.BlockColor.B, 0, 255, 0, 1);
                    videoMemory[i++] = (float)MathsHelper.Map(gameObject.BlockColor.A, 0, 255, 0, 1);
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
