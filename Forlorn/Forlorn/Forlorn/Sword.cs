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
        public int batHealth = 60;
        SpriteFont damageText;

        public int degrees;
        public int swordType;
        public int damage;
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
                    damage = 5;
                    break;
                case 2:
                    this.swordTexture = content.Load<Texture2D>("Tools/gold sword"); //gold
                    damage = 8;
                    break;
                case 3:
                    this.swordTexture = content.Load<Texture2D>("Tools/axe"); //platinum
                    damage = 12;
                    break;
                case 4:
                    this.swordTexture = content.Load<Texture2D>("Tools/crystal sword"); //crystal
                    damage = 15;
                    break;
            }
            swordRect = new Rectangle(x, y, 30, 30);
            degrees = degrees_;
            swordType = swordType_;
            damageText = content.Load<SpriteFont>("DamageText");
            //bat = Bat(content, new Vector2(200, 1000));
        }

        public void Update(Bats[] allBats)
        {
            MouseState mouse = Mouse.GetState();

            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
            {
                if (!isSwinging)
                {
                    isSwinging = true;
                }
            }

            if (isSwinging == true)
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

                for (int i = 0; i < allBats.Length; i++)
                {
                    if (swordRect.Intersects(allBats[i].getRect()))
                    {
                        batHealth = allBats[i].getHealth();
                        batHealth -= 100;
                    }
                }
            }

            oldMouse = mouse;
        }


        public Texture2D getTexture()
        {
            return swordTexture;
        }

        public Rectangle getRect()
        {
            return swordRect;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float radians = MathHelper.ToRadians(degrees);
            spot = new Vector2(0, swordTexture.Height);

            spriteBatch.Draw(swordTexture, swordRect, null, Color.White, radians, spot, SpriteEffects.None, 0);

            if (batHealth > 0)
            {
                //spriteBatch.DrawString(damageText, batHealth + "", new Vector2(0, 100), Color.Red);
            }

        }

    }
}
