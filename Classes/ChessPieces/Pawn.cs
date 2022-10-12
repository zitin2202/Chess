using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Classes.ChessPieces;
using Enums;

namespace Classes
{
    public class Pawn : ChessPiece, ISaveDirectionPoints
    {
        public Pawn(FieldPoint p, PlayerSide side) : base(p, side)
        {
            yDirect = (Side == PlayerSide.First ? -1 : 1);


        }

        public override ChPType ChPType => ChPType.Pawn;

        ///значение выше чем у слона и конья, так как с королем и пешкой еще возможно победить.
        ///(по сути, данные очки сюда вводяться только для этого)
        public override int RelativeValue => 4;


        public int yDirect;
        public FieldPoint[] directionPoints => new FieldPoint[3] { new FieldPoint(yDirect, 0), new FieldPoint(yDirect, 1), new FieldPoint(yDirect, -1)};

        public override IEnumerable<IEnumerable<(FieldPoint, TypeMove)>> GetMoves()
        {
            
            int lenMove = (StartPosition ? 2 : 1);

            yield return PartOfMove(directionPoints[0], lenMove, TypeMove.Simple);
            yield return PartOfMove(directionPoints[1], 1, TypeMove.Attack);
            yield return PartOfMove(directionPoints[2], 1, TypeMove.Attack);

        }


    }
}
