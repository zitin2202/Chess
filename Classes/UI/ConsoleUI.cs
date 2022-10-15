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

        public ConsoleUI(Game game)
        {
            Game = game;
            Game._UI = this;

        }

        public void FieldRender()
        {
            Console.Write("  ");

            for (int x = 0; x < Field.maxX; x++)
            {
                Console.Write($"{Data.FieldPointIntsToName[0,x]}  ");
            }
            Console.WriteLine();


            for (int y = 0; y < Field.maxY; y++)
            {

                for (int x = 0; x < Field.maxX; x++)
                {
                    Console.Write($"{(x == 0 ? $"{Data.FieldPointIntsToName[1,y]} " : "")}");


                    ChessPiece chP = Game._field.GetChP(new FieldPoint(y, x));

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
            Console.WriteLine($"Очередь игрока: {Data.SideToRu[(PlayerSide)Game._turn.Current]}");
        }

        public FieldPoint СellSelection()
        {
            string message = (Game._active.Item1 == null ? "Выберите фигуру" : "Ходы данной фигуры");
            string strPoint;
            FieldPoint p;
            bool firstСycle = true;
            do
            {
                if (!firstСycle)
                    IncorrectInputMessage();

                Console.WriteLine(message + "\n");
                strPoint = Console.ReadLine();
                firstСycle = false;
            }
            while (!Data.GetPointUsingName(strPoint, out p));

            return p;
        }



        public void NotChessPieceReport()
        {
            Console.WriteLine("Здесь нету фигуры, которую вы могли бы взять, выберите другую\n");
        }

        public void SelectedСhessPiece(ChessPiece chP)
        {
            Console.WriteLine(Data.ChPTypeToRu[chP.ChPType]);
        }

        public void PossibleMove(FieldPoint p, TypeMove type)
        {
            ChessPiece targetChP = Game._field.GetChP(p);
            Console.WriteLine($"{Data.GetNamePointsUsingFieldPoint(p)}: {( targetChP == null ? "Пусто" : Data.ChPTypeToRu[targetChP.ChPType])}, {Data.TypeMoveToRu[type]}");
        }

        public void NotChessМoveReport()
        {
            Console.WriteLine("У этой фигуры нет возможных ходов\n");
        }

        public void HaventSuchMove()
        {
            Console.WriteLine("Вы не можете походить сюда\n");
        }

        public void SimpleMove(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP)
        {
            Console.WriteLine($"\n{StartStr(thisChP, startP)} идет на {Data.GetNamePointsUsingFieldPoint(targetP)}\n");

        }

        public void Attack(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP, ChPType typeTargetChP)
        {
            Console.WriteLine($"\n{StartStr(thisChP, startP)} съедает {Data.ChPTypeToRu[typeTargetChP]} на {Data.GetNamePointsUsingFieldPoint(targetP)}\n");
        }

        public void Сastling(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP)
        {
            string castlingType = (targetP.x > startP.x ? "короткую" : "длинную");

            Console.WriteLine($"\n{StartStr(thisChP, startP)} совершает {castlingType} рокировку\n");


        }

        private string StartStr(ChessPiece chP, FieldPoint startP)
        {
            return $"{Data.SideToRu[chP.Side]} {Data.GetNamePointsUsingFieldPoint(startP)}: {Data.ChPTypeToRu[chP.ChPType]}";

        }

        public ChPType Promotion()
        {
            Console.WriteLine("Пешка дошла до конца доски\nВыберите, на какую фигуру ее поменять");
            Console.WriteLine("b - слон, n - лошадь,  r - ладья, q - ферзь");
            string input;
            do
            {
                input = Console.ReadLine().ToLower();
            }
            while (!Validation.ConsoleСhoiceChPValidation(input));

            return Data.StrToChpType[input];


        }


        public void Victory(PlayerSide victorySide)
        {
            Console.WriteLine($"Шах и мат! Победила сторона {Data.SideToRu[victorySide]}!");
        }

        public void Draw()
        {
            Console.WriteLine("Ничья! Конец игры");
        }


        public  static void IncorrectInputMessage()
        {
            Console.WriteLine("Не корректный ввод\n");
        }


    }
}
