using BlockRTS.Core.Shapes;

namespace BlockRTS.Core.GameObjects
{
    public interface IHasBoundingSphere
    {
        Sphere BoundingSphere { get; }
    }
}