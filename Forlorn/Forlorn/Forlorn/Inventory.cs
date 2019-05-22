using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forlorn
{
    class Inventory
    {
        //TODO: Change Rectangle to Item
        private Rectangle[,] items;
        private bool isOpen;
        private Level level;

        private static Texture2D rectText;

        public Inventory(Level level)
        {
            this.level = level;
            items = new Rectangle[5, 8];
            isOpen = false;
            FillArray();
        }

        public void FillArray()
        {
            for(int y = 0; y < 5; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    items[y, x] = new Rectangle(800 + 100 * (x - 4), y * 75, 50, 50);
                }
            }
        }

        public void SwitchOpenState()
        {
            this.isOpen = !isOpen;
        }

        public void Update()
        {
            Vector2 center = new Vector2(level.Camera.Position.X + 800, level.Camera.Position.Y + 16 * 30);
            for(int y = 0; y < 5; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    items[y, x].X = (int) center.X + 25 + 100 * (x - 4);
                    items[y, x].Y = (int) center.Y - ((y - 2) * 75);
                }
            }
        }

        public bool AddItem(Rectangle r)
        {
            for(int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (items[y, x] != null) continue;

                    items[y, x] = r;
                    return true;
                }
            }

            return false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 position = new Vector2(level.Camera.Position.X, level.Camera.Position.Y);
            if (!isOpen)
                return;

            spriteBatch.Draw(rectText, new Rectangle((int) position.X, (int) position.Y, 1600, 16 * 60), new Color(0, 0, 0, .75f));
            for(int y = 0; y < 5; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    spriteBatch.Draw(rectText, items[y, x], Color.LightGray);
                }
            }
        }

        public static void LoadTexture(ContentManager content)
        {
            rectText = content.Load<Texture2D>("test/pixel");
        }
    }
}
