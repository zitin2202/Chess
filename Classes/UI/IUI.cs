using Enums;
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

        Point СellSelection();

        void NotChessPieceReport();

        void SelectedСhessPiece(ChessPiece chP);

        void PossibleMove(Point p, TypeMove type);

        void NotChessМoveReport();

        void HaventSuchMove();
 
        void SimpleMove(ChessPiece thisChP,Point targetP);

        void Attack(ChessPiece thisChP, Point targetP, ChessPiece targetChP);

        void Сastling(ChessPiece thisChP, Point targetP, ChessPiece targetChP);

        Type СhoiceChessPiece();

        void Victory(PlayerSide victorySide);

    }
}
