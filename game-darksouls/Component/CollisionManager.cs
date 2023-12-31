﻿using Collectible;
using game_darksouls.Collectible;
using game_darksouls.Entity;
using game_darksouls.Level;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    public class CollisionManager
    {
        public LevelSetup CurrentLevel { get; set; }
        public ICollectibleManager CollectibleManager { get; set; }

        public CollisionManager(LevelSetup currentLevel, ICollectibleManager collectibleManager)
        {
            CurrentLevel = currentLevel;
            CollectibleManager = collectibleManager;
        }
        public void CheckIfCollectible(AnimatedObject player)
        {
            foreach (var collectible in CollectibleManager.Collectibles)
            {
                if (player.CollisionBox.Rectangle.Intersects(collectible.CollisionBox.Rectangle))
                {
                    collectible.CollectedGem();
                }
            }
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

        public IEntity CheckForHit(AnimatedObject initiator, Rectangle attackFrame)
        {
            foreach (var entity in CurrentLevel.entitys)
            {
                if (entity == initiator)
                    continue;

                if (attackFrame.Intersects(entity.CollisionBox.Rectangle))
                    return entity;
            }

            return null;
        }
    }
}
