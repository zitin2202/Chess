using System;
using System.Collections.Generic;
using System.Text;
using Enums;
using Classes.ChessPieces;
namespace Classes
{
    public class Knight : ChessPiece, SaveDirectionPoints
    {
        public Knight(Point p, PlayerSide side) : base(p, side)
        {
        }

        public override ChPType ChPType => ChPType.Knight;

        public Point[] directionPoints => new Point[] {new Point(2, 1), new Point(2, -1),
                 new Point(-2, 1), new Point(-2, -1), new Point(1, 2),
                 new Point(-1, 2), new Point(1, -2), new Point(-1, -2)};

        private int lenMove = 1;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            return FormingMove(lenMove, directionPoints);
        }
    }
}
