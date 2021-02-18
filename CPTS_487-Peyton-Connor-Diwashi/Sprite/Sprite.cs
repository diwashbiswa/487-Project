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
        private int precidence = 0;

        /// <summary>
        /// Precidence for order in which items should be drawn. Lower is first.
        /// </summary>
        public int Precidence
        {
            get
            {
                return this.precidence;
            }
            set
            {
                this.precidence = value;
            }
        }

        public abstract void Scale(float n);

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Update(GameTime gameTime);

        // Keyboard input.. or computer controlled input
        public abstract void Move(GameTime gameTime);
    }
}