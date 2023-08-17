using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity.EntityMovement
{
    internal class FlyMovement : IMovementBehaviour
    {
        public CollisionManager CollisionManager { get; set; }
        private readonly IAnimationManager animationManager;

        public Box CollisionBox { get; set; }
        public MovementState CurrentMovementState { get; set; }

        private Vector2 SPEED = new Vector2(0.1f, 0.1f);
        private Vector2 direction;

        public FlyMovement(CollisionManager collisionManager, IAnimationManager animationManager, Box collisionBox)
        {
            this.CollisionManager = collisionManager;
            this.animationManager = animationManager;
            this.CollisionBox = collisionBox;

            CurrentMovementState = MovementState.ATTACK;
            direction = Vector2.Zero;
        }

        public void Update(GameTime gameTime)
        {
            UpdatePosition(gameTime);
            ChangeMovingState();
            ChangeFlipOnDirection();
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = CollisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * SPEED.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * SPEED.Y * gameTime.ElapsedGameTime.Milliseconds);
            MoveWithCollision(updatedRectangle);


        }
        private void MoveWithCollision(Rectangle futurePosition)
        {
            if (!CollisionManager.CheckForCollision(futurePosition))
            {
                Vector2 future = new Vector2(futurePosition.X, futurePosition.Y);
                CollisionBox.UpdatePosition(future);
            }
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

        private void ChangeFlipOnDirection()
        {
            if (direction.X > 0)
            {
                animationManager.FacingLeft = false;
            }
            else if (direction.X < 0)
            {
                animationManager.FacingLeft = true;
            }
        }

   
    }
}
