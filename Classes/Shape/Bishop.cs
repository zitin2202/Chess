using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enums;
namespace Classes
{
    public class Bishop : Shape
    {
        public Bishop(PlayerSide side) : base(side)
        {
        }

        public override ShapeType ShapeType => ShapeType.Bishop;

        public override IEnumerable<(Point, TypeMove)> GetMoves(Point p)
        {
            //var list = PartOfMove(p, new Point(1, 1));
            //list = list.Concat(PartOfMove(p, new Point(1, -1)));
            //list = list.Concat(PartOfMove(p, new Point(-1, 1)));
            //list = list.Concat(PartOfMove(p, new Point(-1, -1)));

            return FormingMove(p, 7, new Point(1, 1), new Point(1, -1), new Point(-1, 1), new Point(-1, -1));

        }
    }
}
