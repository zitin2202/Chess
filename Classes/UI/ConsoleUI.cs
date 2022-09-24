using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class ConsoleUI : IUI
    {
        public Game Game { get; set; }

        public ConsoleUI(Game game)
        {
            Game = game;
        }


        public void FieldRender()
        {

            string fieldGui = "  ";

            for (int x = 0; x < Field.maxX; x++)
            {
                fieldGui += $"{x}  ";
            }
            fieldGui += "\n";


            for (int y = 0; y < Field.maxY; y++)
            {

                for (int x = 0; x < Field.maxX; x++)
                {
                    ChessPiece chP = Game._field.GetChP(new Point(y, x));

                    fieldGui += $"{(x == 0 ? $"{y} " : "")}{(chP == null ? "  " : chP.Side.ToString()[0].ToString() + chP.ChPType.ToString()[0].ToString())} ";



                }
                fieldGui += "\n";



            }
            Console.WriteLine(fieldGui);

        }

        public void TurnReport()
        {
            Console.WriteLine($"Очередь игрока: {Game._turn.Current}");
        }

        public Point СellSelection()
        {
            string[] s;
            int[] p;
            do
            {
                Console.WriteLine($"Выберите {str}");
                s = Console.ReadLine().Split(",");

            }
            while (!Exception.ConsoleInputValidation(s, out p));



            return new Point(p[0], p[1]);
        }

        void NotChessPieceReport();

        void NotChessМoveReport();

        void VictoryReport();

       

        public void NotChessPieceReport()
        {
            throw new NotImplementedException();
        }

        public void NotChessМoveReport()
        {
            throw new NotImplementedException();
        }

       

        

        public void VictoryReport()
        {
            throw new NotImplementedException();
        }


    }
}
