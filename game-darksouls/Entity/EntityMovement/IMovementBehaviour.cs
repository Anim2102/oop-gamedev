using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity.EntityMovement
{
    public interface IMovementBehaviour
    { 
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
