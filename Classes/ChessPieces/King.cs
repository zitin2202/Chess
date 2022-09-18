using System;
using System.Collections.Generic;
using System.Text;
using Enums;
namespace Classes
{
    public class King : ChessPiece
    {
        public King(Field f, Point p, PlayerSide side) : base(f, p, side)
        {
        }

        public override ChPType ChPType => ChPType.King;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            return FormingMove(1,new Point(1, 0), new Point(-1, 0),
                new Point(0, 1), new Point(0, -1), new Point(1, 1),
                new Point(1, -1), new Point(-1, 1), new Point(-1, -1));
        }
    }
}
