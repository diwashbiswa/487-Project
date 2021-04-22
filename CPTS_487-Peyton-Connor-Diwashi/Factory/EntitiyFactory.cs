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
    public abstract class EntityFactory
    {
        protected Rectangle spawnBounds;

        private uint lifeSpan = 0;

        /// <summary>
        /// A LifeSpan of '0' will result in an infite life for any created Enemy
        /// For any other LifeSpan, EnemyFactory.DisposeMethod must be set first.
        /// </summary>
        public uint LifeSpanSeconds
        {
            get
            {
                return this.lifeSpan;
            }
            set
            {
                this.lifeSpan = value;
            }
        }

        // Min X Spawn
        protected int XMin
        {
            get { return this.spawnBounds.X + 1; }
        }

        // Max X Spawn
        protected int XMax
        {
            get { return (this.spawnBounds.X + this.spawnBounds.Width) - 1; }
        }

        // Min Y Spawn
        protected int YMin
        {
            get { return this.spawnBounds.Y + 1; }
        }

        // Max Y Spawn
        protected int YMax
        {
            get { return (this.spawnBounds.Y + this.spawnBounds.Height) - 1; }
        }

        public enum EntitiyType { Grunt1, Grunt2, Boss1, Boss2, Player};

        /// <summary>
        /// Creates a new instance of the EnemyFactory Class
        /// </summary>
        /// <param name="spawn_bounds"> Bounds for enemies created with this Factory </param>
        public EntityFactory(Rectangle spawn_bounds)
        {
            this.spawnBounds = spawn_bounds;
            this.LifeSpanSeconds = 0;
        }

        /// <summary>
        /// Gets a new instance of an Enemy of EnemyFactory.EnemyType
        /// </summary>
        /// <param name="type"> EnemyFactory.EnemyType </param>
        /// <returns></returns>
        public Entity CreateEnemy(EntitiyType type)
        {
            return this.createEnemy(type);
        }

        protected abstract Entity createEnemy(EntitiyType type);
    }
}
