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
    /// <summary>
    /// Manages the addition and removal of sprites in this game
    /// </summary>
    public partial class EntityManager
    {
        Vector2 target = new Vector2(0, 0);
        public event EventHandler Exit = delegate { };
        private EntityEventManager eventManager;
        private EntityFactory ef;
        private SpawnerFactory sf;
        private MovementFactory mf;
        private List<Entity> entities = new List<Entity>();
        private List<BulletSpawner> spawners = new List<BulletSpawner>();
        private List<Entity> players = new List<Entity>();
        private List<Bullet> player_bullets = new List<Bullet>();
        private List<Bullet> enemy_bullets = new List<Bullet>();
        private List<Reward> rewards = new List<Reward>();
        private List<GUIComponent> gui_components = new List<GUIComponent>();

        /// <summary>
        /// Returns true if player one is ready.
        /// </summary>
        bool PlayerOneReady
        {
            get
            {
                if (this.players.Count > 0)
                    return true;
                return false;
            }
        }

        /// <summary>
        /// This objects EventManager Observes sprites, subscribes to events, and populates queues
        /// </summary>
        public EntityEventManager EventManager
        {
            set
            {
                this.eventManager = value;
            }
            get
            {
                return this.eventManager;
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
                if (this.players.Count > 0)
                    return this.players.ElementAt(0);

                TextureManager state = TextureManager.Textures;
                return new Player(new Vector2(0, 0), state.Get(TextureManager.Type.SpaceshipPlayer), 0.0f);
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


        public List<Reward> Rewards
        {
            get
            {
                return this.rewards;
            }
        }
        /// <summary>
        /// All dynamic gui components currently being drawn
        /// </summary>
        public List<GUIComponent> GUIComponents
        {
            get
            {
                return this.gui_components;
            }
        }

        public EntityManager()
        {
            this.ef = new StandardEntityFactory();
            this.sf = new StandardSpawnerFactory();
            this.mf = new StandardMovementFactory();
        }

        /// <summary>
        /// Add a player to this game through the queue
        /// </summary>
        /// <param name="spawner"> Add a spawner as well? </param>
        public void AddPlayerOne(SpawnerFactory.SpawnerType spawner = SpawnerFactory.SpawnerType.None)
        {
            Entity player = ef.CreateEnemy(EntityFactory.EntitiyType.Player);
            player.MakeInvincible(3);
            player.Position = new Vector2(600, 600);
            GUIComponent plComponent = new PlayerLives();
            eventManager.ReadyEnqueue(plComponent, new AddGUIEventArgs(plComponent, player));
            eventManager.ReadyEnqueue(player, new AddPlayerEventArgs((Player)player));
            if (spawner != SpawnerFactory.SpawnerType.None)
            {
                BulletSpawner s = this.sf.CreateSpawner(spawner, player);
                MirrorMovement m = (MirrorMovement)mf.CreateMovement(MovementFactory.MovementType.Mirror, player);
                m.ThisSprite = s;
                s.Movement = m;
                eventManager.ReadyEnqueue(s, new AddSpawnerEventArgs(player, s));
            }
        }

        /// <summary>
        /// Update all sprites
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            // Read the queues for insertion, removal, updating of sprites
            this.ReadDisposeQueue(this.eventManager.DisposeQueue);
            this.ReadReadyQueue(this.eventManager.ReadyQueue);
            this.ReadUpdateQueue(this.eventManager.UpdateQueue);

            // Tell all entities to attack player by setting their attack target
            this.BindEntitiesToPlayer();

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
            foreach (Reward r in this.rewards)
            {
                r.Update(gameTime);
            }
            foreach (GUIComponent g in this.gui_components)
            {
                g.Update(gameTime);
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
            foreach (Reward r in this.rewards)
            {
                r.Draw(gameTime, spriteBatch);
            }
            foreach (GUIComponent g in this.gui_components)
            {
                g.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// Subscribe the EventManager to all Sprite * Events
        /// </summary>
        /// <param name="e"></param>
        public void SubscribeAll(Sprite e)
        {
            // Every Sprite has Dispose
            e.Dispose += this.eventManager.Dispose;

            // Entitiy Specific Events
            if (e is Entity)
            {
                Entity v = (Entity)e;
                v.Collided += this.eventManager.Collided;
            }

            // BulletSpawner Specific Events
            if (e is BulletSpawner)
            {
                BulletSpawner b = (BulletSpawner)e;
                b.Fire += this.eventManager.Fire;
            }
        }

        /// <summary>
        /// Tells each entitiy to aim at the player (if targeted)
        /// </summary>
        private void BindEntitiesToPlayer()
        {
            this.target.X = this.PlayerOne.X;
            this.target.Y = this.PlayerOne.Y;
            foreach (Entity s in Entities)
            {
                s.BindToTarget(this.target);
            }
        }

        /// <summary>
        /// Invokes an Exit event for the game application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ExitGame(object sender, EventArgs e)
        {
            this.Exit.Invoke(sender, e);
        }
    }
}
