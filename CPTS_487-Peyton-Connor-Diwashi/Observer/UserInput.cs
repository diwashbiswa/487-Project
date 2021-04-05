using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// Class for carinal movment in Movement.CardinalDirection
    /// </summary>
    public sealed class UserInput
    {
        private UserInput() 
        {
            keyDict.Add(KeyBinds.Up, Keys.W);
            keyDict.Add(KeyBinds.Down, Keys.S);
            keyDict.Add(KeyBinds.Left, Keys.A);
            keyDict.Add(KeyBinds.Right, Keys.D);
            keyDict.Add(KeyBinds.Fire, Keys.Space);
            keyDict.Add(KeyBinds.SlowMode, Keys.LeftShift);
        }

        private static UserInput instance = null;

        private KeyboardState keyState;

        public static UserInput Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserInput();
                }
                return instance;
            }
        }

        public enum KeyBinds {Up, Down, Left, Right, Fire, SlowMode }

        private Dictionary<KeyBinds, Keys> keyDict = new Dictionary<KeyBinds, Keys>();

        public bool IsKeyDown(KeyBinds bind)
        {
            if (!(keyDict.ContainsKey(bind)))
            {
                return false;
            }
            getState();
            Keys k = keyDict[bind];
            if (keyState.IsKeyDown(k))
                return true;
            return false;
        }

        private void getState()
        {
            keyState = Keyboard.GetState();
        }
    }
}