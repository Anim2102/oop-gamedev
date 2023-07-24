using game_darksouls.Entity;
using game_darksouls.Level;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    internal class CollisionManager
    {
        private readonly TempLevel tempLevel;
        public CollisionManager()
        {
            tempLevel = TempLevel.GetInstance();
        }

        public bool CheckForCollision(Box hitbox)
        {

            foreach (var block in tempLevel.rectangles)
            {
                if (block.Intersects(hitbox.DrawingRectangle))
                {
                    return true;
                    
                }
            }
            return false;
        }

        public bool CheckForCollision(Rectangle hitbox)
        {

            foreach (var block in tempLevel.rectangles)
            {
                if (block.Intersects(hitbox))
                {
                    return true;

                }
            }
            return false;
        }
    }
}
