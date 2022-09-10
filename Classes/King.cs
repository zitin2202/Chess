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

        public override void GetMoves()
        {
            throw new NotImplementedException();
        }
    }
}
