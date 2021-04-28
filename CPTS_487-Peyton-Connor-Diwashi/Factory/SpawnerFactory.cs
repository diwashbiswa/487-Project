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

        public enum BulletType { Green, Purple, Boss }

        public SpawnerFactory() { }

        /// <summary>
        /// Gets a new instance of an Enemy of EnemyFactory.EnemyType
        /// </summary>
        /// <param name="type"> EnemyFactory.EnemyType </param>
        /// <returns></returns>
        public BulletSpawner CreateSpawner(SpawnerType type, Entity parent, BulletType btype = BulletType.Green)
        {
            return this.createSpawner(type, parent, btype);
        }

        protected abstract BulletSpawner createSpawner(SpawnerType type, Entity parent, BulletType btype);

        protected Texture2D BType2Tex(BulletType btype)
        {
            TextureManager state = TextureManager.Textures;
            switch (btype)
            {
                case BulletType.Green:
                    return state.Get(TextureManager.Type.BulletGreen);
                case BulletType.Purple:
                    return state.Get(TextureManager.Type.BulletPurple);
                case BulletType.Boss:
                    return state.Get(TextureManager.Type.BossBullet);
                default:
                    throw new NotImplementedException("BType2Tex: Bullet type 2 texture not implemented");
            }
        }
    }
}
