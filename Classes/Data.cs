using Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Classes
{
    public static class Data
    {
        public static Dictionary<string, int> nameToFieldPoint = new Dictionary<string, int>()
        {
            {"a",0 },
            {"b",1 },
            {"c",2 },
            {"d",3 },
            {"e",4 },
            {"f",5 },
            {"g",6 },
            {"h",7 }

        };

        public static string[,] fieldPointToName = new string[,]
        {
            {"a","b","c","d","e","f","g","h",},
            {"8","7","6","5","4","3","2","1",}
           
        };
        public static Dictionary<string, Type> StrToChPClass = new Dictionary<string, Type>()
        {   {"b", typeof(Bishop)},
            {"n", typeof(Knight)},
            {"r", typeof(Rook)},
            {"q", typeof(Queen)},
        };

        public static Dictionary<Type, string> ChPClassToStr = new Dictionary<Type, string>()
        {   {typeof(Bishop),"b"},
            {typeof(Knight),"n"},
            {typeof(Rook),"r"},
            {typeof(Queen),"q"},
        };

        public static Dictionary<string, PlayerType> RuPlayerType = new Dictionary<string, PlayerType>()
        {
            {"Человек",PlayerType.PC},
            {"Компьютер",PlayerType.PC}
        };

        public static int formHeaderSize = 39;
        public static int minFullScreenButton = 120;


        public static FieldPoint GetPointUsingName(string strPoint)
        {
            string x = strPoint.Substring(0, 1);
            int y = int.Parse(strPoint.Substring(1, 1));
            return new FieldPoint(Math.Abs(y - 8), nameToFieldPoint[x]);
        }

    }
}
