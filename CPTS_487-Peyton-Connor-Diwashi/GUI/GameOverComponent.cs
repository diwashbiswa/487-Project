using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class GameOverComponent : GUIComponent
    {
        SpriteFont font;
        GUIButton okay_button;
        private bool win = false;

        public event EventHandler Exit = delegate { };

        public bool Win
        {
            get
            {
                return this.win;
            }
            set
            {
                this.win = value;
            }
        }

        public GameOverComponent()
        {
            TextureManager state = TextureManager.Textures;
            this.font = state.GetFont(TextureManager.Font.Font1);
            okay_button = new GUIButton(new Vector2(500, 600), state.Get(TextureManager.Type.Button1), this.font, Color.Black, "Okay");
            okay_button.Click += this.OkayClicked;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if(win)
                spriteBatch.DrawString(this.font, "You Win!", new Vector2(600, 300), Color.DarkRed);
            else
                spriteBatch.DrawString(this.font, "You Lose...", new Vector2(600, 300), Color.DarkRed);
            okay_button.Draw(gameTime, spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            okay_button.Update(gameTime);
        }

        /// <summary>
        /// Invoked when the user wants to exit the game over screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkayClicked(object sender, EventArgs e)
        {
            this.Exit.Invoke(this.okay_button, new EventArgs());
        }

        public override void Scale(float n) { }
    }
}