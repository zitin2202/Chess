using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.ChessPieces;
using Enums;
namespace Classes
{
    public class Bishop : ChessPiece, ISaveDirectionPoints
    {
        public Bishop(FieldPoint p, PlayerSide side) : base(p, side)
        {
        }

        public override ChPType ChPType => ChPType.Bishop;

        public override int RelativeValue => 3;

        public override char Sign => 'b';

        public FieldPoint[] directionPoints => new FieldPoint[]{ new FieldPoint(1, 1), new FieldPoint(1, -1), new FieldPoint(-1, 1), new FieldPoint(-1, -1) };

        private int lenMove = 7;

        public override IEnumerable<IEnumerable<(FieldPoint, TypeMove)>> GetMoves()
        {
            return FormingMove(lenMove, directionPoints);

        }
    }
}
