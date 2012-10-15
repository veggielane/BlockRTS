using System.Collections.Generic;
using BlockRTS.Core.GameObjects;

namespace BlockRTS.Core.Graphics
{
    public interface IBatchView
    {

        void Add(IGameObject gameObject);
        bool Loaded { get; }
        void Load();
        void UnLoad();
        void Update(double delta);
        void Render();
    }
}