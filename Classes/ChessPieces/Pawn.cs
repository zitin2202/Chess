using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;

namespace Classes
{
    public class Pawn : ChessPiece
    {
        public Pawn(Field f, Point p, PlayerSide side) : base(f, p, side)
        {
        }

        public override ChPType ChPType => ChPType.Pawn;



        public override IEnumerable<(Point, TypeMove)> GetMoves()
        {
            
            int yDirect = (this.Side == PlayerSide.First ? -1 : 1);
            int lenMove = (StartPosition ? 2 : 1);

            var list = PartOfMove(new Point(yDirect, 0), lenMove, TypeMove.Simple);

            list = list.Concat(PartOfMove(new Point(yDirect, 1), 1, TypeMove.Attack));
            list = list.Concat(PartOfMove(new Point(yDirect, -1), 1, TypeMove.Attack));



            return list;




        }
    }
}
