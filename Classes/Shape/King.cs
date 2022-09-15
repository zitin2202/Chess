using System;
using System.Collections.Generic;
using System.Text;
using Enums;
namespace Classes
{
    public class King : Shape
    {
        public King(PlayerSide side) : base(side)
        {
        }

        public override ShapeType ShapeType => ShapeType.King;

        public override IEnumerable<(Point, TypeMove)> GetMoves(Point p)
        {
            return FormingMove(p, 1,new Point(1, 0), new Point(-1, 0),
                new Point(0, 1), new Point(0, -1), new Point(1, 1),
                new Point(1, -1), new Point(-1, 1), new Point(-1, -1));
        }
    }
}
