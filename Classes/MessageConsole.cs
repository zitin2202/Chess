using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public class MessageConsole : IMessage
    {
        public string ShapeInfo(Shape shape)
        {
            string message;
           if (shape == null)
            {
                message = "Пустая клетка";
                Console.WriteLine(message);

            }

            else
            {
                message = $"Игрок: {shape.Side}, Фигура: { shape.ShapeType}";
                Console.WriteLine(message);
            }

            return message;

        }
    }
}
