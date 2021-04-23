using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class AddSpawnerEventArgs : EventArgs
    {
        private Entity parent;
        private BulletSpawner spawner;
        public AddSpawnerEventArgs(Entity parent, BulletSpawner spawner) { this.parent = parent; this.spawner = spawner; }
        public Entity Parent { get { return this.parent; } }
        public BulletSpawner Spawner { get { return this.spawner; } }
    }
}
