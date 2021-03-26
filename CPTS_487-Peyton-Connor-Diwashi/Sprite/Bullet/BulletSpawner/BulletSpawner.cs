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
        public Enemy parent = null;

        protected List<Bullet> bullets;

        private List<Bullet> disposedBullets;

        protected Texture2D bulletTexture;

        private Rectangle body;

        private Movement movement;

        /// <summary>
        /// The X,Y coordinates this spawner will fire from
        /// </summary>
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

        public BulletSpawner(Enemy parent, Texture2D bulletTex, Vector2 position, Movement movement, int width, int height)
        {
            this.bulletTexture = bulletTex;
            this.bullets = new List<Bullet>();
            this.disposedBullets = new List<Bullet>();
            this.body = new Rectangle((int)position.X, (int)position.Y, width, height);
            this.movement = movement;
            this.parent = parent;
        }

        /// <summary>
        /// This method is invoked when one of this spawners bullets dies
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DisposeBulletEvent(object sender, EventArgs e)
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

        /// <summary>
        /// Part of base.Update, this method runs in a superclass when this spawners parent is bound to an attackTarget
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void SpawnBullet(GameTime gameTime);

        public override void Update(GameTime gameTime)
        {
            // Try to super.SpawnBullet if parent is bound to target
            if (parent.IsBoundToTarget)
            {
                this.SpawnBullet(gameTime);
            }

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
