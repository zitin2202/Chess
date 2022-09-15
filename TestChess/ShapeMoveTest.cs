using Classes;
using Enums;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TestChess
{
    public class ShapeMoveTest
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
            yield return new object[] { new Field().Get(p).GetMoves(p), new Point[7] { new Point(1,3), new Point(2, 4), new Point(3, 5), new Point(4, 6), new Point(5, 7), new Point(1, 1), new Point(2, 0) } };
        }

    }
}
