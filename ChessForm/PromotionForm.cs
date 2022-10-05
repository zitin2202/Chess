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
        private Type[] chPClasses = new Type[] { typeof(Bishop), typeof(Knight), typeof(Rook), typeof(Queen)};
        public Dictionary<Button, Type> _chessPieces;
        private FormUI _UI;
        public Size btn_size;
        public СhoiceChessPieceForm(FormUI UI)
        {
            InitializeComponent();

            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.TopLevel = true;

            _chessPieces = new Dictionary<Button, Type>();
            _UI = UI;

            btn_size = (_UI._btnSize < Data.minFullScreenButton ? new Size(Data.minFullScreenButton, Data.minFullScreenButton) : _UI._sizeButtonObject);
            var btnColor = _UI.СolorSelection();

            for (int i = 0; i < 4; i++)
            {
                Button btn = new Button();
                btnColor.MoveNext();
                btn.BackColor = btnColor.Current;
                _chessPieces.Add(btn, chPClasses[i]);
                btn.Size = btn_size;
                btn.Location = new Point(0, i * btn.Height);
                btn.Click += _UI.BtnPromotion_Click;

                this.Controls.Add(btn);
                
            }
        }

        private void СhoiceChessPiece_Load(object sender, EventArgs e)
        {

        }
    }
}
