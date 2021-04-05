using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace CPTS_487_Peyton_Connor_Diwashi
{
    public partial class MainGame : Game
    {
        // This will be changed by Program.cs as needed
        public Vector2 currentWindowResolution = new Vector2(1280, 720);

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private EnemyFactory ef;
        private SpawnerFactory sf;

        private List<Entitiy> enemies = new List<Entitiy>();
        private List<Entitiy> disposedEnemies = new List<Entitiy>();

        private List<BulletSpawner> spawners = new List<BulletSpawner>();
        private List<BulletSpawner> disposedSpawners = new List<BulletSpawner>();

        private List<Sprite> collisionList1 = new List<Sprite>();
        private List<Sprite> collisionList2 = new List<Sprite>();

        private List<GUIComponent> gameOverButtons = new List<GUIComponent>();

        private Player player;
        private Texture2D lives;
        private Rectangle livesPosition;
        private Vector2 pos;

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

        protected override void Initialize()
        {
            // Set scale factor for all objects
            this.scaleFactor = this.currentWindowResolution.Y / 720.0f;

            this._graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            this._graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            this._graphics.ApplyChanges();

            // Create Player
            this.player = new Player(new Vector2(500, 300), Content.Load<Texture2D>("spaceship_player"), 7.0f);
            player.Health = 5;

            // Create enemy and spawner factories
            this.ef = new StandardEnemyFactory(new Rectangle(50, 50, 1180, 600), Content);
            this.sf = new StandardSpawnerFactory(Content);

            // Set Event to Invoke when an enemies Lifespan is Up
            this.ef.DisposeMethod = DisposeEnemyEvent;
            // Set Enemy LifeSpan, only works when a DisposeMethod EventHandler is assigned
            this.ef.LifeSpanSeconds = 45;

            this.sf.Parent = this.player;
            this.AddSpawner(this.sf.CreateSpawner(SpawnerFactory.SpawnerType.Keyboard));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}