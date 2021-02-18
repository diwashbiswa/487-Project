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
        // Represents the body and size of the enemy
        private Rectangle body;

        // The target the Enemy is Focused on
        protected Rectangle attackTarget;
        // Is the enemy bound to a target
        protected bool boundToTarget = false;

        // Texture to draw on the enemy
        private Texture2D tex;

        // Color to draw the enemy with
        private Color col;

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
        /// X,Y Interface for moving the enemy, same as changing Position
        /// </summary>
        public int X { get { return this.body.X; } set { this.body.X = value; } }
        public int Y { get { return this.body.Y; } set { this.body.Y = value; } }

        /// <summary>
        /// Returns a Rectangle representing the area consumed by this enemy
        /// </summary>
        public Rectangle Hitbox { get { return this.body; } }

        /// <summary>
        /// Draw color for the enemy texture
        /// </summary>
        public Color Color { get { return this.col; } set { this.col = value; } }

        /// <summary>
        /// Returns true if the enemy is bound to an attack target
        /// </summary>
        public bool IsBountToTarget { get { return this.boundToTarget; } }

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
        public void BindToTarget(ref Rectangle t)
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
        /// Scale the enemy by a factor
        /// </summary>
        /// <param name="n"> scale factor </param>
        public override void Scale(float n)
        {
            this.body.X = (int)((float)this.body.X * n);
            this.body.Y = (int)((float)this.body.Y * n);
            this.body.Width = (int)((float)this.body.Width * n);
            this.body.Height = (int)((float)this.body.Height * n);
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
            if (this.boundToTarget)
            {
                //super.Attack()
                this.Attack(gameTime, this.attackTarget);
            }

            // Update properties for ALL enemies.
        }

        /// <summary>
        /// Move the enemy
        /// </summary>
        /// <param name="gameTime"></param>
        public abstract void Move(GameTime gameTime);

        /// <summary>
        /// Attack a target, use base.BindToTarget and base.UnbindFromTarget to trigger the attack in an update.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="target"></param>
        public abstract void Attack(GameTime gameTime, Rectangle target);
    }
}