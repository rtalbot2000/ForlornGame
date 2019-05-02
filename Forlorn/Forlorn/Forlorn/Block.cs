using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
     * 4 - Wood
     * 5 - Iron
     * 6 - Gold
     * 7 - Platinum
     */

    class Block
    {
        private int id;
        private Vector2 location;
        private Rectangle rect;
        private Texture2D texture;
        private bool collision;

        public int ID
        {
            get
            {
                return id;
            } 
            set
            {
                this.id = value;
                this.texture = textures[value];
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
        public bool CanCollide
        {
            get
            {
                return collision;
            }
        }

        public Block(int id, int x, int y, bool collision)
        {
            this.id = id;
            this.location = new Vector2(x, y);
            this.rect = new Rectangle(x * 3, y * 3, 3, 3);
            this.texture = textures[id];
            this.collision = collision;
        }

        public bool IsOffScreen(Vector2 playerLocation)
        {
            int xDiff = (int) Math.Abs(playerLocation.X - rect.X);
            int yDiff = (int)Math.Abs(playerLocation.Y - rect.Y);

            return xDiff > 1600 / 2 || yDiff > 1600 / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(texture == null)
            {
                return;
            }
            spriteBatch.Draw(texture, rect, Color.White);
        }

        private static Texture2D[] textures;

        public static void LoadTextures(ContentManager content)
        {
            textures = new Texture2D[8];
            textures[1] = content.Load<Texture2D>("blocks/stone");
            textures[2] = content.Load<Texture2D>("blocks/grass");
            textures[3] = content.Load<Texture2D>("blocks/dirt");
            textures[4] = content.Load<Texture2D>("blocks/wood");
            textures[5] = content.Load<Texture2D>("blocks/iron");
            textures[6] = content.Load<Texture2D>("blocks/gold");
            textures[7] = content.Load<Texture2D>("blocks/platinum");
        }
    }
}
