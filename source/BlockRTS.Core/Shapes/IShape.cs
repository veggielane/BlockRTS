using BlockRTS.Core.Maths;

namespace BlockRTS.Core.Shapes
{
    public interface IShape
    {
        Vect3 Position { get; set; }
        Quat Rotation { get; set; } 
    }
}