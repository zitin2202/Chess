using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ChessForm
{
    public class ButtonCell : Button
    {
        private FormUI _UI;
        public static int BtnSize = 120;
        public static Size ObjectSize = new Size(BtnSize, BtnSize);
        static private int _sizeDiff = 10;
        public static Size SizeImage = new Size(BtnSize - _sizeDiff, BtnSize - _sizeDiff);

        public ButtonCell(FormUI UI)
        {
            _UI = UI;

            this.Size = ObjectSize;

            this.Click += _UI.Btn_Click;
        }
    }
}
