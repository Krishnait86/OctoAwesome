using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OctoAwesome.Components;
using OctoAwesome.Rendering;

namespace OctoAwesomeDX
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class OctoGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        OctoAwesome.Model.Game game;
        Input2 input;

        private Texture2D grass;
        private Texture2D sprite;
        private Texture2D tree;
        private Texture2D box;

        private CellTypeRenderer sandRenderer;
        private CellTypeRenderer waterRenderer;

        public OctoGame() : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Input2 input = new Input2(this);
            Components.Add(input);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            game = new OctoAwesome.Model.Game(input);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            grass = Content.Load<Texture2D>("Textures/grass_center");

            sandRenderer = new CellTypeRenderer(Content, "sand");
            waterRenderer = new CellTypeRenderer(Content, "water");

            sprite = Content.Load<Texture2D>("Textures/sprite");
            tree = Content.Load<Texture2D>("Textures/tree");
            box = Content.Load<Texture2D>("Textures/box");

            Content.Load<Texture2D>("Textures/tree");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //    Exit();

            // TODO: Add your update logic here
            game.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
