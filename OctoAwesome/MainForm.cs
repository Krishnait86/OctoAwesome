using System;
using System.Windows.Forms;
using System.Diagnostics;
using OctoAwesome.Model;
using OctoAwesome.Components;

namespace OctoAwesome {
    public partial class MainForm : Form {
        private Input input = new Input();
        private Game game;
        private Stopwatch watch = new Stopwatch();
        private RenderControl renderControl;
        private InventoryForm inventory;

        public MainForm() {
            InitializeComponent();
            inventory = new InventoryForm();
            game = new Game(input);

            renderControl = new RenderControl(game);
            renderControl.Dock = DockStyle.Fill;
            this.Controls.Add(renderControl);
            
            watch.Start();
            //renderControl.Game = game;
        }

        private void timer_Tick(object sender, EventArgs e) {
            game.Update(watch.Elapsed);
            watch.Restart();
            renderControl.Invalidate();

            if (game.Player.InteractionPartner != null)
            {
                if (!inventory.Visible /*&& game.Player.InventoryIsOpened*/)
                {
                    /*game.Player.InventoryIsOpened = false;*/
                    inventory.Show();
                    inventory.Init(game.Player, game.Player.InteractionPartner);
                }
            }
            else
            {
                if (inventory.Visible)
                {
                    inventory.Hide();
                }
            }
        }

        private void closeMenu_Click(object sender, EventArgs e) {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            input.KeyDown(keyData);
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnKeyUp(KeyEventArgs e) {
            input.KeyUp(e.KeyCode);
            base.OnKeyUp(e);
        }
    }
}
