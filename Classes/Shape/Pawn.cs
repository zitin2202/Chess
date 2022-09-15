using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
using Interfaces;

namespace Classes
{
    public class Pawn : Shape
    {
        public Pawn(PlayerSide side):base(side)
        {
        }

        public override ShapeType ShapeType => ShapeType.Pawn;

        public bool StartPosition = true;
        public bool LineMoves = true;


        public override IEnumerable<(Point, TypeMove)> GetMoves(Point p)
        {
            
            int yDirect = (this.Side == PlayerSide.First ? -1 : 1);
            int lenMove = (StartPosition ? 2 : 1);

            var list = PartOfMove(p, new Point(yDirect, 0), lenMove, TypeMove.Simple);

            list = list.Concat(PartOfMove(p, new Point(yDirect, 1), 1, TypeMove.Attack));
            list = list.Concat(PartOfMove(p, new Point(yDirect, -1), 1, TypeMove.Attack));



            return list;




        }
    }
}
