using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class BulletSpawner : Sprite
    {
        private List<Bullet> bullets;

        private List<Bullet> disposedBullets;

        private Texture2D bulletTexture;

        private Rectangle body;

        private Movement movement;

        public Vector2 Position
        {
            get
            {
                return new Vector2((float)this.body.X, (float)this.body.Y);
            }
            set
            {
                Vector2 v = value;
                this.body.X = (int)v.X;
                this.body.Y = (int)v.Y;
            }
        }

        public BulletSpawner(Texture2D bulletTex, Vector2 position, Movement movement, int width, int height)
        {
            this.bulletTexture = bulletTex;
            this.bullets = new List<Bullet>();
            this.disposedBullets = new List<Bullet>();
            this.body = new Rectangle((int)position.X, (int)position.Y, width, height);
            this.movement = movement;
        }

        public void Fire(Vector2 target, float speed, float lifespanSeconds)
        {
            Bullet b = new CardinalBullet(this.Position, target, this.bulletTexture, speed, lifespanSeconds);
            b.Dispose += this.DisposeBulletEvent;
            this.bullets.Add(b);
        }

        public void Fire(CardinalMovement.CardinalDirection direction, float speed, float lifespanSeconds)
        {
            Bullet b = new CardinalBullet(this.Position, direction, this.bulletTexture, speed, lifespanSeconds);
            b.Dispose += this.DisposeBulletEvent;
            this.bullets.Add(b);
        }

        private void DisposeBulletEvent(object sender, EventArgs e)
        {
            Bullet b = (Bullet)sender;
            this.disposedBullets.Add(b);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (Bullet b in this.bullets)
            {
                b.Draw(gameTime, spriteBatch);
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Bullet b in this.disposedBullets)
            {
                if (this.bullets.Contains(b))
                {
                    this.bullets.Remove(b);
                }
            }
            this.disposedBullets.Clear();

            foreach (Bullet b in this.bullets)
            {
                b.Update(gameTime);
            }

            this.Position += this.movement.Move();
        }
    }
}
