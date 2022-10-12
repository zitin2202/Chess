using System;
using System.Collections.Generic;
using Enums;
namespace Classes
{
    public class Field
    {

        public const int maxX = 8;
        public const int maxY = 8;

        private ChessPiece[,] _cells;


        public Field()
        {

            FieldReset();

        }

        public void FieldReset()
        {
            _cells = new ChessPiece[maxY, maxX]
            {
             { new Rook(new FieldPoint(0,0), PlayerSide.Second), new Knight(new FieldPoint(0,1),PlayerSide.Second), new Bishop(new FieldPoint(0,2),PlayerSide.Second), new Queen(new FieldPoint(0,3),PlayerSide.Second), new King(new FieldPoint(0,4),PlayerSide.Second), new Bishop(new FieldPoint(0,5),PlayerSide.Second), new Knight(new FieldPoint(0,6),PlayerSide.Second), new Rook(new FieldPoint(0,7),PlayerSide.Second), },
             { new Pawn(new FieldPoint(1,0),PlayerSide.Second),  new Pawn(new FieldPoint(1,1),PlayerSide.Second),   new Pawn(new FieldPoint(1,2),PlayerSide.Second),   new Pawn(new FieldPoint(1,3),PlayerSide.Second), new Pawn(new FieldPoint(1,4),PlayerSide.Second),  new Pawn(new FieldPoint(1,5),PlayerSide.Second),   new Pawn(new FieldPoint(1,6),PlayerSide.Second),   new Pawn(new FieldPoint(1,7),PlayerSide.Second), },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { new Pawn(new FieldPoint(6,0),PlayerSide.First), new Pawn(new FieldPoint(6,1),PlayerSide.First),   new Pawn(new FieldPoint(6,2),PlayerSide.First),   new Pawn(new FieldPoint(6,3),PlayerSide.First), new Pawn(new FieldPoint(6,4),PlayerSide.First),  new Pawn(new FieldPoint(6,5),PlayerSide.First),   new Pawn(new FieldPoint(6,6),PlayerSide.First),   new Pawn(new FieldPoint(6,7),PlayerSide.First), },
             { new Rook(new FieldPoint(7,0),PlayerSide.First), new Knight(new FieldPoint(7,1),PlayerSide.First), new Bishop(new FieldPoint(7,2),PlayerSide.First), new Queen(new FieldPoint(7,3),PlayerSide.First),new King(new FieldPoint(7,4),PlayerSide.First), new Bishop(new FieldPoint(7,5),PlayerSide.First), new Knight(new FieldPoint(7,6),PlayerSide.First), new Rook(new FieldPoint(7,7),PlayerSide.First), },
            };
        }
        public bool SetChP (FieldPoint p, ChessPiece chP)
        {
            if (Validation.ValidationCell(p))
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

        public  ChessPiece GetChP(FieldPoint p)
        {
            if (Validation.ValidationCell(p))
            {
                return _cells[p.y, p.x];
            }

            return null;
        }


    }
}
