using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;

namespace game_darksouls.Component
{
    internal class NpcMovementManager : IMovementBehaviour, IComponent
    {
        public CollisionManager CollisionManager { get; set; }
        public AnimationManager AnimationManager { get; set; }
        public MovementState CurrentMovementState { get; set; }
        public Box CollisionBox { get; set; }

        private Vector2 SPEED = new Vector2(0.2f, 0.6f);
        private Vector2 direction;


        public NpcMovementManager(CollisionManager collisionManager, AnimationManager animationManager, Box collisionBox)
        {
            this.CollisionManager = collisionManager;
            this.AnimationManager = animationManager;
            this.CollisionBox = collisionBox;


            direction = Vector2.Zero;

            CurrentMovementState = MovementState.ATTACK;

        }
        public void Update(GameTime gameTime)
        {
            CheckGravity();
            UpdatePosition(gameTime);
            ChangeMovingState();
            ChangeFlipOnDirection();

            // Debug.WriteLine(direction);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = CollisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * SPEED.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * SPEED.Y * gameTime.ElapsedGameTime.Milliseconds);
            CollisionBox.Rectangle = updatedRectangle;
            CollisionBox.Rectangle = updatedRectangle;
        }

        private void CheckGravity()
        {
            Rectangle feetRectangle = new Rectangle(CollisionBox.Rectangle.X,
               CollisionBox.Rectangle.Y + CollisionBox.Rectangle.Height,
               CollisionBox.Rectangle.Width, 5);

            if (CollisionManager.CheckForCollision(feetRectangle))
            {
                direction.Y = 0;
            }
            else
            {
                direction.Y = 1;
            }
        }

        public void Push(Vector2 direction)
        {
            this.direction = direction;
        }

        public void ResetDirection()
        {
            this.direction = Vector2.Zero;
        }

        private void ChangeMovingState()
        {
            if (direction.Y > 0)
            {
                CurrentMovementState = MovementState.FALLING;
            }
            if (direction.X != 0)
            {
                CurrentMovementState = MovementState.MOVING;
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                CurrentMovementState = MovementState.IDLE;
            }

            AnimationManager.UpdateAnimationOnState(CurrentMovementState);
        }

        private void ChangeFlipOnDirection()
        {
            if (direction.X > 0)
            {
                AnimationManager.FacingLeft = false;
            }
            else if (direction.X < 0)
            {
                AnimationManager.FacingLeft = true;
            }
        }
    }
}
