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
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            frames++;

            //Add Enemy
            if (timer > 1 && timer < 1.03)
            {
                this.EntityManager.EnqueueEntitiy(EntitiyFactory.EntitiyType.Grunt1, SpawnerFactory.SpawnerType.CardinalSouth);
                this.EntityManager.EnqueueEntitiy(EntitiyFactory.EntitiyType.Grunt2, SpawnerFactory.SpawnerType.CardinalSouth);
            }

            if (frames == 120)
            {
                this.EntityManager.EnqueueEntitiy(EntitiyFactory.EntitiyType.Boss1, SpawnerFactory.SpawnerType.Targeted);
                this.EntityManager.EnqueueEntitiy(EntitiyFactory.EntitiyType.Boss2, SpawnerFactory.SpawnerType.Targeted);
            }


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
 1. JSON script draft
 
 3. ScriptMovement : Movement

 7. Bring More variables into maingame player creation to get ready for level scripts

 */
