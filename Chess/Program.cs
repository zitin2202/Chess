﻿using System;
using Classes;
using Interfaces;
using Enums;
using TestChess;
using System.Collections.Generic;

namespace Chess
{
    class Program
    {

        static void Main(string[] args)
        {
            MessageConsole mess = new MessageConsole();

            Field field = new Field();
            //mess.ShapeInfo(field.Get(0, 3));

            Point p = new Point(7, 1);



            Shape shape = field.Get(p);
            var moves = shape.GetMoves(p);

            mess.ShapeInfo(shape);

            foreach (var i in moves)
            {
                Console.WriteLine(i);
                Console.WriteLine((i.Item1.y, i.Item1.x));

            }

            //Console.WriteLine("____________________________\n");


            //foreach (var i in field.OccupDel(shape.GetMoves(new Point(shapeY, shapeX))))
            //{
            //    Console.WriteLine(i);
            //    Console.WriteLine((i.Item1.y, i.Item1.x));

            //}



        }
    }
}
