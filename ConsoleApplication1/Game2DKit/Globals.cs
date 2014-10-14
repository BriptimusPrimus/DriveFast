using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace Game2DKit
{
    class Globals
    {
        //Main screen dimensions
        public static Vector2 ScreenDimensions = new Vector2(800, 600);

        //fraction of the screen dimensions that represents
        //in sprite dimensions
        public static float screenDimensionFrac = 12.0f;

        //the default dimensions of a sprite is a fraction of the screen dimensions
        public static Vector2 DefaultSpriteDimensions = new Vector2(
            Globals.ScreenDimensions.X / screenDimensionFrac,
            Globals.ScreenDimensions.Y / screenDimensionFrac);
    }
}
