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

        public void BishopMove(IEnumerable<(FieldPoint, TypeMove)> list, FieldPoint[] expected)
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
            FieldPoint p = new FieldPoint(0, 2);
            Field f = new Field();
            yield return new object[] { f.GetChP(p).GetMoves(), new FieldPoint[7] { new FieldPoint(1,3), new FieldPoint(2, 4), new FieldPoint(3, 5), new FieldPoint(4, 6), new FieldPoint(5, 7), new FieldPoint(1, 1), new FieldPoint(2, 0) } };
        }

    }
}
