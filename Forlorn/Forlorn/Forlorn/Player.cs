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
        bool dead;
        double healthRemaining;
        Vector2 position;
        Vector2 movement;
        Rectangle cameraRectangle;
        Inventory inventory;

        KeyboardState oldKey;

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
        }

        public Rectangle CameraRectangle
        {
            get
            {
                return cameraRectangle;
            }
        }

        public Inventory Inventory
        {
            get
            {
                return inventory;
            }
            set
            {
                this.inventory = value;
            }
        }

        //Creates player dude
        public Player(int x, int y, ContentManager content)
        {
            this.texture = content.Load<Texture2D>("white");
            playerPosition = new Vector2(x, y);
            body = new Rectangle(x, y, 8, 24);
            initialY = body.Y;
            dead = false;
            healthRemaining = 100d;
            this.cameraRectangle = new Rectangle(body.X - 50, body.Y - 50, 100, 100);

            oldKey = Keyboard.GetState();
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
                if(body.Y > 0)
                {
                    body.Y -= 2;
                    position.Y -= 2;
                    movement.Y = -2;
                }
            }
            else if(kb.IsKeyDown(Keys.S))
            {
                if (body.Y < 16 * 800)
                {
                    body.Y += 2;
                    position.Y += 2;
                    movement.Y = 2;
            }
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

            if(kb.IsKeyDown(Keys.E) && !oldKey.IsKeyDown(Keys.E))
            {
                inventory.SwitchOpenState();
            }

            oldKey = kb;

            if(inventory != null)
            {
                inventory.Update();
            }
        }
        public void setHealth(double hit)
        {
            healthRemaining -= hit;
            if (healthRemaining <= 0)
                dead = true;
        }
        }
    }
}
