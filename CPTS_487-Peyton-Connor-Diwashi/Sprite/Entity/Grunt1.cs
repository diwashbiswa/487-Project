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
    public class Grunt1 : Entity
    {
        public Grunt1(Vector2 position, Texture2D texture) : base(position, texture)
        {
            this.Speed = 1;
            this.movement = new BounceMovement(this.Speed, new Rectangle(0, 0, 1280, 720), this.body);
        }

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
                LogConsole.LogPosition("Grunt1 was hit by player", this.X, this.Y);
                base.InvokeDispose(this, e);
            }
        }
    }
}