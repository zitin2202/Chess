using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Point
    {
        public int y { get; set; }
        public int x { get; set; }

        private int[] p = new int[2];
        public Point(int y,int x)
        {
            this.y = y;
            this.x = x;

        }


    }
}
