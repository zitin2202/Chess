using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessForm
{
    public partial class СhoiceChessPiece : Form
    {
        public Dictionary<Button, ChPType> _chessPieces;
        private FormUI _UI;
        public СhoiceChessPiece(FormUI UI)
        {
            InitializeComponent();

            _chessPieces = new Dictionary<Button, ChPType>();
            _UI = UI;

            for (int i = 2; i < 6; i++)
            {
                Button btn = new Button();
                _chessPieces.Add(btn,(ChPType)i);
                btn.Click += _UI.BtnChoiceChess_Click;
                btn.Size = _UI._sizeButtonObject;

            }
        }

        private void СhoiceChessPiece_Load(object sender, EventArgs e)
        {

        }
    }
}
