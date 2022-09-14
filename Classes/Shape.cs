using System;
using Enums;
namespace Classes
{

     public abstract class Shape
    {
        public Shape(PlayerSide side)
        {
            Side = side;
        }
        public abstract ShapeType ShapeType { get;}
        public PlayerSide Side { get; protected set;}

        public abstract void GetMoves(Field field, int x, int y);
    }
}
