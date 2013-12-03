using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using xna_final_project.screen_manager;
using xna_final_project.screens;
using xna_final_project.gameplay_objects;

namespace xna_final_project
{
    public class SpaceShip : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ScreenManager screenManager;
        

        public SpaceShip()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 960;
            graphics.PreferredBackBufferHeight = 630;
            this.IsMouseVisible = true;
            graphics.ApplyChanges();
            screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        
        }

        static readonly string[] preloadAssets =
        {
            "Images/gradient",
        };
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            foreach (string asset in preloadAssets)
            {
                Content.Load<object>(asset);
            }
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            graphics.GraphicsDevice.Clear(Color.Black);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }        
    }
}
