using Enums;
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
            Console.WriteLine($"{thisChP.Side} {(thisChP._p.y, thisChP._p.x)}: {thisChP.ChPType} идет на {(targetP.y, targetP.x)}");

        }

        public void Attack(ChessPiece thisChP, Point targetP, ChessPiece targetChP)
        {
            Console.WriteLine($"{thisChP.Side} {(thisChP._p.y, thisChP._p.x)}: {thisChP.ChPType} съел {targetChP.ChPType} на {(targetP.y, targetP.x)}");
        }

        public void Сastling(ChessPiece thisChP, Point targetP, ChessPiece targetChP)
        {
            string castlingType = (Game._rule.CastlingShift(thisChP, targetP) > 0 ? "короткую" : "длинную");

            Console.WriteLine($"{thisChP.Side} {(thisChP._p.y, thisChP._p.x)}: совершает {castlingType} рокировку");


        }

        public void Victory(PlayerSide victorySide)
        {
            Console.WriteLine($"Шах и мат! Победила сторона {victorySide}!");
        }


    }
}
