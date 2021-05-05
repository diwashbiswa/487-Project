using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class HealthBar : Sprite
    {
        private Texture2D tex;

        private GUIComponent parent;

        public Texture2D Texture
        {
            set { this.tex = value; }
        }

        public HealthBar(Vector2 position, Texture2D tex, GUIComponent parent) : base(position,tex.Width,tex.Height)
        {
            this.tex = tex;
            this.parent = parent;
        }

        public GUIComponent Parent
        {
            get
            {
                return this.parent;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tex, this.body, Color.White);
        }

        public override void Update(GameTime gameTime) { }

        public override void Collide(Sprite sender, EventArgs e) { }
    }
}
