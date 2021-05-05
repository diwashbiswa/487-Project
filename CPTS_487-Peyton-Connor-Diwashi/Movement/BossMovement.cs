using System;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace CPTS_487_Peyton_Connor_Diwashi

{
    public class BossMovement : Movement
    {

        private Vector2 vectorizedDirection;
        Stopwatch time;

        

        public BossMovement(float speed) : base(speed)
        {
            this.vectorizedDirection = new Vector2(0, 0);

            this.time = new Stopwatch();
            this.time.Start();
        }

        public override Vector2 Move()
        {
            if (this.time.ElapsedMilliseconds / 1000 % 10 >= 5)
            {
                this.Cardinal2Vector(ref this.vectorizedDirection, Movement.CardinalDirection.East);
            }
            else
            {
                this.Cardinal2Vector(ref this.vectorizedDirection, Movement.CardinalDirection.West);
            }

            return this.vectorizedDirection * this.Speed;
        }

    }
}
