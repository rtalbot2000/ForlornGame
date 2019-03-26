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

namespace Forlorn
{
    abstract class Sword
    {
        public Texture2D swordTexture;
        public Rectangle swordRect;
        public Texture2D testTexture;
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;
        public LoadContent(IServiceProvider serviceProvider, string path)
        {
            content = new ContentManager(serviceProvider, "Content");
            swordTexture = this.Content.Load<Texture2D>("sword");
            testTexture = this.Content.Load<Texture2D>("white");

            Load
        }
        private void RectangleInterection(SpriteBatch spriteBatch)
        {
            Rectangle rectangle1 = new Rectangle(50, 50, 200, 100);
            Rectangle rectangle2 = new Rectangle(70, 20, 100, 200);
            Rectangle rectangle3 = new Rectangle();

            spriteBatch.Draw(swordTexture, rectangle1, Color.Gray);
            spriteBatch.Draw(swordTexture, rectangle2, Color.White);

            //e.Graphics.DrawRectangle(Pens.Black, rectangle1);
            //e.Graphics.DrawRectangle(Pens.Red, rectangle2);

            if (rectangle1.Intersects(rectangle2))
            {
                rectangle3 = Rectangle.Intersect(rectangle1, rectangle2);
                if (!rectangle3.IsEmpty)
                {
                    spriteBatch.Draw(swordTexture, rectangle3, Color.Green);
                    //e.Graphics.FillRectangle(Brushes.Green, rectangle3);
                }
            }
        }
    }
}
