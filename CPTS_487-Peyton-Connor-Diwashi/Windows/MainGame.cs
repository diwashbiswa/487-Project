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
        private SpawnerFactory sf;

        private List<Enemy> enemies = new List<Enemy>();
        private List<Enemy> disposedEnemies = new List<Enemy>();

        private List<BulletSpawner> spawners = new List<BulletSpawner>();
        private List<BulletSpawner> disposedSpawners = new List<BulletSpawner>();

        private Player player;
        private Rectangle spawn_bounds;
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

        protected void AddSpawner(BulletSpawner s)
        {
            this.spawners.Add(s);
        }

        /// <summary>
        /// Sprite must subscibe to this event to be disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DisposeEnemyEvent(object sender, EventArgs e)
        {
            Enemy x = (Enemy)sender;

            foreach(BulletSpawner s in spawners)
            {
                if (s.parent == x)
                {
                    this.disposedSpawners.Add(s);
                }
            }

            this.disposedEnemies.Add(x);
        }

        protected override void Initialize()
        {
            // Set scale factor for all objects
            this.scaleFactor = this.currentWindowResolution.Y / 720.0f;

            this._graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            this._graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            this._graphics.ApplyChanges();

            // Create bounds for spawning on screen
            this.spawn_bounds = new Rectangle(50, 50, 1180, 600);
            // Create Player
            this.player = new Player(new Vector2(500, 300), Content.Load<Texture2D>("spaceship_player"), ref spawn_bounds, 7.0f);

            // Create enemy and spawner factories
            this.ef = new StandardEnemyFactory(spawn_bounds, Content);
            this.sf = new StandardSpawnerFactory(Content);

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
            {
                Enemy e = ef.CreateEnemy(EnemyFactory.EnemyType.Grunt1);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.CardinalSouth));
            }

            if (timer > 30 && timer < 30.1)
            {
                Enemy e = ef.CreateEnemy(EnemyFactory.EnemyType.Grunt2);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.CardinalSouth));
            }

            if (frames == 15 * 60)
            {
                Enemy e = ef.CreateEnemy(EnemyFactory.EnemyType.Boss1);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.Targeted));
            }

            if (frames == 45 * 60)
            {
                Enemy e = ef.CreateEnemy(EnemyFactory.EnemyType.Boss2);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.Targeted));
            }



            // COLLISION here



            // SPAWNER ----------------------------
            foreach (BulletSpawner s in disposedSpawners)
            {
                if (this.spawners.Contains(s))
                {
                    this.spawners.Remove(s);
                }
            }
            foreach (BulletSpawner s in spawners)
            {
                s.Update(gameTime);
            }
            // ------- ----------------------------




            // ENEMY ----------------------------
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
            // ----- ----------------------------



            // PLAYER ----------------------------
            this.player.Update(gameTime);
            // ------ ----------------------------

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

            foreach(BulletSpawner s in spawners)
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
 *      TODO:
 * 
 1. JSON script draft

 2. Adding controllers between maingame and classes
    - spawning
    - collision detections
    - movement
 
 3. ScriptMovement : Movement

 4. Make enemy and bullet more similar

 5. Extract keyboard input away from sprites

 6. BulletSpawner spawned directly after Enemy using BulletSpawnerFactory


      BUGS:

      Enemy.Position += movement.Move() does not work with any movement vector 
        containing values less than 1 because of integer casting int the Enemy.Position PROPERTY. 
        Position is in PIXELS therefore we cannot move by .5 of a pixel. 
        For this reason, a movement of 0.5 will cause the enemy to infinitely
        stall. Now, minimum cardinal movement is set to 1 in any direction.

 */
