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

        public override void GetMoves(Field field, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
