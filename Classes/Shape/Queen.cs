using System;
using System.Collections.Generic;
using System.Text;
using Enums;
namespace Classes
{

    public class Queen : Shape
    {
        public Queen(PlayerSide side) : base(side)
        {
        }

        public override ShapeType ShapeType => ShapeType.Queen;

        public override IEnumerable<(Point, TypeMove)> GetMoves(Point p)
        {
            throw new NotImplementedException();
        }
    }
}
