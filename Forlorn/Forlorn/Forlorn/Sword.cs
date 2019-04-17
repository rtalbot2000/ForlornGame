using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Forlorn
{
    class Sword
    {
        public Texture2D swordTexture;
        public Rectangle swordRect;
        public int degrees;
        public int swordType;
        bool isSwinging = false;
        private int timer = 0;
        Vector2 spot;
        MouseState oldMouse = Mouse.GetState();

        public Sword(int x, int y, int degrees_, ContentManager content, int swordType_)
        {
            switch (swordType_)
            {
                case 0:
                    this.swordTexture = content.Load<Texture2D>("Tools/wood axe"); //wood
                    break;
                case 1:
                    this.swordTexture = content.Load<Texture2D>("Tools/steel axe"); //iron
                    break;
                case 2:
                    this.swordTexture = content.Load<Texture2D>("Tools/gold pickaxe"); //gold
                    break;
                case 3:
                    this.swordTexture = content.Load<Texture2D>("Tools/axe"); //platinum
                    break;
                case 4:
                    this.swordTexture = content.Load<Texture2D>("Tools/crystal pickaxe"); //crystal
                    break;
            }
            swordRect = new Rectangle(x, y, 30, 30);
            degrees = degrees_;
            swordType = swordType_;
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();
 
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
            {
                if (!isSwinging)
                {
                    isSwinging = true;
                }
            }

            if(isSwinging == true)
            {
                timer++;
                if (timer < 20)
                {
                    double swingvelocity = 20 + -2d * timer;
                    degrees += (int)swingvelocity;
                }
                else
                {
                    isSwinging = false;
                    timer = 0;
                }
            }
            oldMouse = mouse;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float radians = MathHelper.ToRadians(degrees);
            spot = new Vector2(0, swordTexture.Height);
            
            spriteBatch.Draw(swordTexture, swordRect, null, Color.White, radians, spot, SpriteEffects.None, 0);
        }

    }
}
