using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Physics.BoundingBoxes
{
    public interface IBoundingBox
    {
        bool Contains(Vect3 pos);
    }
}
