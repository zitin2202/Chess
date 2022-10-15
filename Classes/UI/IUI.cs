using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Classes
{
    public interface IUI
    {
        Game Game { get; set; }

        void FieldRender();

        void TurnReport();

        FieldPoint СellSelection();

        void NotChessPieceReport();

        void SelectedСhessPiece(ChessPiece chP);

        void PossibleMove(FieldPoint p, TypeMove type);

        void NotChessМoveReport();

        void HaventSuchMove();
 
        void SimpleMove(FieldPoint startP, ChessPiece thisChP,FieldPoint targetP);

        void Attack(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP, ChPType typeTargetChP);

        void Сastling(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP);

        ChPType Promotion();

        void Victory(PlayerSide victorySide);

        void Draw();

    }
}
