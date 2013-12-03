using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace xna_final_project.gameplay_objects
{
    class FinalBoss : AnimatedSprite
    {
        public int velocity;
        public Random random = new Random();
        public bool isAlive = true;
        public Rectangle rectangle;
        public int healthPoint = 1000;
        public int move = 3;
        public List<Bullet> bullets = new List<Bullet>();
        public ContentManager content;
        public int bulletDelayTime;
        public KeyboardState keyState;

        public FinalBoss(int rows, int columns)
            : base(rows, columns)
        {
            this.rows = rows;
            this.columns = columns;
            
        }

        public void Shoot()
        {
            Bullet bullet = new Bullet();
            bullet.kind = 5;
            bullet.LoadContent(content);
            bullet.location = this.location + new Vector2(texture.Width/4, texture.Height/14);
            bullets.Add(bullet);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Images/boss");            
            this.location = new Vector2(random.Next(20, 830), random.Next(-60, 0));
            this.velocity = random.Next(2, 5);
            this.content = content;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            keyState = Keyboard.GetState();
            location.X += move;
            if (location.X > 800)
            {
                move *= -1;
            }
            else if (location.X < 100)
            {
                move = Math.Abs(move);
            }

            foreach (Bullet bullet in bullets)
            {
                bullet.Update(gameTime);
            }


            //if (gameTime.TotalGameTime.Milliseconds % 50 == 0)
            {
                bulletDelayTime += gameTime.ElapsedGameTime.Milliseconds;
                if (bulletDelayTime > 500)
                {
                    bulletDelayTime = 0;
                    Shoot();                    
                }
            }
            //location.Y += velocity;
            rectangle = new Rectangle((int)location.X, (int)location.Y, texture.Width/columns, texture.Height/rows);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();


            base.Draw(spriteBatch);

            foreach (Bullet bullet in bullets)
            {

                if (bullet.isAlive)
                {
                    bullet.Draw(spriteBatch);
                }

            }
        }
    }
}
