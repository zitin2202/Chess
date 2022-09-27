using Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Classes
{
    public class ConsoleUI : IUI
    {
        public Game Game { get; set; }
        private static readonly Regex regexEmoji = new Regex(@"[\u2600-\u26FF]");

        public ConsoleUI(Game game)
        {
            Game = game;

    }

        public void FieldRender()
        {
            File.WriteAllText("WriteLines.txt", "♝");
            Console.Write("  ");

            for (int x = 0; x < Field.maxX; x++)
            {
                Console.Write($"{x}  ");
            }
            Console.WriteLine();


            for (int y = 0; y < Field.maxY; y++)
            {

                for (int x = 0; x < Field.maxX; x++)
                {
                    Console.Write($"{(x == 0 ? $"{y} " : "")}");


                    ChessPiece chP = Game._field.GetChP(new Point(y, x));

                    if (chP!=null)
                        Console.ForegroundColor = (chP.Side == PlayerSide.First ? ConsoleColor.White : ConsoleColor.DarkGray);


                    Console.Write($"{(chP == null ? "  " : SubStrChPType(chP,0,1))} ");


                }
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();



            }

        }

        private string SubStrChPType(ChessPiece chP, int start,int finish)
        {
            string str ="";
            string typeStr = chP.ChPType.ToString();
            for (int i = start; i <= finish && i<typeStr.Length; i++)
            {
                str += typeStr[i].ToString();

            }

            return str;
        }

        public void TurnReport()
        {
            Console.WriteLine($"Очередь игрока: {Game._turn.Current}");
        }

        public Point СellSelection()
        {
            string message = (Game._activeChP.Item1 == null ? "Выберите фигуру" : "Сделайте ход");
            string[] s;
            Point p;
            do
            {
                Console.WriteLine(message);
                s = Console.ReadLine().Split(",");

            }
            while (!Exception.ConsoleInputValidation(s, out p));



            return p;
        }



        public void NotChessPieceReport()
        {
            Console.WriteLine("Здесь нету фигуры, которую вы могли бы взять, выберите другую");
        }

        public void SelectedСhessPiece(ChessPiece chP)
        {
            Console.WriteLine((chP.Side, chP.ChPType));
        }

        public void PossibleMove(Point p, TypeMove type)
        {
            ChessPiece targetChP = Game._field.GetChP(p);
            Console.WriteLine($"{(p.y,p.x)}: {( targetChP == null ? "Пусто" : targetChP.ChPType)}, {type}");
        }

        public void NotChessМoveReport()
        {
            Console.WriteLine("У этой фигуры нет возможных ходов");
        }

        public void HaventSuchMove()
        {
            Console.WriteLine("Вы не можете походить сюда");
        }

        public void SimpleMove(ChessPiece thisChP, Point targetP)
        {
            Console.WriteLine($"{defaultStr(thisChP)}{thisChP.ChPType} идет на {(targetP.y, targetP.x)}");

        }

        public void Attack(ChessPiece thisChP, Point targetP, ChessPiece targetChP)
        {
            Console.WriteLine($"{defaultStr(thisChP)}{thisChP.ChPType} съел {targetChP.ChPType} на {(targetP.y, targetP.x)}");
        }

        public void Сastling(ChessPiece thisChP, Point targetP, ChessPiece targetChP)
        {
            string castlingType = (Game._rule.CastlingShift(thisChP, targetP) > 0 ? "короткую" : "длинную");

            Console.WriteLine($"{defaultStr(thisChP)}совершает {castlingType} рокировку");


        }

        private string defaultStr(ChessPiece chP)
        {
            return $"{chP.Side} {(chP._p.y, chP._p.x)}: ";

        }

        public Type СhoiceChessPiece()
        {
            Console.WriteLine("Пешка дошла до конца доски\nВыберите, на какую фигуру ее поменять");
            Console.WriteLine("B - слон, K - лошадь,  R - ладья, Q - ферзь");
            string input;
            do
            {
                input = Console.ReadLine().ToUpper();
            }
            while (!Exception.ConsoleСhoiceChPValidation(input));

            return Data.StrToChPType[input];


        }


        public void Victory(PlayerSide victorySide)
        {
            Console.WriteLine($"Шах и мат! Победила сторона {victorySide}!");
        }




    }
}
