using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPTS_487_Peyton_Connor_Diwashi
{
    public static class CollisionObserver
    {
        /// <summary>
        /// Detect collisions within a list of sprites. Invoke abstract Sprite.Collide on collision
        /// </summary>
        /// <param name="activeSprites"> List of sprite objects to test for collision </param>
        public static void Collide(List<Sprite> activeSprites)
        {
            List<Sprite> sprites1 = activeSprites;
            List<Sprite> sprites2 = activeSprites;

            foreach (Sprite s1 in sprites1)
            {
                foreach(Sprite s2 in sprites2)
                {
                    if (s1 == s2)
                        continue;

                    
                    if(s1.Body.Intersects(s2.Body))
                    {
                        s1.Collide(s2, new EventArgs());

                    }
                }
            }
        }
    }
}
