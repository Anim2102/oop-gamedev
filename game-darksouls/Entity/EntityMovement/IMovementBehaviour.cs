using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;

namespace game_darksouls.Entity.EntityMovement
{
    public interface IMovementBehaviour
    {
        void Update(GameTime gameTime);
        void UpdatePosition(GameTime gameTime) { }
        void MoveNpc(Vector2 direction) { }
        void ResetDirection() { }
        void ChangeMovingState() { }
        void ChangeFlipOnDirection() { }
    }
}
