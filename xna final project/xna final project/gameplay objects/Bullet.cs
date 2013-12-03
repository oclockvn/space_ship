using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xna_final_project.screens;

namespace xna_final_project.gameplay_objects
{
    class Bullet
    {
        public Texture2D texture;
        public Vector2 location;
        public int velocity = 5;
        public bool isAlive = true;
        public Rectangle rectangle;
        public int kind = 0;

        public Bullet()
        { }

        public void LoadContent(ContentManager content)
        {
            this.texture = content.Load<Texture2D>("Images/bullet_fired");
        }

        public void Update(GameTime gameTime)
        {
            if (kind == 0 || kind == 1 || kind == 2)
            {
                this.location.Y -= velocity;
            }
            
            if (kind == 3)
            {
                this.location.Y -= velocity;
                this.location.X -= velocity;
            }
            if (kind == 4)
            {
                this.location.Y -= velocity;
                this.location.X += velocity;
            }
            if (kind==5)
            {
                this.location.Y += velocity;
            }
            //if (GameplayScreen.powerUpLevel == 2)
            //{
            //    this.location.X += velocity;
            //}
            
            rectangle = new Rectangle((int)location.X, (int)location.Y, texture.Width, texture.Height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location, Color.White);
        }
    }
}
