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
    public abstract class Sprite
    {
        private int draw_order = 0;

        protected Rectangle body = new Rectangle(0, 0, 0, 0);

        //To be invoked when the enemy is removed
        public event EventHandler Dispose = delegate { };

        public Rectangle Body
        {
            get
            {
                return this.body;
            }
        }

        /// <summary>
        /// Position of the (top-left) corner of the Sprite
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
        /// Direct interface to Sprite X and Y coordinates
        /// </summary>
        public int X { get { return this.body.X; } private set { this.body.X = value; } }
        public int Y { get { return this.body.Y; } private set { this.body.Y = value; } }

        public int Width { get { return this.body.Width; } }
        public int Height { get { return this.body.Height; } }

        /// <summary>
        /// Precidence for order in which items should be drawn. Lower is behind.
        /// </summary>
        public int DrawOrder
        {
            get
            {
                return this.draw_order;
            }
            set
            {
                this.draw_order = value;
            }
        }

        /// <summary>
        /// Notify subscribers that this Sprite is done 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InvokeDispose(object sender, EventArgs e)
        {
            this.Dispose.Invoke(sender, e);
        }

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Method to be called when this sprite collides with sender
        /// </summary>
        /// <param name="sender"> The sprite this sprite collided with </param>
        public abstract void Collide(Sprite sender, EventArgs e);
    }
}