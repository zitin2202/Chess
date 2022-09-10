using System;
using Xunit;
using Classes;
using Enums;
using FluentAssertions;
namespace TestChess
{
    public class TestChess1
    {
        [Theory]
        [InlineData(0,0, "�����: Second, ������: Rook")]
        [InlineData(2, 3, "������ ������")]
        [InlineData(4, 5, "������ ������")]
        [InlineData(6, 7, "�����: First, ������: Pawn")]
        [InlineData(3, 3, "������ ������")]
        [InlineData(7, 5, "�����: First, ������: Bishfop")]
        public void Test1(int x, int y,string expected)
        {
            Field field = new Field();

            var shape = field.Get(x, y);

            MessageConsole cons = new MessageConsole();

            string message = cons.ShapeInfo(shape);

            message.Should().BeEquivalentTo(expected);


        }

    }
    }

