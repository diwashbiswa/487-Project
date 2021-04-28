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
    public class StandardEntityFactory : EntityFactory
    {
        public StandardEntityFactory() { }
        protected override Entity createEnemy(EntitiyType type)
        {
            TextureManager texManager = TextureManager.Textures;

            Entity e;

            switch (type)
            {
                case EntitiyType.Grunt1:
                    e = new Grunt1(Vector2.Zero, texManager.Get(TextureManager.Type.Grunt1));
                    break;
                case EntitiyType.Grunt2:
                    e = new Grunt2(Vector2.Zero, texManager.Get(TextureManager.Type.Grunt2));
                    break;
                case EntitiyType.Boss1:
                    e = new Boss1(Vector2.Zero, texManager.Get(TextureManager.Type.Boss1));
                    break;
                case EntitiyType.Boss2:
                    e = new Boss2(Vector2.Zero, texManager.Get(TextureManager.Type.Boss2));
                    break;
                case EntitiyType.Player:
                    e = new Player(Vector2.Zero, texManager.Get(TextureManager.Type.SpaceshipPlayer), 7.0f);
                    e.Health = 5;
                    break;
                default:
                    throw new Exception("Warning: StandardEnemyFactory: Request for unsupported Enemy");
            }
            return e;
        }
    }
}