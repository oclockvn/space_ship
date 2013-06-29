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
    class Explosion : AnimatedSprite
    {
        public bool stoped = false;
        public Explosion(int rows, int columns)
            : base(rows, columns)
        {

            //this.rows = rows;
            //this.columns = columns;
        }

        public void LoadContent(ContentManager content, Vector2 location)
        {
            this.texture = content.Load<Texture2D>("Images/explosion");
            this.location = location;
        }

        public override void Update(GameTime gameTime)
        {
            // base.Update(gameTime);

            currentFrame++;
            if (currentFrame == totalFrames)
            {
                currentFrame = 0;
                stoped = true;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
