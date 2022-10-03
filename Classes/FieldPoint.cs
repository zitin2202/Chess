using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public record FieldPoint
    {
        public int y { get; set; }
        public int x { get; set; }

        public FieldPoint(int y, int x)
        {
            this.y = y;
            this.x = x;

        }


    }
}