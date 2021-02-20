﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{

    public class Boss1 : Enemy
    {
        private Enemy.Direction currentDirection;

        private Random rand;

        private List<Bullet> bullets;

        private List<Bullet> disposedBullets;

        private Texture2D bulletTexture;

        private TimeSpan previousFire = TimeSpan.Zero;

        private double fireRateSeconds = 1.5;

        public Boss1(Vector2 position, Texture2D texture, Texture2D bulletTexture, ref Rectangle bounds) : base(position, texture, ref bounds)
        {
            this.rand = new Random();
            this.currentDirection = this.getRandomDirection();
            this.Speed = 1;
            this.bullets = new List<Bullet>();
            this.disposedBullets = new List<Bullet>();
            this.bulletTexture = bulletTexture;
        }

        /// <summary>
        /// Get a random direction enumerated by Enemy.Direction
        /// </summary>
        /// <returns></returns>
        private Enemy.Direction getRandomDirection()
        {
            if (rand.Next() % 2 == 0)
            {
                return (Enemy.Direction)rand.Next(4, 8);
            }

            return (Enemy.Direction)rand.Next(8);
        }

        /// <summary>
        /// Event invoked to this function when a bullet this Grunt subscribes to becomes disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisposeBulletEvent(object sender, EventArgs e)
        {
            Bullet b = (Bullet)sender;
            this.disposedBullets.Add(b);
        }

        /// <summary>
        /// Move the enemy in a random sequence of directions
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Move(GameTime gameTime)
        {
            // If the sprite will be in bounds for his next move then move it, otherwise switch directions
            if (this.WillIntersectBounds(this.currentDirection))
                this.Transform(this.currentDirection);
            else
                this.currentDirection = this.getRandomDirection();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw all bullets first
            foreach (Bullet b in this.bullets)
            {
                b.Draw(gameTime, spriteBatch);
            }

            // Draw this sprite
            base.Draw(gameTime, spriteBatch);
        }

        // Only runs if the enemy is bounded to a target
        protected override void Attack(GameTime gameTime, Vector2 target)
        {
            // Fire new bullet
            if (gameTime.TotalGameTime.TotalSeconds - this.previousFire.TotalSeconds > this.fireRateSeconds)
            {
                Bullet b = new Bullet(this.Position, this.bulletTexture, 9.0f, 3.0f);
                b.Dispose += this.DisposeBulletEvent;
                b.Direction = this.DirectonTowardsTarget;
                this.bullets.Add(b);
                this.previousFire = gameTime.TotalGameTime;
            }

            // Remove all disposed bullets from the Update list
            foreach (Bullet b in this.disposedBullets)
            {
                if (this.bullets.Contains(b))
                    this.bullets.Remove(b);
            }

            // Update all bullets
            foreach (Bullet b in this.bullets)
            {
                b.Update(gameTime);
            }
        }

        protected override void Disposed(GameTime gameTime, EventHandler dispose)
        {
            // TBI

            // dispose EventHandler Invoke based on some logic, encapsulating Classes subscribe to base.Dispose
        }
    }
}