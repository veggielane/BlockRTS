using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Messaging.Messages;

namespace BlockRTS.Core.GameObjects
{
    public class GameObjectFactory : IGameObjectFactory
    {
        private readonly IObjectCreator _creator;

        public ConcurrentDictionary<Guid, IGameObject> GameObjects { get; protected set; }
        public IMessageBus Bus { get; protected set; }

        public GameObjectFactory(IMessageBus bus,IObjectCreator creator)
        {
            _creator = creator;
            GameObjects = new ConcurrentDictionary<Guid, IGameObject>();
            Bus = bus;
            Bus.OfType<GameObjectRequest>().Subscribe(CreateGameObject);
           // Bus.OfType<DestroyGameObject>().Subscribe(DestroyGameObject);
        }


        public void CreateGameObject(GameObjectRequest m)
        {
            var go = _creator.CreateGameObject(m.GameObjectType,m.Transformation);
            if(go != null)
            {
                 GameObjects.TryAdd(go.Id, go);
                 Bus.Add(new GameObjectCreated(m.TimeSent, go));
            }
        }

        //public void DestroyGameObject(DestroyGameObject m)
        //{
           // throw new NotImplementedException();
            /*
            IGameObject gameobject;
            if(GameObjects.TryRemove(m.GameObject.Id, out gameobject))

             */
        //}
    }
}
