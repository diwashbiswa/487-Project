using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class HealthBarComponent : GUIComponent
    {
        Entity parent;
        HealthBar bar;
        Texture2D currentTex;
        TextureManager state;
        Vector2 pos = Vector2.Zero;

        public event EventHandler<DisposeEventArgs> Dispose = delegate { };

        public HealthBarComponent(Entity parent)
        {
            this.state = TextureManager.Textures;
            this.parent = parent;
            this.parent.Dispose += ParentDisposed;
            this.currentTex = this.Health2Tex(parent.InitialHealth, parent.Health);
            this.SetPosition(this.parent.Position);
            this.bar = new HealthBar(this.pos, this.currentTex, this);
        }

        private Texture2D Health2Tex(int initial_health, int current_health)
        {
            float percent = (float)current_health / (float)initial_health;

            TextureManager.Type t;
            if (percent >= .75f) { t = TextureManager.Type.HB100; }
            else if (percent >= .5f) { t = TextureManager.Type.HB75; }
            else if (percent >= .25f) { t = TextureManager.Type.HB50; }
            else { t = TextureManager.Type.HB25; }

            Texture2D tex = state.Get(t);
            return tex; 
        }

        private void SetPosition(Vector2 parent_pos)
        {
            this.pos = parent_pos;
            this.pos.X += Math.Abs(((float)parent.Width - (float)this.currentTex.Width)) / 2;
            this.pos.Y -= 10.0f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            this.bar.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            this.currentTex = this.Health2Tex(parent.InitialHealth, parent.Health);
            this.bar.Texture = this.currentTex;
            this.SetPosition(this.parent.Position);
            this.bar.Position = this.pos;
            this.bar.Update(gameTime);
        }

        public override void Scale(float n) { throw new NotImplementedException(); }

        public void ParentDisposed(object sender, DisposeEventArgs e)
        {
            this.Dispose.Invoke(this, new DisposeEventArgs(this.bar));
        }
    }
}
