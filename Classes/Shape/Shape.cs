using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        protected IEnumerable<(Point, TypeMove)> PartOfMove(Point p,  Point direct, int lenMove=7,TypeMove type=TypeMove.All)
        {
            int maxX = Field.maxX - 1;
            int maxY = Field.maxY - 1;
            for (int i = 1; i <= lenMove; i++)
            {
                Point newP = new Point(p.y + direct.y*i, p.x + direct.x*i);

                if(newP.x <= maxX && newP.x >= 0 && newP.y <= maxY && newP.y >= 0)
                {
                    yield return (newP, type);

                }

                else
                {
                    yield break;
                }
            }
        }
            
        protected IEnumerable<(Point, TypeMove)> FormingMove(Point p, int lenMoves, params Point[] directs)
        {
            IEnumerable<(Point, TypeMove)> list=null;

            foreach (var direct in directs)
            {
                if (list is null)
                {
                    list = PartOfMove(p, direct,lenMoves);
                    continue;
                }

                list = list.Concat(PartOfMove(p, direct, lenMoves));


            }

            return list;

        }



    }
}
