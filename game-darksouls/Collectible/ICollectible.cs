using game_darksouls.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Collectible
{
    public interface ICollectible
{
        bool IsCollected { get; }

        Box CollisionBox { get;}

        void CollectedGem() { }

        void Update(GameTime gameTime) { }

        void Draw(SpriteBatch spriteBatch) { }
}
}
