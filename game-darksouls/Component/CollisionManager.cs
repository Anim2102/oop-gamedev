using game_darksouls.Entity;
using game_darksouls.Level;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    internal class CollisionManager
    {
        public CollisionManager(AnimatedObject entity)
        {
        }

        public bool CheckForGravity(List<Rectangle> blocks, Box hitbox)
        {

            foreach (var block in TempLevel.GetInstance().rectangles)
            {
                if (block.Intersects(hitbox.DrawingRectangle))
                {
                    return true;
                    
                }
            }
            return false;
        }
    }
}
