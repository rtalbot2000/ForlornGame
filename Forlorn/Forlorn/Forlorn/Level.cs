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
        private List<Vector2> drawnVectors;

        private Texture2D testPixel;

        private int loadTimer;

        private bool isDrawing;

        private Thread renderThread;

        private Player player;

        public Level(Texture2D testPixel, Player p)
        {
            blocks = new Block[800, 800];

            this.testPixel = testPixel;

            this.loadTimer = 0;

            isDrawing = false;

            this.player = p;

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

            drawnBlocks = new List<Block>();
            drawnVectors = new List<Vector2>();

            foreach (Block b in blocks)
            {
                if (!b.IsOffScreen(new Vector2(400 * 16, 1080 / 2)))
                {
                    drawnBlocks.Add(b);
                    drawnVectors.Add(b.Location);
                }
            }

            renderThread = new Thread(() => RenderLevel(player));
            renderThread.Start();
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

            for (int t = 0; t < 135; t++)
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

        public void Update(Player p)
        {
            
        }

        public void RenderLevel(Player p)
        {
            while(true)
            {
                lock(drawnBlocks)
                {
                    if (p.Movement.X < 0)
                    {
                        for (int xOffset = -6; xOffset < 0; xOffset++)
                        {
                            for (int yOffset = -38; yOffset <= 38; yOffset++)
                            {
                                int x = (int)p.GetBlockLocation().X - 60 + xOffset;
                                int y = 650 - (int)p.GetBlockLocation().Y + yOffset;

                                if (x < 0 || x > blocks.GetLength(0) - 1 ||
                                    y < 0 || y > blocks.GetLength(1) - 1 ||
                                drawnVectors.Contains(new Vector2(x, y)))
                                    continue;

                                Block b = blocks[y, x];

                                if (!drawnBlocks.Contains(b))
                                {
                                    drawnBlocks.Add(b);
                                    drawnVectors.Add(b.Location);
                                }
                            }
                        }
                        Thread.Sleep(750);
                    }
                }
            }
            int pX = (int)p.Position.X;
            int pY = (int)p.Position.Y;
            for (int y = -1080 / 2; y <= 1080/2; y += 16)
            {
                for(int x = -1920 / 2; x <= 1920/2; x+= 16) {
                    if(pX + x < 0 || pX + x > 1920 ||
                        pY + y < 0 || pY + y > 1080)
                    {
                        continue;
                    }

                    Block b = blocks[(y + pY) / 16, (x + pX) / 16];

                    drawnBlocks.Add(b);
                }
            }
        }

        public void Draw(Vector2 playerLocation, SpriteBatch spriteBatch, GameTime gameTime)
        {
            isDrawing = true;

            lock(drawnBlocks)
            {
                foreach (Block b in drawnBlocks)
                {
                    b.Draw(spriteBatch);
                }
            }
            isDrawing = false;
        }
    }
}
