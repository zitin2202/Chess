using System;
using Enums;
namespace Interfaces
{
    public interface IShape
    {
        ShapeType ShapeType { get; protected set; }
        PlayerSide Side { get; }

        void GetMoves();



    }
}
