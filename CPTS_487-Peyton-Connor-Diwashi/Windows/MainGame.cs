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
        private List<Player> players = new List<Player>();
        private List<Enemy> disposedEnemies = new List<Enemy>();
        private Player player;
        private Rectangle grunt1_bounds;
        private Rectangle player_bounds;
        private Rectangle enemy_bounds;
        private List<Sprite> sprites = new List<Sprite>();

        // scale each sprite by this.
        private float scaleFactor;
        private float timer = 0.0f;
        private long frames = 0;

        //TEMP
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

        protected void AddSprite(Sprite s)
        {
            this.sprites.Add(s);
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

            this.enemy_bounds = new Rectangle(50, 50, 1180, 350);
            //this.ScaleRectangle(ref enemy_bounds, this.scaleFactor);

            this._graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            this._graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            this._graphics.ApplyChanges();

            // Create new EnemyFactory
            this.ef = new StandardEnemyFactory(new Rectangle(50, 50, 1180, 350), Content);
            // Set Event to Invoke when an enemies Lifespan is Up
            this.ef.DisposeMethod = DisposeEnemyEvent;
            // Set Enemy LifeSpan, only works when a DisposeMethod EventHandler is assigned
            this.ef.LifeSpanSeconds = 15;

            base.Initialize();
        }

            // Example add Grunt1 enemys
           // for(int i = 0; i < 10; i++)

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frames++;

            // Add Player
            this.AddSprite(new Player(new Vector2(500, 300), Content.Load<Texture2D>("spaceship_player"), ref player_bounds));

            //Add Enemy
            if (timer > 1 && timer < 1.1)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Grunt1));

            if (timer > 30 && timer < 30.1)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Grunt2));

            if (frames == 15*60)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Boss1));

            if (frames == 45 * 60)
                this.AddEnemy(ef.CreateEnemy(EnemyFactory.EnemyType.Boss2));


            //----

            // Get rid of all disposed enemies
            foreach (Enemy s in disposedEnemies)
            {
                if(this.enemies.Contains(s))
                {
                    this.enemies.Remove(s);
                }
            }
            this.disposedEnemies.Clear();

            // Set the target for the enemies as the mouse position
            this.target.X = Mouse.GetState().X;
            this.target.Y = Mouse.GetState().Y;

            // Update ALL sprites added with AddSprite
            foreach(Enemy s in enemies)
            {
                s.BindToTarget(this.target);
                s.Update(gameTime);
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
