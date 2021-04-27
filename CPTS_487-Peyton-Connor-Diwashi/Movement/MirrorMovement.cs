using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Class for mirroring the movement of another sprite. set { ThisSprite } property.
    /// </summary>
    public class MirrorMovement : Movement
    {
        private Entity mirror;
        private Vector2 v = Vector2.Zero;

        public MirrorMovement(Entity mirror) : base(0)
        {
            this.mirror = mirror;
        }

        public override Vector2 Move()
        {
            if (this.ThisSprite == null)
                return Vector2.Zero;

            this.v = mirror.Position - ThisSprite.Position;
            return v;
        }
    }
}