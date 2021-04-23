using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class WaveFinishedEventArgs : EventArgs
    {
        private SpriteWave wave;
        private int waveID;

        public WaveFinishedEventArgs(SpriteWave wave, int waveID) { this.wave = wave; this.waveID = waveID; }

        public SpriteWave Wave { get { return this.wave; } }
        public int WaveID { get { return this.waveID; } }
    }
}
