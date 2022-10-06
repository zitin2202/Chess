using Classes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessForm
{
    public class ButtonPromotion : Button
    {
        private FormUI _UI;
        public static int BtnSize = (ButtonCell.BtnSize < Data.minFullScreenButton ? Data.minFullScreenButton : ButtonCell.BtnSize);
        public static Size ObjectSize = new Size(BtnSize,BtnSize);
        private static int _sizeDiff = 10;
        public static Size SizeImage = new Size(BtnSize - _sizeDiff, BtnSize - _sizeDiff);


        public ButtonPromotion(FormUI UI)
        {
            _UI = UI;

            this.Size = ObjectSize;

            this.Click += _UI.BtnPromotion_Click;


        }
    }
}
