using System;
using System.Collections.Generic;
using System.Text;
using Enums;
using Classes.ChessPieces;
namespace Classes
{
    public class Knight : ChessPiece, ISaveDirectionPoints
    {
        public Knight(FieldPoint p, PlayerSide side) : base(p, side)
        {
        }

        public override ChPType ChPType => ChPType.Knight;

        public override int RelativeValue => 3;

        public FieldPoint[] directionPoints => new FieldPoint[] {new FieldPoint(2, 1), new FieldPoint(2, -1),
                 new FieldPoint(-2, 1), new FieldPoint(-2, -1), new FieldPoint(1, 2),
                 new FieldPoint(-1, 2), new FieldPoint(1, -2), new FieldPoint(-1, -2)};

        private int lenMove = 1;

        public override IEnumerable<IEnumerable<(FieldPoint, TypeMove)>> GetMoves()
        {
            return FormingMove(lenMove, directionPoints);
        }
    }
}
