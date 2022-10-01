using ChessForm;
using Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Classes;

namespace ChessForm
{
    public class FormUI: IUI
    {
        private readonly Form form;
        public Button[,] _cells = new Button[8, 8];
        public static int btn_size = 70;
        public Game Game => throw new NotImplementedException();

        public FormUI(Form form)
        {
            this.form = form;
        }

        public void FieldRender()
        {
            form.Size = new Size(btn_size*9, btn_size*9);
            var colorCell = СolorSelection();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Button btn = new Button();
                    _cells[y, x] = btn;

                    btn.Size = new Size(btn_size, btn_size);
                    btn.Location = new Point(x * btn_size, y* btn_size);

                    colorCell.MoveNext();
                    btn.BackColor = colorCell.Current;

                    form.Controls.Add(btn);
                }
                colorCell.MoveNext();
            }
        }

 

        void IUI.TurnReport()
        {
            throw new NotImplementedException();
        }

        Point IUI.СellSelection()
        {
            throw new NotImplementedException();
        }

        void IUI.NotChessPieceReport()
        {
            throw new NotImplementedException();
        }

        void IUI.SelectedСhessPiece(ChessPiece chP)
        {
            throw new NotImplementedException();
        }

        void IUI.PossibleMove(Point p, TypeMove type)
        {
            throw new NotImplementedException();
        }

        void IUI.NotChessМoveReport()
        {
            throw new NotImplementedException();
        }

        void IUI.HaventSuchMove()
        {
            throw new NotImplementedException();
        }

        void IUI.SimpleMove(ChessPiece thisChP, Point targetP)
        {
            throw new NotImplementedException();
        }

        void IUI.Attack(ChessPiece thisChP, Point targetP, ChessPiece targetChP)
        {
            throw new NotImplementedException();
        }

        void IUI.Сastling(ChessPiece thisChP, Point targetP, ChessPiece targetChP)
        {
            throw new NotImplementedException();
        }

        Type IUI.СhoiceChessPiece()
        {
            throw new NotImplementedException();
        }

        void IUI.Victory(PlayerSide victorySide)
        {
            throw new NotImplementedException();
        }
    }
}
