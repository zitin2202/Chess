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
        public СhoiceChessPiece(int btn_size)
        {
            InitializeComponent();

            _chessPieces = new Dictionary<Button, ChPType>();

            for (int i = 2; i < 4; i++)
            {
                _chessPieces.Add(new Button(),(ChPType)i);
            }
        }

        private void СhoiceChessPiece_Load(object sender, EventArgs e)
        {

        }
    }
}
