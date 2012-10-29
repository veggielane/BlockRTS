using BlockRTS.Core.Maths;
using BlockRTS.Core.Physics.Bodys;

namespace BlockRTS.Core.Physics.Forces
{
    public interface IForce
    {
        Vect3 CalculateForce(IBody body);
    }
}
