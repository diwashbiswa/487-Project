using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Homescreen
{
    // Reference: https://www.youtube.com/watch?v=lcrgj26G5Hg

    /// <summary>
    /// Button GUI component which becomes shaded when hovered-over with the mouse, invokes event Click when clicked on
    /// </summary>
    public class GUIButton : GUIComponent
    {
        private Rectangle box;

        private string buttonText;

        private Texture2D tex;

        private SpriteFont font;

        private MouseState currentMouseState;

        private MouseState prevMouseState;

        private bool mouseIsOver;

        /// <summary>
        /// Event Invoked when the user clicks on the button
        /// </summary>
        public event EventHandler Click;

        /// <summary>
        /// Color for any SpriteFont which is drawn on the button
        /// </summary>
        public Color FontColor { get; set; }

        /// <summary>
        /// Position of the (top-left) corner of the button
        /// </summary>
        public Vector2 Position 
        { 
            get 
            {
                return new Vector2((float)this.box.X, (float)this.box.Y);
            }      
            set 
            {
                Vector2 v = value;
                this.box.X = (int)v.X;
                this.box.Y = (int)v.Y;
            }     
        }

        /// <summary>
        /// Text which should be drawn on the button
        /// </summary>
        public string Text
        {
            get
            {
                return this.buttonText;
            }
            set
            {
                this.buttonText = value;
            }
        }

        /// <summary>
        /// Rectangle which represents the barriers of the button (hitbox)
        /// Matches the size of the buttons texture. (TBI adjusted sizing)
        /// </summary>
        public Rectangle Hitbox { get { return this.box; } }

        /// <summary>
        /// Should the button be shaded when the user hovers over it ?
        /// </summary>
        public bool ShadeOnHover { get; set; }

        /// <summary>
        /// Creates a new instance of the GUIButton class
        /// </summary>
        /// <param name="pos"> The position of the button on the screen </param>
        /// <param name="texture"> The texture which is drawn representing the button on the GUI </param>
        /// <param name="font"> The SpriteFont to be used for Text on the button </param>
        /// <param name="fontcolor"> The color to be used for any Text on the button </param>
        /// <param name="buttontext"> Text to be drawn on the button </param>
        public GUIButton(Vector2 pos, Texture2D texture, SpriteFont font, Color fontcolor, string buttontext = null)
        {
            this.tex = texture;
            this.font = font;
            this.FontColor = fontcolor;
            this.Position = pos;
            this.buttonText = buttontext;
            this.ShadeOnHover = true;
            this.box = new Rectangle((int)pos.X, (int)pos.Y, texture.Width, texture.Height);
        }

        /// <summary>
        /// Scale the GUIButton by a factor
        /// </summary>
        /// <param name="n"> Factor by which to scale the object </param>
        public override void Scale(float n)
        {
            this.box.X = (int)((float)this.box.X * n);
            this.box.Y = (int)((float)this.box.Y * n);
            this.box.Width = (int)((float)this.box.Width * n);
            this.box.Height = (int)((float)this.box.Height * n);

            // TODO: add scaling for SpriteFont size to adjust with button scaling.
        }

        /// <summary>
        /// Draw the button on screen
        /// </summary>
        /// <param name="gameTime"> Current time-state of the game </param>
        /// <param name="spriteBatch"> SpriteBatch to draw the button with </param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Color col;

            if (this.mouseIsOver && this.ShadeOnHover)
                col = Color.Gray;
            else
                col = Color.White;

            // Draw the button box
            spriteBatch.Draw(tex, box, col);

            // If there is text set draw the text as well
            if (string.IsNullOrEmpty(this.Text) == false)
            {
                float x = (box.X + (box.Width / 2)) - (font.MeasureString(Text).X / 2);
                float y = (box.Y + (box.Height / 2)) - (font.MeasureString(Text).Y / 2);

                spriteBatch.DrawString(this.font, this.Text, new Vector2(x, y), this.FontColor);
            }
        }

        /// <summary>
        /// Update the buttons state based off the current position of the mouse (Invoke Click event)
        /// </summary>
        /// <param name="gameTime"> Current time-state of the game </param>
        public override void Update(GameTime gameTime)
        {
            this.prevMouseState = currentMouseState;
            this.currentMouseState = Mouse.GetState();

            Rectangle mouseHitbox = new Rectangle(this.currentMouseState.X, this.currentMouseState.Y, 1, 1);

            this.mouseIsOver = false;

            // If the user is hovering over the button
            if (mouseHitbox.Intersects(box))
            {
                this.mouseIsOver = true;

                if (this.currentMouseState.LeftButton == ButtonState.Released && this.prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    if (this.Click != null)
                    {
                        this.Click.Invoke(this, new EventArgs());
                    }
                }
            }
        }
    }
}