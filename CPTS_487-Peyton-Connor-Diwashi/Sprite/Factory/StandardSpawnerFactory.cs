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
        public StandardSpawnerFactory(ContentManager content) : base(content) { }

        protected override BulletSpawner createSpawner(SpawnerType type)
        {
            if (this.Parent == null)
            {
                throw new Exception("StandardSpawnerFactory: parent was null");
            }

            Enemy e = this.Parent;

            switch (type)
            {
                case (SpawnerType.CardinalSouth):
                    return new CardinalBulletSpawner(e, this.Content.Load<Texture2D>("BulletGreen"), e.Position, e.Movement, e.Width, e.Height, Movement.CardinalDirection.South);
                case (SpawnerType.Targeted):
                    return new TargetedBulletSpawner(e, this.Content.Load<Texture2D>("BossBullet"), e.Position, e.Movement, e.Width, e.Height);
                case (SpawnerType.Keyboard):
                    return new KeyboardBulletSpawner(e, this.Content.Load<Texture2D>("BulletPurple"), e.Position, e.Movement, e.Width, e.Height, Movement.CardinalDirection.North);
                default:
                    throw new Exception("StandardSpawnerFactory type requested not yet implemented");
            }
        }
    }
}