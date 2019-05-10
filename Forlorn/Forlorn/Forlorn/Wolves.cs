using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace Forlorn
{
    class Wolves
    {
        Vector2 wolfPosition;
        Texture2D wolfText;
        public Vector2 Position
        {
            get
            {
                return wolfPosition;
            }
        }
        public void setTexture(ContentManager content, String name)
        {
            wolfText = content.Load<Texture2D>(name);
        }
        public Wolves(ContentManager content, Vector2 playerPosition)
        {
            if (playerPosition.X > wolfPosition.X)
                wolfText = content.Load<Texture2D>("wolfStandLeft");
            else
                wolfText = content.Load<Texture2D>("wolfStandRight");

        }
    }
}
