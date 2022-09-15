using System;
using System.Collections.Generic;
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

        public abstract IEnumerable<(Point, TypeMove)> GetMoves( Point p);

        protected IEnumerable<(Point, TypeMove)> FormingMove(Point p,  Point direct, int lenMove,TypeMove type)
        {
            for (int i = 1; i <= lenMove; i++)
            {
                Point newP = new Point(p.y + direct.y*i, p.x + direct.x*i);
                yield return (newP, type);
            }
        }
    }
}
