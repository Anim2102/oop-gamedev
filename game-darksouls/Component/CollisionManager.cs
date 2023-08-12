using game_darksouls.Entity;
using game_darksouls.Level;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    public class CollisionManager
    {
        private readonly LevelOneTemp tempLevel;
        public CollisionManager()
        {
            tempLevel = LevelOneTemp.GetInstance();
        }

        public bool CheckForCollision(Box hitbox)
        {

            foreach (var block in LevelOneTemp.GetInstance().Tiles)
            {
                if (block.TileBox.Intersects(hitbox.Rectangle))
                {
                    return true;

                }
            }
            return false;
        }

        public bool CheckForCollision(Rectangle hitbox)
        {

            foreach (var block in LevelOneTemp.GetInstance().Tiles)
            {
                if (block.TileBox.Intersects(hitbox))
                {
                    return true;

                }
            }
            return false;
        }
    }
}
