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
        СhoiceChessPieceForm promotionForm;
        public Size sizeDisplay = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
        public Dictionary<ButtonCell, FieldPoint> _points = new Dictionary<ButtonCell, FieldPoint>();
        public Dictionary<FieldPoint, ButtonCell> _buttons = new Dictionary<FieldPoint, ButtonCell>();
        public bool pawnTransformtaion = false;
        public ButtonCell _activeButton = null;
        ChPType _promotionChess = 0;
        bool _helpInGame = false;





        public FormUI(Game game)
        {

            Game = game;
            Game._UI = this;

            fieldForm = new FieldForm(this);
            fieldForm.Load += FieldForm_Load;
            fieldForm.FormClosed += FieldForm_FormClosed;

            promotionForm = new СhoiceChessPieceForm(this);
            promotionForm.Load += PromotionForm_Load;



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

                    if (_helpInGame && Game._playersType[(PlayerSide)Game._turn.Current] == PlayerType.Human)
                    {
                        if (Game._rule._unsafeCell[y, x])
                            _buttons[new FieldPoint(y, x)].BackColor = Color.OrangeRed;
                    }


                }

                colorCell.MoveNext();
            }

        }

        public IEnumerator<Color> СolorSelection()
        {
            while (true)
            {

                yield return Color.DarkSlateGray;
                yield return Color.White;
            }
        }

        public void Btn_Click(object sender, EventArgs e)
        {
            if (!pawnTransformtaion)
            {
                _activeButton = (ButtonCell)sender;

            }
        }

        public void BtnPromotion_Click(object sender, EventArgs e)
        {
            _promotionChess = promotionForm._chessPieces[(Button)sender];
            promotionForm.Hide();
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

        private void PromotionForm_Load(object sender, EventArgs e)
        {
            int button_size = ButtonPromotion.ObjectSize.Width;
            Size formSize = new Size(button_size, (button_size) * 4 + Data.formHeaderSize);
            Point formLocation = new Point(fieldForm.Location.X + fieldForm.Size.Width, fieldForm.Location.Y);
            promotionForm.Size = formSize;
            promotionForm.Location = formLocation;
        }

        public void FieldRender()
        {
            FieldColorReset();
            foreach (var i in _points)
            {
                ChessPiece chP = Game._field.GetChP(i.Value);
                if (chP != null)
                {
                    lock (this)
                    {
                        Bitmap bitmap = new Bitmap(new Bitmap($"chess_pieces\\{chP.Side}\\{chP.ChPType}.png"), ButtonCell.SizeImage);
                        i.Key.Image = bitmap;
                    }

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
            Color color = Color.Gray;
            switch (type)
            {
                case TypeMove.Simple:
                    color = Color.Gray;
                    break;

                case TypeMove.Attack:
                    color = Color.Red;
                    break;

                case TypeMove.EnPassant:
                    color = Color.Red;
                    break;

                case TypeMove.Сastling:
                    color = Color.SkyBlue;
                    break;
            }
            _buttons[p].BackColor = color;

        }

        public void NotChessМoveReport()
        {

        }

        public void HaventSuchMove()
        {

        }

        public void SimpleMove(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP)
        {

        }

        public void Attack(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP, ChPType typeTargetChP)
        {

        }

        public void Сastling(FieldPoint startP, ChessPiece thisChP, FieldPoint targetP)
        {

        }

        public ChPType Promotion()
        {
            pawnTransformtaion = true;
            foreach (var i in promotionForm._chessPieces)
            {
                i.Key.Image = new Bitmap(new Bitmap($"chess_pieces\\{Game._turn.Current}\\{i.Value}.png"), ButtonPromotion.SizeImage);
            }

            promotionForm.ShowDialog();
            while (_promotionChess == 0)
            {
            }

            ChPType type = _promotionChess;
            _promotionChess = 0;
            pawnTransformtaion = false;
            return type;
        }

        public void Victory(PlayerSide victorySide)
        {
            End($"Победила сторона {victorySide}");

        }

        public void Draw()
        {
            End($"Ничья!");
        }

        private void End(string text)
        {
            var result = MessageBox.Show(text + "\nХотите сыграть снова?", "Игра окончена", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Game.Start();
            }
            else
            {
                Application.Exit();
            }
        }

        public void MenuItemPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolStripComboBox ComboBox = (ToolStripComboBox)sender;
            PlayerSide side = (PlayerSide)Enum.Parse(typeof(PlayerSide), ComboBox.Name);
            PlayerType type = (PlayerType)ComboBox.SelectedIndex;

            Game.PlayerTypeChange(side, type);

        }

        public void HelpInGame_CheckedChanged(object sender, EventArgs e)
        {
            ToolStripMenuItem itemHelp = (ToolStripMenuItem)sender;
            _helpInGame = itemHelp.Checked;

            FieldColorReset();



        }


    }
}
