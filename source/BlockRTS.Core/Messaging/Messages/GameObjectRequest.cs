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
        public Vect3 Position { get; set; }
        public Quat Rotation { get; set; }
        public GameObjectRequest(IGameTime timeSent, Type gameObjectType, Vect3 position, Quat rotation)
            : base(timeSent)
        {
            GameObjectType = gameObjectType;
            Position = position;
            Rotation = rotation;
        }

        public static GameObjectRequest Create<T>(IGameTime timeSent, Vect3 position, Quat rotation)
        {
            return new GameObjectRequest(timeSent, typeof(T), position,rotation);
        }

        public override string ToString()
        {
            return "<{0}> {1}".Fmt(GetType().Name, GameObjectType);
        }
    }
}
