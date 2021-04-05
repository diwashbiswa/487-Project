using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class KeyboardBulletSpawner : BulletSpawner
    {
        private TimeSpan previousFire = TimeSpan.Zero;

        private double fireRateSeconds = 1.5;

        private Movement.CardinalDirection direction;

        public KeyboardBulletSpawner(Enemy parent, Texture2D bulletTex, Vector2 position, Movement movement, int width, int height, Movement.CardinalDirection direction, double fireRateSeconds = 0.15) : base(parent, bulletTex, position, movement, width, height)
        {
            this.direction = direction;
            this.fireRateSeconds = fireRateSeconds;
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
                UserInput state = UserInput.Instance;
                if (state.IsKeyDown(UserInput.KeyBinds.Fire))
                {
                    this.Fire(this.direction, 9.0f, 3.0f);
                    this.previousFire = gameTime.TotalGameTime;
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
