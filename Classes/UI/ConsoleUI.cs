using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    class ConsoleUI : IUI
    {
        Game IUI._game => 

        ConsoleUI(Game game)
        {
            _game = game;
        }


        public void FieldRender()
        {
            throw new NotImplementedException();
        }

        public void NotChessPieceReport()
        {
            throw new NotImplementedException();
        }

        public void NotChessМoveReport()
        {
            throw new NotImplementedException();
        }

        public void TurnReport()
        {
            Console.WriteLine($"Очередь игрока: {_game._turn.Current}");
        }

        public void VictoryReport()
        {
            throw new NotImplementedException();
        }

        public void СellSelection()
        {
            throw new NotImplementedException();
        }
    }
}
