using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game_darksouls.Entity.EntityMovement
{
    internal class FlyMovement : IMovementBehaviour
    {
        public CollisionManager CollisionManager { get; set; }
        public AnimationManager AnimationManager { get; set; }
        public Box CollisionBox { get; set; }
        public MovementState CurrentMovementState { get; set; }

        private Vector2 SPEED = new Vector2(0.1f, 0.1f);
        private Vector2 direction;

        public FlyMovement(CollisionManager collisionManager, AnimationManager animationManager, Box collisionBox)
        {
            this.CollisionManager = collisionManager;
            this.AnimationManager = animationManager;
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

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle feetRectangle = new Rectangle(CollisionBox.Rectangle.X,
              CollisionBox.Rectangle.Y + CollisionBox.Rectangle.Height,
              CollisionBox.Rectangle.Width, 5);

            Rectangle sideLeftRectangle = new Rectangle(CollisionBox.Rectangle.X,
                CollisionBox.Rectangle.Y,
                5, CollisionBox.Rectangle.Height);

            Rectangle sideRightRectangle = new Rectangle(CollisionBox.Rectangle.X + CollisionBox.Rectangle.Width,
                CollisionBox.Rectangle.Y,
                5, CollisionBox.Rectangle.Height);

            spriteBatch.Draw(Game1.redsquareDebug, sideRightRectangle, Color.Green);

            spriteBatch.Draw(Game1.redsquareDebug, sideLeftRectangle, Color.Green);
            spriteBatch.Draw(Game1.redsquareDebug, feetRectangle, Color.Black);
        }

        
    }
}
