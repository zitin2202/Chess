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

        FormUI _UI;
        int btnSize;



        public FieldForm(FormUI UI)
        {
            InitializeComponent();

            _UI = UI;
            btnSize = UI._btnSize;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;






        }


        private void FormUI_Load(object sender, EventArgs e)
        {





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
