using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class SpecialBulletSpawner : BulletSpawner

    {
        private TimeSpan previousFire = TimeSpan.Zero;
        private TimeSpan creationTime = new TimeSpan(0);
        int timeToLive;
        int type = 1;


        public SpecialBulletSpawner(Entity parent, Texture2D bulletTex, Vector2 position, Movement movement, int width, int height, int timeToLive,int type, double fireRateSeconds = 1.5) : base(parent, bulletTex, position, movement, width, height)
        {
            this.fireRateSeconds = fireRateSeconds;
            this.timeToLive = timeToLive;
            this.type = type;
        }

        /// <summary>
        /// Will only run if parent is bound to the target
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void SpawnBullet(GameTime gameTime)
        {
            if (creationTime == new TimeSpan(0))
            {
                creationTime = gameTime.TotalGameTime;
            }
          
            if ((gameTime.TotalGameTime.TotalSeconds - this.previousFire.TotalSeconds > this.fireRateSeconds && creationTime.Add(new TimeSpan(0, 0, this.timeToLive)) > gameTime.TotalGameTime))
            {
                //Bullet b = new CardinalBullet(this.Position, this.direction, this.bulletTexture, 9.0f, 3.0f);
                Bullet b;
                if (type == 1)
                {
                    b = new CardinalBullet(this.Position, Movement.CardinalDirection.South, this.bulletTexture, 3.0f, 5.0f);
                }
                else
                {
                    b = new CardinalBullet(this.Position, this.parent.attackTarget, this.bulletTexture, 3.0f, 5.0f);
                }
                
                
                base.InvokeFire(b);
                this.previousFire = gameTime.TotalGameTime;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
