using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game_darksouls.Entity.EntityMovement
{
    internal class FlyMovement : IMovementBehaviour
    {
        private readonly CollisionManager collisionManager;
        private readonly AnimationManager animationManager;

        private Vector2 SPEED = new Vector2(0.1f, 0.1f);
        private Vector2 direction;

        private MovementState currentMovementState;

        private Box collisionBox;

        public FlyMovement(CollisionManager collisionManager, AnimationManager animationManager, Box collisionBox)
        {
            this.collisionManager = collisionManager;
            this.animationManager = animationManager;
            this.collisionBox = collisionBox;

            direction = Vector2.Zero;
            currentMovementState = MovementState.ATTACK;
        }

        public void Update(GameTime gameTime)
        {
            UpdatePosition(gameTime);
            ChangeMovingState();
            ChangeFlipOnDirection();
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = collisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * SPEED.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * SPEED.Y * gameTime.ElapsedGameTime.Milliseconds);
            collisionBox.Rectangle = updatedRectangle;
            collisionBox.Rectangle = updatedRectangle;
        }

        public void MoveNpc(Vector2 direction)
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
                currentMovementState = MovementState.MOVING;
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                currentMovementState = MovementState.IDLE;
            }

            animationManager.UpdateAnimationOnState(currentMovementState);
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

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle feetRectangle = new Rectangle(collisionBox.Rectangle.X,
              collisionBox.Rectangle.Y + collisionBox.Rectangle.Height,
              collisionBox.Rectangle.Width, 5);

            Rectangle sideLeftRectangle = new Rectangle(collisionBox.Rectangle.X,
                collisionBox.Rectangle.Y,
                5, collisionBox.Rectangle.Height);

            Rectangle sideRightRectangle = new Rectangle(collisionBox.Rectangle.X + collisionBox.Rectangle.Width,
                collisionBox.Rectangle.Y,
                5, collisionBox.Rectangle.Height);

            spriteBatch.Draw(Game1.redsquareDebug, sideRightRectangle, Color.Green);

            spriteBatch.Draw(Game1.redsquareDebug, sideLeftRectangle, Color.Green);
            spriteBatch.Draw(Game1.redsquareDebug, feetRectangle, Color.Black);
        }
    }
}
