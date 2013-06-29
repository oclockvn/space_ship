using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace xna_final_project.gameplay_objects
{
    public class Background
    {
        #region Properties
        private Texture2D backgroundBack;
        private Texture2D backgroundFront01;
        public Vector2 positionBack;
        public Vector2 positionFront01;
        private Texture2D backgroundFront02;
        private Vector2 positionFront02;

        public Vector2 PositionFront02
        {
            get { return positionFront02; }
            set { positionFront02 = value; }
        }

        public Texture2D BackgroundFront02
        {
            get { return backgroundFront02; }
            set { backgroundFront02 = value; }
        }

        public Vector2 PositionFront
        {
            get { return positionFront01; }
            set { positionFront01 = value; }
        }

        public Vector2 PositionBack
        {
            get { return positionBack; }
            set { positionBack = value; }
        }

        public Texture2D BackgroundFront
        {
            get { return backgroundFront01; }
            set { backgroundFront01 = value; }
        }

        public Texture2D BackgroundBack
        {
            get { return backgroundBack; }
            set { backgroundBack = value; }
        }
        #endregion

        #region Methods
        public Background()
        {
            positionBack = Vector2.Zero;
            positionFront01 = Vector2.Zero;
            positionFront02 = new Vector2(0, -480);
        }

        public void LoadContent(ContentManager content)
        {
            backgroundBack = content.Load<Texture2D>("Images/background004");
            backgroundFront01 = content.Load<Texture2D>("Images/background005");
            backgroundFront02 = content.Load<Texture2D>("Images/background005");

        }

        public void Update(GameTime gameTime)
        {
            positionFront01.Y++;
            if (positionFront01.Y >= 720)
                positionFront01.Y = -720;
            positionFront02.Y++;
            if (positionFront02.Y >= 720)
                positionFront02.Y = -720;
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(backgroundBack, positionBack, Color.White);
            spriteBatch.Draw(backgroundFront01, positionFront01, Color.White);
            spriteBatch.Draw(backgroundFront02, positionFront02, Color.White);
        }
        #endregion
    }
}
