using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class Point
    {
        public int x { get; }
        public int y { get; }
        private int[] p = new int[2];
        public Point(int y,int x)
        {
            this.x = x;
            this.y = y;
        }

 
    }
}
