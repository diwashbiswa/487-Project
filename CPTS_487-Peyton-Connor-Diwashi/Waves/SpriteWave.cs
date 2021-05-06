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
    public class SpriteWave
    {
        public bool lockwave = false;
        private uint max_run_seconds = 60 * 10;
        private int num_entities = 0;
        private int waveID = 0;

        private List<Entity> entities = new List<Entity>();
        private List<BulletSpawner> spawners = new List<BulletSpawner>();
        private ConcurrentBag<Sprite> dispose_bag = new ConcurrentBag<Sprite>();

        /// <summary>
        /// Event invoked when a new sprite is added from the wave. Uses AddEnemyEventArgs or AddSpawnerEventArgs
        /// </summary>
        public EventHandler Spawn = delegate { };
        /// <summary>
        /// Event invoked when the spawning has been finished.. not when enemies have been killes.
        /// </summary>
        public EventHandler<SpawnFinishedEventArgs> SpawnFinished = delegate { };
        /// <summary>
        /// Invoked when each entitiy from this wave is disposed.
        /// </summary>
        public EventHandler<WaveFinishedEventArgs> WaveFinished = delegate { };

        public SpriteWave() { }

        /// <summary>
        /// The ID for this wave
        /// </summary>
        public int WaveID { get { return this.waveID; } set { this.waveID = value; } }

        /// <summary>
        /// Stop the spawning after this many seconds no matter what.
        /// </summary>
        public uint MaxRunSeconds { get { return this.max_run_seconds; } }

        /// <summary>
        /// Directly add an entitiy to this wave
        /// </summary>
        /// <param name="e"></param>
        /// <param name="wavetime_seconds"></param>
        public void AddEntitiy(Entity e, int wavetime_seconds = -1)
        {
            if (lockwave == true)
                throw new Exception("SpriteWave: wave already started. You cannot add more sprites");

            if (wavetime_seconds > -1)
                e.WaveTimeSeconds = wavetime_seconds;
            e.Dispose += this.EntityDisposed;
            this.num_entities++;
            this.entities.Add(e);
        }

        /// <summary>
        /// Directly add a spawner to this wave
        /// </summary>
        /// <param name="e"></param>
        /// <param name="wavetime_seconds"></param>
        public void AddSpawner(BulletSpawner e, int wavetime_seconds = -1)
        {
            if (lockwave == true)
                throw new Exception("SpriteWave: wave already started. You cannot add more sprites");

            if (wavetime_seconds > -1)
                e.WaveTimeSeconds = wavetime_seconds;
            this.spawners.Add(e);
        }

        /// <summary>
        /// Starts this wave as a new Task. Invoking Spawn event approprately
        /// </summary>
        public void StartConcurrent()
        {
            lockwave = true;
            Task t = Task.Factory.StartNew(() => this.Start());
            // When the Start() method returns, incoke the spawn finished event
            t.GetAwaiter().OnCompleted(() => { this.SpawnFinished.Invoke(this, new SpawnFinishedEventArgs(this, this.waveID)); });
        }

        private void Start()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(entities.Count > 0 || spawners.Count > 0)
            {
                double elapse = stopwatch.Elapsed.TotalSeconds;
                entities.Where(x => x.WaveTimeSeconds <= elapse).ToList().ForEach(
                    x => this.Spawn.Invoke(x, new AddEnemyEventArgs(x)));
                entities.RemoveAll(x => x.WaveTimeSeconds <= elapse);

                spawners.Where(x => x.WaveTimeSeconds <= elapse).ToList().ForEach(
                    x => this.Spawn.Invoke(x, new AddSpawnerEventArgs(x.parent, x)));
                spawners.RemoveAll(x => x.WaveTimeSeconds <= elapse);

                if (elapse > this.max_run_seconds)
                    break;
            }
        }

        /// <summary>
        /// Invoked when any of this waves entities is disposed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityDisposed(object sender, DisposeEventArgs e)
        {
            this.dispose_bag.Add(e.Sprite);

            if(this.dispose_bag.Count == this.num_entities)
            {
                this.WaveFinished.Invoke(this, new WaveFinishedEventArgs(this, this.waveID));
            }
        }
    }
}
