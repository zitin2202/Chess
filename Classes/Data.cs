using Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Classes
{
    public static class Data
    {
        public static Dictionary<string, int>[] PointNameToFieldPointInts = new Dictionary<string, int>[]
        {
            new Dictionary<string, int>()
            {
            {"a",0 },
            {"b",1 },
            {"c",2 },
            {"d",3 },
            {"e",4 },
            {"f",5 },
            {"g",6 },
            {"h",7 },
            },

            new Dictionary<string, int>()
            {
            {"8",0 },
            {"7",1 },
            {"6",2 },
            {"5",3 },
            {"4",4 },
            {"3",5 },
            {"2",6 },
            {"1",7 },
            }
        };

        public static string[,] FieldPointIntsToName = new string[,]
        {
            {"a","b","c","d","e","f","g","h",},
            {"8","7","6","5","4","3","2","1",}
           
        };
        public static Dictionary<ChPType, Func<FieldPoint,PlayerSide,ChessPiece>> ChPTypeToFuncClass = new Dictionary<ChPType, Func<FieldPoint, PlayerSide, ChessPiece>>()
        {   {ChPType.Bishop, (point, side) => new Bishop(point, side)},
            {ChPType.Knight, (point, side) => new Knight(point, side)},
            {ChPType.Rook, (point, side) => new Rook(point, side)},
            {ChPType.Queen, (point, side) => new Queen(point, side)},
        };


        public static Dictionary<string, ChPType> StrToChpType = new Dictionary<string, ChPType>()
        {   {"b",ChPType.Bishop},
            {"n",ChPType.Knight},
            {"r",ChPType.Rook},
            {"q",ChPType.Queen},
        };



        public static Dictionary<ChPType, string> ChPTypeToRu = new Dictionary<ChPType, string>()
        {   {ChPType.Bishop, "слон"},
            {ChPType.Knight, "лошадь"},
            {ChPType.Rook, "ладья"},
            {ChPType.Queen, "королева"},
            {ChPType.Pawn, "пешка"},
            {ChPType.King, "король"},
        };

        public static Dictionary<PlayerSide, string> SideToRu = new Dictionary<PlayerSide, string>()
        {   {PlayerSide.First, "Белые"},
            {PlayerSide.Second, "Черные"},

        };

        public static Dictionary<TypeMove, string> TypeMoveToRu = new Dictionary<TypeMove, string>()
        {   {TypeMove.Simple, "Ход"},
            {TypeMove.Attack, "Атака"},
            {TypeMove.EnPassant, "Взятие на проходе"},
            {TypeMove.Сastling, "Рокировка"},
        };


        public static Dictionary<string, PlayerType> RuPlayerType = new Dictionary<string, PlayerType>()
        {
            {"Человек",PlayerType.PC},
            {"Компьютер",PlayerType.PC}
        };

        public static int formHeaderSize = 39;
        public static int minFullScreenButton = 120;


        public static Func<FieldPoint, PlayerSide, ChessPiece> GetChessPiece(string chP)
        {
            ChPType type = StrToChpType[chP];
            return ChPTypeToFuncClass[type];
        }



        public static bool GetPointUsingName(string strPoint, out FieldPoint point)
        {
            string strX = strPoint.Substring(0, 1);
            string strY = strPoint.Substring(1, 1);
            if (strPoint.Length==2 && PointNameToFieldPointInts[1].TryGetValue(strY, out int y) && PointNameToFieldPointInts[0].TryGetValue(strX, out int x))
            {
                point = new FieldPoint(y, x);
                return true;
            }

            else
                point = null;
                return false;
           
        }

        public static string GetNamePointsUsingFieldPoint(FieldPoint p)
        {
            return FieldPointIntsToName[0,p.x] + FieldPointIntsToName[1,p.y];
        }

    }
}
