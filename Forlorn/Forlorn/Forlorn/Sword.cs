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
    class Sword : AnimatedSprite
    {
        private string currentAnim = "Idle";
        private SpriteEffects flip = SpriteEffects.None;

        public Texture2D swordTexture;
        public Rectangle swordRect = new Rectangle(50, 50, 50, 50);
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Sword(IServiceProvider serviceProvider, string path)
        {
            content = new ContentManager(serviceProvider, "Content");
            swordTexture = content.Load<Texture2D>("sword");
            swordRect = new Rectangle(200, 200, 50, 50);
        }

        public void LoadContent()
        {
            SpriteTextures.Add(Content.Load<Texture2D>("player"));
            Animation anim = new Animation();
            anim.LoadAnimation("Idle", 0, new List<int>
            {
                0,
                11,
                0,
                12
            }, 7, true);
            SpriteAnimations.Add("Idle", anim);
            anim = new Animation();
            anim.LoadAnimation("Walking", 0, new List<int>
            {
                0,
                1,
                2,
                3,
                4,
                5,
                6
            }, 7, true);
            SpriteAnimations.Add("Walking", anim);
            anim = new Animation();
            anim.LoadAnimation("Jump", 0, new List<int>
            {
                7,
                8,
                9,
                10,
                9,
                8,
                7
            }, 20, false);
            anim.AnimationCallBack(JumpAnimEnd);
            SpriteAnimations.Add("Jump", anim);

            anim = new Animation();
            anim.LoadAnimation("Dead", 0, new List<int>
            {
            13,
            14,
            15
            }, 6, false);
            //anim.AnimationCallBack(DeadAnimEnd);
            SpriteAnimations.Add("Dead", anim);
        }
        
        public void Reset()
        {
            SpriteAnimations[currentAnim].Stop();
            currentAnim = "Idle";
            SpriteAnimations[currentAnim].ResetPlay();
        }
        public void JumpAnimEnd()
        {
            currentAnim = "Idle";
            SpriteAnimations[currentAnim].Play();
        }

        public void Update(GameTime gameTime)
        {
            SpriteAnimations[currentAnim].Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //Rectangle source = GetFrameRectangle(SpriteAnimations[currentAnim].FrameToDraw);
            //spriteBatch.Draw(SpriteTextures[0], swordRect, source, Color.White);
            spriteBatch.Draw(swordTexture, swordRect, Color.White);
        }
        public void Dispose()
        {
            Content.Unload();
        }
        //private void RectangleInterection(SpriteBatch spriteBatch)
        //{
        //    Rectangle rectangle1 = new Rectangle(50, 50, 200, 100);
        //    Rectangle rectangle2 = new Rectangle(70, 20, 100, 200);
        //    Rectangle rectangle3 = new Rectangle();

        //    spriteBatch.Draw(swordTexture, rectangle1, Color.Gray);
        //    spriteBatch.Draw(swordTexture, rectangle2, Color.White);

        //    e.Graphics.DrawRectangle(Pens.Black, rectangle1);
        //    e.Graphics.DrawRectangle(Pens.Red, rectangle2);

        //    if (rectangle1.Intersects(rectangle2))
        //    {
        //        rectangle3 = Rectangle.Intersect(rectangle1, rectangle2);
        //        if (!rectangle3.IsEmpty)
        //        {
        //            spriteBatch.Draw(swordTexture, rectangle1, Color.Green);
        //            e.Graphics.FillRectangle(Brushes.Green, rectangle3);
        //        }
        //    }
        //}
    }
}
