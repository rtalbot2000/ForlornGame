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

        public Camera(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Update(Vector2 playerPosition)
        {
            position.X = playerPosition.X - (ScreenWidth / 2);
            position.Y = playerPosition.Y - (ScreenHeight / 2);

            if(position.X < 0)
            {
                position.X = 0;
            }
            if(position.Y < 0)
            {
                position.Y = 0;
            }

            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));
        }
    }
}
