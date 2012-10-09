using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Shaders;

namespace BlockRTS.Core.Graphics.OpenGL.Assets
{
    public class AssetManager
    {
        private readonly Dictionary<Type,ITexture> _textures = new Dictionary<Type, ITexture>();
        
        public void Load()
        {

            //Load Texture
            foreach (var viewtype in AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes()).Where(p => typeof(ITexture).IsAssignableFrom(p) && p.IsClass && !p.IsAbstract).ToList())
            {
                var inst = Activator.CreateInstance(viewtype) as ITexture;
                if (inst != null)
                {
                    inst.Load();
                    _textures.Add(viewtype, inst);
                }
            }
        }
        
        public ITexture Texture<T>() where T : ITexture
        {
            return _textures.ContainsKey(typeof(T)) ? _textures[typeof(T)] : new DefaultTexture();
        }
    }
}
