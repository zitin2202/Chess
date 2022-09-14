using System;
using Classes;
using Interfaces;
using Enums;
using TestChess;

namespace Chess
{
    class Program
    {

        static void Main(string[] args)
        {
            MessageConsole mess = new MessageConsole();

            Field field = new Field();
            mess.ShapeInfo(field.Get(0, 3));
        }
    }
}
