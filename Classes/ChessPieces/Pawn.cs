using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.ChessPieces;
using Enums;

namespace Classes
{
    public class Pawn : ChessPiece, SaveDirectionPoints
    {
        public Pawn(Point p, PlayerSide side) : base(p, side)
        {
            int yDirect = (Side == PlayerSide.First ? -1 : 1);
            directionPoints[0] = new Point(yDirect, 0);
            directionPoints[1] = new Point(yDirect, 1);
            directionPoints[2] = new Point(yDirect, -1);

    }

        public override ChPType ChPType => ChPType.Pawn;

        public Point[] directionPoints => new Point[3];

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            
            int lenMove = (StartPosition ? 2 : 1);

            yield return PartOfMove(directionPoints[0], lenMove, TypeMove.Simple);
            yield return PartOfMove(directionPoints[1], 1, TypeMove.Attack);
            yield return PartOfMove(directionPoints[2], 1, TypeMove.Attack);




        }
    }
}
