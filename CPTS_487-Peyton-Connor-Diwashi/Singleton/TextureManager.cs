using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    /// <summary>
    /// TextureManager Singleton class 
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

        public enum Type { SpaceshipPlayer, Grunt1, Grunt2, Boss1, Boss2, BulletPurple, BulletGreen, BossBullet, Heart, Button1, HB100, HB75, HB50, HB25 }
        public enum Font { Font1 }

        private Dictionary<Type, Texture2D> texDict = new Dictionary<Type, Texture2D>();

        private Dictionary<Font, SpriteFont> fontDict = new Dictionary<Font, SpriteFont>();

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

        public void AddFont(SpriteFont pixels, Font bind)
        {
            if (fontDict.ContainsKey(bind))
            {
                fontDict.Remove(bind);
            }
            fontDict.Add(bind, pixels);
        }

        public SpriteFont GetFont(Font key)
        {
            if (!fontDict.ContainsKey(key))
            {
                throw new Exception("Texture unbound");
            }
            return fontDict[key];
        }
    }
}