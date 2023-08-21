using Component.Health;
using game_darksouls.Component;
using game_darksouls.Component.Health;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity
{
    public interface IEntity
    {
        void TakeDamage();
        Box CollisionBox { get; }
        void StartPosition(Vector2 startPosition);
        void Destroy() { }
        void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}
