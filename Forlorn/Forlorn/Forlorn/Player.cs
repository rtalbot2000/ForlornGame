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
        Vector2 playerPosition;
        bool isFalling = false;
        int initialY;
        int remnantY;
        double health;

        public Vector2 Position
        {
            get
            {
                return playerPosition;
            }
        }
        public Vector2 getPosition()
        {
            return playerPosition;
        }
        //Creates player dude
        public Player(int x, int y, ContentManager content)
        {
            this.texture = content.Load<Texture2D>("white");
            playerPosition = new Vector2(x, y);
            body = new Rectangle(x, y, 8, 24);
            initialY = body.Y;
            health = 100d;
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
            playerPosition = new Vector2(body.X, body.Y);
            remnantY = body.Y;
            if (kb.IsKeyDown(Keys.W))
            {
                if (!isJumping)
                    isJumping = true;
            }
            if (kb.IsKeyDown(Keys.A))
                body.X--;
            if (kb.IsKeyDown(Keys.D))
                body.X++;
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
        public void updateHealth(String item)
        {
            switch (item)
            {
                case "bat":
                    health -= .5d;
                    break;
                case "wolf":
                    health -= 2.5d;
                    break;
                case "ghoul":
                    health -= 1.5d;
                    break;
            }
            if (health <= 0)
            {
                //game over screen
            }
        }
    }
}
