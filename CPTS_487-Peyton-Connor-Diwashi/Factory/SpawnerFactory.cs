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
    public abstract class SpawnerFactory
    {

        public enum SpawnerType { None, CardinalSouth, Targeted, Keyboard };

        public SpawnerFactory() { }

        /// <summary>
        /// Gets a new instance of an Enemy of EnemyFactory.EnemyType
        /// </summary>
        /// <param name="type"> EnemyFactory.EnemyType </param>
        /// <returns></returns>
        public BulletSpawner CreateSpawner(SpawnerType type, Entity parent)
        {
            return this.createSpawner(type, parent);
        }

        protected abstract BulletSpawner createSpawner(SpawnerType type, Entity parent);
    }
}
