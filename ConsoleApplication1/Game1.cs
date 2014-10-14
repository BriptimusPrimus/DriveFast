using System;
using Game2DKit;
using Game2DKit.Input;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace MyGameStuff
{
    class Game1 : Game
    {
        SpriteBatch spriteBatch;

        private Random mRandom = new Random();

        private enum State
        {
            Running,
            Crash
        }

        private State mCurrentState;

        private Texture2D mRoad;
        private float mVelocityY;
        private int[] mRoadY = new int[3];

        private Texture2D mCar;
        private Rectangle mCarPosition;

        private KeyboardState mPreviousKeyboardState;
        private int mMoveCarX;

        private Texture2D mHazard;
        private Rectangle mHazardPosition;

        private int mHazardsPassed;
        Font mFont;

        RectangleF TextLocation;
        string TextToWrite;
        

        public Game1()
            : base()
        {
            Initialize();            
            this.Content.RootDirectory = "images";
        }

        protected void Initialize()
        {
            // TODO: Add your initialization logic here                                 
            //StartGame();
        }

        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);

            GL.ClearColor(Color.SaddleBrown);

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch();

            // TODO: use this.Content to load your game content here            
            mRoad = Content.LoadTexture("Road.png");

            mCar = Content.LoadTexture("Car.png");

            mHazard = Content.LoadTexture("Hazard.png");

            //mFont = new Font(FontFamily.GenericSansSerif, 16.0f);
            mFont = new Font(FontFamily.GenericSansSerif, 16.0f);

            StartGame();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // TODO: Add your update logic here
            KeyboardState aCurrentKeyboardState = Game2DKit.Input.Keyboard.GetState();

            if (aCurrentKeyboardState.IsKeyDown(Keys.Escape))
                Exit();

            switch (mCurrentState)
            {
                case State.Running:
                {
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Space) && !mPreviousKeyboardState.IsKeyDown(Keys.Space))
                    {
                        mCarPosition.X += mMoveCarX;
                        mMoveCarX *= -1;
                    }

                    ScrollRoad(e);
                    if (!mHazardPosition.IntersectsWith(mCarPosition))
                    {
                        UpdateHazard(e);
                    }
                    else
                    {
                        mCurrentState = State.Crash;
                    }
                    
                    break;
                }
                case State.Crash:
                {
                    if (aCurrentKeyboardState.IsKeyDown(Keys.Enter))
                    {
                        StartGame();
                    }

                    break;
                }
            }

            mPreviousKeyboardState = aCurrentKeyboardState;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);            

            // TODO: Add your drawing code here
            //Begin
            {
                for (int aIndex = 0; aIndex < mRoadY.Length; aIndex++)
                {
                    spriteBatch.Draw(mRoad, new Vector2(0, mRoadY[aIndex]), Color.White);

                    spriteBatch.Draw(mCar, mCarPosition, Color.White);

                    spriteBatch.Draw(mHazard, mHazardPosition, Color.White);

                    //Text
                    spriteBatch.DrawString(mFont,
                        "Hazards: " + mHazardsPassed.ToString(),                        
                        new RectangleF(5, 25, 0, 0),
                        Color.White);

                    if (mCurrentState == State.Crash)
                    {
                        spriteBatch.DrawString(mFont, 
                            "Crash!",
                            new RectangleF(5, 200, 0, 0), 
                            Color.White);
                        spriteBatch.DrawString(mFont,
                            "'Enter' to play again.",
                            new RectangleF(5, 230, 0, 0),                            
                            Color.White);
                        spriteBatch.DrawString(mFont,
                            "'Escape' to exit.",
                            new RectangleF(5, 260, 0, 0),
                            Color.White);
                    }
                }                
            }
            //End

            base.OnRenderFrame(e);
        }

        protected void StartGame()
        {
            mCurrentState = State.Running;

            mRoadY[0] = 0;
            mRoadY[1] = mRoadY[0] + mRoad.Height - 2;
            mRoadY[2] = mRoadY[1] + mRoad.Height - 2;

            mVelocityY = 100.0F; //mVelocityY = 0.3F;

            mCarPosition = new Rectangle(280, 0, (int)(mCar.Width * 0.2f), (int)(mCar.Height * 0.2f));

            mMoveCarX = 160;

            mHazardPosition = new Rectangle(275,
                (int)Globals.ScreenDimensions.Y  + mHazard.Height,
                (int)(mHazard.Width * 0.4f),
                (int)(mHazard.Height * 0.4f));

            mHazardsPassed = 0;
        }

        private void ScrollRoad(FrameEventArgs theTime)
        {
            //Loop the road
            for (int aIndex = 0; aIndex < mRoadY.Length; aIndex++)
            {
                if (mRoadY[aIndex] < -Globals.ScreenDimensions.Y)
                {
                    int aLastRoadIndex = aIndex;
                    for (int aCounter = 0; aCounter < mRoadY.Length; aCounter++)
                    {
                        if (mRoadY[aCounter] > mRoadY[aLastRoadIndex])
                        {
                            aLastRoadIndex = aCounter;
                        }
                    }

                    mRoadY[aIndex] = mRoadY[aLastRoadIndex] + mRoad.Height - 2;
                }
            }
            
            //Move the road
            for (int aIndex = 0; aIndex < mRoadY.Length; aIndex++)
            {
                mRoadY[aIndex] -= (int)(mVelocityY * theTime.Time);
            }
        }

        private void UpdateHazard(FrameEventArgs theTime)
        {
            mHazardPosition.Y -= (int)(mVelocityY * theTime.Time);

            if (mHazardPosition.Y < -(int)(mHazard.Height * 0.4f))
            {
                mHazardPosition.X = 275;
                if (mRandom.Next(1, 3) == 2)
                {
                    mHazardPosition.X = 440;
                }

                mHazardPosition.Y = (int)Globals.ScreenDimensions.Y + mHazard.Height;

                mHazardsPassed += 1;
                mVelocityY += 50;
            }
        }

    }
}
