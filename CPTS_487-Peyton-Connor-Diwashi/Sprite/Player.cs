using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// All Sprite objects extend from here
    /// </summary>
    public class Player : Enemy
    {
        #region testing
        private float a_timer = 1.0f;
        #endregion


        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="bounds"></param>
        public Player(Vector2 position, Texture2D texture, ref Rectangle bounds, float speed) : base(position, texture)
        {
            this.Color = Color.White;
            this.body = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.movement = new KeyboardMovement(speed);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            // TEST Small animation for when player is hit by a bullet
            a_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (a_timer > 0.2f)
                this.col = Color.White;
            else
                this.col = Color.Red;
            // End animation TEST ------------------------------------
            base.Update(gameTime);
        }

        protected override void Move(GameTime gameTime)
        {
            this.Position += this.movement.Move();
        }

        public override void Collide(Sprite sender)
        {
            if (sender is Bullet)
            {
                LogConsole.LogPosition("Player has been hit", this.X, this.Y);
                a_timer = 0.0f;
            }
        }

        protected override void Disposed(GameTime gameTime, EventHandler dispose)
        {
            // TBI
        }

        protected override void Attack(GameTime gameTime, Vector2 target)
        {
            // TBI
        }
    }
}