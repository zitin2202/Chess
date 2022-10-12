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
             { new King(new FieldPoint(0,0),PlayerSide.Second),null,null,null,null,null,null,null },
             { new Bishop(new FieldPoint(1,0),PlayerSide.First),null,null,null,null,null,null,new Rook(new FieldPoint(1,7),PlayerSide.First) },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { new Bishop(new FieldPoint(4,0),PlayerSide.Second),null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { new King(new FieldPoint(7,0),PlayerSide.First),null,null,null,null,null,null,null },
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
