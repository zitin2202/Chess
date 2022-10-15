using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using Classes.ChessPieces;
namespace Classes
{
    public class Rook : ChessPiece, ISaveDirectionPoints
    {
        public Rook(FieldPoint p, PlayerSide side) : base(p, side)
        {

        }

        public override ChPType ChPType => ChPType.Rook;

        public override int RelativeValue => 5;

        public override char Sign => 'r';


        public FieldPoint[] directionPoints => new FieldPoint[] { new FieldPoint(1, 0), new FieldPoint(-1, 0), new FieldPoint(0, 1), new FieldPoint(0, -1)};

        private int lenMove = 7;

        public override IEnumerable<IEnumerable<(FieldPoint, TypeMove)>> GetMoves()
        {

            return FormingMove(lenMove, directionPoints);

        }
    }
}
