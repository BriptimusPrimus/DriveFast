using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game2DKit;
using MyGameStuff;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // The 'using' idiom guarantees proper resource cleanup.
            // We request 30 UpdateFrame events per second, and unlimited
            // RenderFrame events (as fast as the computer can handle).
            using (Game1 game = new Game1())
            {
                game.Run(30.0);
            }
        }
    }
}
