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
        int timer = 0;
        bool isJumping = false;
        bool isFalling = false;
        int initialY;
        int remnantY;
        Vector2 position;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        //Creates player dude
        public Player(int x, int y, ContentManager content)
        {
            this.texture = content.Load<Texture2D>("white");
            body = new Rectangle(400 * 16, 1080/2, 8, 24);
            position = new Vector2(400 * 16, 1080 /2);
            initialY = body.Y;
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
            remnantY = body.Y;
            if (kb.IsKeyDown(Keys.W))
            {
                if (!isJumping)
                    isJumping = true;
            }
            if (kb.IsKeyDown(Keys.A))
            {
                body.X -= 2;
                position.X -= 2;
            }
            if (kb.IsKeyDown(Keys.D))
            {
                body.X += 2;
                position.X += 2;
            }
            if (isJumping)
            {
                timer++;
                double jumpVelocity = 10 + -0.5d * timer;
                body.Y -= (int)jumpVelocity;
                if (body.Y >= initialY)
                {
                    timer = 0;
                    isJumping = false;
                }
            }
        }
    }
}
