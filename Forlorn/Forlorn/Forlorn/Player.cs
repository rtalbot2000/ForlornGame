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
        Rectangle cameraRectangle;

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

        public Rectangle CameraRectangle
        {
            get
            {
                return cameraRectangle;
            }
        }

        //Creates player dude
        public Player(int x, int y, ContentManager content)
        {
            this.texture = content.Load<Texture2D>("white");
            //body = new Rectangle(6400, 9550, 8, 24);
            body = new Rectangle(16 * 400, 16 * 600 - 50, 8, 24);
            position = new Vector2(body.X, body.Y);
            initialY = body.Y;
            dead = false;
            healthRemaining = 100d;
            this.cameraRectangle = new Rectangle(body.X - 50, body.Y - 50, 100, 100);
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
                if (body.X > 0)
                {
                    body.X -= 2;
                    position.X -= 2;
                    movement.X = -2;
                }
            } else if (kb.IsKeyDown(Keys.D))
            {
                if (body.X + body.Width < 800 * 16)
                {
                    body.X += 2;
                    position.X += 2;
                    movement.X = 2;
                }
            }
            if (isJumping)
            {
                timer++;
                double jumpVelocity = 10 + -0.5d * timer;
                body.Y -= (int)jumpVelocity;
                position.Y = body.Y;
                if (body.Y >= initialY)
                {
                    timer = 0;
                    isJumping = false;
                }
            }
            if(kb.IsKeyDown(Keys.Space))
            {
                Console.WriteLine(GetBlockLocation());
            }

            if(position.X + body.Width >= cameraRectangle.X + cameraRectangle.Width)
            {
                cameraRectangle.X += 2;
            }
            if(position.X <= cameraRectangle.X)
            {
                cameraRectangle.X -= 2;
            }
            if(position.Y + body.Height >= cameraRectangle.Y + cameraRectangle.Height)
            {
                cameraRectangle.Y += 2;
            }
            if(position.Y <= cameraRectangle.Y)
            {
                cameraRectangle.Y -= 2;
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
