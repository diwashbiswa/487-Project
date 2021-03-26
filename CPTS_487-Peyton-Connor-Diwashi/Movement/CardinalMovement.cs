using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Class for carinal movment in Movement.CardinalDirection
    /// </summary>
    public class CardinalMovement : Movement
    {
        protected Movement.CardinalDirection direction;

        private Vector2 vectorizedDirection;

        public Movement.CardinalDirection Direction
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

        public CardinalMovement(float speed, Movement.CardinalDirection direction) : base(speed)
        {
            this.Direction = direction;
            this.vectorizedDirection = new Vector2(0, 0);
        }

        public override Vector2 Move()
        {
            this.Cardinal2Vector(ref this.vectorizedDirection, this.Direction);

            return this.vectorizedDirection * this.Speed;
        }
    } 
}