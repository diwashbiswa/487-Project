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
    public class Grunt1 : Enemy
    {
        private Enemy.Direction currentDirection;

        private TimeSpan timePrev = TimeSpan.Zero;

        public Grunt1(Vector2 position, Texture2D texture, ref Rectangle bounds) : base(position, texture, ref bounds)
        {
            this.currentDirection = this.getRandomDirection();
            this.Speed = 3;
        }

        /// <summary>
        /// Get a random direction enumerated by Enemy.Direction
        /// </summary>
        /// <returns></returns>
        private Enemy.Direction getRandomDirection()
        {
            var rand = new Random();

            if(rand.Next() % 2 == 0)
            {
                return (Enemy.Direction)rand.Next(5, 8);
            }

            return (Enemy.Direction)rand.Next(8);
        }

        /// <summary>
        /// Move the enemy in a random sequence of directions
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Move(GameTime gameTime)
        {
            if(gameTime.TotalGameTime.TotalSeconds - this.timePrev.TotalSeconds > 2)
            {
                this.currentDirection = this.getRandomDirection();
                this.timePrev = gameTime.TotalGameTime;
            }

            if (this.WillIntersectBounds(this.currentDirection))
                this.Transform(this.currentDirection);
            else
                this.currentDirection = this.getRandomDirection();
        }

        // This attack happens only when this enemy is bound to a target with base.BindToTarget()
        public override void Attack(GameTime gameTime, Rectangle target)
        {
            // TBI
        }
    }
}