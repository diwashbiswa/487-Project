using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class AddPlayerEventArgs : EventArgs
    {
        private Player player;
        public AddPlayerEventArgs(Player player) { this.player = player; }
        public Player Player { get { return this.player; } }
    }
}
