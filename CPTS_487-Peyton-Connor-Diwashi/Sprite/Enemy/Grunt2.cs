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
    /// <summary>
    /// All Sprite objects extend from here
    /// </summary>
    public class Grunt2 : Enemy
    {
        public Grunt2(Vector2 position, Texture2D texture, ref Rectangle bounds) : base(position, texture, ref bounds)
        {
            // TBI
        }

        /// <summary>
        /// Move the enemy
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Move(GameTime gameTime)
        {
            // TBI
        }


        // This attack happens only when this enemy is bount to a target with base.BindToTarget()
        public override void Attack(GameTime gameTime, Rectangle target)
        {
            // TBI
        }
    }
}