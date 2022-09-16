using Classes;
using Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestChess
{
    public class ChPMoveTest
    {

        [Theory]
        [MemberData(nameof(GetData))]

        public void BishopMove(IEnumerable<(Point, TypeMove)> list, Point[] expected)
        {
            int i = 0;
            foreach (var el in list)
            {
                el.Item1.Should().BeEquivalentTo(expected[i]);
                i++;
            }
        }


        public static IEnumerable<object[]> GetData()
        {
            Point p = new Point(0, 2);
            Field f = new Field();
            yield return new object[] { f.GetChP(p).GetMoves(), new Point[7] { new Point(1,3), new Point(2, 4), new Point(3, 5), new Point(4, 6), new Point(5, 7), new Point(1, 1), new Point(2, 0) } };
        }

    }
}
