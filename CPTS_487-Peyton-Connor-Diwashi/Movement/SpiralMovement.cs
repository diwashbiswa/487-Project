using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace CPTS_487_Peyton_Connor_Diwashi

{
    public class SpiralMovement : Movement
    {
        Stopwatch time;
        int scalar = 1;
        public SpiralMovement(float speed, bool reversed=false) : base(speed)
        {
            this.time = new Stopwatch();
            this.time.Start();

            if(reversed)
            {
                scalar = -1;
            }

        }

        public override Vector2 Move()
        {
            return (new Vector2((float)Math.Cos(this.scalar*this.time.ElapsedMilliseconds/200), (float)Math.Sin(scalar*this.time.ElapsedMilliseconds/200))) * this.Speed * (float)this.time.ElapsedMilliseconds/500;
        }
    }
}
