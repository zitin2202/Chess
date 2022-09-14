﻿using System;
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

        public override void GetMoves(Field field, int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}
