using Microsoft.Xna.Framework;
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

        public Inventory()
        {
            items = new Rectangle[8, 5];
            isOpen = false;
        }

        public bool addItem(Rectangle r)
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
            for(int y = 0; y < 5; y++)
            {
                for(int x = 0; x < 8; x++)
                {
                    
                }
            }
        }

    }
}
