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
                        // subscribe and add enemy
                        this.SubscribeAll(p.Enemy);
                        this.entities.Add(p.Enemy);
                        LogConsole.Log("New Enemy added.");
                        // Add the health bar
                        HealthBarComponent hbc = new HealthBarComponent(p.Enemy);
                        eventManager.ReadyEnqueue(hbc, new AddGUIEventArgs(hbc, p.Enemy));
                        hbc.Dispose += this.eventManager.Dispose;
                        LogConsole.Log("New Health Bar Component added.");
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
                    if (e is AddRewardEventArgs) //add reward
                    {
                        var p = (AddRewardEventArgs)e;
                        this.SubscribeAll(p.Reward);
                        this.rewards.Add(p.Reward);
                        LogConsole.Log("New Reward added.");
                        continue;
                    }
                    if (e is AddGUIEventArgs)
                    {
                        this.ReadGUIComponent((AddGUIEventArgs)e);
                        LogConsole.Log("GUI component added.");
                        continue;
                    }
                    throw new Exception("EntitiyQueueManager: ReadReadyQueue(): Unrecognized EventArgs");
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
                    if (e is RespawnEventArgs) //respawn
                    {
                        this.ReadRespawnEvent((RespawnEventArgs)e);
                        LogConsole.Log("Enitiy Respawn Event.");
                        continue;
                    }
                    if (e is GameOverEventArgs) //game over
                    {
                        this.ReadGameOverEvent((GameOverEventArgs)e);
                        LogConsole.Log("Game Over Event.");
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
                        return;
                    if (this.entities.Contains(s)) //entities
                    {
                        this.entities.Remove((Entity)s);
                        LogConsole.Log("Entitiy Disposed");

                        if (this.spawners.RemoveAll(x => x.parent == s) > 0)
                            LogConsole.Log("Spawner(s) Disposed.");
                    }
                    if (this.players.Contains(s)) // players
                    {
                        this.players.Remove((Entity)s);
                        LogConsole.Log("Player Disposed.");
                    }
                    if (s is Bullet) // bullets
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
                    if (s is Reward)
                    {
                        if (this.rewards.Contains(s))
                        {
                            this.rewards.Remove((Reward)s);
                            LogConsole.Log("Reward live Disposed.");
                        }
                    }
                    if (s is HealthBar)
                    {
                        HealthBar h = (HealthBar)s;
                        this.gui_components.Remove(h.Parent);
                    }
                }
            }
            if (TESTING_bullets > 0)
                LogConsole.Log(TESTING_bullets.ToString() + " Bullets Disposed.");
        }

        #region helper

        /// <summary>
        /// Add a bullet by EventArgs passed from the EventManager Concurrent Queue
        /// </summary>
        /// <param name="e"></param>
        private void ReadBullet(AddBulletEventArgs e)
        {
            Bullet b = e.Bullet;
            b.Dispose += this.eventManager.Dispose;
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
                this.gui_components.Add(p);
                return;
            }
            if (e.Component is GameOverComponent)
            {
                this.gui_components.Add(e.Component);
                return;
            }
            if (e.Component is HealthBarComponent)
            {
                this.gui_components.Add(e.Component);
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
                this.spawners.Where(x => x.parent == p).ToList().ForEach(x => x.Position = e.NewPosition);
                return;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads a GameOverEvent from the updatequeue, pushes a new GUI component
        /// </summary>
        /// <param name="e"></param>
        private void ReadGameOverEvent(GameOverEventArgs e)
        {
            // We should only have 1 gameovercomponent
            if (this.gui_components.FindAll(x => x is GameOverComponent).Count > 0)
                return;
            // Stop the eventmanager form spawning enemies
            eventManager.WaveManager.Spawn -= eventManager.ReadyEnqueue;
            Random rand = new Random();
            GameOverComponent g = new GameOverComponent();
            g.Win = e.Win;
            g.Exit += this.ExitGame;
            eventManager.ReadyEnqueue(g, new AddGUIEventArgs(g));
            foreach (Entity i in this.entities)
            {
                i.Color = Color.Black;
                i.LifeSpan = (uint)rand.Next(1, 4);
            }
            this.players.Clear();
        }

        #endregion
    }
}
