using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System.IO;

namespace xna_final_project.screen_manager
{

    public enum ScreenState
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    public abstract class GameScreen
    {
        private bool isPopup = false;
        private TimeSpan transitionOnTime = TimeSpan.Zero;
        private TimeSpan transitionOffTime = TimeSpan.Zero;
        private float transitionPosition = 1;
        private ScreenState screenState = ScreenState.TransitionOn;
        private bool isExiting = false;
        private bool isActive;
        private bool otherScreenHasFocus;
        private ScreenManager screenManager;
        private PlayerIndex? controllingPlayer;
        GestureType enabledGestures = GestureType.None;
        

        public TimeSpan TransitionOffTime
        {
            get { return transitionOffTime; }
            set { transitionOffTime = value; }
        }

        public GestureType EnabledGestures
        {
            get { return enabledGestures; }
            set 
            { 
                enabledGestures = value;
                if (ScreenState == screen_manager.ScreenState.Active)
                {
                    TouchPanel.EnabledGestures = value;
                }
            }
        }

        public PlayerIndex? ControllingPlayer
        {
            get { return controllingPlayer; }
            set { controllingPlayer = value; }
        }

        public ScreenManager ScreenManager
        {
            get { return screenManager; }
            set { screenManager = value; }
        }

        public bool IsActive
        {
            get 
            {
                return !otherScreenHasFocus && (screenState == ScreenState.TransitionOn || screenState == ScreenState.Active);
            }           
        }

        public bool IsExiting
        {
            get { return isExiting; }
            set { isExiting = value; }
        }

        public ScreenState ScreenState
        {
            get { return screenState; }
            set { screenState = value; }
        }

        public float TransitionAlpha
        {
            get { return 1f - TransitionPosition; }
            
        }        

        public float TransitionPosition
        {
            get { return transitionPosition; }
            set { transitionPosition = value; }
        }

        public TimeSpan TransitionOnTime
        {
            get { return transitionOnTime; }
            set { transitionOnTime = value; }
        }

        public bool IsPopup
        {
            get { return isPopup; }
            set { isPopup = value; }
        }


        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            this.otherScreenHasFocus = otherScreenHasFocus;
            if (isExiting)
            {
                screenState = ScreenState.TransitionOff;
                if (!UpdateTransition(gameTime, transitionOffTime, 1))
                {
                    ScreenManager.RemoveScreen(this);
                }
            }
            else if (coveredByOtherScreen)
            {
                if (UpdateTransition(gameTime, transitionOffTime, 1))
                {
                    screenState = ScreenState.TransitionOff;
                }
                else
                {
                    screenState = ScreenState.Hidden;
                }
            }
            else
            {
                if (UpdateTransition(gameTime, transitionOnTime, -1))
                {
                    screenState = ScreenState.TransitionOn;
                }
                else
                {
                    screenState = ScreenState.Active;
                }
            }
        }

        bool UpdateTransition(GameTime gameTime, TimeSpan time, int direction)
        {
            float transitionDelta;
            if (time == TimeSpan.Zero)
                transitionDelta = 1;
            else
                transitionDelta = (float)(gameTime.ElapsedGameTime.TotalMilliseconds / time.TotalMilliseconds);
            transitionPosition += transitionDelta * direction;
            if (((direction < 0) && (transitionPosition <= 0)) ||
                ((direction > 0) && (transitionPosition >= 1)))
            {
                transitionPosition = MathHelper.Clamp(transitionPosition, 0, 1);
                return false;
            }
            return true;
        }

        public virtual void HandleInput(InputState input) { }

        public virtual void Draw(GameTime gameTime) { }

        public void ExitScreen()
        {
            if (TransitionOffTime == TimeSpan.Zero)
            {
                ScreenManager.RemoveScreen(this);
            }
            else
            {
                isExiting = true;
            }
        }
    }
}
