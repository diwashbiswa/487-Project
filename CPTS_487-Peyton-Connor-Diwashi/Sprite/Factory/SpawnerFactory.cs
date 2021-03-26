﻿using Microsoft.Xna.Framework;
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
        protected ContentManager Content;

        public enum SpawnerType { CardinalSouth, Targeted };

        private Enemy parent = null;

        public Enemy Parent
        {
            get
            {
                return this.parent;
            }
            set
            {
                this.parent = value;
            }
        }

        public SpawnerFactory(ContentManager content_manager)
        {
            this.Content = content_manager;
        }

        /// <summary>
        /// Gets a new instance of an Enemy of EnemyFactory.EnemyType
        /// </summary>
        /// <param name="type"> EnemyFactory.EnemyType </param>
        /// <returns></returns>
        public BulletSpawner CreateSpawner(SpawnerType type)
        {
            return this.createSpawner(type);
        }

        protected abstract BulletSpawner createSpawner(SpawnerType type);
    }
}