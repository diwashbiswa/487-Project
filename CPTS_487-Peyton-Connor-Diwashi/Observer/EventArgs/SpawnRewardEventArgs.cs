using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class SpawnRewardEventArgs : EventArgs
    {
        private Reward reward;

        private Vector2 newPos;

        public SpawnRewardEventArgs(Reward reward, Vector2 newPos) { this.reward = reward; this.newPos = newPos; }
        public Reward Reward { get { return this.reward; } }
        public Vector2 NewPosition { get { return this.newPos; } }
    }
}
