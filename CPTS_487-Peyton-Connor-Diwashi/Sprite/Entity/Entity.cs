﻿using Microsoft.Xna.Framework;
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

        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = value;
            }
        }

        public bool Dead
        {
            get
            {
                if (this.health <= 0) { return true;  } return false;
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
        public Entity(Vector2 position, Texture2D texture)
        {
            this.Color = Color.White;
            this.tex = texture;
            this.body = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.attackTarget = new Vector2(300, 500);
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
        /// Decrement the health of this Enemy by amount
        /// </summary>
        /// <param name="amount"></param>
        public void TakeDamage(int amount)
        {
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
                    base.InvokeDispose(this, new EventArgs());
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


        public override void Collide(Sprite sender, EventArgs e) { }
    }
}