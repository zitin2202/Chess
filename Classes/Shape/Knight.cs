using System;
using System.Collections.Generic;
using System.Text;
using Enums;

namespace Classes
{
    public class Knight : Shape
    {
        public Knight(PlayerSide side) : base(side)
        {
        }

        public override ShapeType ShapeType => ShapeType.Knight;

        public override IEnumerable<(Point, TypeMove)> GetMoves(Point p)
        {
            return FormingMove(p, 1, new Point(2, 1), new Point(2, -1),
                 new Point(-2, 1), new Point(-2, -1), new Point(1, 2),
                 new Point(-1, 2), new Point(1, -2), new Point(-1, -2));
        }
    }
}
