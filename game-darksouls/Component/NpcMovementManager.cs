using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;

namespace game_darksouls.Component
{/*
    internal class NpcMovementManager : IMovementBehaviour, IComponent
    {
        public CollisionManager CollisionManager { get; set; }
        public AnimationManager AnimationManager { get; set; }
        public MovementState CurrentMovementState { get; set; }
        public Box CollisionBox { get; set; }

        private Vector2 SPEED = new Vector2(0.2f, 0.6f);
        private Vector2 Direction;


        public NpcMovementManager(CollisionManager collisionManager, AnimationManager animationManager, Box collisionBox)
        {
            this.CollisionManager = collisionManager;
            this.AnimationManager = animationManager;
            this.CollisionBox = collisionBox;


            Direction = Vector2.Zero;

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

            updatedRectangle.X += (int)(Direction.X * SPEED.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(Direction.Y * SPEED.Y * gameTime.ElapsedGameTime.Milliseconds);
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
                Direction.Y = 0;
            }
            else
            {
                Direction.Y = 1;
            }
        }

        public void Push(Vector2 direction)
        {
            this.Direction = direction;
        }

        public void ResetDirection()
        {
            this.Direction = Vector2.Zero;
        }

        private void ChangeMovingState()
        {
            if (Direction.Y > 0)
            {
                CurrentMovementState = MovementState.FALLING;
            }
            if (Direction.X != 0)
            {
                CurrentMovementState = MovementState.MOVING;
            }
            else if (Direction.X == 0 && Direction.Y == 0)
            {
                CurrentMovementState = MovementState.IDLE;
            }

            AnimationManager.UpdateAnimationOnState(CurrentMovementState);
        }

        private void ChangeFlipOnDirection()
        {
            if (Direction.X > 0)
            {
                AnimationManager.FacingLeft = false;
            }
            else if (Direction.X < 0)
            {
                AnimationManager.FacingLeft = true;
            }
        }
    }
    */
}
