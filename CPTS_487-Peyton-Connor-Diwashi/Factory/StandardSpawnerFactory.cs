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
    public class StandardSpawnerFactory : SpawnerFactory
    {
        public StandardSpawnerFactory() { }

        protected override BulletSpawner createSpawner(SpawnerType type, Entity parent, BulletType btype)
        {
            Texture2D btex = BType2Tex(btype);

            Entity e = parent;

            switch (type)
            {
                case (SpawnerType.CardinalSouth):
                    return new CardinalBulletSpawner(e, btex, e.Position, e.Movement, e.Width, e.Height, Movement.CardinalDirection.South);
                case (SpawnerType.Targeted):
                    return new TargetedBulletSpawner(e, btex, e.Position, e.Movement, e.Width, e.Height);
                case (SpawnerType.Keyboard):
                    return new KeyboardBulletSpawner(e, btex, e.Position, e.Movement, e.Width, e.Height, Movement.CardinalDirection.North);
                default:
                    throw new Exception("StandardSpawnerFactory type requested not yet implemented");
            }
        }
    }
}