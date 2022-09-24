using System;
using System.Collections.Generic;
using System.Text;
using Classes.ChessPieces;
using Enums;
namespace Classes
{
    public class King : ChessPiece, ISaveDirectionPoints
    {
        public King(Point p, PlayerSide side) : base(p, side)
        {
        }

        public override ChPType ChPType => ChPType.King;

        public  Point[] directionPoints => new Point[] {new Point(1, 0), new Point(-1, 0),
                new Point(0, 1), new Point(0, -1), new Point(1, 1),
                new Point(1, -1), new Point(-1, 1), new Point(-1, -1)};


        private int lenMove = 1;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            return FormingMove(lenMove, directionPoints);
        }
    }
}
