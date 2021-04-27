using System;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public class AddRewardEventArgs : EventArgs
    {
        private Reward reward;
        public AddRewardEventArgs(Reward reward) { this.reward = reward; }
        public Reward Reward { get { return this.reward; } }
    }
}

