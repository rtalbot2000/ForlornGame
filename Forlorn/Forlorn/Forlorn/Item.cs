using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forlorn
{
    abstract class Item
    {
        private int inStack;
        private bool isHeld;
        private Texture2D texture;

        public Item(int inStack, bool isHeld, Texture2D texture)
        {
            this.inStack = inStack;
            this.isHeld = isHeld;
            this.texture = texture;
        }
    }
}
