using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class EntityCollideEventArgs : EventArgs
    {
        private Entity victim;
        private Sprite attacker;
        public EntityCollideEventArgs(Entity victim, Sprite attacker) { this.victim = victim; this.attacker = attacker; }
        public Entity Victim { get { return this.victim; } }
        public Sprite Attacker { get { return this.attacker; } }
    }
}
