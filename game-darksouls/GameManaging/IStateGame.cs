using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.GameManaging
{
    public interface IStateGame
    {
        void Play();
        void Stop();

        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);

    }
}
