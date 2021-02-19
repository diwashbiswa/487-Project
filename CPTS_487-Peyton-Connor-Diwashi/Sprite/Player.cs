using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// All Sprite objects extend from here
    /// </summary>
    public class Player : Sprite
    {
        // Represents the body and size of the player
        private Rectangle body;

        // Represents the area the player is allowed to be inside
        protected Rectangle bounds;

        // Texture to draw on the player
        private Texture2D tex;

        // Color to draw the player with
        private Color col;

        // Normal speed of player
        private uint speed;

        // Slow speed of player
        private uint slowSpeed;

        /// <summary>
        /// Enumerating direction for player
        /// </summary>
        protected enum Direction { Down, Up, Left, Right, DownRight, DownLeft, UpRight, UpLeft };

        /// <summary>
        /// Direct interfact to enemy X and Y coordinates
        /// </summary>
        public int X { get { return this.body.X; } private set { this.body.X = value; } }
        public int Y { get { return this.body.Y; } private set { this.body.Y = value; } }

        // Move this enemy Up,Down,Left,Right, Down-Right, Down-Left, Up-Right, Up-Left
        private void T_Down() { this.Y += (int)speed; }
        private void T_Up() { this.Y -= (int)speed; }
        private void T_Left() { this.X -= (int)speed; }
        private void T_Right() { this.X += (int)speed; }
        private void T_DR() { this.T_Down(); this.T_Right(); }
        private void T_DL() { this.T_Down(); this.T_Left(); }
        private void T_UR() { this.T_Up(); this.T_Right(); }
        private void T_UL() { this.T_Up(); this.T_Left(); }


        /// <summary>
        /// Draw color for the player texture
        /// </summary>
        public Color Color { get { return this.col; } set { this.col = value; } }

        /// <summary>
        /// Gets and sets the speed of the Player
        /// </summary>
        public uint Speed { get { return this.speed; } set { this.speed = value; } }

        /// <summary>
        /// Player constructor
        /// </summary>
        /// <param name="position"></param>
        /// <param name="texture"></param>
        /// <param name="bounds"></param>
        public Player(Vector2 position, Texture2D texture, ref Rectangle bounds)
        {
            this.Color = Color.White;
            this.tex = texture;
            this.body = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.bounds = bounds;
            this.speed = 1;
        }

        public override void Scale(float n)
        {
            this.body.X = (int)((float)this.body.X * n);
            this.body.Y = (int)((float)this.body.Y * n);
            this.body.Width = (int)((float)this.body.Width * n);
            this.body.Height = (int)((float)this.body.Height * n);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tex, this.body, this.col);
        }

        public override void Update(GameTime gameTime)
        {

        }

        // add new function for keyboard input, or use the update function probably
        
    }
}