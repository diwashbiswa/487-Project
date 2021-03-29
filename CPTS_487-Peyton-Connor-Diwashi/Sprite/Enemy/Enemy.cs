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
    public abstract class Enemy : Sprite
    {
        protected Movement movement;

        // The target the Enemy is Focused on
        public Vector2 attackTarget;

        // Is the enemy bound to a target
        protected bool boundToTarget = false;

        //To be invoked when the enemy is removed
        public event EventHandler Dispose;

        // Texture to draw on the enemy
        private Texture2D tex;

        // Color to draw the enemy with
        private Color col;

        // Speed of enemy
        private uint speed;

        // Set a lifespan for this enemy
        private int lifespanSeconds = 0;

        // Lifespan timer when lifespan is set
        private float lifespanTimer = 0;

        // Enemy's health
        private int health;

        /// <summary>
        /// Direct interface to enemy X and Y coordinates
        /// </summary>
        public int X { get { return this.body.X; } private set { this.body.X = value; } }
        public int Y { get { return this.body.Y; } private set { this.body.Y = value; } }

        public int Width { get { return this.body.Width;  } }
        public int Height { get { return this.body.Height; } }

        public Movement Movement
        {
            get
            {
                return this.movement;
            }
            protected set
            {
                this.movement = value;
            }
        }

        /// <summary>
        /// Position of the (top-left) corner of the Enemy
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2((float)this.body.X, (float)this.body.Y);
            }
            set
            {
                Vector2 v = value;
                this.body.X = (int)v.X;
                this.body.Y = (int)v.Y;
            }
        }

        /// <summary>
        /// Returns a Rectangle representing the area consumed by this enemy
        /// </summary>
        public Rectangle Hitbox { get { return this.body; } private set { this.body = value; } }

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
        public Enemy(Vector2 position, Texture2D texture)
        {
            this.Color = Color.White;
            this.tex = texture;
            this.body = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.boundToTarget = false;
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
        /// 
        /// </summary>
        public void takeDamage()
        {
            this.health -= 20; //enemy takes 20 damages each time
        }

        public void die()
        {
            if (this.health <= 0)
            {
                this.Dispose.Invoke(this, new EventArgs()); //dispose the enemy
            }
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
            // super.Move()
            this.Move(gameTime);
            // super.Disposed()
            this.Disposed(gameTime, this.Dispose);
            // super.Attack()
            if (this.boundToTarget)
                this.Attack(gameTime, this.attackTarget);

            // if the enemy has a lifespan keep track and invoke dispose event
            if(this.lifespanSeconds != 0)
            {
                this.lifespanTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Trigger Dispose event to subscribers when timeer is done
                if (this.lifespanTimer >= this.lifespanSeconds)
                    this.Dispose.Invoke(this, new EventArgs());
            }

        }

        public override void Collide(Sprite sender)
        {
            // TBI
        }

        /// CONSIDER MOVING TO Spite.cs CONSIDER MAKING virtual
        /// <summary>
        /// Superclass Invokes when the enemy should be removed from the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="dispose"> Invoked when the Enemy should be destroyed </param>
        protected abstract void Disposed(GameTime gameTime, EventHandler dispose);

        /// <summary>
        /// Move the enemy
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void Move(GameTime gameTime);

        /// <summary>
        /// Attack a target, use base.BindToTarget and base.UnbindFromTarget to trigger the attack in an update.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="target"></param>
        protected abstract void Attack(GameTime gameTime, Vector2 target);

    }
}