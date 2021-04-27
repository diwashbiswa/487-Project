using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public abstract class Reward : Sprite
    {
        // Movement of this sprite (Linear Direction)
        protected Movement movement = null;

        // How long will the Reward last
        private float lifespanSeconds;

        // Incremental timer for how long the bullet has been alive
        private float timer;

        // Texture fo the Reward
        private Texture2D tex;

        private Color col;

        public Color Color
        {
            get
            {
                return this.col;
            }
            set
            {
                this.col = value;
            }
        }

        /// <summary>
        /// Creates a new instance of a bullet using targeted movement
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="speed"></param>
        /// <param name="lifespan_seconds"></param>
        public Reward(Vector2 position, Texture2D texture, float speed, float lifespan_seconds)
        {
            this.lifespanSeconds = lifespan_seconds;
            this.tex = texture;
            this.body = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.Position = position;
            this.col = Color.White;
        }

        /// <summary>
        /// Draw the bullet on screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tex, this.body, this.col);
        }

        /// <summary>
        /// Update the position of the bullet
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Trigger Dispose event to subscribers when timeer is done
            if (timer >= this.lifespanSeconds)
                base.InvokeDispose(this, new EventArgs());

            this.Position += this.movement.Move();
        }

        /// <summary>
        /// When the bullet hits the player it is disposed
        /// </summary>
        /// <param name="sender"></param>
        public override void Collide(Sprite sender, EventArgs e)
        {
            if (sender is Entity)
            {
                base.InvokeDispose(this, new EventArgs());
            }
        }
    }
}