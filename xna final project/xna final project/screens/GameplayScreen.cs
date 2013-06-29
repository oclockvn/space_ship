using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using xna_final_project.screen_manager;
using xna_final_project.gameplay_objects;

namespace xna_final_project.screens
{
    class GameplayScreen : GameScreen
    {
        ContentManager content;        
        float pauseAlpha;
        List<EnemyShip> enemys;
        int addEnemyTime;
        PlayerShip player;
        public Explosion explosion;
        public bool isExplosive = false;
        SoundEffect enemyBurn;
        SoundEffect playerExplosive;
        Song backgroundSong;
        Background background;

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            enemys = new List<EnemyShip>();
            player = new PlayerShip();
            explosion = new Explosion(4, 8);
            background = new Background();
        }

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            player.LoadContent(content);
            explosion.LoadContent(content, new Vector2(400, 200));
            enemyBurn = content.Load<SoundEffect>("Sounds/enemy_burning");
            playerExplosive = content.Load<SoundEffect>("Sounds/player_explosion");
            background.LoadContent(content);
           
            Thread.Sleep(1000);
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);            
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                explosion.Update(gameTime);
                background.Update(gameTime);
                // update enemy

                foreach (EnemyShip enemy in enemys)
                {
                    if (enemy.isAlive)
                    {
                        if (gameTime.TotalGameTime.Milliseconds % 50 == 0)
                        {
                            enemy.Update(gameTime);
                        }
                    }
                }

                // update player

                if (player.isAlive)
                {
                    player.Update(gameTime);
                }

                // add enemy

                addEnemyTime += gameTime.ElapsedGameTime.Milliseconds;
                if (addEnemyTime > 600)
                {
                    addEnemyTime = 0;
                    EnemyShip enemy = new EnemyShip(8, 8);
                    enemy.LoadContent(content);
                    enemys.Add(enemy);
                }

                // intersect

                for (int i = 0; i < enemys.Count; i++)
                {
                    if (enemys[i].isAlive)
                    {
                        if (enemys[i].rectangle.Intersects(player.rectangle))
                        {
                            playerExplosive.Play();
                            isExplosive = true;
                            explosion = new Explosion(4, 8);
                            explosion.LoadContent(content, new Vector2(player.location.X - player.textures[0].Width / 2, player.location.Y - player.textures[0].Height / 2));
                            {
                                explosion.Update(gameTime);
                            } 
                            
                            enemys[i].isAlive = false;
                            // live--
                            // sound play
                        }

                        foreach (Bullet bullet in player.bullets)
                        {
                            if (bullet.rectangle.Intersects(enemys[i].rectangle))
                            {
                                enemyBurn.Play();
                                isExplosive = true;                                
                                explosion = new Explosion(4, 8);
                                explosion.LoadContent(content, new Vector2(enemys[i].location.X - enemys[i].texture.Width / 8, enemys[i].location.Y));
                                {
                                    explosion.Update(gameTime);
                                } 
                                // point ++
                                bullet.isAlive = false;
                                enemys[i].isAlive = false;
                                // sound play
                            }
                        }
                    }
                }
                //const float randomization = 10;

                //enemyPosition.X += (float)(random.NextDouble() - 0.5) * randomization;
                //enemyPosition.Y += (float)(random.NextDouble() - 0.5) * randomization;
               
                //Vector2 targetPosition = new Vector2(
                //    ScreenManager.GraphicsDevice.Viewport.Width / 2 - gameFont.MeasureString("Insert Gameplay Here").X / 2,
                //    200);

                //enemyPosition = Vector2.Lerp(enemyPosition, targetPosition, 0.05f);

            }
        }

        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");
            
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
               
                //Vector2 movement = Vector2.Zero;

                //if (keyboardState.IsKeyDown(Keys.Left))
                //    movement.X--;

                //if (keyboardState.IsKeyDown(Keys.Right))
                //    movement.X++;

                //if (keyboardState.IsKeyDown(Keys.Up))
                //    movement.Y--;

                //if (keyboardState.IsKeyDown(Keys.Down))
                //    movement.Y++;

                //Vector2 thumbstick = gamePadState.ThumbSticks.Left;

                //movement.X += thumbstick.X;
                //movement.Y -= thumbstick.Y;

                //if (movement.Length() > 1)
                //    movement.Normalize();

                //playerPosition += movement * 2;
            }
        }

        

        public override void Draw(GameTime gameTime)
        {           
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
            
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;            

            spriteBatch.Begin();
            background.Draw(spriteBatch);
            if (isExplosive)
            {
                if (!explosion.stoped)
                    explosion.Draw(spriteBatch);
            }
            

            if (player.isAlive)
            {
                player.Draw(spriteBatch);
            }

            foreach (EnemyShip enemy in enemys)
            {
                if (enemy.isAlive)
                {
                    enemy.Draw(spriteBatch);
                }
            }
            //enemy.Draw(spriteBatch);

            //spriteBatch.DrawString(gameFont, "// TODO", playerPosition, Color.Green);

            //spriteBatch.DrawString(gameFont, "Insert Gameplay Here",
            //                       enemyPosition, Color.DarkRed);

            spriteBatch.End();

            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }
    }
}
