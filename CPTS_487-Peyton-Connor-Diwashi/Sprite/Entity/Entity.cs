using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// All Sprite objects extend from here
    /// </summary>
    public abstract class Entity : Sprite
    {
        protected Movement movement;

        // The target the Enemy is Focused on
        public Vector2 attackTarget;

        // Is the enemy bound to a target
        protected bool boundToTarget = false;

        // Texture to draw on the enemy
        private Texture2D tex;

        // Color to draw the enemy with
        protected Color col;

        // Speed of enemy
        private uint speed;

        // Set a lifespan for this enemy
        private int lifespanSeconds = 0;

        // Lifespan timer when lifespan is set
        private float lifespanTimer = 0;

        // Enemy's health
        private int health = 1;

        // Health this enemy was spawned with
        private int initialHealth = 1;

        bool invincible = false;
        private float invincibleTimer = 0;
        private int invincibleSeconds = 0;

        public EventHandler<EntityCollideEventArgs> Collided = delegate { };

        public Movement Movement
        {
            get
            {
                return this.movement;
            }
            set
            {
                this.movement = value;
            }
        }

        public int InitialHealth
        {
            get { return this.initialHealth; }
        }

        bool fs = true;
        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = value;
                if(fs) { this.initialHealth = value; this.fs = false; }
            }
        }

        public bool Dead
        {
            get
            {
                if (this.health <= 0) { return true;  } return false;
            }
        }

        public bool Invincible
        {
            get
            {
                return this.invincible;
            }
            set
            {
                this.invincible = value;
            }
        }

        /// <summary>
        /// Draw color for the enemy texture
        /// </summary>
        public Color Color { get { return this.col; } set { this.col = value; } }

        /// <summary>
        /// Returns true if the enemy is bound to an attack target
        /// </summary>
        public bool IsBoundToTarget { get { return this.boundToTarget; } }

        /// <summary>
        /// Gets and sets the speed of the Enemy during transformations
        /// </summary>
        public uint Speed { get { return this.speed; } set { this.speed = value; } }

        /// <summary>
        /// Set a lifespan in seconds for this enemy
        /// </summary>
        public uint LifeSpan { set { this.lifespanSeconds = (int)value; } }

        /// <summary>
        /// Initialize the base class for Enemy
        /// </summary>
        /// <param name="position"> position of the enemy on the screen </param>
        /// <param name="texture"> texture to draw the enemy with </param>
        public Entity(Vector2 position, Texture2D texture) : base(position, texture.Width, texture.Height)
        {
            this.Color = Color.White;
            this.tex = texture;
            this.attackTarget = Vector2.Zero;
            this.speed = 1;

            StandardMovementFactory mf = new StandardMovementFactory();
            this.movement = mf.CreateMovement(MovementFactory.MovementType.None, this);
        }

        /// <summary>
        /// Bind the enemy to a target to attack.
        /// </summary>
        /// <param name="t"> attack target </param>
        public void BindToTarget(Vector2 t)
        {
            this.boundToTarget = true;
            this.attackTarget = t;
        }

        /// <summary>
        /// Unbind the enemy from its target.
        /// </summary>
        public void UnbindFromTarget()
        {
            this.boundToTarget = false;
        }

        /// <summary>
        /// Make this entitiy invincible for seconds
        /// </summary>
        /// <param name="seconds"></param>
        public void MakeInvincible(int seconds)
        {
            this.invincible = true;
            this.invincibleSeconds = seconds;
            this.invincibleTimer = 0;
        }

        /// <summary>
        /// Decrement the health of this Enemy by amount
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
            if (this.invincible)
                return;

            if (amount >= this.health)
            {
                this.health = 0;
                this.InvokeDispose(this, new EventArgs());
            }
            else
            {
                this.health -= amount; //enemy takes 20 damages each time
            }
            return;
        }

        /// <summary>
        /// Draw the enemy with the given spriteBatch
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tex, this.body, this.col);
        }

        /// <summary>
        /// Update for all enemies
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Move this entity
            this.Move(gameTime);

            // if the enemy has a lifespan keep track and invoke dispose event
            if(this.lifespanSeconds != 0)
            {
                this.lifespanTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Trigger Dispose event to subscribers when timeer is done
                if (this.lifespanTimer >= this.lifespanSeconds)
                {
                    base.InvokeDispose(this, new EventArgs());
                }
            }

            // invincibility
            if (this.invincibleSeconds != 0)
            {
                this.col = Color.BlueViolet;
                this.invincibleTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (this.invincibleTimer >= this.invincibleSeconds)
                {
                    this.col = Color.White;
                    this.invincible = false;
                    this.invincibleSeconds = 0;
                    this.invincibleTimer = 0;
                }
            }

        }

        /// <summary>
        /// Move the enemy
        /// </summary>
        /// <param name="gameTime"></param>
        protected void Move(GameTime gameTime)
        {
            this.Position += movement.Move();
        }

        public void InvokeCollide(object sender, EntityCollideEventArgs e)
        {
            this.Collided.Invoke(sender, e);
        }

        public override void Collide(Sprite sender, EventArgs e) { }
    }
}