using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.Messaging.Messages
{
    public class GameObjectRequest : BaseMessage
    {
        public Type GameObjectType { get; private set; }
        public Mat4 Transformation { get; private set; }

        public GameObjectRequest(IGameTime timeSent, Type gameObjectType, Mat4 transformation) : base(timeSent)
        {
            GameObjectType = gameObjectType;
            Transformation = transformation;
        }

        public static GameObjectRequest Create<T>(IGameTime timeSent, Mat4 transformation)
        {
            return new GameObjectRequest(timeSent,typeof(T),transformation);
        }

        public override string ToString()
        {
            return "<{0}> {1}".Fmt(GetType().Name, GameObjectType);
        }
    }
}
