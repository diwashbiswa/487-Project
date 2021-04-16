using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class CardinalBulletSpawner : BulletSpawner
    {
        private TimeSpan previousFire = TimeSpan.Zero;

        private double fireRateSeconds = 1.5;

        private Movement.CardinalDirection direction;

        public CardinalBulletSpawner(Entitiy parent, Texture2D bulletTex, Vector2 position, Movement movement, int width, int height, Movement.CardinalDirection direction, double fireRateSeconds = 1.5) : base(parent, bulletTex, position, movement, width, height)
        {
            this.fireRateSeconds = fireRateSeconds;
            this.direction = direction;
        }

        private void Fire(Movement.CardinalDirection direc, float speed, float lifespanSeconds)
        {
            Bullet b = new CardinalBullet(this.Position, direc, this.bulletTexture, speed, lifespanSeconds);
            b.Dispose += this.DisposeBulletEvent;
            this.bullets.Add(b);
        }

        /// <summary>
        /// Will only run if parent is bound to the target
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void SpawnBullet(GameTime gameTime)
        {
            if ((gameTime.TotalGameTime.TotalSeconds - this.previousFire.TotalSeconds > this.fireRateSeconds))
            {
                this.Fire(this.direction, 9.0f, 3.0f);
                this.previousFire = gameTime.TotalGameTime;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
