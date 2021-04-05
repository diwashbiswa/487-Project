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
            if (timer > 1 && timer < 1.1)
            {
                Entitiy e = ef.CreateEnemy(EnemyFactory.EnemyType.Grunt1);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.CardinalSouth));
            }

            if (timer > 100 && timer < 100.1)
            {
                Entitiy e = ef.CreateEnemy(EnemyFactory.EnemyType.Grunt2);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.CardinalSouth));
            }

            if (frames == 45 * 60)
            {
                Entitiy e = ef.CreateEnemy(EnemyFactory.EnemyType.Boss1);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.Targeted));
            }

            if (frames == 145 * 60)
            {
                Entitiy e = ef.CreateEnemy(EnemyFactory.EnemyType.Boss2);
                this.AddEnemy(e);
                this.sf.Parent = e;
                this.AddSpawner(sf.CreateSpawner(SpawnerFactory.SpawnerType.Targeted));
            }



            // COLLISION ---------------------------
            this.BuildList_PlayerEnemyBullet();
            this.BuildList_EnemiesPlayerBullet();
            CollisionObserver.Collide(collisionList1);
            CollisionObserver.Collide(collisionList2);
            // --------- ---------------------------


            // SPAWNER ----------------------------
            foreach (BulletSpawner s in disposedSpawners)
            {
                if (this.spawners.Contains(s))
                {
                    this.spawners.Remove(s);
                }
            }
            foreach (BulletSpawner s in spawners)
            {
                s.Update(gameTime);
            }
            // ------- ----------------------------




            // ENEMY ----------------------------
            foreach (Entitiy s in disposedEnemies)
            {
                if (this.enemies.Contains(s))
                {
                    this.enemies.Remove(s);
                }
            }
            this.disposedEnemies.Clear();
            this.target.X = this.player.X;
            this.target.Y = this.player.Y;
            foreach (Entitiy s in enemies)
            {
                s.BindToTarget(this.target);
                s.Update(gameTime);
            }
            // ----- ----------------------------



            // PLAYER ----------------------------
            this.player.Update(gameTime);

            this.pos = new Vector2(50, 20);
            this.lives = Content.Load<Texture2D>("heart");
            this.livesPosition = new Rectangle((int)pos.X, (int)pos.Y, 20, 20);

            // ------ ----------------------------

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);

            _spriteBatch.Begin(SpriteSortMode.Immediate, null, null, null, null, null, Matrix.CreateScale(this.scaleFactor));

            // Draw All Enemies
            foreach (Sprite s in enemies)
            {
                s.Draw(gameTime, _spriteBatch);
            }

            // Draw All Bullets
            foreach (BulletSpawner s in spawners)
            {
                s.Draw(gameTime, _spriteBatch);
            }

            // Draw Player
            this.player.Draw(gameTime, _spriteBatch);


            // Displays player lives on the screen for each lives they have
            var incrementX = 30;
            for (int i = 0; i < player.Health; i++)
            {
                var newPos = pos + new Vector2((incrementX * i), 0);
                _spriteBatch.Draw(lives, newPos, Color.Red);
            }
            //_spriteBatch.Draw(lives, livesPosition, Color.Red);

            if (player.Dead)
            {
                //Show game over screen
                GameOverPopUp(_spriteBatch, gameTime);
            }
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

 2. Adding controllers between maingame and classes
    - spawning
    - collision detections
    - movement
 
 3. ScriptMovement : Movement

 4. Make enemy and bullet more similar

 5. Extract keyboard input away from sprites

 6. BulletSpawner spawned directly after Enemy using BulletSpawnerFactory

 7. EntityTracker class managed by MainGame

 8. KeyboardListener intermediate class

 9. Player Observer in Maingame


      BUGS:

      Enemy.Position += movement.Move() does not work with any movement vector 
        containing values less than 1 because of integer casting int the Enemy.Position PROPERTY. 
        Position is in PIXELS therefore we cannot move by .5 of a pixel. 
        For this reason, a movement of 0.5 will cause the enemy to infinitely
        stall. Now, minimum cardinal movement is set to 1 in any direction.

 */
