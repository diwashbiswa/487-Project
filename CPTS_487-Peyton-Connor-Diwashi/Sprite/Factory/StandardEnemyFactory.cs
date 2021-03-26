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
    public class StandardEnemyFactory : EnemyFactory
    {
        public StandardEnemyFactory(Rectangle spawn_bounds, ContentManager content_manager) : base(spawn_bounds, content_manager) { }

        protected override Enemy createEnemy(EnemyType type)
        {
            Random rand = new Random();
            // Get random spawn
            float xS = (float)rand.Next(this.XMin, this.XMax);
            float yS = (float)rand.Next(this.YMin, this.YMax);

            Enemy e;

            switch (type)
            {
                case EnemyType.Grunt1:
                    e = new Grunt1(new Vector2(xS, yS), this.Content.Load<Texture2D>("Grunt1"), this.Content.Load<Texture2D>("BulletGreen"), ref this.spawnBounds);
                    break;
                case EnemyType.Grunt2:
                    e = new Grunt2(new Vector2(xS, yS), this.Content.Load<Texture2D>("Grunt2"), this.Content.Load<Texture2D>("BulletPurple"), ref this.spawnBounds);
                    break;
                case EnemyType.Boss1:
                    e = new Boss1(new Vector2(xS, yS), this.Content.Load<Texture2D>("Boss1"), this.Content.Load<Texture2D>("BossBullet"), ref this.spawnBounds);
                    break;
                case EnemyType.Boss2:
                    e = new Boss2(new Vector2(xS, yS), this.Content.Load<Texture2D>("Boss2"), this.Content.Load<Texture2D>("BossBullet"), ref this.spawnBounds);
                    break;
                default:
                    throw new Exception("Warning: StandardEnemyFactory: Request for unsupported Enemy");
            }

            // Subscribe to a specified event handler for Enemy.Dispose
            if (this.disposeMethod != null)
            {
                e.Dispose += this.disposeMethod;
            }

            e.LifeSpan = this.LifeSpanSeconds;

            return e;
        }
    }
}