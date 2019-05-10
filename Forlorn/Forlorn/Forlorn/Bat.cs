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
    class Bat
    {
        private Texture2D batText;
        private Rectangle batRect;
        int leftMostPoint;
        Vector2 batPosition;
        Random randomGen = new Random();
        Boolean spawned;
        Boolean swoop;
        Boolean flyAround;
        int flyTimer;
        int spawnTimer;
        public Vector2 Position
        {
            get
            {
                return batPosition;
            }
        }
        public Bat(ContentManager content, Vector2 position)
        {
            batText = content.Load<Texture2D>("white");
            leftMostPoint = (int)position.X - 800;
            //batPosition = new Vector2(randomGen.Next(leftMostPoint + 1800), -10);
            batPosition = new Vector2(500, 200);
            batRect = new Rectangle((int)batPosition.X, (int)batPosition.Y, 30, 15);
            spawned = true;
            spawnTimer = 300;
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
        public void batUpdate()
        {
            if (spawned)
            {
                if (spawnTimer % 60 == 0)
                    batRect.X += 4;
                spawnTimer--;
                if (spawnTimer <= 0)
                {
                    spawned = false;
                    spawnTimer = 0;
                    flyAround = true;
                }
            }
            else
            {
                if (flyAround)
                {
                    if (batRect.X < leftMostPoint)
                        batRect.X += 8;
                    else
                    {
                        int xRandomize = randomGen.Next(2);
                        switch (xRandomize)
                        {
                            case 1:
                                batRect.X += randomGen.Next(10) + 5;
                                break;
                            case 2:
                                batRect.X -= randomGen.Next(10) + 5;
                                break;
                        }
                        int randomize = randomGen.Next(2);
                        if (batRect.Y < 10)
                            randomize = 1;
                        if (batRect.Y > 50)
                            randomize = 0;
                        switch (randomize)
                        {
                            case 1:
                                batRect.Y += 2;
                                break;
                            case 0:
                                batRect.Y -= 2;
                                break;
                        }
                    }
                    flyTimer--;
                    if (flyTimer < 0)
                    {
                        flyAround = false;
                        flyTimer = randomGen.Next(10) + 5;
                        swoop = true;
                    }
                }

            }
        }
    }
}

