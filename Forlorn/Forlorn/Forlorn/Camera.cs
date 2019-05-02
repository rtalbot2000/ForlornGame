using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Forlorn
{
    class Camera
    {
        Vector2 position;
        private Matrix viewMatrix;
        private int width;
        private int height;

        public Matrix ViewMatrix
        {
            get
            {
                return viewMatrix;
            }
        }

        public int ScreenWidth
        {
            get
            {
                return width;
            }
        }

        public int ScreenHeight
        {
            get
            {
                return height;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Camera(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Update(Player player)
        {
            position.X = (player.CameraRectangle.X + player.CameraRectangle.Width / 2) - (ScreenWidth / 2);
            position.Y = (player.CameraRectangle.Y + player.CameraRectangle.Height / 2) - (ScreenHeight / 2) + 300;

            
            if(position.X < 0)
            {
                position.X = 0;
            }
            if(position.Y < 0)
            {
                position.Y = 0;
            }
            if(position.X > 12000 - (ScreenWidth / 2))
            {
                position.X = 12000 - (ScreenWidth / 2);
            }
            if(position.Y > 12000 - (ScreenHeight / 2) + 300)
            {
                position.Y = 12000 - (ScreenHeight / 2) + 300;
            }

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}
