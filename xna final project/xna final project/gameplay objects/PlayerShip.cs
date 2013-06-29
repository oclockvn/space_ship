using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using xna_final_project.screen_manager;
using Microsoft.Xna.Framework.Audio;

namespace xna_final_project.gameplay_objects
{
    enum ShipState
    { 
        Balance,
        Left,
        Right,
        Up,
        Down
    }

    class PlayerShip
    {
        public List<Texture2D> textures = new List<Texture2D>();
        public Vector2 location;
        public KeyboardState keyState;
        public ContentManager content;
        public int velocity = 3;
        public int bulletDelayTime;
        public bool isAlive = true;
        public Rectangle rectangle;
        public List<Bullet> bullets = new List<Bullet>();
        public ShipState shipState;
        //public int currentTextureIndex = 0;
        SoundEffect shootSound;

        public PlayerShip()
        {  

        }

        public void LoadContent(ContentManager content)
        {
            Texture2D shipBalance = content.Load<Texture2D>("Images/ship_balance");
            Texture2D shipMoveLeft = content.Load<Texture2D>("Images/ship_move_left");
            Texture2D shipMoveRight = content.Load<Texture2D>("Images/ship_move_right");
            Texture2D shipMoveFoward = content.Load<Texture2D>("Images/ship_move_foward");
            this.textures.Add(shipBalance);
            this.textures.Add(shipMoveLeft);
            this.textures.Add(shipMoveRight);
            this.textures.Add(shipMoveFoward);

            shootSound = content.Load<SoundEffect>("Sounds/ship_shooting");

            this.location = new Vector2(480, 600);
            this.content = content;
        }

        public void Shoot(GameTime gameTime)
        {
            Bullet bullet = new Bullet();
            bullet.LoadContent(content);
            bullet.location = this.location + new Vector2(25, 0);
            bullets.Add(bullet);
        }

        public void Update(GameTime gameTime)
        {
            keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.Up) && this.location.Y > 0)
            {
                this.location.Y -= velocity;
                shipState = ShipState.Up;
                //currentTextureIndex = 3;
            }
            if (keyState.IsKeyDown(Keys.Down) && this.location.Y < 600)
            {
                this.location.Y += velocity;
                shipState = ShipState.Down;
                //currentTextureIndex = 4;
            }
            if (keyState.IsKeyDown(Keys.Left) && this.location.X > 0)
            {
                this.location.X -= velocity;
                shipState = ShipState.Left;
                //currentTextureIndex = 1;
            }
            if (keyState.IsKeyDown(Keys.Right) && this.location.X < 930)
            {
                this.location.X += velocity;
                shipState = ShipState.Right;
                //currentTextureIndex = 2;
            }

            if (keyState.GetPressedKeys().Length == 0)
            {
                shipState = ShipState.Balance;
                //currentTextureIndex = 0;
            }

            foreach (Bullet bullet in bullets)
	        {
                bullet.Update(gameTime);
	        }

            if (keyState.IsKeyDown(Keys.Space))
            {
                bulletDelayTime += gameTime.ElapsedGameTime.Milliseconds;
                if (bulletDelayTime > 100)
                {
                    bulletDelayTime = 0;
                    Shoot(gameTime);
                    shootSound.Play();
                }
            }

            this.rectangle = new Rectangle((int)location.X, (int)location.Y,
                this.textures[0].Width, this.textures[0].Height);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (shipState == ShipState.Balance)
            {
                spriteBatch.Draw(this.textures[0], this.location, Color.White);
            }
            else if (shipState == ShipState.Left)
            {
                spriteBatch.Draw(this.textures[1], this.location, Color.White);
            }
            else if (shipState == ShipState.Right)
            {
                spriteBatch.Draw(this.textures[2], this.location, Color.White);
            }
            else if (shipState == ShipState.Up)
            {
                spriteBatch.Draw(this.textures[3], this.location, Color.White);
            }
            else if (shipState == ShipState.Down)
            {
                spriteBatch.Draw(this.textures[3], this.location, Color.White);
            }
            //spriteBatch.Draw(this.textures[0], this.location, Color.White);

            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
                //if (bullet.isAlive)
                //{
                    
                //}
                
            }
        }
    }
}
