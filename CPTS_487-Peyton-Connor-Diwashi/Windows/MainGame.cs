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


            // MAKE PART OF CLASS {
            if (this.EntityManager.PlayerOne.Dead)
            {
                Exit();
                //Show game over screen
                GameOverPopUp(_spriteBatch, gameTime);
            }
            // }


            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public void GameOverPopUp(SpriteBatch spriteBatch, GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // Game Over text
            spriteBatch.DrawString(Content.Load<SpriteFont>("Font"), "Game Over", new Vector2(Window.ClientBounds.Width/2, Window.ClientBounds.Height/2 - 100), Color.DarkRed);

            // Exit button
            GUIButton button_exit = new GUIButton(new Vector2(Window.ClientBounds.Width / 2 - 300, Window.ClientBounds.Height/2 + 100), Content.Load<Texture2D>("Button1"), Content.Load<SpriteFont>("Font"), Color.Black, "Exit");

            // Play again button
            GUIButton button_playgame = new GUIButton(new Vector2(Window.ClientBounds.Width / 2 + 100, Window.ClientBounds.Height / 2 + 100), Content.Load<Texture2D>("Button1"), Content.Load<SpriteFont>("Font"), Color.Black, "Start");

            gameOverButtons.Add(button_exit);
            gameOverButtons.Add(button_playgame);

            // Draw all exit and play buttons
            foreach (GUIComponent gc in gameOverButtons)
            {
                gc.Draw(gameTime, _spriteBatch);
            }
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
