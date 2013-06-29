using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace xna_final_project.gameplay_objects
{
    public class AnimatedSprite
    {
        public Texture2D texture;        
        protected int currentFrame;
        protected int totalFrames;
        public Vector2 location;
        public int rows;
        public int columns;

        public AnimatedSprite(int rows, int columns)
        {
            //this.texture = texture;
            //Location = location;
            this.rows = rows;
            this.columns = columns;
            
            currentFrame = 0;
            totalFrames = rows * columns;
        }

        public virtual void Update(GameTime gameTime)
        {
            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            int width = this.texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            //spriteBatch.End();
        }

        public virtual void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = this.texture.Width / columns;
            int height = texture.Height / rows;
            int row = (int)((float)currentFrame / (float)columns);
            int column = currentFrame % columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Begin();
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}
