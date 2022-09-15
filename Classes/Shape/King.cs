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
            throw new NotImplementedException();
        }
    }
}
