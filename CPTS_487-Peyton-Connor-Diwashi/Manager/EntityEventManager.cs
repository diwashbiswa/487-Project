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

            if (e.Parent.Invincible == true)
                return;

            Bullet b = (Bullet)sender;
            b.Dispose += this.Dispose;
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
    }
}
