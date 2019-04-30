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
    class Bats
    {
        private Texture2D batText;
        private Rectangle batRect;
        Vector2 batPosition;
        Random randomGen = new Random();
        public Boolean spawned;
        int leftMostPoint;
        int randomVelocity = -5;
        Boolean swoop;
        Boolean flyAround;
        int flyTimer;
        int timer;
        public Vector2 Position
        {
            get
            {
                return batPosition;
            }
        }
        public Bats(ContentManager content, Vector2 playerPosition)
        {
            batText = content.Load<Texture2D>("white");
            leftMostPoint = (int)playerPosition.X - 450;
            batPosition = new Vector2(randomGen.Next(1000), 50);
            batRect = new Rectangle((int)batPosition.X, (int)batPosition.Y, 30, 15);
            spawned = true;
            timer = (int)randomGen.Next(300);
            flyTimer = randomGen.Next(10) + 5;
            swoop = false;
            flyAround = false;
        }
        public Texture2D getTexture()
        {
            return batText;
        }
        public Rectangle getRect()
        {
            return batRect;
        }
        public void setPosition(Vector2 newPosition)
        {
            batPosition = newPosition;
            batRect.X = (int)newPosition.X;
            batRect.Y = (int)newPosition.Y;
        }
        public void setVelocity(int randomVel)
        {
            randomVelocity = randomVel;
            spawned = false;
            
        }
        public void batUpdate(KeyboardState kb)
        {
            
            batRect.X += randomVelocity;
        }
    }
}

