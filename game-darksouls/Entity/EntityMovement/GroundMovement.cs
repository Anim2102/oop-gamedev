using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.EntityMovement
{
    internal class GroundMovement : EntityBaseMovement,IMovementBehaviour
    {
        public MovementState CurrentMovementState { get; set; }


        public GroundMovement(CollisionManager collisionManager, IAnimationManager animationManager, Box collisionBox) : base(collisionBox,collisionManager,animationManager)
        {
            
            this.CurrentMovementState = MovementState.IDLE;
            direction = Vector2.Zero;
            speed = new Vector2(0.1f, 0.6f); ;
        }

        public override void Update(GameTime gameTime)
        {
            CheckGravity();
            ChangeMovingState();
            ChangeFlipOnDirection();
            base.Update(gameTime);
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

        void IMovementBehaviour.Push(Vector2 direction)
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
                this.CurrentMovementState = MovementState.FALLING;
            }
            if (direction.X != 0)
            {
                this.CurrentMovementState = MovementState.MOVING;
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                this.CurrentMovementState = MovementState.IDLE;
            }

            animationManager.UpdateAnimationOnState(this.CurrentMovementState);
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
