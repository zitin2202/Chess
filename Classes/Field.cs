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
             { new Rook(this,new Point(0,0), PlayerSide.Second), new Knight(this,new Point(0,1),PlayerSide.Second), new Bishop(this,new Point(0,2),PlayerSide.Second), new King(this,new Point(0,3),PlayerSide.Second), new Queen(this,new Point(0,4),PlayerSide.Second), new Bishop(this,new Point(0,5),PlayerSide.Second), new Knight(this,new Point(0,6),PlayerSide.Second), new Rook(this,new Point(0,7),PlayerSide.Second), },
             { new Pawn(this,new Point(1,0),PlayerSide.Second),  new Pawn(this,new Point(1,1),PlayerSide.Second),   new Pawn(this,new Point(1,2),PlayerSide.Second),   new Pawn(this,new Point(1,3),PlayerSide.Second), new Pawn(this,new Point(1,4),PlayerSide.Second),  new Pawn(this,new Point(1,5),PlayerSide.Second),   new Pawn(this,new Point(1,6),PlayerSide.Second),   new Pawn(this,new Point(1,7),PlayerSide.Second), },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { new Pawn(this,new Point(6,0),PlayerSide.First), new Pawn(this,new Point(6,1),PlayerSide.First),   new Pawn(this,new Point(6,2),PlayerSide.First),   new Pawn(this,new Point(6,3),PlayerSide.First), new Pawn(this,new Point(6,4),PlayerSide.First),  new Pawn(this,new Point(6,5),PlayerSide.First),   new Pawn(this,new Point(6,6),PlayerSide.First),   new Pawn(this,new Point(6,7),PlayerSide.First), },
             { new Rook(this,new Point(7,0),PlayerSide.First), new Knight(this,new Point(7,1),PlayerSide.First), new Bishop(this,new Point(7,2),PlayerSide.First), new King(this,new Point(7,3),PlayerSide.First), new Queen(this,new Point(7,4),PlayerSide.First), new Bishop(this,new Point(7,5),PlayerSide.First), new Knight(this,new Point(7,6),PlayerSide.First), new Rook(this,new Point(7,7),PlayerSide.First), },
            };


        }
        public void SetChP (Point p, ChessPiece chP)
        {
            _cells[p.y, p.x] = chP;

            if (chP != null)
            {
                chP._p = p;
                chP.StartPosition = false;
            }
                
        }

        public ChessPiece GetChP(Point p)
        {
            return _cells[p.y, p.x];
        }


    }
}
