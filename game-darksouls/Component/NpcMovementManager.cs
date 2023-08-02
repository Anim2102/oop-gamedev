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

        private Vector2 SPEED = new Vector2(0.2f, 0.6f);
        private Vector2 direction;

        private MovementState currentMovementState;

        private Box collisionBox;

        

        public NpcMovementManager(AnimatedObject animatedObject, CollisionManager collisionManager, AnimationManager animationManager, Box collisionBox)
        {
            this.animatedObject = animatedObject;
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

           // Debug.WriteLine(direction);
        }

        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = collisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * SPEED.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * SPEED.Y * gameTime.ElapsedGameTime.Milliseconds);
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
            //debugging
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
