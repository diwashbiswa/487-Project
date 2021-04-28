using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Movement that does not move
    /// </summary>
    public class NoneMovement : Movement
    {
        Vector2 still = Vector2.Zero;

        public NoneMovement() : base(0) { }

        public override Vector2 Move()
        {
            return this.still;
        }
    }
}