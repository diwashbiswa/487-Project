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
    /// <summary>
    /// All Sprite objects extend from here
    /// </summary>
    public class Boss1 : Enemy
    {
        private Enemy.Direction currentDirection;

        private Random rand;

        public Boss1(Vector2 position, Texture2D texture, ref Rectangle bounds) : base(position, texture, ref bounds)
        {
            this.rand = new Random();
            this.currentDirection = this.getRandomDirection();
            this.Speed = 1;
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
        /// Move the enemy in a random sequence of directions
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Move(GameTime gameTime)
        {
            if (this.WillIntersectBounds(this.currentDirection))
                this.Transform(this.currentDirection);
            else
                this.currentDirection = this.getRandomDirection();
        }

        // This attack happens only when this enemy is bound to a target with base.BindToTarget()
        protected override void Attack(GameTime gameTime, Rectangle target)
        {
            // TBI
        }

        protected override void Disposed(GameTime gameTime, EventHandler dispose)
        {
            // TBI

            // dispose EventHandler Invoke based on some logic, encapsulating Classes subscribe to base.Dispose
        }
    }
}