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
    class Player
    {
        private Texture2D texture;
        private Rectangle body;
        
        //Creates player dude
        public Player(int x, int y, ContentManager content)
        {
            this.texture = content.Load<Texture2D>("white");
            body = new Rectangle(x, y, 8, 24);
        }
        //Returns texture of character
        public Texture2D getTexture()
        {
            return texture;
        }
        //Returens body of character
        public Rectangle getRect()
        {
            return body;
        }
        public void update(KeyboardState kb)
        {
            if (kb.IsKeyDown(Keys.W))
            {
                jump();
                //body.Y--;
            }
            if (kb.IsKeyDown(Keys.A))
                body.X--;
            if (kb.IsKeyDown(Keys.D))
                body.X++;
          
        }
        public void jump()
        {
            int velocityY = 5;
            do
            {
                body.Y -= velocityY;
                velocityY -= 2;
            } while (velocityY >= -5);
        }
    }
}
