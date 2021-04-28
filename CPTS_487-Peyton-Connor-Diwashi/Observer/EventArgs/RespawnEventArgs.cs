using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class RespawnEventArgs : EventArgs
    {
        private Entity entity;

        private Vector2 newPos;

        public RespawnEventArgs(Entity entity, Vector2 newPos) { this.entity = entity; this.newPos = newPos; }
        public Entity Entity { get { return this.entity; } }
        public Vector2 NewPosition { get { return this.newPos; } }
    }
}
