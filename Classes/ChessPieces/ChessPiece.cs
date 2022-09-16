using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Enums;
namespace Classes
{

     public abstract class ChessPiece
    {
        public ChessPiece( Field f, Point p, PlayerSide side)
        {
            _f = f;
            _p = p;
            Side = side;

        }
        public abstract ChPType ChPType { get;}
        public PlayerSide Side { get; protected set;}

        public Field _f { get;}

        public Point _p;

        public bool StartPosition = true;
        public abstract IEnumerable<(Point, TypeMove)> GetMoves();

        protected IEnumerable<(Point, TypeMove)> PartOfMove(Point direct, int lenMove,TypeMove type=TypeMove.All)
        {
            int maxX = Field.maxX - 1;
            int maxY = Field.maxY - 1;

            for (int i = 1; i <= lenMove; i++)
            {
                Point newP = new Point(_p.y + direct.y*i, _p.x + direct.x*i);

                if(newP.x <= maxX && newP.x >= 0 && newP.y <= maxY && newP.y >= 0)
                {
                    ChessPiece chP = _f.GetChP(newP);

                    if (chP == null && type != TypeMove.Attack)
                    {
                        yield return (newP, TypeMove.Simple);
                    }

                    else if (chP != null)
                    {
                        if (this.Side != chP.Side && type != TypeMove.Simple)
                        {
                            yield return (newP, TypeMove.Attack);
                            yield break;
                        }

                        else
                        {
                            yield break;
                        }

                    }

                }

                else
                {
                    yield break;
                }
            }
        }
            
        protected IEnumerable<(Point, TypeMove)> FormingMove(int lenMoves,  params Point[] directs)
        {
            IEnumerable<(Point, TypeMove)> list=null;

            foreach (var direct in directs)
            {
                if (list is null)
                {
                    list = PartOfMove(direct,lenMoves);
                }
                else
                {
                    list = list.Concat(PartOfMove(direct, lenMoves));

                }



            }

            return list;

        }



    }
}
