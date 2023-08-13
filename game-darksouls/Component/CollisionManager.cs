using game_darksouls.Entity;
using game_darksouls.Level;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    public class CollisionManager
    {
        public LevelSetup CurrentLevel { get; set; }

        public CollisionManager(LevelSetup currentLevel)
        {
            CurrentLevel = currentLevel;
        }

        public bool CheckForCollision(Box hitbox)
        {

            foreach (var block in CurrentLevel.Tiles)
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

            foreach (var block in CurrentLevel.Tiles)
            {
                if (block.TileBox.Intersects(hitbox))
                {
                    return true;

                }
            }
            return false;
        }

        public AnimatedObject CheckForHit(Rectangle attackFrame)
        {
            foreach (var entity in CurrentLevel.entitys)
            {
                if (attackFrame.Intersects(entity.CollisionBox.Rectangle))
                    return entity;
            }

            return null;
        }
    }
}
