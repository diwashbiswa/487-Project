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
    public class Grunt2 : Enemy
    {
        private Enemy.Direction currentDirection;

        private TimeSpan timePrev_ChangeDirection = TimeSpan.Zero;

        private int secondsBetweenDirectionChange;

        private Random rand;

        public Grunt2(Vector2 position, Texture2D texture, ref Rectangle bounds) : base(position, texture, ref bounds)
        {
            this.rand = new Random();
            this.currentDirection = this.getRandomDirection();
            this.secondsBetweenDirectionChange = rand.Next(1, 5);
            this.Speed = (uint)rand.Next(2, 4);
        }

        /// <summary>
        /// Get a random direction enumerated by Enemy.Direction
        /// </summary>
        /// <returns></returns>
        private Enemy.Direction getRandomDirection()
        {
            Enemy.Direction[] direc = { Direction.Left, Direction.Right, Direction.DownLeft, Direction.DownRight, Direction.UpLeft, Direction.UpRight };

            return direc[rand.Next(6)];
        }

        /// <summary>
        /// Move the enemy
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Move(GameTime gameTime)
        {
            // Randomly change the direction somtimes without hitting a boundary
            if (gameTime.TotalGameTime.TotalSeconds - this.timePrev_ChangeDirection.TotalSeconds > this.secondsBetweenDirectionChange)
            {
                this.currentDirection = this.getRandomDirection();
                this.timePrev_ChangeDirection = gameTime.TotalGameTime;
            }

            if (this.WillIntersectBounds(this.currentDirection))
                this.Transform(this.currentDirection);
            else
                this.currentDirection = this.getRandomDirection();
        }

        // This attack happens only when this enemy is bount to a target with base.BindToTarget()
        protected override void Attack(GameTime gameTime, Rectangle target)
        {
            // TBI
        }

        protected override void Disposed(GameTime gameTime, EventHandler dispose)
        {
            //TBI

            // base.Dispose EventHandler Invoke
        }
    }
}