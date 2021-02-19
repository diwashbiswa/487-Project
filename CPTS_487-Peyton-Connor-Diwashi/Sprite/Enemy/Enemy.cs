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
    public abstract class Enemy : Sprite
    {
        // Represents the body and size of the enemy
        private Rectangle body;

        // Represents the area the enemy is allowed to be inside
        protected Rectangle bounds;

        // The target the Enemy is Focused on
        protected Rectangle attackTarget;

        // Is the enemy bound to a target
        protected bool boundToTarget = false;

        //To be invoked when the enemy is removed
        public event EventHandler Dispose;

        // Texture to draw on the enemy
        private Texture2D tex;

        // Color to draw the enemy with
        private Color col;

        // Speed of enemy
        private uint speed;

        /// <summary>
        /// Position of the (top-left) corner of the Enemy
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
        /// Enumerating direction for enemies
        /// </summary>
        protected enum Direction { Down, Up, Left, Right, DownRight, DownLeft, UpRight, UpLeft };

        /// <summary>
        /// Direct interfact to enemy X and Y coordinates
        /// </summary>
        public int X { get { return this.body.X; } private set { this.body.X = value; } }
        public int Y { get { return this.body.Y; } private set { this.body.Y = value; } }

        // Move this enemy Up,Down,Left,Right, Down-Right, Down-Left, Up-Right, Up-Left
        private void T_Down() { this.Y+=(int)speed; }
        private void T_Up() { this.Y-= (int)speed; }
        private void T_Left() { this.X-= (int)speed; }
        private void T_Right() { this.X+= (int)speed; }
        private void T_DR() { this.T_Down(); this.T_Right(); }
        private void T_DL() { this.T_Down(); this.T_Left(); }
        private void T_UR() { this.T_Up(); this.T_Right(); }
        private void T_UL() { this.T_Up(); this.T_Left(); }

        /// <summary>
        /// Returns a Rectangle representing the area consumed by this enemy
        /// </summary>
        public Rectangle Hitbox { get { return this.body; } private set { this.body = value; } }

        /// <summary>
        /// Draw color for the enemy texture
        /// </summary>
        public Color Color { get { return this.col; } set { this.col = value; } }

        /// <summary>
        /// Returns true if the enemy is bound to an attack target
        /// </summary>
        public bool IsBoundToTarget { get { return this.boundToTarget; } }

        /// <summary>
        /// Gets and sets the speed of the Enemy during transformations
        /// </summary>
        public uint Speed { get { return this.speed; } set { this.speed = value; } }

        /// <summary>
        /// Initialize the base class for Enemy
        /// </summary>
        /// <param name="position"> position of the enemy on the screen </param>
        /// <param name="texture"> texture to draw the enemy with </param>
        public Enemy(Vector2 position, Texture2D texture, ref Rectangle bounds)
        {
            this.Color = Color.White;
            this.tex = texture;
            this.body = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.boundToTarget = false;
            this.bounds = bounds;
            this.speed = 1;

            // Check for enemy spawned out of bounds
            if (!this.Hitbox.Intersects(this.bounds))
            {
                throw new Exception("Enemy was spawned out of bounds\n");
            }
        }

        /// <summary>
        /// Transforms the Enemy in a direction
        /// </summary>
        /// <param name="direc"></param>
        protected void Transform(Enemy.Direction direc)
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

        /// <summary>
        /// Returns true if a movement in direction will be WITHIN bounds
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        protected bool WillIntersectBounds(Enemy.Direction direction)
        {
            bool s = false;
            Rectangle revert = this.Hitbox;
            this.Transform(direction);

            if (this.Hitbox.Intersects(this.bounds)) { s = true; }
            else { s = false; }

            this.Hitbox = revert;

            return s;
        }

        /// <summary>
        /// Bind the enemy to a target to attack.
        /// </summary>
        /// <param name="t"> attack target </param>
        public void BindToTarget(ref Rectangle t)
        {
            this.boundToTarget = true;
            this.attackTarget = t;
        }

        /// <summary>
        /// Unbind the enemy from its target.
        /// </summary>
        public void UnbindFromTarget()
        {
            this.boundToTarget = false;
        }

        /// <summary>
        /// Scale the enemy by a factor
        /// </summary>
        /// <param name="n"> scale factor </param>
        public override void Scale(float n)
        {
            this.body.X = (int)((float)this.body.X * n);
            this.body.Y = (int)((float)this.body.Y * n);
            this.body.Width = (int)((float)this.body.Width * n);
            this.body.Height = (int)((float)this.body.Height * n);
        }

        /// <summary>
        /// Draw the enemy with the given spriteBatch
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.tex, this.body, this.col);
        }

        /// <summary>
        /// Update for all enemies
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // super.Move()
            this.Move(gameTime);
            // super.Disposed()
            this.Disposed(gameTime, this.Dispose);
            // super.Attack()
            if (this.boundToTarget)
                this.Attack(gameTime, this.attackTarget);
        }

        /// <summary>
        /// Superclass Invokes when the enemy should be removed from the game
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="dispose"> Invoked when the Enemy should be destroyed </param>
        protected abstract void Disposed(GameTime gameTime, EventHandler dispose);

        /// <summary>
        /// Move the enemy
        /// </summary>
        /// <param name="gameTime"></param>
        protected abstract void Move(GameTime gameTime);

        /// <summary>
        /// Attack a target, use base.BindToTarget and base.UnbindFromTarget to trigger the attack in an update.
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="target"></param>
        protected abstract void Attack(GameTime gameTime, Rectangle target);
    }
}