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
    class Wolves
    {
        private Texture2D wolfText;
        private Rectangle wolfRect;
        long speed;
        int leftMostPoint;
        int chaseTimer;
        int spawnTimer;
        Vector2 playerPosition;
        Vector2 wolfPosition;
        Boolean attack = false;
        Random randomGen = new Random();
        public Vector2 Position
        {
            get
            {
                return wolfPosition;
            }
        }
        public Wolves(ContentManager content, Vector2 position)
        {
            wolfText = content.Load<Texture2D>("white");
            leftMostPoint = (int)position.X - 800;
            wolfPosition = new Vector2(randomGen.Next(leftMostPoint + 1800, -10));
            wolfRect = new Rectangle((int)wolfPosition.X, (int)wolfPosition.Y, 20, 40);
            spawnTimer = randomGen.Next(300) + 140;
            speed = randomGen.Next(4);
            playerPosition = position;
        }
        public Texture2D getTexture()
        {
            return wolfText;
        }
        public Rectangle getRect()
        {
            return wolfRect; ;
        }
        public void wolfUpdate(KeyboardState kb)
        {
            if (!attack)
                spawnTimer--;
            if (spawnTimer < 0)
            {
                attack = true;
                spawnTimer = 0;
            }
            if (attack)
            {
            }
        }
    }
}
