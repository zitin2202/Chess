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
        public abstract IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves();

        protected IEnumerable<(Point, TypeMove)> PartOfMove(Point direct, int lenMove,TypeMove type=TypeMove.All) //определяет один конктретный ход у
                                                                                                                  //фигуры или одну линию ходов, если фигура ходит линиями.
        {
            int maxX = Field.maxX - 1;
            int maxY = Field.maxY - 1;

            for (int i = 1; i <= lenMove; i++)//длина линии
            {
                Point newP = new Point(_p.y + direct.y*i, _p.x + direct.x*i);// вычисление клетки следующего шага

                if (newP.x <= maxX && newP.x >= 0 && newP.y <= maxY && newP.y >= 0) //определение, находиться ли клетка внутри поля
                {
                    yield return (newP, type);

                }

                else //если фигура вышла за границы поля, линия обрывается
                {
                    yield break;
                }
            }
        }
            
        protected IEnumerable<IEnumerable<(Point, TypeMove)>>  FormingMove(int lenMoves,  params Point[] directs) //формирует набор из линий. Для пешки метод не применяется
        {

            foreach (var direct in directs)
            {

                yield return PartOfMove(direct, lenMoves);


            }


        }



    }
}
