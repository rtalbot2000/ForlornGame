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
    class Pickaxe
    {
        public Texture2D pickTexture;
        public Rectangle pickRect;
        public int degrees;
        private int damage;
        public int pickType;
        bool isSwinging = false;
        private int timer = 0;
        Vector2 spot;
        private List<Bats> hitBats;
        MouseState oldMouse = Mouse.GetState();

        public Pickaxe(int x, int y, int degrees_, ContentManager content, int pickType_)
        {
            switch (pickType_)
            {
                case 0:
                    this.pickTexture = content.Load<Texture2D>("Tools/wood axe"); //wood
                    damage = 1;
                    break;
                case 1:
                    this.pickTexture = content.Load<Texture2D>("Tools/steel axe"); //iron
                    damage = 3;
                    break;
                case 2:
                    this.pickTexture = content.Load<Texture2D>("Tools/gold pickaxe"); //gold
                    damage = 5;
                    break;
                case 3:
                    this.pickTexture = content.Load<Texture2D>("Tools/platinum axe"); //platinum
                    damage = 6;
                    break;
                case 4:
                    this.pickTexture = content.Load<Texture2D>("Tools/crystal pickaxe"); //crystal
                    damage = 7;
                    break;
            }
            pickRect = new Rectangle(x, y, 30, 30);
            degrees = degrees_;
            pickType = pickType_;
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

                    if (pickRect.Intersects(allBats[i].getRect()))
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

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float radians = MathHelper.ToRadians(degrees);
            spot = new Vector2(0, pickTexture.Height);

            spriteBatch.Draw(pickTexture, pickRect, null, Color.White, radians, spot, SpriteEffects.None, 0);
        }

    }
}
