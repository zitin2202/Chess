using System;
using System.Collections.Generic;
using System.Text;
using Enums;
using Interfaces;

namespace Classes
{
    public class Pawn : Shape
    {
        public Pawn(PlayerSide side):base(side)
        {
        }

        public override ShapeType ShapeType => ShapeType.Pawn;

        public override void GetMoves()
        {
            throw new NotImplementedException();
        }
    }
}
