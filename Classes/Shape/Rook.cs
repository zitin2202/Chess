using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;

namespace Classes
{
    public class Rook : Shape
    {
        public Rook(PlayerSide side) : base(side)
        {
        }

        public override ShapeType ShapeType => ShapeType.Rook;

        public override IEnumerable<(Point, TypeMove)> GetMoves(Point p)
        {
            //var list = PartOfMove(p, new Point(1, 0));
            //list = list.Concat(PartOfMove(p, new Point(-1, 0)));
            //list = list.Concat(PartOfMove(p, new Point(0, 1)));
            //list = list.Concat(PartOfMove(p, new Point(0, -1)));
            //return list;

            return FormingMove(p, 7,new Point(1, 0), new Point(-1, 0), new Point(0, 1), new Point(0, -1));

        }
    }
}
