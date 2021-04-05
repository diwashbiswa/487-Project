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

        public Rectangle Body
        {
            get
            {
                return this.body;
            }
        }

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

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Method to be called when this sprite collides with sender
        /// </summary>
        /// <param name="sender"> The sprite this sprite collided with </param>
        public abstract void Collide(Sprite sender, EventArgs e);
    }
}