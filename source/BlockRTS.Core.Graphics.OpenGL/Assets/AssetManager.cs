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
        private readonly Dictionary<Type, IShaderProgram> _shaderPrograms = new Dictionary<Type, IShaderProgram>();
        
        public void Load()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).ToList();
            //Load Textures
            foreach (var viewtype in types.Where(p => p.IsClass && !p.IsAbstract && typeof(ITexture).IsAssignableFrom(p)))
            {
                var inst = Activator.CreateInstance(viewtype) as ITexture;
                if (inst != null)
                {
                    inst.Load();
                    _textures.Add(viewtype, inst);
                }
            }

            foreach (var viewtype in types.Where(p => p.IsClass && !p.IsAbstract && typeof(IShaderProgram).IsAssignableFrom(p)))
            {
                var inst = Activator.CreateInstance(viewtype) as IShaderProgram;
                if (inst != null)
                {
                    inst.Link();
                    inst.AddUniforms();
                    _shaderPrograms.Add(viewtype, inst);
                }
            }
        }
        
        public ITexture Texture<T>() where T : ITexture
        {
            return _textures.ContainsKey(typeof(T)) ? _textures[typeof(T)] : new DefaultTexture();
        }

        public IShaderProgram Shader<T>() where T : IShaderProgram
        {

            return _shaderPrograms.ContainsKey(typeof(T)) ? _shaderPrograms[typeof(T)] : new DefaultShaderProgram();
        }

    }
}
