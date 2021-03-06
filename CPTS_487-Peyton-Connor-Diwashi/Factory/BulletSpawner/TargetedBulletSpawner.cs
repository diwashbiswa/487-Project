using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class TargetedBulletSpawner : BulletSpawner
    {
        private TimeSpan previousFire = TimeSpan.Zero;

        public TargetedBulletSpawner(Entity parent, Texture2D bulletTex, Vector2 position, Movement movement, int width, int height, double fireRateSeconds = 1.5) : base(parent, bulletTex, position, movement, width, height)
        {
            this.fireRateSeconds = fireRateSeconds;
        }

        /// <summary>
        /// Will only run if parent is bound to the target, fires a bullet targeted at this spawners parents attackTarget
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void SpawnBullet(GameTime gameTime)
        {
            if ((gameTime.TotalGameTime.TotalSeconds - this.previousFire.TotalSeconds > this.fireRateSeconds))
            {
                Bullet b = new CardinalBullet(this.Position, this.parent.attackTarget, this.bulletTexture, this.bulletSpeed, this.bulletLifeSpan);
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
