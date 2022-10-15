using System;
using Classes;
using Enums;
using TestChess;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Chess
{
    class Program
    {
        string str;

        static void Main(string[] args)
        {

            ConsoleUI UI = new ConsoleUI(new Game(new Field()));
            UI.Game.Start();

        }


    }
}
