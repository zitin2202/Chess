using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public interface IUI
    {
        Game Game { get; }

        void FieldRender();

        void TurnReport();

        ChessPiece СellSelection();

        void NotChessPieceReport();

        void NotChessМoveReport();

        void VictoryReport();

    }
}
