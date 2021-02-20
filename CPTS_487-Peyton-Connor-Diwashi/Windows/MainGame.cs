using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class MainGame : Game
    {
        //-----test
        private Boss2 boss2;
        //---------

        public Vector2 currentWindowResolution = new Vector2(1280, 720);
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle enemy_bounds;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> disposedEnemies = new List<Enemy>();
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
        protected void AddEnemy(Enemy s)
        {
            this.enemies.Add(s);
        }

        /// <summary>
        /// Sprite must subscibe to this event to be disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DisposeEnemyEvent(object sender, EventArgs e)
        {
            Enemy x = (Enemy)sender;
            this.disposedEnemies.Add(x);
        }

        protected override void Initialize()
        {
            // Set scale factor for all objects
            this.scaleFactor = this.currentWindowResolution.Y / 720.0f;

            this._graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            this._graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            this._graphics.ApplyChanges();




            // ---------------------------------  Sprite Examples ------------------------------
            this.enemy_bounds = new Rectangle(50, 50, 1180, 350);

            Enemy a = new Boss1(new Vector2(250, 150), Content.Load<Texture2D>("Boss1"), Content.Load<Texture2D>("BossBullet"), ref enemy_bounds);
            this.AddEnemy(a);
            a.BindToTarget(new Vector2(1280, 720));
            a.Dispose += this.DisposeEnemyEvent;
            a.LifeSpan = 100;

            this.boss2 = new Boss2(new Vector2(750, 150), Content.Load<Texture2D>("Boss2"), Content.Load<Texture2D>("BossBullet"), ref enemy_bounds);
            this.AddEnemy(this.boss2);
            this.boss2.BindToTarget(new Vector2(0, 720));
            this.boss2.Dispose += this.DisposeEnemyEvent;

            for (int i = 0; i < 2; i++)
            {
                a = new Grunt1(new Vector2(250, 150), Content.Load<Texture2D>("Grunt1"), Content.Load<Texture2D>("BulletGreen"), ref enemy_bounds);
                this.AddEnemy(a);
                a.Dispose += this.DisposeEnemyEvent;
                a = new Grunt2(new Vector2(750, 150), Content.Load<Texture2D>("Grunt2"), Content.Load<Texture2D>("BulletPurple"), ref enemy_bounds);
                a.BindToTarget(new Vector2(0, 0)); //they dont aim does not matter
                this.AddEnemy(a);
                a.Dispose += this.DisposeEnemyEvent;
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

            // Get rid of all disposed enemies
            foreach(Enemy s in disposedEnemies)
            {
                if(this.enemies.Contains(s))
                {
                    this.enemies.Remove(s);
                }
            }
            this.disposedEnemies.Clear();


            // Update ALL sprites added with AddSprite
            foreach(Sprite s in enemies)
            {
                s.Update(gameTime);

                // Make all the grunts fire at Boss2
                if (s is Grunt1)
                {
                    Grunt1 g = (Grunt1)s;
                    g.BindToTarget(this.boss2.Position);
                }
            } 
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            // Draw ALL sprites added with AddSprite
            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(this.scaleFactor));
            foreach (Sprite s in enemies)
            {
                s.Draw(gameTime, _spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
