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
    public abstract class EnemyFactory
    {
        protected Rectangle spawnBounds;

        protected ContentManager Content;

        protected EventHandler disposeMethod = null;

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
                if (this.disposeMethod == null)
                {
                    this.lifeSpan = 0;
                }
                else
                {
                    this.lifeSpan = value;
                }
            }
        }

        /// <summary>
        /// EventHandler for Disposed Enemies
        /// </summary>
        public EventHandler DisposeMethod
        {
            set
            {
                this.disposeMethod = value;
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

        public enum EnemyType { Grunt1, Grunt2, Boss1, Boss2};

        /// <summary>
        /// Creates a new instance of the EnemyFactory Class
        /// </summary>
        /// <param name="spawn_bounds"> Bounds for enemies created with this Factory </param>
        public EnemyFactory(Rectangle spawn_bounds, ContentManager content_manager, EventHandler disposeMethod = null)
        {
            this.spawnBounds = spawn_bounds;
            this.Content = content_manager;
            this.disposeMethod = disposeMethod;
            this.LifeSpanSeconds = 0;
        }

        /// <summary>
        /// Gets a new instance of an Enemy of EnemyFactory.EnemyType
        /// </summary>
        /// <param name="type"> EnemyFactory.EnemyType </param>
        /// <returns></returns>
        public Entitiy CreateEnemy(EnemyType type)
        {
            return this.createEnemy(type);
        }

        protected abstract Entitiy createEnemy(EnemyType type);
    }
}
