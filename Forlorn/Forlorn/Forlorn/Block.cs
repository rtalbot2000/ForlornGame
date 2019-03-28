using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forlorn
{
    /*
     * ID'S:
     * 0 - Air
     * 1 - Stone
     * 2 - Grass
     * 3 - Dirt
     * 
     */

    class Block
    {
        private int id;
        private Vector2 location;
        private Rectangle rect;
        private Texture2D texture;

        public Vector2 Location
        {
            get
            {
                return location;
            }
        }
        public Rectangle Rectangle
        {
            get
            {
                return rect;
            }
            set
            {
                this.rect = value;
            }
        }

        public Block(int id, int x, int y, Texture2D text)
        {
            this.id = id;
            this.location = new Vector2(x, y);
            this.rect = new Rectangle(x * 8, y * 8, 8, 8);
            this.texture = text;
        }

        public Color GetColor()
        {
            switch(id)
            {
                case 1:
                    return Color.DarkGray;
                case 2:
                    return Color.DarkGreen;
                case 3:
                    return Color.Brown;
                default:
                    return Color.Transparent;
            }
        }

        public void UpdateOffset(float x, float y)
        {
            float xDiff = x - location.X;
            float yDiff = y - location.Y;

            rect.X = (int) (x + xDiff) * 8;
            rect.Y = (int) (y + yDiff) * 8;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rect.X < 0 || rect.X > 1920 || rect.Y < 0 || rect.Y > 1080)
                return;
            
            spriteBatch.Draw(texture, rect, GetColor());
        }
    }
}
