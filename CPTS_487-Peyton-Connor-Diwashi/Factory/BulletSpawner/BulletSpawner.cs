using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public abstract class BulletSpawner : Sprite
    {
        public Entity parent = null;

        protected Texture2D bulletTexture;

        private Movement movement;

        public event EventHandler<AddBulletEventArgs> Fire = delegate { };

        protected double fireRateSeconds = 1.5;

        public BulletSpawner(Entity parent, Texture2D bulletTex, Vector2 position, Movement movement, int width, int height) : base(position, width, height)
        {
            this.bulletTexture = bulletTex;
            this.movement = movement;
            this.parent = parent;
        }

        public Movement Movement
        {
            get
            {
                return this.movement;
            }
            set
            {
                this.movement = value;
            }
        }

        public double FireRateSeconds
        {
            get
            {
                return this.fireRateSeconds;
            }
            set
            {
                this.fireRateSeconds = value;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch) { }

        /// <summary>
        /// Part of base.Update, this method runs in a superclass when this spawners parent is bound to an attackTarget
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void SpawnBullet(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
            this.SpawnBullet(gameTime);
            this.Position += this.movement.Move();
        }

        /// <summary>
        /// To be invoked providing a Fire notification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InvokeFire(object sender)
        {
            this.Fire.Invoke(sender, new AddBulletEventArgs(this.parent, (Bullet)sender));
        }

        public override void Collide(Sprite sender, EventArgs e) { }
    }
}
