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

        public bool StartPosition = true;
        public bool LineMoves = true;


        public override IEnumerable<(Point, TypeMove)> GetMoves(Point p)
        {
            int maxX = Field.maxX-1;
            int maxY = Field.maxY-1;
            int yDirect = (this.Side == PlayerSide.First ? -1 : 1);
            int lenMove = (StartPosition ? 2 : 1);

            int y;
            for (int i=1; i <= lenMove; i += 1)
            {
                y = p.y + i * yDirect;
                    
                if (y <= maxY && y >= 0)
                {
                    yield return (new Point(y, p.x), TypeMove.Simple);
                }
                else
                {
                    yield break;
                }            

            }

            int x = p.x + 1;
            y = p.y + yDirect;

            if (x <= maxX && x >= 0)
            {
                yield return (new Point(y, x), TypeMove.Attack);
            }

            x = p.x - 1;

            if (x <= maxX && x >= 0)
            {
                yield return (new Point(y, x), TypeMove.Attack);
            }

        }
    }
}
