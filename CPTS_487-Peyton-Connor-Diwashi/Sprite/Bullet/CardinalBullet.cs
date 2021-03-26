using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class CardinalBullet : Bullet
    {
        public CardinalBullet(Vector2 position, Vector2 target, Texture2D texture, float speed, float lifespan_seconds) : base(position, texture, speed, lifespan_seconds)
        {
            this.movement = new LinearDirectionMovement(speed, position, target);
        }

        public CardinalBullet(Vector2 position, Movement.CardinalDirection direction, Texture2D texture, float speed, float lifespan_seconds) : base(position, texture, speed, lifespan_seconds)
        {
            this.movement = new CardinalMovement(speed, direction);
        }
    }
}
