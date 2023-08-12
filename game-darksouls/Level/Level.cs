using game_darksouls.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace game_darksouls.Level
{
    public class Level : ILevel
    {
        private List<Tile> tiles;
        private List<AnimatedObject> entitys;

        public List<Tile> Tiles
        {
            get { return tiles; }
        }

        public List<AnimatedObject> Entitys
        {
            get
            {
                return entitys;
            }
        }

        public Level()
        {

        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (var entity in entitys)
            {
                entity.Update(gameTime);
            }
        }

        
    }
}
