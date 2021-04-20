using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class AddGUIEventArgs : EventArgs
    {
        private GUIComponent component;

        private Sprite parent = null;
        public AddGUIEventArgs(GUIComponent component, Sprite parent) { this.component = component; this.parent = parent; }
        public GUIComponent Component { get { return this.component; } }

        public Sprite Parent { get { return this.parent; } }
    }
}
