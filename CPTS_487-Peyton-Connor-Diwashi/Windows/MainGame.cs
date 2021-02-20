using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class MainGame : Game
    {
        public Vector2 currentWindowResolution = new Vector2(1280, 720);
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle grunt1_bounds;
        private Rectangle player_bounds;
        private Rectangle enemy_bounds;
        private List<Sprite> sprites = new List<Sprite>();

        // scale each sprite by this.
        private float scaleFactor;

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Add sprite to list of drawable, updatable sprites.
        /// </summary>
        /// <param name="s"></param>
        protected void AddSprite(Sprite s)
        {
            s.Scale(this.scaleFactor);
            this.sprites.Add(s);
        }

        /// <summary>
        /// Scale rectangle by factor of n
        /// </summary>
        /// <param name="r"> Rectangle </param>
        /// <param name="n"> Scale Factor </param>
        private void ScaleRectangle(ref Rectangle r, float n)
        {
            r.X = (int)((float)r.X * n);
            r.Y = (int)((float)r.Y * n);
            r.Width = (int)((float)r.Width * n);
            r.Height = (int)((float)r.Height * n);
        }

        protected override void Initialize()
        {
            // Set scale factor for all objects
            this.scaleFactor = this.currentWindowResolution.Y / 720.0f;

            this.player_bounds = new Rectangle(10, 10, 1180, 350);
            ScaleRectangle(ref player_bounds, this.scaleFactor);

            this.grunt1_bounds = new Rectangle(50, 50, 1180, 350);
            ScaleRectangle(ref grunt1_bounds, this.scaleFactor);

            this.enemy_bounds = new Rectangle(50, 50, 1180, 350);
            ScaleRectangle(ref enemy_bounds, this.scaleFactor);

            this._graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            this._graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            this._graphics.ApplyChanges();

            // Add Player
            this.AddSprite(new Player(new Vector2(500, 300), Content.Load<Texture2D>("spaceship_player"), ref player_bounds));

            // Example add Grunt1 enemys
            for(int i = 0; i < 10; i++)

            // ---------------------------------  Sprite Examples ------------------------------
            this.AddSprite(new Boss1(new Vector2(250, 150), Content.Load<Texture2D>("Boss1"), ref enemy_bounds));
            this.AddSprite(new Boss2(new Vector2(750, 150), Content.Load<Texture2D>("Boss2"), ref enemy_bounds));
            for (int i = 0; i < 5; i++)
            {
                this.AddSprite(new Grunt1(new Vector2(250, 150), Content.Load<Texture2D>("Grunt1"), ref enemy_bounds));
                this.AddSprite(new Grunt2(new Vector2(750, 150), Content.Load<Texture2D>("Grunt2"), ref enemy_bounds));
            }
            // ---------------------------------------------------------------------------------



            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Update ALL sprites added with AddSprite
            foreach(Sprite s in sprites)
            {
                s.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // Draw ALL sprites added with AddSprite
            _spriteBatch.Begin();
            foreach (Sprite s in sprites)
            {
                s.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
