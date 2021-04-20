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
    public class StandardEntityFactory : EntitiyFactory
    {
        public StandardEntityFactory(Rectangle spawn_bounds) : base(spawn_bounds) { }

        protected override Entity createEnemy(EntitiyType type)
        {
            TextureManager texManager = TextureManager.Textures;

            Random rand = new Random();
            // Get random spawn
            float xS = (float)rand.Next(this.XMin, this.XMax);
            float yS = (float)rand.Next(this.YMin, this.YMax);

            Entity e;

            switch (type)
            {
                case EntitiyType.Grunt1:
                    e = new Grunt1(new Vector2(xS, yS), texManager.Get(TextureManager.Type.Grunt1));
                    break;
                case EntitiyType.Grunt2:
                    e = new Grunt2(new Vector2(xS, yS), texManager.Get(TextureManager.Type.Grunt2));
                    break;
                case EntitiyType.Boss1:
                    e = new Boss1(new Vector2(xS, yS), texManager.Get(TextureManager.Type.Boss1));
                    break;
                case EntitiyType.Boss2:
                    e = new Boss2(new Vector2(xS, yS), texManager.Get(TextureManager.Type.Boss2));
                    break;
                case EntitiyType.Player:
                    e = new Player(new Vector2(500, 300), texManager.Get(TextureManager.Type.SpaceshipPlayer), 7.0f);
                    break;
                default:
                    throw new Exception("Warning: StandardEnemyFactory: Request for unsupported Enemy");
            }

            e.LifeSpan = this.LifeSpanSeconds;

            return e;
        }
    }
}