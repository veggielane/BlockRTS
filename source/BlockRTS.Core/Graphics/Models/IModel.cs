using BlockRTS.Core.Graphics.Meshing;

namespace BlockRTS.Core.Graphics.Models
{
    public interface IModel
    {
        Mesh ToMesh();
    }
}