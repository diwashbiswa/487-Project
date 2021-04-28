using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class WaveManager
    {
        private int cid = 1;
        bool lockmanager = false;

        /// <summary>
        /// Event invoked when a new sprite is added from a wave. Uses AddEnemyEventArgs or AddSpawnerEventArgs
        /// </summary>
        public EventHandler Spawn = delegate { };

        /// <summary>
        /// Invoked when any wave finishes spawning, provides SpawnFinishedEventArgs
        /// </summary>
        public EventHandler<SpawnFinishedEventArgs> WaveSpawnFinished = delegate { };

        /// <summary>
        /// Invoked when the final wave finishes spawning
        /// </summary>
        public EventHandler<SpawnFinishedEventArgs> FinalWaveSpawnFinished = delegate { };

        /// <summary>
        /// Invoked when each enemy in a wave are dead
        /// </summary>
        public EventHandler<WaveFinishedEventArgs> WaveFinished = delegate { };

        /// <summary>
        /// Invoked when each enemy in the final wave is dead
        /// </summary>
        public EventHandler<WaveFinishedEventArgs> FinalWaveFinished = delegate { };

        /// <summary>
        /// The queue of waves.
        /// </summary>
        private Queue<SpriteWave> waveQueue = new Queue<SpriteWave>();
        public WaveManager() { }

        /// <summary>
        /// Enqueue a wave 
        /// </summary>
        /// <param name="wave"></param>
        /// <param name="begin_time_seconds"></param>
        public void Enqueue(SpriteWave wave)
        {
            if (lockmanager)
                throw new Exception("You cannot add waves once they have started spawning!");

            wave.WaveID = this.cid;
            this.cid++;
            wave.Spawn += spawnEnemy;
            wave.SpawnFinished += this.waveSpawnFinished;
            wave.WaveFinished += this.waveFinished;
            this.waveQueue.Enqueue(wave);
        }

        /// <summary>
        /// Concurrently run the next wave in the queue
        /// </summary>
        /// <returns></returns>
        public bool StartNextWave()
        {
            if (this.waveQueue.Count < 1)
            {
                return false;
            }
            this.lockmanager = true;
            SpriteWave next = this.waveQueue.Dequeue();
            next.StartConcurrent();
            return true;
        }

        private void spawnEnemy(object sender, EventArgs e)
        {
            this.Spawn.Invoke(sender, e);
        }

        private void waveSpawnFinished(object sender, SpawnFinishedEventArgs e)
        {
            this.WaveSpawnFinished.Invoke(sender, e);
            if (e.WaveID == this.cid-1)
            {
                this.FinalWaveSpawnFinished.Invoke(sender, e);
            }
        }

        private void waveFinished(object sender, WaveFinishedEventArgs e)
        {
            this.WaveFinished.Invoke(sender, e);
            if (e.WaveID == this.cid - 1)
            {
                this.FinalWaveFinished.Invoke(sender, e);
            }
        }
    }
}
