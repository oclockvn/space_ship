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
    class EnemyShip : AnimatedSprite
    {
        public int velocity;
        public Random random = new Random();
        public bool isAlive = true;
        public Rectangle rectangle;

        public EnemyShip(int rows, int columns)
            : base(rows, columns)
        {
            //random = new Random();
            //location = new Vector2(random.Next(853), random.Next(-60, 0));
            //velocity = random.Next(2, 5);
            this.rows = rows;
            this.columns = columns;
            
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Images/enemy_edited");
            this.location = new Vector2(random.Next(20, 830), random.Next(-60, 0));
            this.velocity = random.Next(2, 5);            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            location.Y += velocity;
            rectangle = new Rectangle((int)location.X, (int)location.Y, texture.Width/columns, texture.Height/rows);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Begin();
            base.Draw(spriteBatch);
        }
    }
}
