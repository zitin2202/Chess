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

        public override void GetMoves(Field field, int x, int y)
        {
            int[,] moves = new int[4,2];
            int moves_count = 0;
            int direction;


            if (this.Side == PlayerSide.First)
            {
                direction = -1;
            }
            else
                direction = 1;

            for (int i = 0; i < 2; i += 1)
            {
                moves[i, 0] = 0;
                moves[i, 1] = (i+1)*direction;

            }
           
            moves[2, 0] = 1;
            moves[2, 1] = direction;

            moves[3, 0] = -1;
            moves[3, 1] = direction;













            //for (int i = 1; i < 3; i+=direction)
            //{
            //    if ()
            //    moves[moves_count,0] = x;
            //    moves[moves_count, 1] = y+i;
            //}

        }
    }
}
