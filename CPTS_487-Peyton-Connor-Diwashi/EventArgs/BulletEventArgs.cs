using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class BulletEventArgs : EventArgs
    {
        private Entity parent;
        private Bullet bullet;
        public BulletEventArgs(Entity parent, Bullet bullet) { this.parent = parent; this.bullet = bullet; }
        public Entity Parent { get { return this.parent; } }
        public Bullet Bullet { get { return this.bullet; } }
    }
}
