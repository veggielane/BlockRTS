using System;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Graphics;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core
{
    public interface IObjectCreator
    {
        IGameObject CreateGameObject(Type gameObjectType, Vect3 position, Quat rotation);
        IView CreateView(Type viewType, IGameObject gameObject);
        IBatchView CreateBatchView(Type viewType);
       // void Bind(Type )
    }
}