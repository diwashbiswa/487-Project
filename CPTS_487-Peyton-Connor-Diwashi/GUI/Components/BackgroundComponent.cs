using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class BackgroundComponent : GUIComponent
    {
        private Texture2D tex;
        private Rectangle box;
        public BackgroundComponent()
        {
            TextureManager state = TextureManager.Textures;
            this.tex = state.Get(TextureManager.Type.Background);
            this.box = new Rectangle(0, 0, tex.Width, tex.Height);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tex, box, Color.White);
        }
        public override void Update(GameTime gameTime) { }

        public override void Scale(float n) { }
    }
}
