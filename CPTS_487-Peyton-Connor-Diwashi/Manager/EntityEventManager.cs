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
    /// Manages concurrent queues populated by Events in the game.
    /// </summary>
    public class EntityEventManager
    {
        private EntityManager objectManager;

        private WaveManager waveManager;

        public EntityEventManager() { }

        private ConcurrentQueue<EventArgs> readyQueue = new ConcurrentQueue<EventArgs>();
        private ConcurrentQueue<DisposeEventArgs> disposeQueue = new ConcurrentQueue<DisposeEventArgs>();
        private ConcurrentQueue<EventArgs> updateQueue = new ConcurrentQueue<EventArgs>();

        /// <summary>
        /// Items ready to be dynamically added to the game (as EventArgs)
        /// </summary>
        public ConcurrentQueue<EventArgs> ReadyQueue { get { return this.readyQueue; } }

        /// <summary>
        /// Sprites ready to be removed from the game (as DisposeEventArgs)
        /// </summary>
        public ConcurrentQueue<DisposeEventArgs> DisposeQueue {  get { return this.disposeQueue; } }

        /// <summary>
        /// Queue of EventArgs which require an update of the lists
        /// </summary>
        public ConcurrentQueue<EventArgs> UpdateQueue {  get { return this.updateQueue; } }

        public EntityManager ObjectManager
        {
            set
            {
                this.objectManager = value;
            }
        }

        public WaveManager WaveManager
        {
            set
            {
                this.waveManager = value;
                value.Spawn += this.ReadyEnqueue;
                value.WaveSpawnFinished += this.WaveSpawnFinished;
                value.FinalWaveSpawnFinished += this.FinalWaveSpawnFinished;
                value.WaveFinished += this.WaveFinished;
                value.FinalWaveFinished += this.FinalWaveFinished;
            }
        }

        /// <summary>
        /// Enqueues a Sprite to the ReadyQueue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReadyEnqueue(object sender, EventArgs e)
        {
            this.readyQueue.Enqueue(e);
        }

        /// <summary>
        /// Enqueues an update event to the update queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateEnqueue(object sender, EventArgs e)
        {
            this.updateQueue.Enqueue(e);
        }

        /// <summary>
        /// Enqueues an item to the Concurrent DisposeQueue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Dispose(object sender, DisposeEventArgs e)
        {
            // PlayerOne being disposed.. game over.
            if (e.Sprite == objectManager.PlayerOne)
                this.updateQueue.Enqueue(new GameOverEventArgs(false));

             disposeQueue.Enqueue(e);
        }

        /// <summary>
        /// Enqueues an item to the Concurrent ReadyQueue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Fire(object sender, AddBulletEventArgs e)
        {
            if (sender is not Bullet)
            {
                throw new Exception("Fire event Invoked with non-bullet sender");
            }

            // Cant fire with invincibility
            if (e.Parent.Invincible == true)
                return;

            Bullet b = (Bullet)sender;
            readyQueue.Enqueue(e);
        }

        /// <summary>
        /// Handles Collision events invoked from Entitiy types
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Collided(object sender, EntityCollideEventArgs e)
        {
            if (e.Victim is Player)
            {
                Player player = (Player)e.Victim;
                if (e.Attacker is Bullet)
                {
                    if(!player.Invincible)
                        this.updateQueue.Enqueue(new RespawnEventArgs(player, new Vector2(600, 600)));

                    LogConsole.Log("Player has been hit");
                    return;
                }

                throw new NotImplementedException("EntitiyManager: Collided(): Non-Bullet Attacker");
            }
            
            throw new NotImplementedException("EntityManager: Collided(): Non-Player Victim");
        }

        /// <summary>
        /// Some wave has finished spawning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WaveSpawnFinished(object sender, SpawnFinishedEventArgs e)
        {
            LogConsole.Log("Wave: " + e.WaveID.ToString() + " finished spawning.");
        }

        /// <summary>
        /// Final wave has finished spawning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FinalWaveSpawnFinished(object sender, SpawnFinishedEventArgs e)
        {
            LogConsole.Log("Final Wave spawn finished.");
        }

        /// <summary>
        /// All enemies are dead in some wave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void WaveFinished(object sender, WaveFinishedEventArgs e)
        {
            LogConsole.Log("Wave: " + e.WaveID.ToString() + " has been completed.");
            // all enemies in current wave are dead.. start next wave.
            this.waveManager.StartNextWave();
        }

        /// <summary>
        /// All enemies are dead in the final wave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FinalWaveFinished(object sender, WaveFinishedEventArgs e)
        {
            LogConsole.Log("Final wave has been completed.");
            // No more enemies in the final wave.. player wins.
            this.updateQueue.Enqueue(new GameOverEventArgs(true));
        }
    }
}
