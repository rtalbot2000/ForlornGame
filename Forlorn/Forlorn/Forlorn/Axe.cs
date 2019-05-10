﻿using System;
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
    class Axe
    {
        public Texture2D axeTexture;
        public Rectangle axeRect;
        public Rectangle batRect;
        public int batHealth;
        public int degrees;
        private int damage;
        bool isSwinging = false;
        private int timer = 0;
        Vector2 spot;
        MouseState oldMouse = Mouse.GetState();

        public Axe(int x, int y, int degrees_, ContentManager content, int axeType_)
        {
            switch (axeType_)
            {
                case 0:
                    this.axeTexture = content.Load<Texture2D>("Tools/iron axe"); //iron
                    damage = 1;
                    break;
                case 1:
                    this.axeTexture = content.Load<Texture2D>("Tools/gold axe"); //gold
                    damage = 2;
                    break;
                case 2:
                    this.axeTexture = content.Load<Texture2D>("Tools/platinum axe"); //platinum
                    damage = 3;
                    break;
            }
            axeRect = new Rectangle(x, y, 30, 30);
            degrees = degrees_;
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
            }
            if (axeRect.Intersects(batRect))
            {
                batHealth -= damage;
            }
            oldMouse = mouse;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float radians = MathHelper.ToRadians(degrees);
            spot = new Vector2(0, axeTexture.Height);

            spriteBatch.Draw(axeTexture, axeRect, null, Color.White, radians, spot, SpriteEffects.None, 0);
        }

    }
}