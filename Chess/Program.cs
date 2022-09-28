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


            game.Start();


        }
    }
}
