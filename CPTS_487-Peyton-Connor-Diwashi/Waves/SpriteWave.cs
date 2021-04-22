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
        private uint begin_time = 0;
        private uint max_run_seconds = 60 * 2;
        private int waveID = 0;
        private bool done = false;

        /// <summary>
        /// Event invoked when a new sprite is added from the wave
        /// </summary>
        public EventHandler Spawn = delegate { };
        private List<Entity> entities = new List<Entity>();
        private List<BulletSpawner> spawners = new List<BulletSpawner>();

        public SpriteWave(uint t_begin, int wave_id = 0)
        {
            this.waveID = wave_id;
            this.begin_time = t_begin;
        }

        /// <summary>
        /// The time this wave should start (seconds)
        /// </summary>
        public uint BeginTime { get { return this.begin_time; } }

        /// <summary>
        /// The ID for this wave
        /// </summary>
        public int WaveID { get { return this.waveID; } }

        /// <summary>
        /// Stop the spawning after this many seconds no matter what.
        /// </summary>
        public uint MaxRunSeconds
        {
            get { return this.max_run_seconds; }
            set { this.max_run_seconds = value; }
        }

        /// <summary>
        /// Is this wave over?
        /// </summary>
        public bool WaveDone { get { return this.done; } }

        /// <summary>
        /// Directly add an entitiy to this wave
        /// </summary>
        /// <param name="e"></param>
        /// <param name="wavetime_seconds"></param>
        public void AddEntitiy(Entity e, int wavetime_seconds = -1)
        {
            if (wavetime_seconds > -1)
                e.WaveTimeSeconds = wavetime_seconds;
            this.entities.Add(e);
        }

        /// <summary>
        /// Directly add a spawner to this wave
        /// </summary>
        /// <param name="e"></param>
        /// <param name="wavetime_seconds"></param>
        public void AddSpawner(BulletSpawner e, int wavetime_seconds = -1)
        {
            if (wavetime_seconds > -1)
                e.WaveTimeSeconds = wavetime_seconds;
            this.spawners.Add(e);
        }

        /// <summary>
        /// Starts this wave as a new Task. Invoking Spawn event approprately
        /// </summary>
        public void StartConcurrent()
        {
            Task.Factory.StartNew(() => this.Start());
        }

        private void Start()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            while(entities.Count > 0 || spawners.Count > 0)
            {
                int TESTING_s_initial = this.entities.Count + this.spawners.Count;

                double elapse = stopwatch.Elapsed.TotalSeconds;
                entities.Where(x => x.WaveTimeSeconds <= elapse).ToList().ForEach(
                    x => this.Spawn.Invoke(x, new AddEnemyEventArgs(x)));
                entities.RemoveAll(x => x.WaveTimeSeconds <= elapse);
                spawners.Where(x => x.WaveTimeSeconds <= elapse).ToList().ForEach(
                    x => this.Spawn.Invoke(x, new AddSpawnerEventArgs(x.parent, x)));
                spawners.RemoveAll(x => x.WaveTimeSeconds <= elapse);


                int TESTING_s_final = (TESTING_s_initial - (this.spawners.Count + this.entities.Count));
                if(TESTING_s_final > 0)
                    LogConsole.Log(TESTING_s_final.ToString() + " sprites spawned from SpriteWave: " + this.waveID.ToString() + ".");


                if (elapse > this.max_run_seconds)
                    break;
            }
            this.done = true;
        }
    }
}
