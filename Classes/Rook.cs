using System;
using System.Collections.Generic;
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

        public override void GetMoves(Field field, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
