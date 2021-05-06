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
    public class Player : Entity
    {
        private float a_timer = 1.0f;

        private bool god_mode = false;

        /// <summary>
        /// Is the player in GodMode
        /// </summary>
        public bool GodMode
        {
            get
            {
                return this.god_mode;
            }
        }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        public Player(Vector2 position, Texture2D texture, float speed) : base(position, texture)
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
            a_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (a_timer > 0.2f)
                this.col = Color.White;
            else
                this.col = Color.Red;

            UserInput state = UserInput.Instance;
            bool s = false;
            if (state.IsKeyPressed(UserInput.KeyBinds.GodMode))
            {
                this.god_mode = !this.god_mode;
                s = true;
            }

            if(this.god_mode)
            {
                base.Update(gameTime);
                this.Invincible = true;
                this.col = Color.Green;
            }
            else if(s && !god_mode)
            {
                this.Invincible = false;
                this.allowInvincible = true;
                this.col = Color.White;
                base.Update(gameTime);
            }
            else
            {
                base.Update(gameTime);
            }
        }

        public override void Collide(Sprite sender, EventArgs e)
        {
            if (sender is Bullet)
            {
                this.TakeDamage(1);
                base.InvokeCollide(this, new EntityCollideEventArgs(this, (Bullet)sender));
            }

            if(sender is Entity)
            {
                Entity k = (Entity)sender;
                this.Position += ((k.Position - this.Position) * (float)(-0.5f * k.Speed));
            }

            if(sender is Reward)
            {
                this.Health += 1;
                LogConsole.Log("Player collided with reward - health + 1!");

                Reward r = (Reward)sender;
                r.InvokeDispose(r, new DisposeEventArgs(r));
            }
        }
    }
}