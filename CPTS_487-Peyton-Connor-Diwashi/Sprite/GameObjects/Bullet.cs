using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    class Bullet : Sprite
    {
        // Rectangle for Bullets Hitbox
        private Rectangle box;

        // Movement of this sprite (Linear Direction)
        private Movement movement = null;

        // How long will the Bullet last
        private float lifespanSeconds;

        // Incremental timer for how long the bullet has been alive
        private float timer;

        // Texture fo the Bullet
        private Texture2D tex;

        // To be Invoked when the bullet should be removed
        public event EventHandler Dispose = delegate { };

        // X,Y Position of the Bullet
        public Vector2 Position
        {
            get
            {
                return new Vector2((float)this.box.X, (float)this.box.Y);
            }
            set
            {
                Vector2 v = value;
                this.box.X = (int)v.X;
                this.box.Y = (int)v.Y;
            }
        }

        /// <summary>
        /// Creates a new instance of a bullet using targeted movement
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="speed"></param>
        /// <param name="lifespan_seconds"></param>
        public Bullet(Vector2 position, Vector2 target, Texture2D texture, float speed, float lifespan_seconds)
        {
            this.movement = new LinearDirectionMovement(speed, position, target);
            this.lifespanSeconds = lifespan_seconds;
            this.tex = texture;
            this.box = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.Position = position;
        }

        /// <summary>
        /// creates a new instance of a bullet using cardinal movement
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="texture"></param>
        /// <param name="speed"></param>
        /// <param name="lifespan_seconds"></param>
        public Bullet(Vector2 position, Movement.CardinalDirection direction, Texture2D texture, float speed, float lifespan_seconds)
        {
            this.movement = new CardinalMovement(speed, direction);
            this.lifespanSeconds = lifespan_seconds;
            this.tex = texture;
            this.box = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.Position = position;
        }

        /// <summary>
        /// Draw the bullet on screen
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tex, this.box, Color.White);
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
                this.Dispose.Invoke(this, new EventArgs());

            this.Position += this.movement.Move();
        }
    }
}