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
        public event EventHandler<AddSpawnerEventArgs> NewSpawner = delegate { };

        public Boss1(Vector2 position, Texture2D texture) : base(position, texture) { }

        int lastspecial;

        public override void Update(GameTime gameTime)
        {
            if((int)gameTime.TotalGameTime.TotalSeconds%5 == 0 && (int)gameTime.TotalGameTime.TotalSeconds != lastspecial)
            {
                lastspecial = (int)gameTime.TotalGameTime.TotalSeconds;
                BulletSpawner westspawner = new SpecialBulletSpawner(this, TextureManager.Textures.Get(TextureManager.Type.BulletGreen), this.Position, new CardinalMovement(4.0f, Movement.CardinalDirection.West), this.Width, this.Height, 1, 1, .05);
                this.NewSpawner.Invoke(westspawner, new AddSpawnerEventArgs(this, westspawner));
                BulletSpawner eastspawner = new SpecialBulletSpawner(this, TextureManager.Textures.Get(TextureManager.Type.BulletGreen), this.Position, new CardinalMovement(4.0f, Movement.CardinalDirection.East), this.Width, this.Height, 1, 1, .05);
                this.NewSpawner.Invoke(eastspawner, new AddSpawnerEventArgs(this, eastspawner));
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
                LogConsole.LogPosition("Boss1 was hit by player", this.X, this.Y);
                this.TakeDamage(1);
            }
        }
    }
}