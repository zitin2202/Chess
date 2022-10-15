using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
   public static class Validation
    {

        public static bool ValidationCell(FieldPoint p )
        {

            if (p.y < Field.maxY && p.y >= 0 && p.x < Field.maxX && p.x >= 0)
            {
                return true;
            }

            return false;
        }

        public static bool ConsoleСhoiceChPValidation(string input)
        {
            if (Data.StrToChpType.ContainsKey(input))
                return true;
            else
            {
                ConsoleUI.IncorrectInputMessage();
                return false;
            }
          
        }

    }


}
