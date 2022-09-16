using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class MessageConsole : IMessage
    {
        public string ChPInfo(ChessPiece chP)
        {
            string message;
           if (chP == null)
            {
                message = "Пустая клетка";
                Console.WriteLine(message);

            }

            else
            {
                message = $"Игрок: {chP.Side}, Фигура: { chP.ChPType}";
                Console.WriteLine(message);
            }

            return message;

        }


    }
}
