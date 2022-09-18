using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
namespace Classes
{

    public class Queen : ChessPiece
    {
        public Queen(Field f, Point p, PlayerSide side) : base(f, p, side)
        {
        }

        public override ChPType ChPType => ChPType.Queen;

        public override IEnumerable<IEnumerable<(Point, TypeMove)>> GetMoves()
        {
            return new Rook(_f, _p,Side).GetMoves().Concat(new Bishop(_f,_p, Side).GetMoves());
            
        }
    }
}
