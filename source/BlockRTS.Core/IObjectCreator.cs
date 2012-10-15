using System;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core
{
    public interface IObjectCreator
    {
        IGameObject CreateGameObject(Type gameObjectType, Mat4 transformation);
        IView CreateView(Type viewType, IGameObject gameObject);
        IBatchView CreateBatchView(Type viewType);
       // void Bind(Type )
    }
}