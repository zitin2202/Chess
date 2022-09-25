using System;
using System.Collections.Generic;
using Enums;
namespace Classes
{
    public class Field
    {

        public const int maxX  = 8;
        public const int maxY  = 8;

        private ChessPiece[,] _cells;

        public Field()
        {
            _cells = new ChessPiece[maxY, maxX]
            {
             { new Rook(new Point(0,0), PlayerSide.Second), new Knight(new Point(0,1),PlayerSide.Second), new Bishop(new Point(0,2),PlayerSide.Second), new Queen(new Point(0,3),PlayerSide.Second), new King(new Point(0,4),PlayerSide.Second), new Bishop(new Point(0,5),PlayerSide.Second), new Knight(new Point(0,6),PlayerSide.Second), new Rook(new Point(0,7),PlayerSide.Second), },
             { new Pawn(new Point(1,0),PlayerSide.Second),  new Pawn(new Point(1,1),PlayerSide.Second),   new Pawn(new Point(1,2),PlayerSide.Second),   new Pawn(new Point(1,3),PlayerSide.Second), new Pawn(new Point(1,4),PlayerSide.Second),  new Pawn(new Point(1,5),PlayerSide.Second),   new Pawn(new Point(1,6),PlayerSide.Second),   new Pawn(new Point(1,7),PlayerSide.Second), },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { new Pawn(new Point(6,0),PlayerSide.First), new Pawn(new Point(6,1),PlayerSide.First),   new Pawn(new Point(6,2),PlayerSide.First),   new Pawn(new Point(6,3),PlayerSide.First), new Pawn(new Point(6,4),PlayerSide.First),  new Pawn(new Point(6,5),PlayerSide.First),   new Pawn(new Point(6,6),PlayerSide.First),   new Pawn(new Point(6,7),PlayerSide.First), },
             { new Rook(new Point(7,0),PlayerSide.First), new Knight(new Point(7,1),PlayerSide.First), new Bishop(new Point(7,2),PlayerSide.First), new Queen(new Point(7,3),PlayerSide.First),new King(new Point(7,4),PlayerSide.First), new Bishop(new Point(7,5),PlayerSide.First), new Knight(new Point(7,6),PlayerSide.First), new Rook(new Point(7,7),PlayerSide.First), },
            };


        }
        public bool SetChP (Point p, ChessPiece chP)
        {
            if (Exception.ValidationCell(p))
            {
                _cells[p.y, p.x] = chP;

                if (chP != null)
                {
                    chP._p = p;
                    chP.StartPosition = false;
                }
            }

            return false;

                
        }

        public  ChessPiece GetChP(Point p)
        {
            if (Exception.ValidationCell(p))
            {
                return _cells[p.y, p.x];
            }

            return null;
        }


    }
}
