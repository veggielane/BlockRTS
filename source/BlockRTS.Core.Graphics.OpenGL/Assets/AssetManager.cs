using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Graphics.OpenGL.Assets.Textures;
using BlockRTS.Core.Graphics.OpenGL.Shaders;

namespace BlockRTS.Core.Graphics.OpenGL.Assets
{
    public class AssetManager:IAssetManager
    {
        private readonly Dictionary<Type,ITexture> _textures = new Dictionary<Type, ITexture>();
        private readonly Dictionary<Type, IShaderProgram> _shaderPrograms = new Dictionary<Type, IShaderProgram>();
        private readonly Dictionary<Type, IUBO> _ubos = new Dictionary<Type, IUBO>();

        private IObjectCreator _objectCreator;

        public AssetManager(IObjectCreator objectCreator)
        {
            _objectCreator = objectCreator;
        }

        public void Load()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).ToList();

            foreach (var ubotype in types.Where(p => p.IsClass && !p.IsAbstract && typeof(IUBO).IsAssignableFrom(p)))
            {
                var inst = _objectCreator.Create<IUBO>(ubotype);
                if (inst != null)
                {
                    _ubos.Add(ubotype, inst);
                }
            }

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
            //load shaders
            foreach (var shadertype in types.Where(p => p.IsClass && !p.IsAbstract && typeof(IShaderProgram).IsAssignableFrom(p)))
            {
                var inst = _objectCreator.Create<IShaderProgram>(shadertype);
                if (inst != null)
                {
                    _shaderPrograms.Add(shadertype, inst);
                }
            }

        }
        
        public ITexture Texture<T>() where T : ITexture
        {
            return _textures.ContainsKey(typeof(T)) ? _textures[typeof(T)] : new DefaultTexture();
        }

        public T Shader<T>() where T : IShaderProgram
        {
            if(_shaderPrograms.ContainsKey(typeof(T)))
            {
               return (T)_shaderPrograms[typeof (T)];
            }
            throw new Exception("shader not found");
        }

        public T UBO<T>() where T: IUBO
        {
            return _ubos.Where(kvp => kvp.Key == typeof (T)).Select(kvp=>(T)kvp.Value).Single();
        }
    }
}
