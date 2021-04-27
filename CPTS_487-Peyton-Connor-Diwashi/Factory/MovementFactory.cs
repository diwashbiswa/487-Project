using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public abstract class MovementFactory
    {
        public enum MovementType { None, Bounce, CardinalSouth, Keyboard };

        public Movement CreateMovement(MovementType type, Entity parent)
        {
            return this.createMovement(type, parent);
        }

        protected abstract Movement createMovement(MovementType type, Entity parent);
    }
}
