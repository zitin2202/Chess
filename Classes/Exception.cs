using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
   public static class Exception
    {

        public static bool ValidationCell(Point p )
        {

            if (p.y < Field.maxY && p.y >= 0 && p.x < Field.maxX && p.x >= 0)
            {
                return true;
            }

            return false;
        }

        public static bool ConsoleInputValidation(string[] mass, out Point p)
        {
            int[] xy = new int[2];
            if (mass.Length==2 && int.TryParse(mass[0],out xy[0]) && int.TryParse(mass[1], out xy[1]))
            {

                p = new Point(xy[0],xy[1]);
                if (ValidationCell(p))
                    return true;


            }

            IncorrectInputMessage();
            p = null;
            return false;


        }

        public static bool ConsoleСhoiceChPValidation(string input)
        {
            if (Data.StrToChPType.ContainsKey(input))
                return true;
            else
            {
                IncorrectInputMessage();
                return false;
            }
          
        }

        private static void IncorrectInputMessage()
        {
            Console.WriteLine("Не корректный ввод\n");
        }
    }


}
