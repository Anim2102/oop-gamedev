using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;

namespace game_darksouls.Entity.EntityMovement
{
    internal class GroundMovement : IMovementBehaviour
    {
        private readonly CollisionManager collisionManager;
        private readonly AnimationManager animationManager;

        private Vector2 speed = new Vector2(0.1f, 0.6f);
        private Vector2 direction;

        private MovementState currentMovementState;

        private Box collisionBox;

        public GroundMovement(CollisionManager collisionManager, AnimationManager animationManager, Box collisionBox)
        {
            this.collisionManager = collisionManager;
            this.animationManager = animationManager;


            direction = Vector2.Zero;

            currentMovementState = MovementState.ATTACK;

            this.collisionBox = collisionBox;
        }

        public void Update(GameTime gameTime)
        {
            CheckGravity();
            UpdatePosition(gameTime);
            ChangeMovingState();
            ChangeFlipOnDirection();
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = collisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * speed.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * speed.Y * gameTime.ElapsedGameTime.Milliseconds);
            collisionBox.Rectangle = updatedRectangle;
            collisionBox.Rectangle = updatedRectangle;
        }

        private void CheckGravity()
        {
            Rectangle feetRectangle = new Rectangle(collisionBox.Rectangle.X,
               collisionBox.Rectangle.Y + collisionBox.Rectangle.Height,
               collisionBox.Rectangle.Width, 5);

            if (collisionManager.CheckForCollision(feetRectangle))
            {
                direction.Y = 0;
            }
            else
            {
                direction.Y = 1;
            }
        }

        public void MoveNpc(Vector2 direction)
        {
            this.direction = direction;
        }

        public void ResetDirection()
        {
            this.direction = Vector2.Zero;
        }

        public void ChangeMovingState()
        {
            if (direction.Y > 0)
            {
                currentMovementState = MovementState.FALLING;
            }
            if (direction.X != 0)
            {
                currentMovementState = MovementState.MOVING;
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                currentMovementState = MovementState.IDLE;
            }

            animationManager.UpdateAnimationOnState(currentMovementState);
        }

        public void ChangeFlipOnDirection()
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
