using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public record Point
    {
        public int y { get; set; }
        public int x { get; set; }

        public int xx { get; set; }

        public Point(int y, int x)
        {
            this.y = y;
            this.x = x;

        }


    }
}