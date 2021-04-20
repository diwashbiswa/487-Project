using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class PlayerLives : GUIComponent
    {
        private Texture2D lives;
        private Vector2 pos;
        Vector2 newPos;
        Player parent = null;

        public Player Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        public PlayerLives() 
        {
            TextureManager state = TextureManager.Textures;
            this.lives = state.Get(TextureManager.Type.Heart);
            this.pos = new Vector2(50, 20);
            this.newPos = new Vector2(0, 0);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < this.parent.Health; i++)
            {
                this.newPos = this.pos + new Vector2((30 * i), 0);
                spriteBatch.Draw(this.lives, this.newPos, Color.Red);
            }
        }

        public override void Update(GameTime gameTime) { }

        public override void Scale(float n) { }
    }
}