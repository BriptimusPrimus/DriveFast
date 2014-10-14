using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Text = OpenTK.Graphics;

namespace Game2DKit
{
    public class SpriteBatch
    {
        Text.TextPrinter PrintText = new Text.TextPrinter();

        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.PushMatrix();

            GL.Translate(new Vector3(position));
            GL.Color4(color);
            GL.Begin(BeginMode.Quads);
            {                
                GL.TexCoord2(0, 0);
                GL.Vertex2(0, texture.Height);
                GL.TexCoord2(1, 0);
                GL.Vertex2(texture.Width, texture.Height);
                GL.TexCoord2(1, 1);
                GL.Vertex2(texture.Width, 0);
                GL.TexCoord2(0, 1);
                GL.Vertex2(0, 0);
            }
            GL.End();

            GL.PopMatrix();
        }

        //position + size in Rectangle source
        public void Draw(Texture2D texture, Rectangle source, Color color)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.PushMatrix();

            GL.Translate(new Vector3(new Vector2(source.X, source.Y)));
            GL.Color4(color);
            GL.Begin(BeginMode.Quads);
            {
                GL.TexCoord2(0, 0);
                GL.Vertex2(0, source.Height);
                GL.TexCoord2(1, 0);
                GL.Vertex2(source.Width, source.Height);
                GL.TexCoord2(1, 1);
                GL.Vertex2(source.Width, 0);
                GL.TexCoord2(0, 1);
                GL.Vertex2(0, 0);
            }
            GL.End();

            GL.PopMatrix();
        }

        //position + size in Rectangle source ; rotation
        public void Draw(Texture2D texture, Rectangle source, Color color, float angle)
        {
            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.PushMatrix();

            GL.Translate(new Vector3(new Vector2(source.X, source.Y)));
            GL.Rotate(angle, 0, 0, 1);
            GL.Color4(color);
            GL.Begin(BeginMode.Quads);
            {
                GL.TexCoord2(0, 0);
                GL.Vertex2(0, source.Height);
                GL.TexCoord2(1, 0);
                GL.Vertex2(source.Width, source.Height);
                GL.TexCoord2(1, 1);
                GL.Vertex2(source.Width, 0);
                GL.TexCoord2(0, 1);
                GL.Vertex2(0, 0);
            }
            GL.End();

            GL.PopMatrix();
        }

        //subrectangle and scale
        public void Draw(Texture2D texture, Vector2 position, Rectangle source,
            Color color, float scale)
        {
            //TODO: implement source and scale here

            //calculate Tex Coordinates Indexes
            float initialX = (float)source.X / texture.Width;
            float initialY = (float)source.Y / texture.Height;
            float finalX = (float)(source.X + source.Width) / texture.Width;
            float finalY = (float)(source.Y + source.Height) / texture.Height;

            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.PushMatrix();

            GL.Translate(new Vector3(position));
            GL.Color4(color);
            GL.Begin(BeginMode.Quads);
            {                
                GL.TexCoord2(initialX, initialY);
                GL.Vertex2(0, source.Height * scale);
                GL.TexCoord2(finalX, initialY);
                GL.Vertex2(source.Width * scale, source.Height * scale);
                GL.TexCoord2(finalX, finalY);
                GL.Vertex2(source.Width * scale, 0);
                GL.TexCoord2(initialX, finalY);
                GL.Vertex2(0, 0);
            }
            GL.End();

            GL.PopMatrix();
        }

        //rotate
        public void Draw(Texture2D texture, Vector2 position, Rectangle source,
            Color color, float angle, float scale = 1)
        {
            //TODO: implement source and scale here

            //calculate Tex Coordinates Indexes
            float initialX = (float)source.X / texture.Width;
            float initialY = (float)source.Y / texture.Height;
            float finalX = (float)(source.X + source.Width) / texture.Width;
            float finalY = (float)(source.Y + source.Height) / texture.Height;

            GL.BindTexture(TextureTarget.Texture2D, texture.TextureID);
            GL.PushMatrix();

            GL.Translate(new Vector3(position));
            GL.Rotate(angle, 0, 0, 1);
            GL.Color4(color);
            GL.Begin(BeginMode.Quads);
            {
                GL.TexCoord2(initialX, initialY);
                GL.Vertex2(0, source.Height * scale);
                GL.TexCoord2(finalX, initialY);
                GL.Vertex2(source.Width * scale, source.Height * scale);
                GL.TexCoord2(finalX, finalY);
                GL.Vertex2(source.Width * scale, 0);
                GL.TexCoord2(initialX, finalY);
                GL.Vertex2(0, 0);
            }
            GL.End();

            GL.PopMatrix();
        }

        //Text Print
        public void DrawString(Font TextFont, string TextToWrite, Color color)
        {
            PrintText.Begin();
            PrintText.Print(TextToWrite, TextFont, color);
            PrintText.End();              
        }

        //Text Print + position
        public void DrawString(Font TextFont, string TextToWrite, RectangleF rect, Color color)
        {
            PrintText.Begin();
            PrintText.Print(TextToWrite, TextFont, color, rect);
            PrintText.End();
        }

    }
}
