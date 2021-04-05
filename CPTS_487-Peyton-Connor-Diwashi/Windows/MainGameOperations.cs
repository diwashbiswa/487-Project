using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace CPTS_487_Peyton_Connor_Diwashi
{
    public partial class MainGame : Game
    {
        /// <summary>
        /// Collision detection group 1
        /// </summary>
        protected void BuildList_PlayerEnemyBullet()
        {
            collisionList1.Clear();
            collisionList1.Add(player);
            foreach (BulletSpawner s in spawners)
            {
                if (s.parent is not Player)
                    collisionList1.AddRange(s.Bullets);
            }
        }

        /// <summary>
        /// Collision detection group 2
        /// </summary>
        protected void BuildList_EnemiesPlayerBullet()
        {
            collisionList2.Clear();
            collisionList2.AddRange(enemies);
            foreach (BulletSpawner s in spawners)
            {
                if (s.parent is Player)
                    collisionList2.AddRange(s.Bullets);
            }
        }

        /// <summary>
        /// Add sprite to list of drawable, updatable sprites.
        /// </summary>
        /// <param name="s"></param>
        protected void AddEnemy(Entitiy s)
        {
            this.enemies.Add(s);
        }

        /// <summary>
        /// Add a spawner to list of enemy spawners
        /// </summary>
        /// <param name="s"></param>
        protected void AddSpawner(BulletSpawner s)
        {
            this.spawners.Add(s);
        }

        /// <summary>
        /// Sprite must subscibe to this event to be disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DisposeEnemyEvent(object sender, EventArgs e)
        {
            Entitiy x = (Entitiy)sender;

            // Remove each spawner associated with the enemy
            foreach (BulletSpawner s in spawners)
            {
                if (s.parent == x)
                {
                    this.disposedSpawners.Add(s);
                }
            }

            this.disposedEnemies.Add(x);
        }
    }
}