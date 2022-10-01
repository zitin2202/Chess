using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessForm
{
    public partial class FieldForm : Form
    {
        public Button[,] _cells = new Button[8,8];
        public static int btn_size = 70;
        public FieldForm()
        {
            InitializeComponent();
        }

        private void ChessForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(btn_size*9, btn_size*9);
            var colorCell = СolorSelection();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Button btn = new Button();
                    _cells[y, x] = btn;

                    btn.Size = new Size(btn_size, btn_size);
                    btn.Location = new Point(x * btn_size, y* btn_size);

                    colorCell.MoveNext();
                    btn.BackColor = colorCell.Current;

                   this.Controls.Add(btn);
                }
                colorCell.MoveNext();
            }
        }

        private IEnumerator<System.Drawing.Color> СolorSelection()
        {

            while (true)
            {
                yield return System.Drawing.Color.Black;
                yield return System.Drawing.Color.White;
            }
        }
    }
}
