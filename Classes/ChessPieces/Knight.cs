using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Classes
{
    public class Knight : ChessPiece
    {
        public Knight(Field f, Point p, PlayerSide side) : base(f, p, side)
        {
        }

        public override ChPType ChPType => ChPType.Knight;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            return FormingMove(1, new Point(2, 1), new Point(2, -1),
                 new Point(-2, 1), new Point(-2, -1), new Point(1, 2),
                 new Point(-1, 2), new Point(1, -2), new Point(-1, -2));
        }
    }
}
