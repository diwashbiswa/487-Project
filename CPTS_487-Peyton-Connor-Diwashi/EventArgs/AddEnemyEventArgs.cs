using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class AddEnemyEventArgs : EventArgs
    {
        private Entity enemy;
        public AddEnemyEventArgs(Entity enemy) { this.enemy = enemy; }
        public Entity Enemy { get { return this.enemy; } }
    }
}
