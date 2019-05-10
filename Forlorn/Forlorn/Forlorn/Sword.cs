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
        public Texture2D batTexture;
        public Rectangle swordRect;
        public Rectangle batRect;
        public Rectangle topPoint, midPoint, botPoint;
        public Texture2D topTex, midTex, botTex;
        public int batHealth = 200;
        SpriteFont damageText;

        public int degrees, degrees2;
        public int swordType;
        private int damage;
        bool isSwinging = false;
        private int timer = 0;
        Vector2 spot;
        MouseState oldMouse = Mouse.GetState();

        public Sword(int x, int y, int degrees_, ContentManager content, int swordType_)
        {
            switch (swordType_)
            {
                case 0:
                    this.swordTexture = content.Load<Texture2D>("Tools/wood sword"); //wood
                    damage = 2;
                    break;
                case 1:
                    this.swordTexture = content.Load<Texture2D>("Tools/iron sword"); //iron
                    damage = 3;
                    break;
                case 2:
                    this.swordTexture = content.Load<Texture2D>("Tools/gold sword"); //gold
                    damage = 4;
                    break;
                case 3:
                    this.swordTexture = content.Load<Texture2D>("Tools/axe"); //platinum
                    damage = 5;
                    break;
                case 4:
                    this.swordTexture = content.Load<Texture2D>("Tools/crystal sword"); //crystal
                    damage = 6;
                    break;
            }
            swordRect = new Rectangle(x, y, 30, 30);
            degrees = degrees_;
            swordType = swordType_;
            damageText = content.Load<SpriteFont>("DamageText");
            batRect = new Rectangle(310, 210, 30, 30);
            topPoint = new Rectangle(swordRect.X + 45, swordRect.Y + 16, 10, 10);
            botPoint = new Rectangle(swordRect.X + 16, swordRect.Y + 16, 10, 10);
            midPoint = new Rectangle(swordRect.X + 31, swordRect.Y + 16, 10, 10);
            topTex = content.Load<Texture2D>("Tools/sword");
            midTex = content.Load<Texture2D>("Tools/sword");
            botTex = content.Load<Texture2D>("Tools/sword");
            degrees2 = 0;
            batTexture = content.Load<Texture2D>("bat");
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

                if (swordRect.Intersects(batRect))
                {
                    batHealth -= damage;
                }
            }
            oldMouse = mouse;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float radians = MathHelper.ToRadians(degrees);
            float radians2 = MathHelper.ToRadians(degrees2);
            spot = new Vector2(0, swordTexture.Height);
            
            spriteBatch.Draw(swordTexture, swordRect, null, Color.White, radians, spot, SpriteEffects.None, 0);
            spriteBatch.Draw(botTex, botPoint, null, Color.Red, radians2, spot, SpriteEffects.None, 0);
            spriteBatch.Draw(midTex, midPoint, null, Color.Red, radians2, spot, SpriteEffects.None, 0);
            spriteBatch.Draw(topTex, topPoint, null, Color.Red, radians2, spot, SpriteEffects.None, 0);
            if (batHealth > 0)
            {
                spriteBatch.DrawString(damageText, batHealth + "", new Vector2(0, 100), Color.Red);
                spriteBatch.Draw(batTexture, batRect, Color.Gray);
            }

        }

    }
}
