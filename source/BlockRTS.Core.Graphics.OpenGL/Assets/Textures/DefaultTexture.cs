using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockRTS.Core.Graphics.OpenGL.Assets.Textures
{
    public class DefaultTexture : BaseTexture
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
                    return img;
                }
            }
        }
    }
}
