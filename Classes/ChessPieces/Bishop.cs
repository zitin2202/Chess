using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
namespace Classes
{
    public class Bishop : ChessPiece
    {
        public Bishop(Point p, PlayerSide side) : base(p, side)
        {
        }

        public override ChPType ChPType => ChPType.Bishop;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            return FormingMove(7, new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1));

        }
    }
}
