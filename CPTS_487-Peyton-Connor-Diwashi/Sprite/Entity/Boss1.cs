using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{

    public class Boss1 : Entity
    {
        public Boss1(Vector2 position, Texture2D texture) : base(position, texture) { }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw this sprite
            base.Draw(gameTime, spriteBatch);
        }

        public override void Collide(Sprite sender, EventArgs e)
        {
            if (sender is Bullet)
            {
                LogConsole.LogPosition("Boss1 was hit by player", this.X, this.Y);
                this.TakeDamage(1);
            }
        }
    }
}