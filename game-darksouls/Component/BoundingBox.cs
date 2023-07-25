using Microsoft.Xna.Framework;

namespace game_darksouls.Component
{
    public class Box
    {
        public Rectangle DrawingRectangle { get; set; }
        public Vector2 Offset { get; set; }

        public Box() { }

        public Box(int posX, int posY, int width, int height, Vector2 offset)
        {
            Offset = offset;
            DrawingRectangle = new Rectangle((int)(posX + offset.X), (int)(posY + offset.Y), width, height);
        }

        public Vector2 CenterOfBox()
        {
            Vector2 centerPoint = new Vector2(DrawingRectangle.X + (DrawingRectangle.Width / 2),
                DrawingRectangle.Y + (DrawingRectangle.Height / 2));

            return centerPoint;
                
        }
    }
}
