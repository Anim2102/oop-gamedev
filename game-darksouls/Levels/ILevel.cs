using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Levels
{
    public interface ILevel
{
        bool IsComplete { get; }
        bool IsLost { get; }
        void Reset();
        void Update(GameTime gameTime) { }
        void Draw(SpriteBatch spriteBatch) { }
}
}
