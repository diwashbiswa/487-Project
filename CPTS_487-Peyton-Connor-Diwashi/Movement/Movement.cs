using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public abstract class Movement
    {
        private float speed;

        public enum CardinalDirection { North, South, East, West, NorthWest, SouthWest, NorthEast, SouthEast }

        public float Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }

        public Movement(float speed)
        {
            this.Speed = speed;
        }

        /// <summary>
        /// Sets a reference to a Vector2 to represent a CardinalDirection
        /// </summary>
        /// <param name="set"></param>
        /// <param name="direction"></param>
        /// <param name="add">
        /// This allows a contruction of carinal movements in the set vector if set to true otherwise set vector will be overwritten and start from 0.0
        /// </param>
        protected void Cardinal2Vector(ref Vector2 set, CardinalDirection direction, bool add = false)
        {
            // if not adding we reset vectors
            if (add == false)
            {
                set.X = 0.0f;
                set.Y = 0.0f;
            }

            // Minimum in any direction should never be less than 1
            switch (direction)
            {
                case (CardinalDirection.North):
                    set.Y += -1.0f;
                    break;
                case (CardinalDirection.South):
                    set.Y += 1.0f;
                    break;
                case (CardinalDirection.East):
                    set.X += 1.0f;
                    break;
                case (CardinalDirection.West):
                    set.X += -1.0f;
                    break;
                case (CardinalDirection.NorthWest):
                    set.Y += -1f;
                    set.X += -1f;
                    break;
                case (CardinalDirection.NorthEast):
                    set.Y += -1f;
                    set.X += 1f;
                    break;
                case (CardinalDirection.SouthWest):
                    set.Y += 1f;
                    set.X += -1f;
                    break;
                case (CardinalDirection.SouthEast):
                    set.Y += 1f;
                    set.X += 1f;
                    break;
            }
        }

        /// <summary>
        /// Move the enemy according to this movement class specification
        /// </summary>
        /// <returns>
        /// A finalized vector to be added to a Sprites position
        /// </returns>
        public abstract Vector2 Move();
    }
}
