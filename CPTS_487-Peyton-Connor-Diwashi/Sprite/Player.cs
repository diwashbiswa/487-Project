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

        // Represents the area the player is allowed to be inside
        protected Rectangle bounds;

        // Texture to draw on the player
        private Texture2D tex;

        // Color to draw the player with
        private Color col;

        // Normal speed of player
        private uint speed;

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
            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))
            {
                this.speed = 5;
                this.handleKeyboardInput(state);
            }
            else
            {
                this.speed = 10;
                this.handleKeyboardInput(state);
            }
            
        }

        /// <summary>
        /// Handles keyboard input for the player
        /// </summary>
        /// <param name="state"></param>
        private void handleKeyboardInput(KeyboardState state)
        {

            if (state.IsKeyDown(Keys.Right))
            {
                if (this.WillIntersectBounds(Direction.Right))
                    this.T_Right();
            }
            if (state.IsKeyDown(Keys.Left))
            {
                if (this.WillIntersectBounds(Direction.Left))
                    this.T_Left();
            }
            if (state.IsKeyDown(Keys.Up))
            {
                if (this.WillIntersectBounds(Direction.Up))
                    this.T_Up();
            }
            if (state.IsKeyDown(Keys.Down))
            {
                if (this.WillIntersectBounds(Direction.Down))
                    this.T_Down();
            }
        }

        /// <summary>
        /// Returns true if the next move will be IN Player.bounds, else false.
        /// </summary>
        /// <param name="direction"> Player.Direction </param>
        /// <returns></returns>
        protected bool WillIntersectBounds(Player.Direction direction)
        {
            bool s = false;
            Rectangle revert = this.body;
            this.Transform(direction);

            if (this.body.Intersects(this.bounds)) { s = true; }
            else { s = false; }

            this.body = revert;

            return s;
        }

        /// <summary>
        /// Moves the player in Player.Direction
        /// </summary>
        /// <param name="direc"></param>
        protected void Transform(Player.Direction direc)
        {
            switch (direc)
            {
                case Direction.Down:
                    this.T_Down();
                    break;
                case Direction.Up:
                    this.T_Up();
                    break;
                case Direction.Left:
                    this.T_Left();
                    break;
                case Direction.Right:
                    this.T_Right();
                    break;
                case Direction.UpLeft:
                    this.T_UL();
                    break;
                case Direction.UpRight:
                    this.T_UR();
                    break;
                case Direction.DownLeft:
                    this.T_DL();
                    break;
                case Direction.DownRight:
                    this.T_DR();
                    break;
            }
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