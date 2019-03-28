using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Forlorn
{
    class Level
    {
        private Block[,] blocks;

        private Texture2D testPixel;

        public Level(Texture2D testPixel)
        {
            blocks = new Block[800, 800];

            this.testPixel = testPixel;

            GenerateLevel();
        }

        public void GenerateLevel()
        {
            for (int x = 0; x < 800; x++)
            {
                for (int y = 0; y < 600; y++)
                {
                    blocks[y, x] = new Block(0, x, y, testPixel);
                }
                blocks[600, x] = new Block(2, x, 600, testPixel);
                for (int y = 601; y < 605; y++)
                {
                    blocks[y, x] = new Block(3, x, y, testPixel);
                }
                for (int y = 605; y < 800; y++)
                {
                    blocks[y, x] = new Block(1, x, y, testPixel);
                }
            }

            GenerateCaves();
        }

        public void GenerateCaves()
        {
            Random rand = new Random();
            for(int x = 0; x < 800; x++)
            {
                for(int y = 605; y < 799; y++)
                {
                    if (rand.NextDouble() < .004)
                    {
                        blocks[y, x].ID = 0;
                    }
                }
            }

            for (int t = 0; t < 75; t++)
            {
                for (int x = 0; x < 800; x++)
                {
                    for (int y = 605; y < 799; y++)
                    {
                        if (blocks[y, x].ID != 0 ||
                            (x > 395 && x < 405 && y > 399 && y < 405)) continue;

                        if (rand.NextDouble() < .04 && y < 798)
                        {
                            blocks[y + 1, x].ID = 0;
                        }
                        if (rand.NextDouble() < .02 && x > 0)
                        {
                            blocks[y, x - 1].ID = 0;
                        }
                        if (rand.NextDouble() < .02 && x < 798)
                        {
                            blocks[y, x + 1].ID = 0;
                        }
                        if (rand.NextDouble() < .04 && y > 600)
                        {
                            blocks[y - 1, x].ID = 0;
                        }
                    }
                }
            }
        }

        public void Update()
        {
            

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(Block b in blocks)
            {
                b.Draw(spriteBatch);
            }
        }
    }
}
