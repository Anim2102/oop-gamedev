using game_darksouls.Entity;
using game_darksouls.Level;
using Microsoft.Xna.Framework;

namespace game_darksouls.Component
{
    internal class CollisionManager
    {
       
        public bool IsOnFloor { get; private set; }
        private AnimatedObject entity;

        public CollisionManager(AnimatedObject entity)
        {
            this.entity = entity;
        }

        public void CheckForGravity()
        {
            IsOnFloor = false;

            foreach (var block in TempLevel.GetInstance().rectangles)
            {
                if (block.Intersects(entity.drawingBox.DrawingRectangle))
                {
                    IsOnFloor = true;
                    break;
                }
            }
        }
    }
}
