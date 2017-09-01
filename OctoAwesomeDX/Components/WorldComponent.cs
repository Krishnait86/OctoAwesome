using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OctoAwesome.Model;

namespace OctoAwesome.Components
{
    internal sealed class WorldComponent : GameComponent
    {
        public World World { get; private set; }

        public WorldComponent(Game game, InputComponent input) : base(game)
        {
            World = new World(input);
        }

        public override void Update(GameTime gameTime)
        {
            World.Update(gameTime);
        }
    }
}
