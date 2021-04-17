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
    public class EntityEventManager
    {
        private EntityManager objectManager;

        public EntityEventManager() { }

        private ConcurrentQueue<EventArgs> readyQueue = new ConcurrentQueue<EventArgs>();
        private ConcurrentQueue<Sprite> disposeQueue = new ConcurrentQueue<Sprite>();

        /// <summary>
        /// Items ready to be dynamically added to the game (as EventArgs)
        /// </summary>
        public ConcurrentQueue<EventArgs> ReadyQueue { get { return this.readyQueue; } }

        /// <summary>
        /// Sprites ready to be dynamically removed from the game
        /// </summary>
        public ConcurrentQueue<Sprite> DisposeQueue {  get { return this.disposeQueue; } }

        public EntityManager ObjectManager
        {
            set
            {
                this.objectManager = value;
            }
        }

        /// <summary>
        /// Enqueues an item to the Concurrent DisposeQueue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Dispose(object sender, EventArgs e)
        {
                Sprite s = (Sprite)sender;
                disposeQueue.Enqueue(s);
        }

        /// <summary>
        /// Enqueues an item to the Concurrent ReadyQueue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Fire(object sender, BulletEventArgs e)
        {
            if (sender is not Bullet)
                throw new Exception("Fire event Invoked with non-bullet sender");

                Bullet b = (Bullet)sender;
                b.Dispose += this.Dispose;
                readyQueue.Enqueue(e);
        }
    }
}
