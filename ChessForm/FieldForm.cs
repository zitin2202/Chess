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


            MenuStrip menu = new MenuStrip();
            ToolStripMenuItem Setting = new ToolStripMenuItem("Настройки");
            ToolStripMenuItem[] SettingItems = new ToolStripMenuItem[2];
            SettingItems[0] = new ToolStripMenuItem("Игроки");
            SettingItems[1] = new ToolStripMenuItem("Помощь"){ Checked = false, CheckOnClick = true };
            SettingItems[1].CheckedChanged += _UI.HelpInGame_CheckedChanged;

            for (int i = 0; i < 2; i++)
            {
                ToolStripComboBox player = new ToolStripComboBox();
                player.Name = ((PlayerSide)i).ToString();
                SettingItems[0].DropDownItems.Add(player);
                foreach (string type in Data.RuPlayerType.Keys)
                {
                    player.Items.Add(type);
                }
                player.SelectedIndex = (int)_UI.Game._playersType[(PlayerSide)i];
                player.SelectedIndexChanged += _UI.MenuItemPlayer_SelectedIndexChanged;

            }
            foreach (var item in SettingItems)
            {
                Setting.DropDownItems.Add(item);
            }
            menu.Items.Add(Setting);

            Controls.Add(menu);

            Size formSize = new Size((ButtonCell.BtnSize + 2) * 8, (ButtonCell.BtnSize + 5) * 8 + menu.Height);
            Point formLocation = new Point((_UI.sizeDisplay.Width - formSize.Width) / 2, (_UI.sizeDisplay.Height - formSize.Height) / 2);
            Size = formSize;
            Location = formLocation;

            GroupBox groupBox = new GroupBox();
            groupBox.Location = new Point(0, menu.Height);
            groupBox.Size = new Size(this.Size.Width, this.Size.Height - menu.Height);
            Controls.Add(groupBox);


            for (int y = 0; y < Field.maxY; y++)
            {
                for (int x = 0; x < Field.maxX; x++)
                {
                    ButtonCell btn = new ButtonCell(_UI);
                    FieldPoint p = new FieldPoint(y, x);
                    _UI._points[btn] = p;
                    _UI._buttons[p] = btn;

                    btn.Location = new Point(x * ButtonCell.BtnSize, y * ButtonCell.BtnSize);

                    groupBox.Controls.Add(btn);


                }
            }

        }



        private void игрок1ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripComboBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
