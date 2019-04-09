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
        Vector2 spot;
        MouseState oldMouse = Mouse.GetState();
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Sword(int x, int y, int degrees_, ContentManager content)
        {
            this.swordTexture = content.Load<Texture2D>("Sword/sword");
            swordRect = new Rectangle(x, y, 10, 50);
            degrees = degrees_;
        }

        public void Update(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released);
            {
                degrees += 20;
            }

            oldMouse = mouse;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float radians = MathHelper.ToRadians(degrees);
            spot = new Vector2(swordRect.Width / 2, swordRect.Bottom / 2);
            
            spriteBatch.Draw(swordTexture, swordRect, null, Color.White, radians, spot, SpriteEffects.None, 0);
        }

        public Texture2D getTexture()
        {
            return swordTexture;
        }
        public Rectangle getRect()
        {
            return swordRect;
        }

    }
}
