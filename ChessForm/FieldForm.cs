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


        public FieldForm(FormUI UI)
        {
            InitializeComponent();

            _UI = UI;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;






        }


        private void FormUI_Load(object sender, EventArgs e)
        {





            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxX; x++)
                {
                    ButtonCell btn = new ButtonCell(_UI);
                    FieldPoint p = new FieldPoint(y, x);
                    _UI._points[btn] = p;
                    _UI._buttons[p] = btn;

                    btn.Location = new Point(x * ButtonCell.BtnSize, y * ButtonCell.BtnSize);

                    this.Controls.Add(btn);


                }
            }

        }




    }
}
