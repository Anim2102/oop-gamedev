using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity.EntityMovement
{
    internal class FlyMovement : EntityBaseMovement, IMovementBehaviour
    {

        public MovementState CurrentMovementState { get; set; }

        public FlyMovement(CollisionManager collisionManager, IAnimationManager animationManager, Box collisionBox) : base(collisionBox,collisionManager,animationManager)
        {

            CurrentMovementState = MovementState.ATTACK;
            direction = Vector2.Zero;
            speed = new Vector2(0.1f, 0.1f);

        }

        public override void Update(GameTime gameTime)
        {
            ChangeMovingState();
            base.Update(gameTime);
        }

        void IMovementBehaviour.Push(Vector2 direction)
        {
            this.direction = direction;
        }

        public void ResetDirection()
        {
            this.direction = Vector2.Zero;
        }

        private void ChangeMovingState()
        {
            if (direction.X != 0)
            {
                CurrentMovementState = MovementState.MOVING;
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                CurrentMovementState = MovementState.IDLE;
            }

            animationManager.UpdateAnimationOnState(CurrentMovementState);
        }

        

   
    }
}
