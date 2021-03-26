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
    public class Player : Sprite
    {
        // Represents the body and size of the player
        private Rectangle body;

        // Texture to draw on the player
        private Texture2D tex;

        // Color to draw the player with
        private Color col;

        // movement for player
        private Movement movement;

        /// <summary>
        /// Direct interfact to enemy X and Y coordinates
        /// </summary>
        public int X { get { return this.body.X; } private set { this.body.X = value; } }
        public int Y { get { return this.body.Y; } private set { this.body.Y = value; } }

        /// <summary>
        /// Draw color for the player texture
        /// </summary>
        public Color Color { get { return this.col; } set { this.col = value; } }

        /// <summary>
        /// Position of the (top-left) corner of the Player
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return new Vector2((float)this.body.X, (float)this.body.Y);
            }
            set
            {
                Vector2 v = value;
                this.body.X = (int)v.X;
                this.body.Y = (int)v.Y;
            }
        }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="bounds"></param>
        public Player(Vector2 position, Texture2D texture, ref Rectangle bounds, float speed)
        {
            this.Color = Color.White;
            this.tex = texture;
            this.body = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.movement = new KeyboardMovement(speed);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tex, this.body, this.col);
        }

        public override void Update(GameTime gameTime)
        {
            this.Position += this.movement.Move();
        }
    }
}




// Feedback from professor
//Build a bullet factory
// only need grunt position from the grunt1.cs
// bulllet and enemies are identical
// bullet classes just need to know the location of the enemies
//      -- make them spawn from that locatiiiiiiiiiiiiiiiiiiiiiion
    //  -- looks likkkkkkke                                the enemmmmmmmmies are firing the bullet
// Have Movement class - for movement of the bullets that go dddddifferent directions

//Controller class should be handling the keyboard input
//Player just responds to up, down, etc.
//controller in main game
//Make it versatile - Xbox controller can control it, up down left right key, and WASD button
// Add configuration manual in the beginning of the game
// Or make it customizatizable from the JSON Script