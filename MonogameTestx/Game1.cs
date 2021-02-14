using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using MonogameELP.Gameobjects;

namespace MonogameELP
{
    public class Game1 : Game
    {
        //public static ContentManager content;
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        private Texture2D bg;
        private Vector2 bgPos;
        private Vector2 bgOrg;

        public static Texture2D plyrTxtr { get; private set; }
        private Player plyr;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            bgPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            plyr = new Player();
            plyr.Initialize();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bg = Content.Load<Texture2D>("Background");
            bgOrg = new Vector2(bg.Width / 2, bg.Height / 2);

            plyrTxtr = Content.Load<Texture2D>("Player-Sheet");
            plyr.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            plyr.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            /*var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Left))
                plyrPos.X -= plyrSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (kstate.IsKeyDown(Keys.Right))
                plyrPos.X += plyrSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);*/
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin(sortMode:SpriteSortMode.FrontToBack, 
                               blendState:BlendState.AlphaBlend, 
                               samplerState:SamplerState.PointClamp,
                               depthStencilState:DepthStencilState.None,
                               rasterizerState:RasterizerState.CullNone);
                _spriteBatch.Draw(bg, bgPos, null, Color.White, 0f, bgOrg, new Vector2(6.25f,6.25f), SpriteEffects.None, 0f);
                plyr.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
