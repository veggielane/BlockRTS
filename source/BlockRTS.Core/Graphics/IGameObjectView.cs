using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;

namespace BlockRTS.Core.Graphics
{
    public interface IGameObjectView
    {
        IGameObject GameObject { get; }
        bool Loaded { get; }
        void Load();
        void UnLoad();
        void Update(double delta);
        void Render(ICamera camera);
    }
}
