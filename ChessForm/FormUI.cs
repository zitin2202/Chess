using Classes;
using Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChessForm
{
    public class FormUI : IUI
    {
        public Game Game { get; set;}
        public FieldForm fieldForm;
        СhoiceChessPiece choiceForm;
        public Dictionary<Button, FieldPoint> _points = new Dictionary<Button, FieldPoint>();
        public Dictionary<FieldPoint, Button> _buttons = new Dictionary<FieldPoint, Button>();
        bool pawnTransformtaion = false;
        Button _activeButton = null;
        ChPType _choiceChess = 0;
        public int _btnSize = 80;
        public Size _sizeButtonObject;




        public FormUI(Game game)
        {
            _sizeButtonObject = new Size(_btnSize, _btnSize);
            Game = game;
            Game._UI = this;
            fieldForm = new FieldForm(this);
            fieldForm.Load += FieldForm_Load;
            fieldForm.FormClosed += FieldForm_FormClosed;
            fieldForm.Show();
            choiceForm = new СhoiceChessPiece(this);



        }



        private void FieldColorReset()
        {
            var colorCell = СolorSelection();

            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxX; x++)
                {
                    colorCell.MoveNext();

                    _buttons[new FieldPoint(y, x)].BackColor = colorCell.Current;
                }

                colorCell.MoveNext();
            }

        }

        private IEnumerator<Color> СolorSelection()
        {
            while (true)
            {
                yield return Color.DarkSlateGray;
                yield return Color.White;
            }
        }

        public void Btn_Click(object sender, EventArgs e)
        {
            _activeButton = (Button)sender;
        }

        public void BtnChoiceChess_Click(object sender, EventArgs e)
        {
            _choiceChess = choiceForm._chessPieces[(Button)sender];
        }

        private void FieldForm_Load(object sender, EventArgs e)
        {
            FieldColorReset();
            Thread threadGame = new Thread(Game.Start);
            threadGame.Start();
        }
        private void FieldForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        public void FieldRender()
        {
            FieldColorReset();
            foreach (var i in _points)
            {
                ChessPiece chP = Game._field.GetChP(i.Value);
                if (chP != null)
                {
                    Bitmap bitmap = new Bitmap(new Bitmap($"chess_pieces\\{chP.Side}\\{chP.ChPType}.png"), i.Key.Size);
                    i.Key.Image = bitmap;

                }
                else
                {
                    i.Key.Image = null;
                }

            }
        }

        public void TurnReport()
        {
        }

        public FieldPoint СellSelection()
        {
            while (_activeButton == null)
            {

            }
            FieldPoint p = _points[_activeButton];
            _activeButton = null;
            FieldColorReset();

            return p;

        }

        public void NotChessPieceReport()
        {

        }

        public void SelectedСhessPiece(ChessPiece chP)
        {

        }

        public void PossibleMove(FieldPoint p, TypeMove type)
        {
            Color color = (type == TypeMove.Simple ? Color.Gray : Color.Red);
            _buttons[p].BackColor = color;

        }

        public void NotChessМoveReport()
        {

        }

        public void HaventSuchMove()
        {

        }

        public void SimpleMove(ChessPiece thisChP, FieldPoint targetP)
        {

        }

        public void Attack(ChessPiece thisChP, FieldPoint targetP, ChessPiece targetChP)
        {

        }

        public void Сastling(ChessPiece thisChP, FieldPoint targetP, ChessPiece targetChP)
        {

        }

        public Type СhoiceChessPiece()
        {
            pawnTransformtaion = true;
            foreach (var i in choiceForm._chessPieces)
            {
                i.Key.Image = new Bitmap(new Bitmap($"chess_pieces\\{Game._turn.Current}\\{i.Value}.png"), i.Key.Size);
            }
            return null;


        }

        public void Victory(PlayerSide victorySide)
        {
            var result = MessageBox.Show($"Победила сторона {victorySide}\nХотите сыграть снова?", "Игра окончена", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Game.Start();
            }
            else
            {
                fieldForm.Close();
            }

        }

    }
}
