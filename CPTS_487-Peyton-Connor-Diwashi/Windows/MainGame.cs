using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


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

        private List<Sprite> collision_list = new List<Sprite>();

        private Player player;
        private Texture2D lives;
        private Rectangle livesPosition;
        private Vector2 pos;
        //SpriteFont font;

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
        /// Build a list of all sprites needed for collision detection
        /// </summary>
        protected void buildSpriteList()
        {
            collision_list.Clear();
            collision_list.Add(player);
            collision_list.AddRange(enemies);
            foreach (BulletSpawner s in spawners)
            {
                collision_list.AddRange(s.Bullets);
            }
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
        /// Add a spawner to list of enemy spawners
        /// </summary>
        /// <param name="s"></param>
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

            // Remove each spawner associated with the enemy
            foreach (BulletSpawner s in spawners)
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
            this.player.setHealth(5);
            //this.font = Content.Load<SpriteFont>("font");


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



            // COLLISION ---------------------------
            this.buildSpriteList();
            CollisionObserver.Collide(collision_list);
            // --------- ---------------------------


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
                if (this.enemies.Contains(s))
                {
                    this.enemies.Remove(s);
                }
            }
            this.disposedEnemies.Clear();
            this.target.X = this.player.X;
            this.target.Y = this.player.Y;
            foreach (Enemy s in enemies)
            {
                s.BindToTarget(this.target);
                s.Update(gameTime);
            }
            // ----- ----------------------------



            // PLAYER ----------------------------
            this.player.Update(gameTime);

            this.pos = new Vector2(50, 20);
            this.lives = Content.Load<Texture2D>("heart");
            this.livesPosition = new Rectangle((int)pos.X, (int)pos.Y, 20, 20);

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

            foreach (BulletSpawner s in spawners)
            {
                s.Draw(gameTime, _spriteBatch);
            }

            this.player.Draw(gameTime, _spriteBatch);


            // Displays player lives on the screen for each lives they have
            var incrementX = 30;
            for (int i = 0; i < player.getHealth(); i++)
            {
                var newPos = pos + new Vector2((incrementX * i), 0);
                _spriteBatch.Draw(lives, newPos, Color.Red);
            }
            //_spriteBatch.Draw(lives, livesPosition, Color.Red);

            if (player.getHealth() == 0)
            {
                Console.WriteLine("Player has lost all lives. He dead!");

                //Show game over screen
                GameOverPopUp(_spriteBatch, gameTime);

                //Exit();
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public void GameOverPopUp(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // Game Over text
            spriteBatch.DrawString(Content.Load<SpriteFont>("Font"), "Game Over", new Vector2(100, 100), Color.Blue);

            // Exit button
            GUIButton button_exit = new GUIButton(new Vector2(340, 600), Content.Load<Texture2D>("Button1"), Content.Load<SpriteFont>("Font"), Color.Black, "Exit");

            // Play again button
            GUIButton button_playgame = new GUIButton(new Vector2(20, 600), Content.Load<Texture2D>("Button1"), Content.Load<SpriteFont>("Font"), Color.Black, "Start");
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
