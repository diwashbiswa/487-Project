using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Class for carinal movment in Movement.CardinalDirection
    /// </summary>
    public class KeyboardMovement : Movement
    {
        private float initialSpeed = 0.0f;

        Vector2 direction = new Vector2(0, 0);

        public KeyboardMovement(float speed) : base(speed)
        {
            this.initialSpeed = speed;
        }

        public override Vector2 Move()
        {
            UserInput state = UserInput.Instance;

            direction.X = 0.0f;
            direction.Y = 0.0f;

            if (state.IsKeyDown(UserInput.KeyBinds.SlowMode))
            {
                this.Speed = initialSpeed / 2.0f;
            }
            else
            {
                this.Speed = initialSpeed;
            }

            if(state.IsKeyDown(UserInput.KeyBinds.Up))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.North, true);
            }
            if (state.IsKeyDown(UserInput.KeyBinds.Left))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.West, true);
            }
            if (state.IsKeyDown(UserInput.KeyBinds.Down))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.South, true);
            }
            if (state.IsKeyDown(UserInput.KeyBinds.Right))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.East, true);
            }

            return this.direction * this.Speed;
        }
    }
}