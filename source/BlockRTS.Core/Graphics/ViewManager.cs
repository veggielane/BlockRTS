using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Messaging.Messages;

namespace BlockRTS.Core.Graphics
{
    public class ViewManager:IViewManager
    {
        private readonly IObjectCreator _creator;
        public IMessageBus Bus { get; private set; }
        public ICamera Camera { get; set; }

        public IDictionary<IGameObject,IView> Views { get; private set; }

        private readonly Dictionary<Type, IBatchView> _batchViews = new Dictionary<Type, IBatchView>();

        private readonly Dictionary<Type, Type> _availableViews = new Dictionary<Type, Type>();

        public ViewManager(IMessageBus bus, ICamera camera, IObjectCreator creator)
        {
            _creator = creator;
            Bus = bus;
            Camera = camera;
            Views = new ConcurrentDictionary<IGameObject, IView>();

            foreach (var viewtype in AppDomain.CurrentDomain.GetAssemblies().ToList().SelectMany(s => s.GetTypes()).Where(p => typeof(IView).IsAssignableFrom(p) && p.IsClass && p.IsDefined(typeof(BindViewAttribute), false)).ToList())
            {
                var bindViewAttribute = viewtype.GetCustomAttributes(typeof (BindViewAttribute), true).Cast<BindViewAttribute>().ToList();
                foreach (var viewAttribute in bindViewAttribute)
                {
                    _availableViews.Add(viewAttribute.GameObjectType, viewtype);
                }
            }

            Bus.OfType<GameObjectCreated>().Subscribe(Created);
        }

        private void Created(GameObjectCreated m)
        {
            if(_availableViews.ContainsKey(m.GameObject.GetType()))
            {
                Views.Add(m.GameObject, _creator.CreateView(_availableViews[m.GameObject.GetType()], m.GameObject));
            }else
            {
                Bus.Add(new DebugMessage(m.TimeSent, "Can't find view for: {0}".Fmt(m.GameObject.GetType())));
            }
        }

        public void Load()
        {
            //load Batch
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).ToList();
            foreach (var batch in types.Where(p => p.IsClass && !p.IsAbstract && typeof(IBatchView).IsAssignableFrom(p)))
            {
                var inst = _creator.CreateBatchView(batch);
                if (inst != null)
                {
                    inst.Load();
                    _batchViews.Add(batch, inst);
                }
            }
            foreach (var gameObjectView in Views)
            {
                if (!gameObjectView.Value.Loaded)
                    gameObjectView.Value.Load();
            }
        }

        public void Update(double delta)
        {
            foreach (var gameObjectView in Views)
            {
                if (!gameObjectView.Value.Loaded)
                    gameObjectView.Value.Load();
                gameObjectView.Value.Update(delta);
            }
            foreach (var batchView in _batchViews.Values)
            {
                batchView.Update(delta);
            }
        }

        public void Render()
        {
            foreach (var view in Views.Values)
            {
                view.Render();
            }
            foreach (var batchView in _batchViews.Values)
            {
                batchView.Render();
            }
        }

        public IBatchView Batch<T>() where T : IBatchView
        {
            return _batchViews[typeof(T)];
        }
    }
}
