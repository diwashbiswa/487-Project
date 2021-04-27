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
    public class StandardMovementFactory : MovementFactory
    {
        protected override Movement createMovement(MovementType type, Entity parent)
        {
            int speed = (int)parent.Speed;
            switch(type)
            {
                case MovementType.Bounce:
                    return new BounceMovement(speed, new Rectangle(50, 50, 1180, 600), parent.Body);
                case MovementType.CardinalSouth:
                    return new CardinalMovement(speed, Movement.CardinalDirection.South);
                case MovementType.Keyboard:
                    return new KeyboardMovement(speed);
                case MovementType.None:
                    return new NoneMovement();
                case MovementType.Mirror:
                    return new MirrorMovement(parent);
                default:
                    throw new NotImplementedException("StandardMovementFactory: MovementType: " + type.ToString() + "not recognized");
            }
        }
    }
}
