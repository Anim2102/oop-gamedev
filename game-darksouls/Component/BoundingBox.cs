using Microsoft.Xna.Framework;

namespace game_darksouls.Component
{
    public class Box
    {
        public Rectangle Rectangle { get; set; }
        public Vector2 Offset { get; set; }
        public Vector2 Position => new Vector2(Rectangle.X, Rectangle.Y);   
        public Box() { }

        public Box(int posX, int posY, int width, int height, Vector2 offset)
        {
            Offset = offset;
            Rectangle = new Rectangle((int)(posX + offset.X), (int)(posY + offset.Y), width, height);
        }

        public Vector2 CenterOfBox()
        {
            Vector2 centerPoint = new Vector2(Rectangle.X + (Rectangle.Width / 2),
                Rectangle.Y + (Rectangle.Height / 2));

            return centerPoint;
                
        }

        public void UpdatePosition(Vector2 position)
        {
            Rectangle newPosition = new Rectangle((int)(position.X + Offset.X),
                (int)(position.Y + Offset.Y)
                ,Rectangle.Width,Rectangle.Height);
            this.Rectangle = newPosition;
        }
    }
}
