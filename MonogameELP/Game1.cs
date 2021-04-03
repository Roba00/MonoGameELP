using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using MonogameELP.Gameobjects;
using MonogameELP.Components;
using Microsoft.Xna.Framework.Audio;

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

        Texture2D groundTxtr;
        Transform groundTf;
        BoxCollider groundCol;

        public static Texture2D plyrTxtr { get; private set; }
        private Player plyr;

        public static List<BoxCollider> colliders { get; private set; }

        private Vector3 cameraPosition;
        private Vector3 cameraTarget;
        private static Matrix cameraTransform;

        private SoundEffect happyTune;
        private SoundEffect dynasticSong;
        private AudioSource musicSource;

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
            _graphics.PreferredBackBufferWidth = 128*8;
            _graphics.PreferredBackBufferHeight = 64*8;
            _graphics.ApplyChanges();
            bgPos = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            colliders = new List<BoxCollider>();
            plyr = new Player();
            plyr.Initialize();

            groundTf = new Transform();
            groundTf.Position = new Vector2(0, 385f);
            groundTf.Rotation = 0f;
            groundTf.Scale = new Vector2(_graphics.PreferredBackBufferWidth, 50f);
            groundCol = new BoxCollider(groundTf, false);

            //cameraTransform = new Matrix();
            //cameraTarget = new Vector3(plyr.transform.Position.X, plyr.transform.Position.Y, -100);
            //cameraTransform = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);

            for (int i = 0; i < colliders.Count; i++)
            {
                System.Diagnostics.Debug.WriteLine(colliders[i]);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            dynasticSong = Content.Load<SoundEffect>("DynasticSong");
            happyTune = Content.Load<SoundEffect>("Videogame");
            musicSource = new AudioSource(happyTune, true, 0.5f, 0f, 0f);
            musicSource.Play();

            // TODO: use this.Content to load your game content here
            bg = Content.Load<Texture2D>("Background");
            bgOrg = new Vector2(bg.Width / 2, bg.Height / 2);

            plyrTxtr = Content.Load<Texture2D>("Player-Sheet");
            groundTxtr = new Texture2D(GraphicsDevice, 1, 1);
            groundTxtr.SetData(new[] { Color.White });
            plyr.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            plyr.Update(gameTime);
            groundCol.Update();

            //cameraTarget = new Vector3(plyr.transform.Position.X, plyr.transform.Position.Y, 0);
            //cameraTransform = Matrix.CreateLookAt(cameraPosition, cameraTarget, Vector3.Up);
            //Gonna want to clamp this later
            cameraTransform = Matrix.CreateTranslation(-(plyr.transform.Position.X-500f), 0, 0);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            // TODO: Add your drawing code here
            _spriteBatch.Begin(sortMode:SpriteSortMode.FrontToBack, 
                               blendState:BlendState.AlphaBlend, 
                               samplerState:SamplerState.PointClamp,
                               depthStencilState:DepthStencilState.None,
                               rasterizerState:RasterizerState.CullNone,
                               effect:null,
                               transformMatrix:cameraTransform);
                _spriteBatch.Draw(bg, bgPos, null, Color.White, 0f, bgOrg, new Vector2(8f,8f), SpriteEffects.None, 0f);
                _spriteBatch.Draw(groundTxtr, groundCol.Rectangle, Color.White);
                plyr.Draw(_spriteBatch);
            _spriteBatch.End();
            //float frameRate = (float)Math.Round(1 / gameTime.ElapsedGameTime.TotalSeconds);
            //System.Diagnostics.Debug.WriteLine(frameRate + " fps");
            base.Draw(gameTime);
        }
    }
}
