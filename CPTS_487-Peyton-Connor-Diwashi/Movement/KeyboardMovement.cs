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
            KeyboardState state = Keyboard.GetState();

            direction.X = 0.0f;
            direction.Y = 0.0f;

            if (state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift))
            {
                this.Speed = initialSpeed / 2.0f;
            }
            else
            {
                this.Speed = initialSpeed;
            }

            if(state.IsKeyDown(Keys.W))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.North, true);
            }
            if (state.IsKeyDown(Keys.A))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.West, true);
            }
            if (state.IsKeyDown(Keys.S))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.South, true);
            }
            if (state.IsKeyDown(Keys.D))
            {
                this.Cardinal2Vector(ref direction, CardinalDirection.East, true);
            }

            return this.direction * this.Speed;
        }
    }
}