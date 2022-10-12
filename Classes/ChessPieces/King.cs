using System;
using System.Collections.Generic;
using System.Text;
using Classes.ChessPieces;
using Enums;
namespace Classes
{
    public class King : ChessPiece, ISaveDirectionPoints
    {
        public King(FieldPoint p, PlayerSide side) : base(p, side)
        {
        }

        public override ChPType ChPType => ChPType.King;

        public override int RelativeValue => 0;


        public FieldPoint[] directionPoints => new FieldPoint[] {new FieldPoint(1, 0), new FieldPoint(-1, 0),
                new FieldPoint(0, 1), new FieldPoint(0, -1), new FieldPoint(1, 1),
                new FieldPoint(1, -1), new FieldPoint(-1, 1), new FieldPoint(-1, -1),new FieldPoint(0,2),new FieldPoint(0,-2)};


        private int lenMove = 1;

        public override IEnumerable<IEnumerable<(FieldPoint, TypeMove)>> GetMoves()
        {
            for (int i = 0; i < 8; i++)
            {
                yield return PartOfMove(directionPoints[i], lenMove);

            }
            for (int i = 8; i < directionPoints.Length; i++)
            {
                yield return PartOfMove(directionPoints[i], lenMove,TypeMove.Сastling);

            }

        }
    }
}
