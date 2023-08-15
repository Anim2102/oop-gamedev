using game_darksouls.Collectible;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collectible
{
    public interface ICollectibleManager
    {
        bool IsComplete { get; }
        List<ICollectible> Collectibles { get; }
        List<ICollectible> RemoveCollectibles { get; }
        void AddCollectible(ICollectible collectible) { }
        void Update(GameTime gameTime) { }
        void Draw(SpriteBatch spriteBatch) { }
    }
}
