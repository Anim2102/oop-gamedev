using game_darksouls.Entity;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class NpcMovementManager : IComponent
    {
        private readonly AnimatedObject animatedObject;
        private readonly CollisionManager collisionManager;
        private AnimationManager animationManager;

        private Vector2 SPEED = new Vector2(0.2f, 0.4f);
        private Vector2 direction;

        private MovementState currentMovementState;

        public NpcMovementManager(AnimatedObject animatedObject, CollisionManager collisionManager, AnimationManager animationManager)
        {
            this.animatedObject = animatedObject;
            this.collisionManager = collisionManager;
            this.animationManager = animationManager;


            direction = Vector2.Zero;

            currentMovementState = MovementState.IDLE;
        }
        public void Update(GameTime gameTime)
        {
            CheckGravity();
            UpdatePosition(gameTime);
            ChangeMovingState();
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = animatedObject.drawingBox.DrawingRectangle;

            updatedRectangle.X += (int)(direction.X * SPEED.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * SPEED.Y * gameTime.ElapsedGameTime.Milliseconds);
            animatedObject.drawingBox.DrawingRectangle = updatedRectangle;
            animatedObject.drawingBox.DrawingRectangle = updatedRectangle;
        }

        private void CheckGravity()
        {
            Rectangle feetRectangle = new Rectangle(animatedObject.drawingBox.DrawingRectangle.X,
               animatedObject.drawingBox.DrawingRectangle.Y + animatedObject.drawingBox.DrawingRectangle.Height,
               animatedObject.drawingBox.DrawingRectangle.Width, 5);
            
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

        private void ChangeMovingState()
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
        public void Draw(SpriteBatch spriteBatch)
        {
            //debugging
            Rectangle feetRectangle = new Rectangle(animatedObject.drawingBox.DrawingRectangle.X,
               animatedObject.drawingBox.DrawingRectangle.Y + animatedObject.drawingBox.DrawingRectangle.Height,
               animatedObject.drawingBox.DrawingRectangle.Width, 5);

            Rectangle sideLeftRectangle = new Rectangle(animatedObject.drawingBox.DrawingRectangle.X,
                animatedObject.drawingBox.DrawingRectangle.Y,
                5, animatedObject.drawingBox.DrawingRectangle.Height);

            Rectangle sideRightRectangle = new Rectangle(animatedObject.drawingBox.DrawingRectangle.X + animatedObject.drawingBox.DrawingRectangle.Width,
                animatedObject.drawingBox.DrawingRectangle.Y,
                5, animatedObject.drawingBox.DrawingRectangle.Height);

            spriteBatch.Draw(Game1.redsquareDebug, sideRightRectangle, Color.Green);

            spriteBatch.Draw(Game1.redsquareDebug, sideLeftRectangle, Color.Green);
            spriteBatch.Draw(Game1.redsquareDebug, feetRectangle, Color.Black);
        }
    }
}
