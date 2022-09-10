using System;
using Classes;
using Interfaces;
using Enums;
namespace Chess
{
    class Program
    {
      
        static void Main(string[] args)
        {
            Field field = new Field();

            Console.WriteLine(field.Get(6, 3).ShapeType);
        }
    }
}
