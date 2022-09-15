using System;
using System.Collections.Generic;
using Enums;
namespace Classes
{
    public class Field
    {

        public const int maxX  = 8;
        public const int maxY  = 8;

        private Shape[,] _cells;

        public Field()
        {
            _cells = new Shape[maxY, maxX]
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
        public void Set (int y, int x, Shape shape)
        {
            _cells[y, x] = shape;
        }

        public Shape Get(int y, int x)
        {
            return _cells[y, x];
        }

        public IEnumerable<(Point, TypeMove)> OccupDel(IEnumerable<(Point, TypeMove)> list)
        {
            foreach (var i in list)
            {
                if ((i.Item2 == TypeMove.Simple && this.Get(i.Item1.y, i.Item1.x) == null) || (i.Item2 == TypeMove.Attack && this.Get(i.Item1.y, i.Item1.x) != null))
                {
                    yield return (i.Item1, i.Item2);
                }
            }
        }
    }
}
