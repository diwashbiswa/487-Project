using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Collections.Concurrent;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class EntityManager
    {
        private EntityEventManager eventManager;
        private EntitiyFactory ef;
        private SpawnerFactory sf;
        private List<Entity> entities = new List<Entity>();
        private List<BulletSpawner> spawners = new List<BulletSpawner>();
        private List<Entity> players = new List<Entity>();

        private List<Bullet> player_bullets = new List<Bullet>();
        private List<Bullet> enemy_bullets = new List<Bullet>();

        /// <summary>
        /// This objects EventManager Observes sprites, subscribes to events, and populates queues
        /// </summary>
        public EntityEventManager EventManager
        {
            set
            {
                this.eventManager = value;
            }
        }

        /// <summary>
        /// All non-player entities in this manager
        /// </summary>
        public List<Entity> Entities
        {
            get
            {
                return this.entities;
            }
        }

        /// <summary>
        /// The first player added to the mamanger
        /// </summary>
        public Entity PlayerOne
        {
            get
            {
                return this.players.ElementAt(0);
            }
        }

        /// <summary>
        /// All players in this manager
        /// </summary>
        public List<Entity> Players
        {
            get
            {
                return this.players;
            }
        }

        /// <summary>
        /// All spawners in this manager
        /// </summary>
        public List<BulletSpawner> Spawners
        {
            get
            {
                return this.spawners;
            }
        } 

        /// <summary>
        /// All live bullets which were fired by a player
        /// </summary>
        public List<Bullet> PlayerBullets
        {
            get
            {
                return this.player_bullets;
            }
        }

        /// <summary>
        /// All bullets which were fired by a non-player entitiy
        /// </summary>
        public List<Bullet> EnemyBullets
        {
            get
            {
                return this.enemy_bullets;
            }
        }

        public EntityManager()
        {
            this.ef = new StandardEntityFactory(new Rectangle(50, 50, 1180, 600));
            this.sf = new StandardSpawnerFactory();
        }

        /// <summary>
        /// Add a player to this game
        /// </summary>
        /// <param name="spawner"> Add a spawner as well? </param>
        public void AddPlayer(SpawnerFactory.SpawnerType spawner = SpawnerFactory.SpawnerType.None)
        {

                Entity player = ef.CreateEnemy(EntitiyFactory.EntitiyType.Player);
                player.Health = 5;
                this.SubscribeAll(player);
                this.players.Add(player);

                if (spawner != SpawnerFactory.SpawnerType.None)
                {
                    this.sf.Parent = player;
                    BulletSpawner s = this.sf.CreateSpawner(spawner);
                    this.SubscribeAll(s);
                    this.spawners.Add(s);
                }
        }

        /// <summary>
        /// Add a sprite to this game
        /// </summary>
        /// <param name="type"> Type of sprite </param>
        /// <param name="spawner"> Add a spawner as well? </param>
        public void Add(EntitiyFactory.EntitiyType type, SpawnerFactory.SpawnerType spawner = SpawnerFactory.SpawnerType.None)
        {
                Entity e = ef.CreateEnemy(type);
                this.SubscribeAll(e);
                this.entities.Add(e);

                if (spawner != SpawnerFactory.SpawnerType.None)
                {
                    this.sf.Parent = e;
                    BulletSpawner s = this.sf.CreateSpawner(spawner);
                    this.SubscribeAll(s);
                    this.spawners.Add(s);
                }
        }

        /// <summary>
        /// Update all sprites
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Read the queues for dynamic insertion and removal of sprites
            this.ReadDisposeQueue(this.eventManager.DisposeQueue);
            this.ReadReadyQueue(this.eventManager.ReadyQueue);

                foreach (Player p in this.players)
                {
                    p.Update(gameTime);
                }
                foreach (Entity e in this.entities)
                {
                    e.Update(gameTime);
                }
                foreach (BulletSpawner s in this.spawners)
                {
                    s.Update(gameTime);
                }
                foreach (Bullet b in this.player_bullets)
                {
                    b.Update(gameTime);
                }
                foreach (Bullet b in this.enemy_bullets)
                {
                    b.Update(gameTime);
                }
        }

        /// <summary>
        /// Draw all sprites
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
                foreach (Player p in this.players)
                {
                    p.Draw(gameTime, spriteBatch);
                }
                foreach (Entity e in this.entities)
                {
                    e.Draw(gameTime, spriteBatch);
                }
                foreach (BulletSpawner s in this.spawners)
                {
                    s.Draw(gameTime, spriteBatch);
                }
                foreach (Bullet b in this.player_bullets)
                {
                    b.Draw(gameTime, spriteBatch);
                }
                foreach (Bullet b in this.enemy_bullets)
                {
                    b.Draw(gameTime, spriteBatch);
                }
        }

        /// <summary>
        /// Subscribe the EventManager to all Entity Events
        /// </summary>
        /// <param name="e"></param>
        private void SubscribeAll(Sprite e)
        {
            e.Dispose += this.eventManager.Dispose;

            if (e is BulletSpawner)
            {
                BulletSpawner b = (BulletSpawner)e;
                b.Fire += this.eventManager.Fire;
            }
        }

        /// <summary>
        /// Insert sprites into game-lists from a queue
        /// </summary>
        /// <param name="queue"></param>
        private void ReadReadyQueue(ConcurrentQueue<EventArgs> queue)
        {
            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                EventArgs e;
                if (queue.TryDequeue(out e))
                {
                    // Decide which list we will add to depending on type of EventArgs
                    if (e is BulletEventArgs)
                    {
                        this.ReadBullet((BulletEventArgs)e);
                    }

                }
            }
        }

        /// <summary>
        /// Remove sprites from game-lists from a queue
        /// </summary>
        /// <param name="queue"></param>
        private void ReadDisposeQueue(ConcurrentQueue<DisposeEventArgs> queue)
        {
            int count = queue.Count;

            for (int i = 0; i < count; i++)
            {
                DisposeEventArgs e;
                Sprite s;
                if (queue.TryDequeue(out e))
                {
                    s = e.Sprite;
                    if (s == PlayerOne)
                    {
                        return;
                    }

                    if (this.entities.Contains(s))
                    {
                        this.entities.Remove((Entity)s);
                        this.spawners.RemoveAll(x => x.parent == s);
                    }

                    if (this.players.Contains(s))
                    {
                        this.players.Remove((Entity)s);
                    }

                    if (s is Bullet)
                    {
                        if (this.player_bullets.Contains(s))
                        {
                            this.player_bullets.Remove((Bullet)s);
                        }

                        if (this.enemy_bullets.Contains(s))
                        {
                            this.enemy_bullets.Remove((Bullet)s);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Add a bullet by EventArgs passed from the EventManager Concurrent Queue
        /// </summary>
        /// <param name="e"></param>
        private void ReadBullet(BulletEventArgs e)
        {
            Bullet b = e.Bullet;

            if (e.Parent is Player)
            {
                this.player_bullets.Add(b);
            }
            else
            {
                this.enemy_bullets.Add(b);
            }
        }
    }
}
