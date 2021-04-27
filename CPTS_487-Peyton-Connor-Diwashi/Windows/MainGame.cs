using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace CPTS_487_Peyton_Connor_Diwashi
{
    public partial class MainGame : Game
    {
        /// <summary>
        /// To be invoked on exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Exit(object sender, EventArgs e)
        {
            base.Exit();
        }

        protected override void Update(GameTime gameTime)
        {

            // Collide the range of Player Bullets with the range of Entities (entities are non-player)
            this.CollisionList.Clear();
            this.CollisionList.AddRange(EntityManager.PlayerBullets);
            this.CollisionList.AddRange(EntityManager.Entities);
            Collision.Collide(this.CollisionList);

            // Collide the range of Enemy Bullets with the range of Players (probably just 1)
            this.CollisionList.Clear();
            this.CollisionList.AddRange(EntityManager.EnemyBullets);
            this.CollisionList.AddRange(EntityManager.Players);
            Collision.Collide(this.CollisionList);

            // Collide Entities and players
            this.CollisionList.Clear();
            this.CollisionList.AddRange(EntityManager.Entities);
            this.CollisionList.AddRange(EntityManager.Players);
            Collision.Collide(this.CollisionList);


            this.EntityManager.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(this.scaleFactor));

            this.EntityManager.Draw(gameTime, _spriteBatch);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}

/*
 *      TODO:
 * 
 1. ScriptMovement

 2. Make a bunch of int type doubles

 3. Add more custom variable to WaveScriptParser

 4. LoadAllWaves() method. (Count root children) return List<T>

 */
