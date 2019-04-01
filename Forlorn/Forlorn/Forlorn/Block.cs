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

        public int ID
        {
            get
            {
                return id;
            } 
            set
            {
                this.id = value;
            }
        }
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
            this.rect = new Rectangle((400 - x) * 16, (y - 550) * 16, 16, 16);
            //this.rect = new Rectangle((400 - x) * 3, (y - 550) * 3, 3, 3);
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

        public void Move(float x, float y)
        {
            rect.X = (int)(rect.X + x);
            rect.Y = (int)(rect.Y + y);
        }

        public bool IsOffScreen()
        {
            return rect.X < 0 || rect.X > 1920 || rect.Y < 0 || rect.Y > 1080;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (IsOffScreen()) return;
            
            spriteBatch.Draw(texture, rect, GetColor());
        }
    }
}
