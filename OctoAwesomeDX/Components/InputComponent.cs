using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace OctoAwesome.Components
{
    internal sealed class InputComponent : GameComponent, IInputSet
    {
        public bool Left { get; private set; }
        public bool Right { get; private set; }
        public bool Up { get; private set; }
        public bool Down { get; private set; }
        public bool Interact { get; private set; }

        private bool lastInteract = false;

        private GamePadInput gamepad;
        private KeyboardInput keyboard;

        public InputComponent(Game game) : base(game)
        {
            gamepad = new GamePadInput();
            keyboard = new KeyboardInput();
        }

        public override void Update(GameTime gameTime)
        {
            bool nextInteract = false;

            gamepad.Update();
            nextInteract = gamepad.Interact;
            Left = gamepad.Left;
            Right = gamepad.Right;
            Up = gamepad.Up;
            Down = gamepad.Down;

            keyboard.Update();
            nextInteract |= keyboard.Interact;
            Left |= keyboard.Left;
            Right |= keyboard.Right;
            Up |= keyboard.Up;
            Down |= keyboard.Down;

            if (nextInteract && !lastInteract)
                Interact = true;
            else
                Interact = false;

            lastInteract = nextInteract;
        }
    }
}
