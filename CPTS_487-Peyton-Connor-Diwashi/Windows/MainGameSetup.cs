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
        EntityManager EntityManager = null;
        EntityEventManager EventManager = null;


        // This will be changed by Program.cs as needed
        public Vector2 currentWindowResolution = new Vector2(1280, 720);
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GUIComponent> gameOverButtons = new List<GUIComponent>();
        private List<Sprite> CollisionList = new List<Sprite>();
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
            // Do this first so we have our textures bound
            this.InitTextures();

            // Set scale factor for all objects
            this.scaleFactor = this.currentWindowResolution.Y / 720.0f;
            this._graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            this._graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            this._graphics.ApplyChanges();

            this.EntityManager = new EntityManager();
            this.EventManager = new EntityEventManager();
            this.EntityManager.EventManager = EventManager;
            this.EventManager.ObjectManager = EntityManager;

            this.EntityManager.EnqueuePlayer(SpawnerFactory.SpawnerType.Keyboard);

            base.Initialize();
        }

        protected void InitTextures()
        {
            TextureManager texManager = TextureManager.Textures;
            texManager.Add(this.Content.Load<Texture2D>("Grunt1"), TextureManager.Type.Grunt1);
            texManager.Add(this.Content.Load<Texture2D>("Grunt2"), TextureManager.Type.Grunt2);
            texManager.Add(this.Content.Load<Texture2D>("Boss1"), TextureManager.Type.Boss1);
            texManager.Add(this.Content.Load<Texture2D>("Boss2"), TextureManager.Type.Boss2);          
            texManager.Add(this.Content.Load<Texture2D>("BulletGreen"), TextureManager.Type.BulletGreen);
            texManager.Add(this.Content.Load<Texture2D>("BulletPurple"), TextureManager.Type.BulletPurple);
            texManager.Add(this.Content.Load<Texture2D>("BossBullet"), TextureManager.Type.BossBullet);         
            texManager.Add(Content.Load<Texture2D>("spaceship_player"), TextureManager.Type.SpaceshipPlayer);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}