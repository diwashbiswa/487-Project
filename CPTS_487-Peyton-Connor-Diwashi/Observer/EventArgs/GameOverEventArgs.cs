using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class GameOverEventArgs : EventArgs
    {
        private bool win;

        public GameOverEventArgs(bool win) { this.win = win; }
        
        public bool Win { get { return this.win; } }
    }
}
