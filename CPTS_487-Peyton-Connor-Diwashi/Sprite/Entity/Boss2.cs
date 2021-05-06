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
    public class Boss2 : Entity
    {
        int lastspecial = 0;

        public event EventHandler<AddSpawnerEventArgs> NewSpawner = delegate { };

        public Boss2(Vector2 position, Texture2D texture) : base(position, texture) { }

        public override void Update(GameTime gameTime)
        {
            if ((int)gameTime.TotalGameTime.TotalSeconds % 5 == 0 && (int)gameTime.TotalGameTime.TotalSeconds != lastspecial)
            {

                lastspecial = (int)gameTime.TotalGameTime.TotalSeconds;
                BulletSpawner clockwise = new SpecialBulletSpawner(this, TextureManager.Textures.Get(TextureManager.Type.BulletGreen), this.Position, new SpiralMovement(3.5f), this.Width, this.Height, 3, 2, .05);
                this.NewSpawner.Invoke(clockwise, new AddSpawnerEventArgs(this, clockwise));
                BulletSpawner cclockwise = new SpecialBulletSpawner(this, TextureManager.Textures.Get(TextureManager.Type.BulletGreen), this.Position, new SpiralMovement(3.5f, true), this.Width, this.Height, 3, 2, .05);
                this.NewSpawner.Invoke(cclockwise, new AddSpawnerEventArgs(this, cclockwise));
            }

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
                LogConsole.LogPosition("Boss2 was hit by player", this.X, this.Y);
                this.TakeDamage(1);
            }
        }
    }
}