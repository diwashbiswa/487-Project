using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public partial class MainGame : Game
    {
        public Vector2 currentWindowResolution = new Vector2(1280, 720);
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private float scaleFactor;
        EntityManager EntityManager = null;
        EntityEventManager EventManager = null;
        WaveManager WaveManager = null;
        private List<Sprite> CollisionList = new List<Sprite>();

        public MainGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {     
            this.InitTextures(); // DO THIS FIRST
            this.scaleFactor = this.currentWindowResolution.Y / 720.0f;
            this._graphics.PreferredBackBufferHeight = (int)this.currentWindowResolution.Y;
            this._graphics.PreferredBackBufferWidth = (int)this.currentWindowResolution.X;
            this._graphics.ApplyChanges();
            this.EntityManager = new EntityManager();
            this.EventManager = new EntityEventManager();
            this.WaveManager = new WaveManager();
            this.EntityManager.EventManager = EventManager;
            this.EventManager.ObjectManager = EntityManager;
            this.EventManager.WaveManager = WaveManager;
            this.EntityManager.Exit += this.Exit;
            this.EntityManager.AddPlayerOne(SpawnerFactory.SpawnerType.Keyboard);


            // WAVE TESTING: will be done from waveparser in the future --------------------------
            EntityFactory ef = new StandardEntityFactory(new Rectangle(50, 50, 1180, 600));
            SpawnerFactory sf = new StandardSpawnerFactory();
            SpriteWave wave_one = new SpriteWave();
            for (int i = 0; i < 3; i++)
            {
                Entity e = ef.CreateEnemy(EntityFactory.EntitiyType.Grunt1);
                BulletSpawner s = sf.CreateSpawner(SpawnerFactory.SpawnerType.CardinalSouth, e);
                e.WaveTimeSeconds = i*2;
                s.WaveTimeSeconds = i*2;
                wave_one.AddEntitiy(e);
                wave_one.AddSpawner(s);
            }
            this.WaveManager.Enqueue(wave_one);
            SpriteWave wave_2 = new SpriteWave();
            for (int i = 0; i < 3; i++)
            {
                Entity e = ef.CreateEnemy(EntityFactory.EntitiyType.Grunt2);
                BulletSpawner s = sf.CreateSpawner(SpawnerFactory.SpawnerType.CardinalSouth, e);
                e.WaveTimeSeconds = (i * 2) + 5;
                s.WaveTimeSeconds = (i * 2) + 5;
                wave_2.AddEntitiy(e);
                wave_2.AddSpawner(s);
            }
            this.WaveManager.Enqueue(wave_2);
            // ----------------------------------------------------------------------------------


            this.WaveManager.StartNextWave();
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
            texManager.Add(this.Content.Load<Texture2D>("spaceship_player"), TextureManager.Type.SpaceshipPlayer);
            texManager.Add(this.Content.Load<Texture2D>("heart"), TextureManager.Type.Heart);
            texManager.Add(this.Content.Load<Texture2D>("Button1"), TextureManager.Type.Button1);
            texManager.AddFont(this.Content.Load<SpriteFont>("Font"), TextureManager.Font.Font1);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}