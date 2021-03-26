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
    public class Boss2 : Enemy
    {
        private Movement.CardinalDirection currentDirection;

        private Random rand;

        private BulletSpawner bulletSpawner;

        private TimeSpan previousFire = TimeSpan.Zero;

        private double fireRateSeconds = 1.5;

        public Boss2(Vector2 position, Texture2D texture, Texture2D bulletTexture, ref Rectangle bounds) : base(position, texture)
        {
            this.rand = new Random();
            this.currentDirection = this.getRandomDirection();
            this.Speed = 1;
            this.movement = new CardinalMovement(this.Speed, this.currentDirection);
            this.bulletSpawner = new BulletSpawner(bulletTexture, this.Position, this.movement, this.body.Width, this.body.Height);
        }

        /// <summary>
        /// Get a random direction enumerated by Enemy.Direction
        /// </summary>
        /// <returns></returns>
        private Movement.CardinalDirection getRandomDirection()
        {
            return (Movement.CardinalDirection)rand.Next(8);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.bulletSpawner.Update(gameTime);
        }

        /// <summary>
        /// Move the enemy in a random sequence of directions
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Move(GameTime gameTime)
        {
            this.Position += this.movement.Move();
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // Draw all bullets
            this.bulletSpawner.Draw(gameTime, spriteBatch);

            // Draw this sprite
            base.Draw(gameTime, spriteBatch);
        }

        // Only runs if the enemy is bounded to a target
        protected override void Attack(GameTime gameTime, Vector2 target)
        {
            // Fire new bullet
            if (gameTime.TotalGameTime.TotalSeconds - this.previousFire.TotalSeconds > this.fireRateSeconds)
            {
                this.bulletSpawner.Fire(this.attackTarget, 9.0f, 3.0f);
                this.previousFire = gameTime.TotalGameTime;
            }
        }

        protected override void Disposed(GameTime gameTime, EventHandler dispose)
        {
            // if enemy is dead Dispose.Invoke

            // TBI

            // dispose EventHandler Invoke based on some logic, encapsulating Classes subscribe to base.Dispose
        }
    }
}