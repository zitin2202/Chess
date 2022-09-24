using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
   public static class Exception
    {


        public static bool ValidationCell(Point p )
        {
            if(p.y<Field.maxY && p.y>=0 && p.x<Field.maxX && p.x >= 0)
            {
                return true;
            }

            return false;
        }

        public static bool ConsoleInputValidation(string[] mass, out int[] result)
        {
            result = new int[2];
            if (mass.Length==2 && int.TryParse(mass[0],out result[0]) && int.TryParse(mass[1], out result[1]))
            {              
                return true;
                 
            }

            Console.WriteLine("Не корректный ввод\n");
            return false;


        }
    }


}
