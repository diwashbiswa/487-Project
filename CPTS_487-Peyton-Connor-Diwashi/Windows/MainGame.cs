using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class MainGame : Game
    {
        // This will be changed by Program.cs as needed
        public Vector2 currentWindowResolution = new Vector2(1280, 720);
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private EnemyFactory ef;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> disposedEnemies = new List<Enemy>();
        private Player player;
        private Rectangle enemy_bounds;
        private float scaleFactor;
        private float timer = 0.0f;
        private long frames = 0;

        Vector2 target = new Vector2(0, 0);

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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


            this.enemy_bounds = new Rectangle(50, 50, 1180, 600);
            // Create Player
            this.player = new Player(new Vector2(500, 300), Content.Load<Texture2D>("spaceship_player"), ref enemy_bounds);
            // Create new EnemyFactory

            // TODO: Lifespan as a paramater
            this.ef = new StandardEnemyFactory(enemy_bounds, Content);
            // Set Event to Invoke when an enemies Lifespan is Up
            this.ef.DisposeMethod = DisposeEnemyEvent;
            // Set Enemy LifeSpan, only works when a DisposeMethod EventHandler is assigned
            this.ef.LifeSpanSeconds = 15;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frames++;

            //Add Enemy
            if (timer > 1 && timer < 1.1)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Grunt1));

            if (timer > 30 && timer < 30.1)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Grunt2));

            if (frames == 15*60)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Boss1));

            if (frames == 45 * 60)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Boss2));


            // Get rid of all disposed enemies
            foreach (Enemy s in disposedEnemies)
            {
                if(this.enemies.Contains(s))
                {
                    this.enemies.Remove(s);
                }
            }
            this.disposedEnemies.Clear();

            this.target.X = this.player.X;
            this.target.Y = this.player.Y;

            foreach(Enemy s in enemies)
            {
                s.BindToTarget(this.target);
                s.Update(gameTime);
            }

            this.player.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(this.scaleFactor));
            foreach (Sprite s in enemies)
            {
                s.Draw(gameTime, _spriteBatch);
            }
            this.player.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

/*
 1. JSON script draft

 2. Adding controllers between maingame and classes
    - spawning
    - collision detection
    - movement
 
 3. Movement Class

 4. Make enemy and bullet more similar

 5. Extract keyboard input away from sprites

 */
