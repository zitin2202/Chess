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

    public partial class FieldForm : Form
    {
        Size sizeDisplay = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
        Size formSize;
        Point formLocation;
        FormUI _UI;
        int btnSize;



        public FieldForm(FormUI UI)
        {
            _UI = UI;
            btnSize = UI._btnSize;
            InitializeComponent();





        }


        private void FormUI_Load(object sender, EventArgs e)
        {
            formSize = new Size((btnSize + 2) * 8, (btnSize + 5) * 8);
            formLocation = new Point((sizeDisplay.Width - formSize.Width) / 2, (sizeDisplay.Height - formSize.Height) / 2);
            this.Size = formSize;
            this.Location = formLocation;



            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxX; x++)
                {
                    Button btn = new Button();
                    FieldPoint p = new FieldPoint(y, x);
                    _UI._points[btn] = p;
                    _UI._buttons[p] = btn;
                    btn.Click += _UI.Btn_Click;
                    btn.Size = _UI._sizeButtonObject;
                    btn.Location = new Point(x * btnSize, y * btnSize);

                    this.Controls.Add(btn);


                }
            }

        }

              


    }
}
