using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.ChessPieces;
using Enums;
namespace Classes
{
    public class Bishop : ChessPiece, SaveDirectionPoints
    {
        public Bishop(Point p, PlayerSide side) : base(p, side)
        {
        }

        public override ChPType ChPType => ChPType.Bishop;

        public Point[] directionPoints => new Point[]{ new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1) };

        private int lenMove = 7;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            return FormingMove(lenMove, directionPoints);

        }
    }
}
