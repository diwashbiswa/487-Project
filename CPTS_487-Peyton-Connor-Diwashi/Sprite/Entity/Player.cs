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

        public event EventHandler<AddRewardEventArgs> Reward = delegate { };


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
            // TEST Small animation for when player is hit by a bullet
            a_timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (a_timer > 0.2f)
                this.col = Color.White;
            else
                this.col = Color.Red;
            // End animation TEST ------------------------------------
            base.Update(gameTime);
        }

        public override void Collide(Sprite sender, EventArgs e)
        {
            if (sender is Bullet)
            {
                if (this.Health == 3)
                {
                    TextureManager text = TextureManager.Textures;
                    Reward reward = new Reward(new Vector2(500, 300), text.Get(TextureManager.Type.Reward), 5, 5);

                    this.Reward.Invoke(this, new AddRewardEventArgs(reward));
                    LogConsole.Log("New reward should spawn!");
                }

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