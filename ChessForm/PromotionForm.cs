using Classes;
using Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Classes;

namespace ChessForm
{
    public partial class СhoiceChessPieceForm : Form
    {
        private Type[] chPClasses = new Type[] { Data.StrToChPClass["b"], Data.StrToChPClass["n"], Data.StrToChPClass["r"], Data.StrToChPClass["q"]};
        public Dictionary<Button, Type> _chessPieces;
        private FormUI _UI;
        public СhoiceChessPieceForm(FormUI UI)
        {
            InitializeComponent();

            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            TopLevel = true;

            _chessPieces = new Dictionary<Button, Type>();
            _UI = UI;

            var btnColor = _UI.СolorSelection();

            for (int i = 0; i < 4; i++)
            {
                ButtonPromotion btn = new ButtonPromotion(_UI);
                btnColor.MoveNext();
                btn.BackColor = btnColor.Current;
                _chessPieces.Add(btn, chPClasses[i]);
                btn.Location = new Point(0, i * btn.Height);
                

                Controls.Add(btn);
                
            }
        }

        private void СhoiceChessPiece_Load(object sender, EventArgs e)
        {

        }
    }
}
