using Classes;
using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ChessForm
{

    public partial class FormUI : Form,IUI
    {
        public Dictionary<Button,FieldPoint> _points = new Dictionary<Button,FieldPoint>();
        public Dictionary<FieldPoint, Button> _buttons = new Dictionary<FieldPoint, Button>();
        public static int btn_size = 70;
        Size sizeDisplay = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
        Size formSize;
        Point formLocation;
        Button _activeButton = null;
        СhoiceChessPiece СhoiceForm;
        bool pawnTransformtaion = false;
        public Game Game { get; set;}

        public FormUI(Game game)
        {
            InitializeComponent();

            Game = game;
            Game._UI = this;

            СhoiceForm = new СhoiceChessPiece(btn_size);
            foreach (Button btn in СhoiceForm._chessPieces.Keys)
            {
                btn.Click += Btn_Click;
            }


        }


        private void FormUI_Load(object sender, EventArgs e)
        {
            formSize = new Size((btn_size + 2) * 8, (btn_size + 5) * 8);
            formLocation = new Point((sizeDisplay.Width - formSize.Width) / 2, (sizeDisplay.Height - formSize.Height) / 2);
            this.Size = formSize;
            this.Location = formLocation;


            Size btnSize = new Size(btn_size, btn_size);

            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxX; x++)
                {
                    Button btn = new Button();
                    FieldPoint p = new FieldPoint(y, x);
                    _points[btn] = p;
                    _buttons[p] = btn;
                    btn.Click += Btn_Click;
                    btn.Size = btnSize;
                    btn.Location = new Point(x * btn_size, y * btn_size);

                    this.Controls.Add(btn);


                }
            }

            FieldColorReset();

            Thread Thread2 = new Thread(Game.Start);
            Thread2.Start();

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

        private void Btn_Click(object sender, EventArgs e)
        {
            _activeButton = (Button)sender;
        }

        private IEnumerator<Color> СolorSelection()
        {
            while (true)
            {
                yield return Color.DarkSlateGray;
                yield return Color.White;
            }
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
            while (_activeButton==null)
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
            foreach (var i in СhoiceForm._chessPieces)
            {
                i.Key.Image = new Bitmap(new Bitmap($"chess_pieces\\{Game._turn.Current}\\{i.Value}.png"), i.Key.Size);
            }

        }

        public void Victory(PlayerSide victorySide)
        {
            var result = MessageBox.Show($"Победила сторона {victorySide}","Игра окончена\nХотите сыграть снова?", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Game.Start();
            }

        }


    }
}
