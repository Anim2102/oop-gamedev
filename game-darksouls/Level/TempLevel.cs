using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game_darksouls.Level
{
    public class TempLevel
    {
        private static TempLevel instance;
        List<Rectangle> rectangles = new List<Rectangle>();
        Texture2D texture;

        public TempLevel(GraphicsDevice graphicsDevice)
        {
            const int SQUARESIZE = 50;
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new[] { Color.Red });

            int positionX = 0;
            for (int i = 0; i < 10; i++)
            {
                Rectangle squareRect = new Rectangle(positionX, 150, SQUARESIZE, SQUARESIZE);
                rectangles.Add(squareRect);
                positionX += 50;
            }
        }
        public static TempLevel GetInstance(GraphicsDevice graphicsDevice)
        {
            if (instance == null)
            {
                instance = new TempLevel(graphicsDevice);
            }

            return instance;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var square in rectangles)
            {
                spriteBatch.Draw(texture, square, Color.White);
            }
        }
    }
}
