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
        public ContentManager content;        
        public float pauseAlpha;
        public List<EnemyShip> enemys;
        public int addEnemyTime;
        public PlayerShip player;
        public Explosion explosion;
        public PowerUp powerUp;
        public bool isPowerUp = false;
        public bool isExplosive = false;
        public static int powerUpLevel = 0;
        public SoundEffect enemyBurn;
        public SoundEffect playerExplosive;        
        public Background background;
        public Texture2D gameScreen;
        public SpriteFont playerinfo;
        public int playerLife = 3;        
        public int level = 1;
        public int score = 0;
        public Random randomPowerUp = new Random();
        public Random randomSpeed = new Random();
        public int powerUpTime = -1;
        public int timePowerUpAppear = -1;
        public int timeSpeedAppear = -1;
        public bool isSpeedAppread = false;
        public Rectangle speedRectangle;
        public Vector2 speedLocation;
        public Texture2D speedIncrease;
        // final boss
        public FinalBoss finalBoss;
        

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            enemys = new List<EnemyShip>();
            player = new PlayerShip();
            explosion = new Explosion(4, 8);
            background = new Background();
            powerUp = new PowerUp(1, 23);
            finalBoss = new FinalBoss(7, 2);
        }

        public override void LoadContent()
        {
            if (content == null)
            {
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            }
            player.LoadContent(content);            
            enemyBurn = content.Load<SoundEffect>("Sounds/enemy_burning");
            playerExplosive = content.Load<SoundEffect>("Sounds/player_explosion");
            gameScreen = content.Load<Texture2D>("Images/game_screen");
            playerinfo = content.Load<SpriteFont>("Fonts/playerinfo");
            speedIncrease = content.Load<Texture2D>("Images/speed");
            background.LoadContent(content);
            finalBoss.LoadContent(content);
            Thread.Sleep(1000);
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void UnloadContent()
        {
            content.Unload();
        }

        public void LevelUp()
        {
            if (score >= 150 && score <= 155)
            {
                level = 2;
            }
            if (score >= 300 && score <= 305)
            {
                level = 3;
            }
            if (score >= 500 && score <= 505)
            {
                level = 4;
            }
            if (score >= 750 && score <= 755)
            {
                level = 5;
            }
            if (score >= 999)
            {
                level = 6;
            }
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
                timePowerUpAppear = gameTime.TotalGameTime.Seconds % 60;                
                timeSpeedAppear = gameTime.TotalGameTime.Seconds % 60;
                // lose game
                if (playerLife < 0)
                {
                    ScreenManager.AddScreen(new BackgroundScreen(), null);
                    ScreenManager.AddScreen(new ExitScreen(), null);
                }
                // won game
                if (score >= 999)
                {

                    // fight final boss

                    //finalBoss = new FinalBoss(7, 2);
                    //finalBoss.LoadContent(content);
                    FightingFinalBoss(gameTime);



                }
                else

                    UpdateOther(gameTime);

                #region all 

                /*
                LevelUp();

                if (timeSpeedAppear == 0)
                {
                    if (!isSpeedAppread)
                    {
                        isSpeedAppread = true;
                        speedLocation = new Vector2(randomSpeed.Next(200, 800), randomSpeed.Next(200, 400));
                        speedRectangle = new Rectangle((int)speedLocation.X, (int)speedLocation.Y, speedIncrease.Width, speedIncrease.Height);
                    }

                }

                if (isSpeedAppread)
                {
                    if (speedRectangle.Intersects(player.rectangle))
                    {
                        if (player.velocity < 10)
                        {
                            player.velocity += 2;
                        }
                        isSpeedAppread = false;
                    }
                }

                if (timePowerUpAppear == 0)
                {
                    if (!isPowerUp)
                    {

                        isPowerUp = true;
                        powerUp = new PowerUp(1, 23);
                        powerUp.LoadContent(content, new Vector2(randomPowerUp.Next(50, 850), randomPowerUp.Next(50, 550)));

                    }
                }

                PowerUpPlayerWeapon();

                if (isPowerUp)
                {
                    powerUp.Update(gameTime);
                }

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


                if (player.isAlive)
                {
                    player.Update(gameTime);
                }

                // add enemy

                addEnemyTime += gameTime.ElapsedGameTime.Milliseconds;
                addEnemy();

                // intersect
                CheckIntersects(gameTime);
                */
                #endregion
            }
        }

        private void UpdateOther(GameTime gameTime)
        {

            LevelUp();

            if (timeSpeedAppear == 0)
            {
                if (!isSpeedAppread)
                {
                    isSpeedAppread = true;
                    speedLocation = new Vector2(randomSpeed.Next(200, 800), randomSpeed.Next(200, 400));
                    speedRectangle = new Rectangle((int)speedLocation.X, (int)speedLocation.Y, speedIncrease.Width, speedIncrease.Height);
                }

            }

            if (isSpeedAppread)
            {
                if (speedRectangle.Intersects(player.rectangle))
                {
                    if (player.velocity < 10)
                    {
                        player.velocity += 2;
                    }
                    isSpeedAppread = false;
                }
            }

            if (timePowerUpAppear == 0)
            {
                if (!isPowerUp)
                {

                    isPowerUp = true;
                    powerUp = new PowerUp(1, 23);
                    powerUp.LoadContent(content, new Vector2(randomPowerUp.Next(50, 850), randomPowerUp.Next(50, 550)));

                }
            }

            PowerUpPlayerWeapon();

            if (isPowerUp)
            {
                powerUp.Update(gameTime);
            }

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


            if (player.isAlive)
            {
                player.Update(gameTime);
            }

            // add enemy

            addEnemyTime += gameTime.ElapsedGameTime.Milliseconds;
            addEnemy();

            // intersect
            CheckIntersects(gameTime);
        }

        private void FightingFinalBoss(GameTime gameTime)
        {
            if (finalBoss.healthPoint > 0)
            {
                UpdateOther(gameTime);

                if (finalBoss.isAlive)
                {
                    if (gameTime.TotalGameTime.Milliseconds % 100 == 0)
                    {
                        finalBoss.Update(gameTime);
                    }
                    
                    foreach (Bullet bullet in player.bullets)
                    {
                        if (bullet.isAlive)
                        {
                            if (bullet.rectangle.Intersects(finalBoss.rectangle))
                            {
                                isExplosive = true;
                                playerExplosive.Play();
                                explosion = new Explosion(4, 8);
                                explosion.LoadContent(content, new Vector2(finalBoss.location.X + finalBoss.texture.Width / 8, finalBoss.location.Y + finalBoss.texture.Height / 28));
                                {
                                    explosion.Update(gameTime);
                                }
                                bullet.isAlive = false;
                                finalBoss.healthPoint--;
                            }

                            foreach (Bullet ebullet in finalBoss.bullets)
                            {
                                if (ebullet.isAlive)
                                {
                                    if (ebullet.rectangle.Intersects(bullet.rectangle))
                                    {
                                        ebullet.isAlive = false;
                                        bullet.isAlive = false;
                                    }

                                    if (ebullet.rectangle.Intersects(player.rectangle))
                                    {
                                        ebullet.isAlive = false;
                                        playerLife--;

                                        playerExplosive.Play();
                                        isExplosive = true;
                                        explosion = new Explosion(4, 8);
                                        explosion.LoadContent(content, new Vector2(player.location.X - player.textures[0].Width / 2, player.location.Y - player.textures[0].Height / 2));
                                        {
                                            explosion.Update(gameTime);
                                        }

                                        if (powerUpLevel > 0)
                                        {
                                            powerUpLevel--;
                                        }
                                        if (player.velocity > 3)
                                        {
                                            player.velocity -= 2;
                                        }
                                    }
                                }
                            }
                           
                        }
                    }
                }


            }
            else
            {
                ScreenManager.AddScreen(new BackgroundScreen(), null);
                ScreenManager.AddScreen(new WonGameScreen(), null);
            }
        }

        private void CheckIntersects(GameTime gameTime)
        {
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
                        playerLife--;
                        if (powerUpLevel > 0)
                        {
                            powerUpLevel--;
                        }
                        if (player.velocity > 3)
                        {
                            player.velocity -= 2;
                        }

                        // sound play
                    }

                    foreach (Bullet bullet in player.bullets)
                    {
                        if (bullet.isAlive)
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
                                score++;
                                bullet.isAlive = false;
                                enemys[i].isAlive = false;
                                // sound play
                            }
                        }
                    }
                }
            }
        }

        private void PowerUpPlayerWeapon()
        {

            if (isPowerUp)
            {
                if (powerUp.rectangle.Intersects(player.rectangle))
                {
                    isPowerUp = false;
                    if (powerUpLevel < 4)
                    {
                        powerUpLevel++;
                    }
                }

            }
        }

        public void addEnemy()
        {
            if (level == 1)
            {
                if (addEnemyTime > 1000)
                {
                    addEnemyTime = 0;
                    EnemyShip enemy = new EnemyShip(8, 8);
                    enemy.LoadContent(content);
                    enemys.Add(enemy);
                }
            }
            if (level == 2)
            {
                if (addEnemyTime > 750)
                {
                    addEnemyTime = 0;
                    EnemyShip enemy = new EnemyShip(8, 8);
                    enemy.LoadContent(content);
                    enemys.Add(enemy);
                }
            }
            if (level == 3)
            {
                if (addEnemyTime > 500)
                {
                    addEnemyTime = 0;
                    EnemyShip enemy = new EnemyShip(8, 8);
                    enemy.LoadContent(content);
                    enemys.Add(enemy);
                }
            }
            if (level == 4)
            {
                if (addEnemyTime > 300)
                {
                    addEnemyTime = 0;
                    EnemyShip enemy = new EnemyShip(8, 8);
                    enemy.LoadContent(content);
                    enemys.Add(enemy);
                }
            }
            if (level == 5)
            {
                if (addEnemyTime > 155)
                {
                    addEnemyTime = 0;
                    EnemyShip enemy = new EnemyShip(8, 8);
                    enemy.LoadContent(content);
                    enemys.Add(enemy);
                }
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
               
            }
        }

        public void DrawString(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(playerinfo, powerUpLevel.ToString(), new Vector2(160, 595), Color.White);
            spriteBatch.DrawString(playerinfo, "New power up in: " + (60 - timePowerUpAppear), new Vector2(350, 585), Color.White);
            spriteBatch.DrawString(playerinfo, level.ToString(), new Vector2(800, 585), Color.White);
            spriteBatch.DrawString(playerinfo, playerLife.ToString(), new Vector2(70, 595), Color.White);
            spriteBatch.DrawString(playerinfo, score.ToString(), new Vector2(800, 605), Color.White);
            if (score >= 999)
            {
                spriteBatch.DrawString(playerinfo, "Boss health point: " + finalBoss.healthPoint.ToString(), new Vector2(350, 605), Color.White);
            }
            
        }

        public override void Draw(GameTime gameTime)
        {           
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.CornflowerBlue, 0, 0);
            
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;            

            spriteBatch.Begin();

            
            background.Draw(spriteBatch);
            // final boss
            if (score >= 999)
            {
                finalBoss.Draw(spriteBatch);
                DrawOther(spriteBatch);
            }
            else
                DrawOther(spriteBatch);

            spriteBatch.End();

            if (TransitionPosition > 0 || pauseAlpha > 0)
            {
                float alpha = MathHelper.Lerp(1f - TransitionAlpha, 1f, pauseAlpha / 2);

                ScreenManager.FadeBackBufferToBlack(alpha);
            }
        }

        private void DrawOther(SpriteBatch spriteBatch)
        {
            if (isExplosive)
            {
                if (!explosion.stoped)
                    explosion.Draw(spriteBatch);
            }

            if (isPowerUp)
            {
                {
                    powerUp.Draw(spriteBatch);
                }

            }

            if (isSpeedAppread)
            {
                spriteBatch.Draw(speedIncrease, speedLocation, Color.White);
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

            spriteBatch.Draw(gameScreen, new Vector2(0, 112), Color.White);

            DrawString(spriteBatch);

        }
    }
}
