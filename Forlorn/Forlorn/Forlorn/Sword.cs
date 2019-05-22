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
    class Sword : Item
    {
        public Texture2D swordTexture;
        public Rectangle swordRect;
        SpriteFont damageText;
        public int degrees;
        public int swordType;
        public int damage;
        bool isSwinging = false;
        private int timer = 0;
        Vector2 spot;
        ContentManager Content;
        MouseState oldMouse = Mouse.GetState();

        private List<Bats> hitBats;

        //public Sword(int x, int y, int degrees_, ContentManager content, int swordType_)
        //{
        //    switch (swordType_)
        //    {
        //        case 0:
        //            this.swordTexture = content.Load<Texture2D>("Tools/wood sword"); //wood
        //            damage = 5;
        //            break;
        //        case 1:
        //            this.swordTexture = content.Load<Texture2D>("Tools/iron sword"); //iron
        //            damage = 8;
        //            break;
        //        case 2:
        //            this.swordTexture = content.Load<Texture2D>("Tools/gold sword"); //gold
        //            damage = 10;
        //            break;
        //        case 3:
        //            this.swordTexture = content.Load<Texture2D>("Tools/axe"); //platinum
        //            damage = 15;
        //            break;
        //        case 4:
        //            this.swordTexture = content.Load<Texture2D>("Tools/crystal sword"); //crystal
        //            damage = 20;
        //            break;
        //    }
        //    swordRect = new Rectangle(x, y, 30, 30);
        //    degrees = degrees_;
        //    swordType = swordType_;
        //    damageText = content.Load<SpriteFont>("DamageText");
        //    hitBats = new List<Bats>();
        //}

        public Sword(int inStack, bool isHeld, Texture2D texture, int x, int y, int degrees_, ContentManager content, int swordType_) : base(inStack, isHeld, texture)
        {
            bool isHeld_ = isHeld;
            switch (swordType_)
            {
                case 0:
                    this.swordTexture = content.Load<Texture2D>("Tools/wood sword"); //wood
                    damage = 5;
                    break;
                case 1:
                    this.swordTexture = content.Load<Texture2D>("Tools/iron sword"); //iron
                    damage = 8;
                    break;
                case 2:
                    this.swordTexture = content.Load<Texture2D>("Tools/gold sword"); //gold
                    damage = 10;
                    break;
                case 3:
                    this.swordTexture = content.Load<Texture2D>("Tools/axe"); //platinum
                    damage = 15;
                    break;
                case 4:
                    this.swordTexture = content.Load<Texture2D>("Tools/crystal sword"); //crystal
                    damage = 20;
                    break;
            }
            swordRect = new Rectangle(x, y, 30, 30);
            degrees = degrees_;
            swordType = swordType_;
            damageText = content.Load<SpriteFont>("DamageText");
            hitBats = new List<Bats>();
        }

        public void Update(List<Bats> allBats)
        {
            MouseState mouse = Mouse.GetState();
                if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton != ButtonState.Pressed)
                {
                    if (!isSwinging)
                    {
                        isSwinging = true;
                    }
                }

                if (!isSwinging && hitBats.Count != 0)
                {
                    hitBats.Clear();
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

                    for (int i = 0; i < allBats.Count; i++)
                    {
                        if (hitBats.Contains(allBats[i]))
                            continue;

                        if (swordRect.Intersects(allBats[i].getRect()))
                        {
                            allBats[i].batHealth -= 5 + damage;
                            hitBats.Add(allBats[i]);
                            if (allBats[i].batHealth <= 0)
                            {
                                allBats.RemoveAt(i);
                                i--;
                            }
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

        
        //public override void itemTexture()
        //{
        //    this.swordTexture = content.Load<Texture2D>("Tools/wood sword");
        //}
        //public override void isHeld()
        //{
        //    isItemVisible = true;
        //}
        //public override void inStack()
        //{
        //    stackCount = 1;
        //}
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float radians = MathHelper.ToRadians(degrees);
            spot = new Vector2(0, swordTexture.Height);
            //if (isHeld == true)
            //{
            //    spriteBatch.Draw(swordTexture, swordRect, null, Color.White, radians, spot, SpriteEffects.None, 0);
            //}
            spriteBatch.Draw(swordTexture, swordRect, null, Color.White, radians, spot, SpriteEffects.None, 0);
        }

    }
}
