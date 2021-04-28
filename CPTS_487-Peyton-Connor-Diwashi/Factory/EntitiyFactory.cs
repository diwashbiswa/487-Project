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
        public enum EntitiyType { Grunt1, Grunt2, Boss1, Boss2, Player};

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
