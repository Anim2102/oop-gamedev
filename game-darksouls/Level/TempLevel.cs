using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game_darksouls.Level
{
    public class TempLevel
    {
        private static TempLevel instance;
        public readonly List<Rectangle> rectangles = new List<Rectangle>();
        

        public TempLevel()
        {
            const int SQUARESIZE = 50;

            int positionX = 0;
            for (int i = 0; i < 50; i++)
            {
                Rectangle squareRect = new Rectangle(positionX, 150, SQUARESIZE, SQUARESIZE);
                rectangles.Add(squareRect);
                positionX += 50;
            }

            //rectangles.Add(new Rectangle(100,100,SQUARESIZE, SQUARESIZE));
        }
        public static TempLevel GetInstance()
        {
            if (instance == null)
            {
                instance = new TempLevel();
            }

            return instance;
        }
        public void Draw(SpriteBatch spriteBatch,Texture2D texture)
        {
            foreach (var square in rectangles)
            {
                spriteBatch.Draw(texture, square, Color.White);
            }
        }
    }
}
