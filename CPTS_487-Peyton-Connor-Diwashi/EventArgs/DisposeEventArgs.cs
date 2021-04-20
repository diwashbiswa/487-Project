using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class DisposeEventArgs : EventArgs
    {
        private Sprite sprite;
        public DisposeEventArgs(Sprite sprite) { this.sprite = sprite; }
        public Sprite Sprite { get { return this.sprite; } }
    }
}
