using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;

namespace game_darksouls.Entity.EntityMovement
{
    public interface IMovementBehaviour
    {

        public CollisionManager CollisionManager { get; set; }
        public AnimationManager AnimationManager { get; set; }
        public Box CollisionBox { get; set; }
        public MovementState CurrentMovementState { get; set; }

        void Update(GameTime gameTime);
        void UpdatePosition(GameTime gameTime) { }
        void Push(Vector2 direction);
        void ResetDirection() { }
        void ChangeMovingState() { }
        void ChangeFlipOnDirection() { }
        void PushAfterHit(Vector2 pushDirection) { }
    }
}
