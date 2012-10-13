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
        Dictionary<IGameObject, IView> Views { get; }
        void Load();
        void Update(double delta);
        void Render();
    }
}
