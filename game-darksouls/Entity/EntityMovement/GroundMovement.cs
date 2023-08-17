using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;

namespace game_darksouls.Entity.EntityMovement
{
    internal class GroundMovement : IMovementBehaviour
    {
        public CollisionManager CollisionManager { get; set; }
        private readonly IAnimationManager animationManager;
        public Box CollisionBox { get; set; }
        public MovementState CurrentMovementState { get; set; }

        private Vector2 speed = new Vector2(0.1f, 0.6f);
        private Vector2 direction;

        public GroundMovement(CollisionManager collisionManager, IAnimationManager animationManager, Box collisionBox)
        {
            this.CollisionManager = collisionManager;
            this.animationManager = animationManager;
            this.CurrentMovementState = MovementState.ATTACK;
            this.CollisionBox = collisionBox;

            direction = Vector2.Zero;


        }

        public void Update(GameTime gameTime)
        {
            //Debug.WriteLine(currentMovementState);
            CheckGravity();
            UpdatePosition(gameTime);
            ChangeMovingState();
            ChangeFlipOnDirection();
        }
       
        private void UpdatePosition(GameTime gameTime)
        {
            Rectangle updatedRectangle = CollisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * speed.X * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(direction.Y * speed.Y * gameTime.ElapsedGameTime.Milliseconds);

            if (!CollisionManager.CheckForCollision(updatedRectangle))
            {
                CollisionBox.Rectangle = updatedRectangle;
            }
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
