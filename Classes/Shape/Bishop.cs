using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }
    }
}
