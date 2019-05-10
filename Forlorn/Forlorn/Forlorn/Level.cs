using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Forlorn
{
    class Level
    {
        private Block[,] blocks;

        volatile private List<Block> drawnBlocks;

        private Texture2D testPixel;

        private Player player;

        private Camera camera;

        public Level(Texture2D testPixel, Player p, Camera camera)
        {
            blocks = new Block[800, 800];

            this.testPixel = testPixel;

            this.player = p;

            this.camera = camera;

            GenerateLevel();
        }

        public void GenerateLevel()
        {
            for (int x = 0; x < 800; x++)
            {
                for (int y = 0; y < 600; y++)
                {
                    blocks[y, x] = new Block(0, x, y, true);
                }
                blocks[600, x] = new Block(2, x, 600, true);
                for (int y = 601; y < 605; y++)
                {
                    blocks[y, x] = new Block(3, x, y, true);
                }
                for (int y = 605; y < 800; y++)
                {
                    blocks[y, x] = new Block(1, x, y, true);
                }
            }

            GenerateCaves();

            CheckForLonelyBlocks();

            GenerateOres();

            GenerateTrees();
        }

        public void GenerateCaves()
        {
            Random rand = new Random();
            for(int x = 0; x < 800; x++)
            {
                for(int y = 605; y < 799; y++)
                {
                    if (rand.NextDouble() < .002)
                    {
                        blocks[y, x].ID = 0;
                    }
                }
            }

            for (int t = 0; t < 145; t++)
            {
                for (int x = 0; x < 800; x++)
                {
                    for (int y = 605; y < 799; y++)
                    {
                        if (blocks[y, x].ID != 0 ||
                            (x > 395 && x < 405 && y > 399 && y < 405)) continue;

                        double startChance = .022;
                        double chance = startChance;

                        for(int yy = 1; yy < 20; yy++)
                        {
                            if (y + yy > 798) break;
                            if(blocks[y + yy, x].ID == 0)
                            {
                                chance += .005;
                                if (chance == .03)
                                    break;
                            }
                        }

                        if (rand.NextDouble() < chance && y < 798)
                        {
                            blocks[y + 1, x].ID = 0;
                        }

                        chance = startChance;
                        for(int xx = -1; xx < -20; xx--)
                        {
                            if (x + xx < 0) break;
                            if(blocks[y, x + xx].ID == 0)
                            {
                                chance += .005;
                                if (chance >= .03) break;
                            }
                        }

                        if (rand.NextDouble() < chance && x > 0)
                        {
                            blocks[y, x - 1].ID = 0;
                        }

                        chance = startChance;
                        for (int xx = 1; xx < 20; xx++)
                        {
                            if (x + xx > 799) break;
                            if (blocks[y, x + xx].ID == 0)
                            {
                                chance += .005;
                                if (chance >= .03) break;
                            }
                        }

                        if (rand.NextDouble() < chance && x < 798)
                        {
                            blocks[y, x + 1].ID = 0;
                        }

                        chance = startChance;
                        for (int yy = -1; yy > -20; yy--)
                        {
                            if (y + yy < 605) break;
                            if (blocks[y + yy, x].ID == 0)
                            {
                                chance += .005;
                                if (chance == .03)
                                    break;
                            }
                        }

                        if (rand.NextDouble() < chance && y > 600)
                        {
                            blocks[y - 1, x].ID = 0;
                        }
                    }
                }
            }
        }

        public void CheckForLonelyBlocks()
        {
            for(int y = 601; y < 799; y++)
            {
                for(int x = 1; x < 799; x++)
                {
                    int amtAir = 0;

                    for(int yOffset = -1; yOffset < 2; yOffset++)
                    {
                        for(int xOffset = -1; xOffset < 2; xOffset++)
                        {
                            if (xOffset == yOffset)
                                continue;

                            if (blocks[y + yOffset, x + xOffset].ID == 0)
                                amtAir++;
                        }
                    }

                    if(amtAir == 8)
                    {
                        blocks[y, x].ID = 0;
                    }
                }
            }
        }

        public void GenerateOres()
        {
            Random rand = new Random();

            // Platinum

            for (int y = 735; y < 800; y++)
            {
                for (int x = 0; x < 800; x++)
                {
                    if (blocks[y, x].ID != 1)
                        continue;

                    double chance = .002;

                    int amtAir = 0;

                    for (int xOffset = -2; xOffset <= 2; xOffset++)
                    {
                        for (int yOffset = -2; yOffset <= 2; yOffset++)
                        {
                            if (xOffset == 0 && yOffset == 0 || (x + xOffset < 0 || x + xOffset >= 800 || y + yOffset >= 800))
                                continue;

                            if (blocks[y + yOffset, x + xOffset].ID == 0)
                            {
                                amtAir++;
                                chance += .0004;
                            }
                        }
                    }

                    //Console.WriteLine(((double)amtAir) / 25.0);

                    if (((double)amtAir) / 25.0 >= .7)
                    {
                        chance = .0001;
                    }

                    if (rand.NextDouble() < chance)
                    {
                        blocks[y, x].ID = 7;
                    }
                }
            }

            for (int times = 0; times < 4; times++)
            {
                for (int y = 610; y < 799; y++)
                {
                    for (int x = 1; x < 799; x++)
                    {
                        if (blocks[y, x].ID != 7)
                        {
                            continue;
                        }

                        if (blocks[y - 1, x].ID != 7)
                        {
                            if (rand.NextDouble() < .12)
                            {
                                blocks[y - 1, x].ID = 7;
                            }
                        }
                        if (blocks[y + 1, x].ID != 7)
                        {
                            if (rand.NextDouble() < .12)
                            {
                                blocks[y + 1, x].ID = 7;
                            }
                        }
                        if (blocks[y, x - 1].ID != 7)
                        {
                            if (rand.NextDouble() < .12)
                            {
                                blocks[y, x - 1].ID = 7;
                            }
                        }
                        if (blocks[y, x + 1].ID != 7)
                        {
                            if (rand.NextDouble() < .12)
                            {
                                blocks[y, x + 1].ID = 7;
                            }
                        }
                    }
                }
            }

            // Gold

            for (int y = 675; y < 800; y++)
            {
                for (int x = 0; x < 800; x++)
                {
                    if (blocks[y, x].ID != 1)
                        continue;

                    double chance = .0027;

                    int amtAir = 0;

                    for (int xOffset = -2; xOffset <= 2; xOffset++)
                    {
                        for (int yOffset = -2; yOffset <= 2; yOffset++)
                        {
                            if (xOffset == 0 && yOffset == 0 || (x + xOffset < 0 || x + xOffset >= 800 || y + yOffset >= 800))
                                continue;

                            if (blocks[y + yOffset, x + xOffset].ID == 0)
                            {
                                amtAir++;
                                chance += .0004;
                            }
                        }
                    }

                    //Console.WriteLine(((double)amtAir) / 25.0);

                    if (((double)amtAir) / 25.0 >= .7)
                    {
                        chance = .0001;
                    }

                    if (rand.NextDouble() < chance)
                    {
                        blocks[y, x].ID = 6;
                    }
                }
            }

            for (int times = 0; times < 8; times++)
            {
                for (int y = 610; y < 799; y++)
                {
                    for (int x = 1; x < 799; x++)
                    {
                        if (blocks[y, x].ID != 6)
                        {
                            continue;
                        }

                        if (blocks[y - 1, x].ID != 6)
                        {
                            if (rand.NextDouble() < .055)
                            {
                                blocks[y - 1, x].ID = 6;
                            }
                        }
                        if (blocks[y + 1, x].ID != 6)
                        {
                            if (rand.NextDouble() < .055)
                            {
                                blocks[y + 1, x].ID = 6;
                            }
                        }
                        if (blocks[y, x - 1].ID != 6)
                        {
                            if (rand.NextDouble() < .055)
                            {
                                blocks[y, x - 1].ID = 6;
                            }
                        }
                        if (blocks[y, x + 1].ID != 6)
                        {
                            if (rand.NextDouble() < .055)
                            {
                                blocks[y, x + 1].ID = 6;
                            }
                        }
                    }
                }
            }

            //Iron
            for (int y = 610; y < 800; y++)
            {
                for (int x = 0; x < 800; x++)
                {
                    if (blocks[y, x].ID != 1)
                        continue;

                    double chance = .006 - (y - 610) * .00002;

                    int amtAir = 0;

                    for (int xOffset = -2; xOffset <= 2; xOffset++)
                    {
                        for (int yOffset = -2; yOffset <= 2; yOffset++)
                        {
                            if (xOffset == 0 && yOffset == 0 || (x + xOffset < 0 || x + xOffset >= 800 || y + yOffset >= 800))
                                continue;

                            if (blocks[y + yOffset, x + xOffset].ID == 0)
                            {
                                amtAir++;
                                chance += .00035;
                            }
                        }
                    }

                    //Console.WriteLine(((double)amtAir) / 25.0);

                    if (((double)amtAir) / 25.0 >= .7)
                    {
                        chance = .0001;
                    }

                    if (rand.NextDouble() < chance)
                    {
                        blocks[y, x].ID = 5;
                    }
                }
            }

            for (int times = 0; times < 10; times++)
            {
                for (int y = 610; y < 799; y++)
                {
                    for (int x = 1; x < 799; x++)
                    {
                        if (blocks[y, x].ID != 5)
                        {
                            continue;
                        }

                        if (blocks[y - 1, x].ID != 5)
                        {
                            if (rand.NextDouble() < .0525)
                            {
                                blocks[y - 1, x].ID = 5;
                            }
                        }
                        if (blocks[y + 1, x].ID != 5)
                        {
                            if (rand.NextDouble() < .0525)
                            {
                                blocks[y + 1, x].ID = 5;
                            }
                        }
                        if (blocks[y, x - 1].ID != 5)
                        {
                            if (rand.NextDouble() < .0525)
                            {
                                blocks[y, x - 1].ID = 5;
                            }
                        }
                        if (blocks[y, x + 1].ID != 5)
                        {
                            if (rand.NextDouble() < .0525)
                            {
                                blocks[y, x + 1].ID = 5;
                            }
                        }
                    }
                }
            }
        }

        public void GenerateTrees()
        {
            Random rand = new Random();

            for(int x = 0; x < 800; x++)
            {
                if(rand.NextDouble() < .2)
                {
                    blocks[599, x].ID = 4;

                    int height = rand.Next(4, 32);

                    for(int y = 1; y < height; y++)
                    {
                        blocks[599 - y, x].ID = 4;
                    }

                    x += 3;
                }
            }
        }

        public void Update(Player p)
        {
            
        }

        public void Draw(Vector2 playerLocation, SpriteBatch spriteBatch, GameTime gameTime)
        {
            for (int yDiff = -50; yDiff <= 50; yDiff++)
            {
                for (int xDiff = -50; xDiff <= 50; xDiff++)
                {
                    int x = (int)(player.CameraRectangle.X + player.CameraRectangle.Width / 2) / 16;
                    int y = (int)(player.CameraRectangle.Y + player.CameraRectangle.Height / 2) / 16;

                    if (x < 50)
                        x = 50;

                    if (x > 750)
                        x = 750;

                    if (y < 50)
                        y = 50;

                    if (y > 750)
                        y = 750;

                    x += xDiff;
                    y += yDiff;

                    if (x < 0 || x >= 800 || y < 0 || y >= 800) continue;

                    blocks[y, x].Draw(spriteBatch);
                }
            }
        }
    }
}
