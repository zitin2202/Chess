using System;
using Classes;
using Enums;
using TestChess;
using System.Collections.Generic;

namespace Chess
{
    class Program
    {

        static void Main(string[] args)
        {

            Game game = new Game(new Field());

            //while (true)
            //{
            //    Console.WriteLine("Выберите фигуру");
            //    string[] s = Console.ReadLine().Split(',');
            //    game.Select(new Point(int.Parse(s[0]), int.Parse(s[1])));

            //    Console.WriteLine("Выберите координаты");
            //    s = Console.ReadLine().Split(',');
            //    game.Action(new Point(int.Parse(s[0]), int.Parse(s[1])));



            //}
            List<Point> list = new List<Point>();
            Point p = new Point(2, 2);
            list.Add(p);



            p.y = 3;
            foreach (var item in list)
            {
                Console.WriteLine((item.y, item.x));

            }



            //game.Select(new)

            //MessageConsole mess = new MessageConsole();

            //Field field = new Field();
            ////mess.ShapeInfo(field.Get(0, 3));

            //Point p = new Point(0, 1);



            //ChessPiece shape = field.GetChP(p);
            //var moves = shape.GetMoves(p,field);


            //mess.ShapeInfo(shape);

            //foreach (var i in moves)
            //{
            //    Console.WriteLine(i);
            //    Console.WriteLine((i.Item1.y, i.Item1.x));

            //}

            //Console.WriteLine("____________________________\n");


            //foreach (var i in field.OccupDel(shape.GetMoves(new Point(shapeY, shapeX))))
            //{
            //    Console.WriteLine(i);
            //    Console.WriteLine((i.Item1.y, i.Item1.x));

            //}



        }
    }
}
