using System;
using BlockRTS.Core.Maths;

namespace BlockRTS.Core.GameObjects
{
    public interface IGameObjectCreator
    {
        IGameObject Create(Type gameObjectType, Mat4 transformation);
    }
}