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
                for (int y = 600; y < 800; y++)
                {
                    blocks[y, x] = new Block(1, x, y, testPixel);
                }
            }
        }

        public void Update()
        {
            //foreach(Block b in blocks)
            //{
            //    b.UpdateOffset(400, 700);
            //}
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
