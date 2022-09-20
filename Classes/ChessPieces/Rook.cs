using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;

namespace Classes
{
    public class Rook : ChessPiece
    {
        public Rook(Point p, PlayerSide side) : base(p,side)
        {
        }

        public override ChPType ChPType => ChPType.Rook;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {

            return FormingMove(7, new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1));

        }
    }
}
