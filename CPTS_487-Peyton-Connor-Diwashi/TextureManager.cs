using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// UserInput Singleton class 
    /// </summary>
    public sealed class TextureManager
    {
        private TextureManager() { }

        private static TextureManager instance = null;
        public static TextureManager Textures
        {
            get
            {
                if (instance == null)
                {
                    instance = new TextureManager();
                }
                return instance;
            }
        }

        public enum Type { SpaceshipPlayer, Grunt1, Grunt2, Boss1, Boss2, BulletPurple, BulletGreen, BossBullet, Heart }

        private Dictionary<Type, Texture2D> texDict = new Dictionary<Type, Texture2D>();

        public void Add(Texture2D pixels, Type bind)
        {
            if (texDict.ContainsKey(bind))
            {
                texDict.Remove(bind);
            }
            texDict.Add(bind, pixels);
        }

        public Texture2D Get(Type key)
        {
            if(!texDict.ContainsKey(key))
            {
                throw new Exception("Texture unbound");
            }
            return texDict[key];
        }
    }
}