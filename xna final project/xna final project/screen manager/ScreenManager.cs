﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Diagnostics;

namespace xna_final_project.screen_manager
{
    public class ScreenManager : DrawableGameComponent
    {
        
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>();
        InputState input = new InputState();
        SpriteBatch spriteBatch;
        SpriteFont font;
        Texture2D blankTexture;
        bool isInitialized;
        bool traceEnabled;

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }

        public SpriteFont Font
        {
            get { return font; }
        }

        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }
            
        public ScreenManager(Game game)
            : base(game)
        {
            TouchPanel.EnabledGestures = GestureType.None;
        }

        public override void Initialize()
        {
            base.Initialize();

            isInitialized = true;
        }

        protected override void LoadContent()
        {
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Fonts/menufont");
            blankTexture = content.Load<Texture2D>("Images/blank");
            
            foreach (GameScreen screen in screens)
            {
                screen.LoadContent();
            }
        }

        protected override void UnloadContent()
        {        
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }

        public override void Update(GameTime gameTime)
        {           
            input.Update();
            screensToUpdate.Clear();
            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;
           
            while (screensToUpdate.Count > 0)
            {               
                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);
               
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }
           
            if (traceEnabled)
                TraceScreens();
        }

        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            Debug.WriteLine(string.Join(", ", screenNames.ToArray()));
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }

        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;
            
            if (isInitialized)
            {
                screen.LoadContent();
            }

            screens.Add(screen);

            // update the TouchPanel to respond to gestures this screen is interested in
            TouchPanel.EnabledGestures = screen.EnabledGestures;
        }

        public void RemoveScreen(GameScreen screen)
        {           
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);

            if (screens.Count > 0)
            {
                TouchPanel.EnabledGestures = screens[screens.Count - 1].EnabledGestures;
            }
        }

        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        public void FadeBackBufferToBlack(float alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             Color.Black * alpha);

            spriteBatch.End();
        }
    }
}