using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Collections.Concurrent;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Manages queue operations of EntitiyManager
    /// </summary>
    public partial class EntityManager
    {
        /// <summary>
        /// Insert sprites into game-lists from a concurrent queue
        /// </summary>
        /// <param name="queue"></param>
        private void ReadReadyQueue(ConcurrentQueue<EventArgs> queue)
        {
            int TESTING_bullets = 0;

            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                EventArgs e;
                if (queue.TryDequeue(out e))
                {
                    if (e is AddBulletEventArgs) //add bullet
                    {
                        this.ReadBullet((AddBulletEventArgs)e);

                        TESTING_bullets++;
                        continue;
                    }
                    if (e is AddPlayerEventArgs) //add player
                    {
                        var p = (AddPlayerEventArgs)e;
                        this.SubscribeAll(p.Player);
                        this.players.Add(p.Player);

                        LogConsole.Log("New Player added.");
                        continue;
                    }
                    if (e is AddEnemyEventArgs) //add enemy
                    {
                        var p = (AddEnemyEventArgs)e;
                        this.SubscribeAll(p.Enemy);
                        this.entities.Add(p.Enemy);

                        LogConsole.Log("New Enemy added.");
                        continue;
                    }
                    if (e is AddSpawnerEventArgs) //add spawner
                    {
                        var p = (AddSpawnerEventArgs)e;
                        p.Spawner.parent = p.Parent;
                        this.SubscribeAll(p.Spawner);
                        this.spawners.Add(p.Spawner);

                        LogConsole.Log("New Spawner added.");
                        continue;
                    }
                    if (e is AddGUIEventArgs)
                    {
                        this.ReadGUIComponent((AddGUIEventArgs)e);
                        LogConsole.Log("GUI component added.");
                        continue;
                    }
                    throw new Exception("Warning: ReadReadyQueue(): Unrecognized EventArgs");
                }
            }

            if (TESTING_bullets > 0)
                LogConsole.Log(TESTING_bullets.ToString() + " Bullets were fired.");
        }

        /// <summary>
        /// Reads events which update game-lists. Excluding additions and removals
        /// </summary>
        /// <param name="queue"></param>
        private void ReadUpdateQueue(ConcurrentQueue<EventArgs> queue)
        {
            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                EventArgs e;
                if (queue.TryDequeue(out e))
                {
                    if (e is RespawnEventArgs)
                    {
                        this.ReadRespawnEvent((RespawnEventArgs)e);
                        LogConsole.Log("Enitiy Respawn Event.");
                        continue;
                    }

                    throw new Exception("EntitiyManager: ReadUpdateQueue: Non-Recognized EventArgs");
                }
            }
        }

        /// <summary>
        /// Remove sprites from game-lists from a concurrent queue
        /// </summary>
        /// <param name="queue"></param>
        private void ReadDisposeQueue(ConcurrentQueue<DisposeEventArgs> queue)
        {
            int TESTING_bullets = 0;

            int count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                DisposeEventArgs e;
                Sprite s;
                if (queue.TryDequeue(out e))
                {
                    s = e.Sprite;
                    if (s == PlayerOne)
                    {
                        return;
                    }

                    if (this.entities.Contains(s))
                    {
                        this.entities.Remove((Entity)s);
                        this.spawners.RemoveAll(x => x.parent == s);
                        LogConsole.Log("Entitiy and Spawner Disposed.");
                    }

                    if (this.players.Contains(s))
                    {
                        this.players.Remove((Entity)s);
                        LogConsole.Log("Player Disposed.");
                    }

                    if (s is Bullet)
                    {
                        if (this.player_bullets.Contains(s))
                        {
                            this.player_bullets.Remove((Bullet)s);
                        }

                        if (this.enemy_bullets.Contains(s))
                        {
                            this.enemy_bullets.Remove((Bullet)s);
                        }
                        TESTING_bullets++;
                    }
                }
            }

            if (TESTING_bullets > 0)
                LogConsole.Log(TESTING_bullets.ToString() + " Bullets Disposed.");
        }

        /// <summary>
        /// Add a bullet by EventArgs passed from the EventManager Concurrent Queue
        /// </summary>
        /// <param name="e"></param>
        private void ReadBullet(AddBulletEventArgs e)
        {
            Bullet b = e.Bullet;

            if (e.Parent is Player)
            {
                this.player_bullets.Add(b);
            }
            else
            {
                this.enemy_bullets.Add(b);
            }
        }

        /// <summary>
        /// Add a guiComponent from the EventManager Concurrent Queue
        /// </summary>
        /// <param name="e"></param>
        private void ReadGUIComponent(AddGUIEventArgs e)
        {
            if (e.Component is PlayerLives)
            {
                PlayerLives p = (PlayerLives)e.Component;
                p.Parent = (Player)e.Parent;
                this.GUIComponents.Add(p);
                return;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates lists when an entity must be respawned
        /// </summary>
        /// <param name="e"></param>
        private void ReadRespawnEvent(RespawnEventArgs e)
        {
            if (e.Entity is Player)
            {
                Player p = (Player)e.Entity;
                p.MakeInvincible(3);
                p.Position = e.NewPosition;
                foreach (BulletSpawner s in this.spawners)
                {
                    if (s.parent == p)
                    {
                        s.Position = e.NewPosition;
                    }
                }
                return;
            }

            throw new NotImplementedException();
        }
    }
}
