using System;
using Enums;
namespace Classes
{
    public class Field
    {

        private Shape[,] _cells;

        public Field()
        {
            _cells = new Shape[8, 8]
            {
             { new Rook(PlayerSide.Second), new Knight(PlayerSide.Second), new Bishop(PlayerSide.Second), new King(PlayerSide.Second), new Queen(PlayerSide.Second), new Bishop(PlayerSide.Second), new Knight(PlayerSide.Second), new Rook(PlayerSide.Second), },
             { new Pawn(PlayerSide.Second), new Pawn(PlayerSide.Second),   new Pawn(PlayerSide.Second),   new Pawn(PlayerSide.Second), new Pawn(PlayerSide.Second),  new Pawn(PlayerSide.Second),   new Pawn(PlayerSide.Second),   new Pawn(PlayerSide.Second), },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { null,null,null,null,null,null,null,null },
             { new Pawn(PlayerSide.First), new Pawn(PlayerSide.First),   new Pawn(PlayerSide.First),   new Pawn(PlayerSide.First), new Pawn(PlayerSide.First),  new Pawn(PlayerSide.First),   new Pawn(PlayerSide.First),   new Pawn(PlayerSide.First), },
             { new Rook(PlayerSide.First), new Knight(PlayerSide.First), new Bishop(PlayerSide.First), new King(PlayerSide.First), new Queen(PlayerSide.First), new Bishop(PlayerSide.First), new Knight(PlayerSide.First), new Rook(PlayerSide.First), },
            };


        }
        public void Set (int x, int y, Shape shape)
        {
            _cells[x, y] = shape;
        }

        public Shape Get(int x, int y)
        {
            return _cells[x, y];
        }
    }
}
