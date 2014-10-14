using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game2DKit
{
    public class Texture2D
    {
        protected int textureID;

        protected string file;
        
        //Texture Sheet Dimensions
        protected int width;
        protected int height;

        //Properties
        public int TextureID { get { return textureID; } }

        public string File { get { return file; } }

        public int Width { get { return width; } }

        public int Height { get { return height; } }

        public Texture2D(int TextureID, string file, int Width, int Height)
        {
            this.textureID = TextureID;
            this.file = file;
            this.width = Width;
            this.height = Height;
        }
    }
}
