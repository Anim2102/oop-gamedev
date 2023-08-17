using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.GameManaging
{
    public interface IStateGame
    {
        void Start() { }
        void Stop() { }

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

    }
}
