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
            this.rect = new Rectangle((x) * 16, (y) * 16, 16, 16);
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

        public bool IsOffScreen(Vector2 playerLocation)
        {
            int xDiff = (int) Math.Abs(playerLocation.X - rect.X);
            int yDiff = (int)Math.Abs(playerLocation.Y - rect.Y);

            return xDiff > 1600 / 2 || yDiff > 1600 / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rect, GetColor());
        }
    }
}
