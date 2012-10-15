using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.GameObjects;
using BlockRTS.Core.Messaging;

namespace BlockRTS.Core.Graphics
{
    public interface IViewManager:IHasMessageBus
    {
        IDictionary<IGameObject, IView> Views { get; }
        void Load();
        void Update(double delta);
        void Render();

        IBatchView Batch<T>() where T : IBatchView;
    }
}
