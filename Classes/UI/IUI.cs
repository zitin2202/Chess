using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    interface IUI
    {
        public Game _game { get; }

        public void FieldRender();

        public void TurnReport();

        public void СellSelection();

        public void NotChessPieceReport();

        public void NotChessМoveReport();

        public void VictoryReport();

    }
}
