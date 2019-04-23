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
        bool dead;
        double healthRemaining;
        Vector2 position;
        Vector2 movement;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Vector2 Movement
        {
            get
            {
                return movement;
            }
        }

        //Creates player dude
        public Player(int x, int y, ContentManager content)
        {
            this.texture = content.Load<Texture2D>("white");
            body = new Rectangle(12800, 1600, 8, 24);
            position = new Vector2(body.X, body.Y);
            initialY = body.Y;
            dead = false;
            healthRemaining = 100d;
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

        public Vector2 GetBlockLocation()
        {
            return new Vector2((float) Math.Floor(position.X / 16), 
                (float) Math.Floor(position.Y / 16));
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
                movement.X = -2;
            } else if (kb.IsKeyDown(Keys.D))
            {
                body.X += 2;
                position.X += 2;
                movement.X = 2;
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
        public void setHealth(double hit)
        {
            healthRemaining -= hit;
            if (healthRemaining <= 0)
                dead = true;
        }
        public Boolean isDead()
        {
            return dead;
        }
    }
}
