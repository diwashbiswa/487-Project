using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// UserInput Singleton class 
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

        /// <summary>
        /// Is the Key associated with KeyBind down currently being presses?
        /// </summary>
        /// <param name="bind"> A UserInput.Keybinds enum </param>
        /// <returns></returns>
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

        /// <summary>
        /// Associate a KeyBind with a KEy
        /// </summary>
        /// <param name="bind"></param>
        /// <param name="key"></param>
        public void Bind(KeyBinds bind, Keys key)
        {
            if (keyDict.ContainsKey(bind))
            {
                keyDict.Remove(bind);
            }
            keyDict.Add(bind, key);
        }

        private void getState()
        {
            keyState = Keyboard.GetState();
        }
    }
}