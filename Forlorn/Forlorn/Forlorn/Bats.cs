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
        ContentManager content;
        Random randomGen = new Random();
        public Boolean spawned;
        int leftMostPoint;
        int randomVelocity = -5;
        int flyTimer;
        int timer;
        double fallVelocity;
        Boolean swoop;
        Boolean flyAround;
        public Vector2 Position
        {
            get
            {
                return batPosition;
            }
        }
        public void setTexture(ContentManager content, String name)
        {
            batText = content.Load<Texture2D>(name);
        }
        public Bats(ContentManager content, Vector2 playerPosition)
        {
            this.content = content;
            batText = content.Load<Texture2D>("batOfficial.fw");
            leftMostPoint = (int)playerPosition.X - 450;
            batPosition = new Vector2(randomGen.Next(1000), 50);
            batRect = new Rectangle((int)batPosition.X, (int)batPosition.Y, 60, 30);
            spawned = true;
            timer = (int)randomGen.Next(300);
            flyTimer = randomGen.Next(10) + 5;
            fallVelocity = 10;
            flyAround = true;
            swoop = false;
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
        public void batUpdate(KeyboardState kb, Vector2 playerPosition)
        {
            batPosition = new Vector2(batRect.X, batRect.Y);
            if (flyAround)
            {
                if (batRect.X <= 0)
                {
                    randomVelocity *= -1;
                    setTexture(content, "batOfficialRight.fw");
                }
                else if (batRect.X >= 4800)
                {
                    setTexture(content, "batOfficial.fw");
                    randomVelocity *= -1;
                }
                batRect.X += randomVelocity;
                if (batPosition.X - 100 < playerPosition.X && batPosition.X + 100 > playerPosition.X
                    )
                {
                    swoop = true;
                    flyAround = false;
                }
            }

            if (swoop)
            {

                batRect.Y += (int)fallVelocity;
                batRect.X += randomVelocity;
                fallVelocity -= .09;
                if (batRect.X <= 0)
                    randomVelocity *= -1;
                if (batRect.X >= 4800)
                    randomVelocity *= -1;

                if (batRect.Y <= 50)
                {                   
                    swoop = false;
                    fallVelocity = 10;
                    flyAround = true;
                }
            }
        }
    }
}

