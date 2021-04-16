using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class EntityManager
    {
        private static object _lock = new object();

        private ContentManager Content;

        private EntityEventManager eventManager;

        private EnemyFactory ef;
        private SpawnerFactory sf;

        private List<Entitiy> entities = new List<Entitiy>();
        private List<BulletSpawner> spawners = new List<BulletSpawner>();
        private List<Player> players = new List<Player>();

        private List<Bullet> player_bullets = new List<Bullet>();
        private List<Bullet> enemy_bullets = new List<Bullet>();

        /// <summary>
        /// Locking object for all list operations
        /// </summary>
        public object Lock
        {
            get
            {
                return _lock;
            }
        }

        public EntityEventManager EventManager
        {
            set
            {
                this.eventManager = value;
            }
        }

        public List<Entitiy> Entities
        {
            get
            {
                return this.entities;
            }
        }

        public Player PlayerOne
        {
            get
            {
                return this.players[0];
            }
        }

        public List<Player> Players
        {
            get
            {
                return this.players;
            }
        }

        public List<BulletSpawner> Spawners
        {
            get
            {
                return this.spawners;
            }
        }

        public List<Bullet> PlayerBullets
        {
            get
            {
                this.player_bullets.Clear();
                foreach(BulletSpawner s in this.spawners)
                {
                    if(s.parent is Player)
                    {
                        this.player_bullets.AddRange(s.Bullets);
                    }
                }
                return this.player_bullets;
            }
        }

        public List<Bullet> EnemyBullets
        {
            get
            {
                this.enemy_bullets.Clear();
                foreach (BulletSpawner s in this.spawners)
                {
                    if (s.parent is not Player)
                    {
                        this.enemy_bullets.AddRange(s.Bullets);
                    }
                }
                return this.enemy_bullets;
            }
        }

        public EntityManager(ContentManager Content)
        {
            this.Content = Content;
            this.ef = new StandardEnemyFactory(new Rectangle(50, 50, 1180, 600), Content);
            this.sf = new StandardSpawnerFactory(Content);
        }

        public void AddPlayer(SpawnerFactory.SpawnerType spawner = SpawnerFactory.SpawnerType.None)
        {
            lock (this.Lock)
            {
                Player player = new Player(new Vector2(500, 300), Content.Load<Texture2D>("spaceship_player"), 7.0f);
                player.Health = 5;
                this.SubscribeAll(player);
                this.players.Add(player);

                if (spawner != SpawnerFactory.SpawnerType.None)
                {
                    this.sf.Parent = player;
                    this.spawners.Add(this.sf.CreateSpawner(SpawnerFactory.SpawnerType.Keyboard));
                }
            }
        }

        public void Add(EnemyFactory.EnemyType type, SpawnerFactory.SpawnerType spawner = SpawnerFactory.SpawnerType.None)
        {
            lock (this.Lock)
            {
                Entitiy e = ef.CreateEnemy(type);
                this.SubscribeAll(e);
                this.entities.Add(e);

                if (spawner != SpawnerFactory.SpawnerType.None)
                {
                    this.sf.Parent = e;
                    this.spawners.Add(sf.CreateSpawner(spawner));
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach(Player p in this.players)
            {
                p.Update(gameTime);
            }
            foreach(Entitiy e in this.entities)
            {
                e.Update(gameTime);
            }
            foreach(BulletSpawner s in this.spawners)
            {
                s.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Player p in this.players)
            {
                p.Draw(gameTime, spriteBatch);
            }
            foreach (Entitiy e in this.entities)
            {
                e.Draw(gameTime, spriteBatch);
            }
            foreach (BulletSpawner s in this.spawners)
            {
                s.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Subscribe the EventManager to all Entity Events
        /// </summary>
        /// <param name="e"></param>
        private void SubscribeAll(Entitiy e)
        {
            e.Dispose += this.eventManager.Dispose;
        }
    }
}
