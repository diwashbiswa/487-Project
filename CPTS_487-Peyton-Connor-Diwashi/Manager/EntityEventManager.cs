using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class EntityEventManager
    {
        private EntityManager objectManager;

        public EntityEventManager() { }

        public EntityManager ObjectManager
        {
            set
            {
                this.objectManager = value;
            }
        }

        public void Dispose(object sender, EventArgs e)
        {
            // Lock the locking object for thread safety
            lock (objectManager.Lock)
            {
                Entitiy en = (Entitiy)sender;

                // remove any spawners which have disposing parent
                this.objectManager.Spawners.RemoveAll(x => x.parent == en);

                // remove any entities matching sender
                this.objectManager.Entities.RemoveAll(x => x == en);

                // remove any players matching sender
                this.objectManager.Players.RemoveAll(x => x == en);
            }
        }
    }
}
