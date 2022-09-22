using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using Classes.ChessPieces;
namespace Classes
{
    public class Rook : ChessPiece, SaveDirectionPoints
    {
        public Rook(Point p, PlayerSide side) : base(p,side)
        {
            
        }

        public override ChPType ChPType => ChPType.Rook;

        public Point[] directionPoints => new Point[] { new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1)};

        private int lenMove = 7;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {

            return FormingMove(lenMove, directionPoints);

        }
    }
}
