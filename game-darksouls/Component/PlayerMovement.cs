using game_darksouls.Entity;
using game_darksouls.Enum;
using game_darksouls.Input;
using game_darksouls.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class PlayerMovement : IComponent
    {
        private readonly Player player;
        private CollisionManager collisionManager;
        private readonly AnimationManager playerAnimation;
        private MovementState currentMovingState;
        private InputManager inputManager;


        private bool onFloor;
        private bool IsJumping;
        private float JumpTime;

        private const float maxJumpDuration = 0.2f;

        private Vector2 speed;

        public PlayerMovement(Player player, AnimationManager playerAnimation)
        {

            //rework dependency injection either player or playeranimation or more specific
            this.player = player;
            this.playerAnimation = playerAnimation;
            this.collisionManager = new();
            this.inputManager = new();

            currentMovingState = MovementState.IDLE;
            speed = new Vector2(0.2f, 0.2f);
            onFloor = false;
            IsJumping = false;
            JumpTime = 0;

        }

        public void Update(GameTime gameTime)
        {
            var direction = inputManager.GetInput();
            
            direction = ApplyGravity(direction);
            direction = JumpPlayer(gameTime, direction);
            MovePlayer(direction,gameTime);
            ChangeMovingState(direction);
            ChangeFlipOnDirection(direction);
        }

        private Vector2 JumpPlayer(GameTime gameTime,Vector2 direction)
        {
           //Debug.WriteLine("jumping: " + IsJumping + " " + "onFloor; "
             //   + onFloor + "up button: " + inputManager.IsJumpButtonPress());

            if (!IsJumping && onFloor && inputManager.IsJumpButtonPress())
            {
                IsJumping = true;
                JumpTime = 0;
            }
            if (IsJumping)
            {
                JumpTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (JumpTime < maxJumpDuration)
                {
                    direction.Y -= (float)(0.1f * gameTime.ElapsedGameTime.Milliseconds);
                }
                else
                    IsJumping = false;
            }
            return direction;

        }
        private void MovePlayer(Vector2 direction, GameTime gameTime)
        {
            
            Rectangle updatedRectangle = player.drawingBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * speed.X * gameTime.ElapsedGameTime.Milliseconds);
            if (!collisionManager.CheckForCollision(updatedRectangle))
            {
                player.drawingBox.Rectangle = updatedRectangle;
            }
            updatedRectangle.Y += (int)(direction.Y * speed.Y * gameTime.ElapsedGameTime.Milliseconds);

            if (!collisionManager.CheckForCollision(updatedRectangle))
            {
                player.drawingBox.Rectangle = updatedRectangle;
                onFloor = false;
            }
            else
            {
                onFloor = true;
                IsJumping = false;
            }

            //player.drawingBox.DrawingRectangle = updatedRectangle;

        }

        private Vector2 ApplyGravity(Vector2 direction)
        {
            //Debug.WriteLine("before: " + onFloor);
            Rectangle feetRectangle = new Rectangle(player.drawingBox.Rectangle.X,
                player.drawingBox.Rectangle.Y + player.drawingBox.Rectangle.Height,
                player.drawingBox.Rectangle.Width, 5);
            
            onFloor = collisionManager.CheckForCollision(feetRectangle);
           // Debug.WriteLine(onFloor);
            if (!onFloor && !IsJumping)
            {
                //Debug.WriteLine("hit if");
                direction.Y += 1;
            }

            return direction;
        }

        private void ChangeFlipOnDirection(Vector2 direction)
        {
            if (direction.X > 0)
            {
                playerAnimation.FacingLeft = false;
            }
            else if (direction.X < 0)
            {
                playerAnimation.FacingLeft = true;
            }
        }
        private void ChangeMovingState(Vector2 direction)
        {
            if (direction.Y > 0)
            {
                currentMovingState = MovementState.FALLING;
            }
            if (direction.X != 0)
            {
                currentMovingState = MovementState.MOVING;
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                currentMovingState = MovementState.IDLE;
            }

            playerAnimation.UpdateAnimationOnState(currentMovingState);
        }

        /*private void FlipOnMovement(Vector2 direction)
        {
            if (direction.X > 0)
            {
                playerAnimation.FlipAnimation = false;
            }
            else
                playerAnimation.FlipAnimation = true;
        }
        */
    }
}
