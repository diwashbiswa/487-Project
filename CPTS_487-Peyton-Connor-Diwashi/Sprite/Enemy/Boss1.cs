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

    public class Boss1 : Enemy
    {
        private Movement.CardinalDirection currentDirection;

        private Random rand;

        public Boss1(Vector2 position, Texture2D texture) : base(position, texture)
        {
            this.rand = new Random();
            this.currentDirection = this.getRandomDirection();
            this.Speed = 1;
            this.movement = new BounceMovement(this.Speed, new Rectangle(0, 0, 1280, 720), this.body);
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
            // Draw this sprite
            base.Draw(gameTime, spriteBatch);
        }

        public override void Collide(Sprite sender, EventArgs e)
        {
            if (sender is Bullet)
            {
                LogConsole.LogPosition("Boss1 was hit by player", this.X, this.Y);
                base.InvokeDispose(this, e);
            }
        }

        // Only runs if the enemy is bounded to a target
        protected override void Attack(GameTime gameTime, Vector2 target)
        {
            // Handled in BulletSpawner
        }

        protected override void Disposed(GameTime gameTime, EventHandler dispose)
        {
            // if enemy is dead Dispose.Invoke

            // TBI

            // dispose EventHandler Invoke based on some logic, encapsulating Classes subscribe to base.Dispose
        }
    }
}