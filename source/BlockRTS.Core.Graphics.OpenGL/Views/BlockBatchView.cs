using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics.OpenGL.Assets;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using BlockRTS.Core.Graphics.Shapes;
using BlockRTS.Core.Maths;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL.Views
{
    class BlockBatchView:IBatchView
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
            };
            //_cubevao = new VAO(_shader, new Cube { Color = Color.FromArgb(255, 94, 169, 198) }.ToMesh().ToVBO());


            GL.GenVertexArrays(1, out _squareVao);
            GL.GenBuffers(1, out _squareVbo);
            GL.BindVertexArray(_squareVao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _squareVbo);


            GL.EnableVertexAttribArray(0);
            GL.EnableVertexAttribArray(1);
            GL.EnableVertexAttribArray(2);

            GL.Arb.VertexAttribDivisor(1, 1);
            GL.Arb.VertexAttribDivisor(2, 1);

            Loaded = true;
        }

        public void UnLoad()
        {
            throw new NotImplementedException();
        }

        private readonly List<float> _positions = new List<float>();
        private readonly List<float> _rotations = new List<float>();
        private float[] _squareVertices;
        private int _count;

        public void Update(double delta)
        {
            _count = 0;
            foreach (var o in _gameObjects)
            {
                _positions.Add((float)o.Position.X);
                _positions.Add((float)o.Position.Y);
                _positions.Add((float)o.Position.Z);
                _rotations.Add((float)o.Rotation.X);
                _rotations.Add((float)o.Rotation.Y);
                _rotations.Add((float)o.Rotation.Z);
                _rotations.Add((float)o.Rotation.W);
                _count++;
            }
            GL.BindVertexArray(_squareVao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, _squareVbo);
            GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr((_squareVertices.Length + _count * 3 + _count * 4) * sizeof(float)), IntPtr.Zero, BufferUsageHint.StreamDraw);


            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, new IntPtr(_squareVertices.Length * sizeof(float)), _squareVertices);
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr(_squareVertices.Length * sizeof(float)), new IntPtr(_count * 3 * sizeof(float)), _positions.ToArray());
            GL.BufferSubData(BufferTarget.ArrayBuffer, new IntPtr((_squareVertices.Length + _count * 3) * sizeof(float)), new IntPtr(_count * 4 * sizeof(float)), _rotations.ToArray());


            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 0, _squareVertices.Length * sizeof(float));
            GL.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, 0, (_squareVertices.Length + _count * 3) * sizeof(float));


            //_objects.Clear();
            _rotations.Clear();
            _positions.Clear();


        }

        public void Render()
        {
            using (Bind.Asset(_shader))
            {
                GL.BindVertexArray(_squareVao);
                GL.DrawArraysInstanced(BeginMode.Quads, 0, 24, _count);
            }
        }
    }
}
