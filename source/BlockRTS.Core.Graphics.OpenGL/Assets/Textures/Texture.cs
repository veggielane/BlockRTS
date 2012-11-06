using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL;
using BlockRTS.Core.Graphics.OpenGL.Buffers;
using BlockRTS.Core.Graphics.OpenGL.Shaders;
using OpenTK.Graphics.OpenGL;

namespace BlockRTS.Core.Graphics.OpenGL
{

    public interface ITexture:IAsset
    {
        int Handle { get; }
        Image Source { get; }
        void Load();
    }
    /*
    public class FBOTexture : BaseTexture
    {
        public override Image Source
        {
            get
            {
                var img = new Bitmap(2, 2);
                using (var gfx = System.Drawing.Graphics.FromImage(img))
                using (var brush = new SolidBrush(Color.White))
                {
                    gfx.FillRectangle(brush, 0, 0, 2, 2);
                    gfx.DrawLine(Pens.Green, 0, 0, 20, 20);
                    return img;
                }
            }
        }
    }*/

    public abstract class BaseTexture : ITexture
    {
        private int _handle;
        public int Handle
        {
            get { return _handle; }
            private set { _handle = value; }
        }

        public abstract Image Source { get; }

        public BaseTexture()
        {
            GL.GenTextures(1, out _handle);
        }

        public void Load()
        {

            var bitmap = new Bitmap(Source);
            using (new Bind(this))
            {
                var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
                bitmap.UnlockBits(data);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            }
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void UnBind()
        {
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }
    }

}
