using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;
using OpenTK.Input;
using System.Drawing;

namespace Game2DKit
{
    class Game : GameWindow
    {
        protected ContentManager Content = new ContentManager();

        /// <summary>Creates a 800x600 window with the specified title.</summary>
        public Game()
            : base((int)Globals.ScreenDimensions.X,
                (int)Globals.ScreenDimensions.Y, 
                OpenTK.Graphics.GraphicsMode.Default, "Game")
        {
            VSync = VSyncMode.On;
        }

        /// <summary>Load resources here.</summary>
        /// <param name="e">Not used.</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.CornflowerBlue);
            SetupViewport();

            //GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //GL.AlphaFunc(AlphaFunction.Greater, 0.5f);
            //GL.Enable(EnableCap.AlphaTest);

            GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);

            Game2DKit.Input.Keyboard.GameKeyboard = this.Keyboard;
        }

        protected void SetupViewport()
        {
            GL.Viewport(new System.Drawing.Size(Width, Height));
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, (int)Globals.ScreenDimensions.X,
                0, (int)Globals.ScreenDimensions.Y, -1, 1); //GL.Ortho(0, Width, 0, Height, -1, 1);
        }

        /// <summary>
        /// Called when your window is resized. Set your viewport here. It is also
        /// a good place to set up your projection matrix (which probably changes
        /// along when the aspect ratio of your window).
        /// </summary>
        /// <param name="e">Not used.</param>
        protected override void OnResize(EventArgs e)
        {
            SetupViewport();
        }

        /// <summary>
        /// Called when it is time to setup the next frame. Add you game logic here.
        /// </summary>
        /// <param name="e">Contains timing information for framerate independent logic.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            if (Keyboard[Key.Escape])
                Exit();
        }

        /// <summary>
        /// Called when it is time to render the next frame. Add your rendering code here.
        /// </summary>
        /// <param name="e">Contains timing information.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Flush();
            SwapBuffers();
        }

    }
}
