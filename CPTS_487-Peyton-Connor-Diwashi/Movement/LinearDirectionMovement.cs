using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Class for linear movement in a certain direction
    /// </summary>
    public class LinearDirectionMovement : Movement
    {
        private Vector2 direction;

        /// <summary>
        /// The linear direction of movement
        /// </summary>
        public Vector2 Direction
        {
            get
            {
                return this.direction;
            }
            protected set
            {
                this.direction = value;
            }
        }

        /// <summary>
        /// Create a new instance of LinearDirectionMovement for moving in direction
        /// </summary>
        /// <param name="speed"> Speed of movement </param>
        /// <param name="direction"> Direction of movement </param>
        public LinearDirectionMovement(float speed, Vector2 direction) : base(speed)
        {
            this.Direction = direction;
        }

        /// <summary>
        /// Create a new instance of LinearDirectionMovement for moving from position towards target
        /// </summary>
        /// <param name="speed"> Speed of movement </param>
        /// <param name="position"> To move from this position  </param>
        /// <param name="target"> To move towards this position </param>
        public LinearDirectionMovement(float speed, Vector2 position, Vector2 target) : base(speed)
        {
            Vector2 dv = target - position;
            this.Direction = dv / dv.Length();
        }

        /// <summary>
        /// Create a new instance of LinearDirectionMovement for moving at speed and angle
        /// </summary>
        /// <param name="speed"> Speed of movement </param>
        /// <param name="angle"> Angle of movement </param>
        /// <param name="degrees"> Use degrees or radians? </param>
        public LinearDirectionMovement(float speed, double angle, bool degrees=false) : base(speed)
        {
            if (degrees)
            {
                while (angle >= 360.0)
                    angle -= 360.0;

                angle = (Math.PI / 180) * angle;
            }

            this.Direction = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        /// <summary>
        /// Move the enemy according to this movement class specification
        /// </summary>
        /// <returns>
        /// A finalized vector to be added to a Sprites position
        /// </returns>
        public override Vector2 Move()
        {
            return this.Direction * this.Speed;
        }
    }
}