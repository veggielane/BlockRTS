using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockRTS.Core.Maths;
using BlockRTS.Core.Messaging;
using BlockRTS.Core.Timing;

namespace BlockRTS.Core.GameObjects
{
    class Planet:BaseGameObject
    {
        public Planet(IMessageBus bus, Vect3 position, Quat rotation) : base(bus, position, rotation)
        {
            var PlanetSize = 9;
            for (int x = 0; x < PlanetSize; ++x)
            {
                for (int y = 0; y < PlanetSize; ++y)
                {
                    for (int z = 0; z < PlanetSize; ++z)
                    {
                        if ((Math.Pow(x - (PlanetSize - 1) / 2.0, 2) + Math.Pow(y - (PlanetSize - 1) / 2.0, 2) + Math.Pow(z - (PlanetSize - 1) / 2.0, 2) - Math.Pow(PlanetSize / 2.0, 2)) <= 0)
                        {
                            /*


                            var child = new VoxelGreen(_idTally++, Bus)
                            {
                                Position = new Vect3(x, y, z)
                            };
                            GameObjects.Add(child);
                            item.AddChild(child);*/
                        }
                    }
                }
            }
        }

        public override void Update(TickTime delta)
        {
            
        }
    }
}
