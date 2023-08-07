using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;

namespace game_darksouls.Entity.EntityMovement
{
    internal interface IMovementBehaviour
    {

        CollisionManager CollisionManager { get; set; }
        AnimationManager AnimationManager { get; set; }
        Box CollisionBox { get; set; }
        MovementState CurrentMovementState { get; set; }

        void Update(GameTime gameTime);
        void UpdatePosition(GameTime gameTime) { }
        void Push(Vector2 direction);
        void ResetDirection() { }
        void ChangeMovingState() { }
        void ChangeFlipOnDirection() { }
    }
}
