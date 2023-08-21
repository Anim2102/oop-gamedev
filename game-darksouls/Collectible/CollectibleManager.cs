﻿using game_darksouls.Collectible;
using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace Collectible
{
    public class CollectibleManager : ICollectibleManager, IUpdate
    {
        public bool IsComplete
        {
            get
            {
                return RemoveCollectibles.Count == startCrystals;
            }
        }

        private int startCrystals = 0;

        private List<ICollectible> collectibles = new List<ICollectible>();
        public List<ICollectible> Collectibles
        {
            get { return collectibles; }
        }

        private List<ICollectible> removeCollectibles = new List<ICollectible>();
        public List<ICollectible> RemoveCollectibles
        {
            get { return removeCollectibles; }
        }



        public void AddCollectible(ICollectible collectible)
        {
            collectibles.Add(collectible);
            startCrystals++;
        }

        public void Update(GameTime gameTime)
        {
            Debug.WriteLine(IsComplete);
            foreach (var collectible in Collectibles)
            {
                collectible.Update(gameTime);

                if (collectible.IsCollected)
                    removeCollectibles.Add(collectible);
            }

            foreach (var collectibleToRemove in RemoveCollectibles)
            {
                Collectibles.Remove(collectibleToRemove);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var collect in Collectibles)
            {
                collect.Draw(spriteBatch);
            }
        }

    }
}
