﻿using Microsoft.Xna.Framework;
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
        private ConcurrentQueue<DisposeEventArgs> disposeQueue = new ConcurrentQueue<DisposeEventArgs>();

        /// <summary>
        /// Items ready to be dynamically added to the game (as EventArgs)
        /// </summary>
        public ConcurrentQueue<EventArgs> ReadyQueue { get { return this.readyQueue; } }

        /// <summary>
        /// Sprites ready to be removed from the game (as DisposeEventArgs)
        /// </summary>
        public ConcurrentQueue<DisposeEventArgs> DisposeQueue {  get { return this.disposeQueue; } }

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
        public void Dispose(object sender, DisposeEventArgs e)
        {
             disposeQueue.Enqueue(e);
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
