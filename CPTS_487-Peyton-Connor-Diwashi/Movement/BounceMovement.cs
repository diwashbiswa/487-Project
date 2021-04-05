using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// WORK IN PROGRESS
    /// 
    /// This class will be replaced by ScriptMovement
    /// </summary>
    public class BounceMovement : Movement
    {
        private Rectangle fence;

        private Rectangle track;

        private Vector2 cd = new Vector2(0, 0);

        private CardinalDirection direc;

        private Random rand = new Random();

        private Vector2 Position
        {
            get
            {
                return new Vector2((float)this.track.X, (float)this.track.Y);
            }
            set
            {
                Vector2 v = value;
                this.track.X = (int)v.X;
                this.track.Y = (int)v.Y;
            }
        }

        private CardinalDirection getRandomDirection()
        {
            return (CardinalDirection)rand.Next(0, 8);
        }

        public BounceMovement(float speed, Rectangle fence, Rectangle track) : base(speed)
        {
            this.fence = fence;
            this.track = track;
            direc = getRandomDirection();
            Cardinal2Vector(ref cd, this.direc);
        }

        public override Vector2 Move()
        {
            Vector2 fm = this.Position += (this.cd * this.Speed);

            while (!(fence.Contains(fm.ToPoint())))
            {
                this.direc = getRandomDirection();
                Cardinal2Vector(ref cd, this.direc);
                fm = this.Position += (this.cd * this.Speed);
            }

            return this.cd * this.Speed;
        }
    }
}