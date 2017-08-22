// music https://youtu.be/a_gzph6pkEc?list=PLrQpK27RKimxOw41eTUSMyqRATkCA-1In

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MainForm() {
            InitializeComponent();

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
                MessageBox.Show("Hurra!");
                game.Player.InteractionPartner = null;
            }
        }

        private void closeMenu_Click(object sender, EventArgs e) {
            this.Close();
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
