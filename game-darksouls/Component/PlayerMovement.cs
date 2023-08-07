using game_darksouls.Entity;
using game_darksouls.Enum;
using game_darksouls.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Component
{
    internal class PlayerMovement : IComponent
    {
        private readonly Player player;
        private CollisionManager collisionManager;
        private readonly AnimationManager playerAnimation;
        private MovementState currentMovingState;
        private InputManager inputManager;

        private Vector2 direction;
        private Vector2 speed;
        private Vector2 velocity;

        private bool onFloor;

        public PlayerMovement(Player player, CollisionManager collisionManager, AnimationManager playerAnimation, InputManager inputManager)
        {
            this.player = player;
            this.collisionManager = collisionManager;
            this.playerAnimation = playerAnimation;
            this.inputManager = inputManager;

            this.direction = Vector2.Zero;
            this.onFloor = false;

            speed = new Vector2(0.3f, 0.3f);
            velocity = new Vector2(1, 1);
        }

        public void Update(GameTime gameTime)
        {

            //direction = ApplyGravity(direction);
            //direction = JumpPlayer(gameTime, direction);
            //MovePlayer(direction,gameTime);
            CheckFloor();
            ApplyGravity(gameTime);
            Move(gameTime);
            ChangeMovingState(direction);
            ChangeFlipOnDirection(direction);


        }
        private void Move(GameTime gameTime)
        {
            direction = inputManager.GetInput();
            Vector2 currentPosition = this.player.collisionBox.Position;
            Vector2 futurePosition = currentPosition;

            if (!direction.Equals(Vector2.Zero))
            {
                currentPosition = this.player.collisionBox.Position;
                futurePosition = currentPosition + direction * velocity * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            }

            Rectangle newPosition = new Rectangle((int)futurePosition.X, (int)futurePosition.Y, player.collisionBox.Rectangle.Width,
                player.collisionBox.Rectangle.Height);

            if (!collisionManager.CheckForCollision(newPosition))
                this.player.collisionBox.UpdatePosition(futurePosition);
        }


        private void ApplyGravity(GameTime gameTime)
        {
            if (!onFloor)
            {
                Vector2 currentPosition = this.player.collisionBox.Position;
                Vector2 futurePosition = currentPosition + new Vector2(0, velocity.Y * speed.Y * (float)gameTime.ElapsedGameTime.TotalMilliseconds);

                this.player.collisionBox.UpdatePosition(futurePosition);
            }

        }
        private void CheckFloor()
        {
            Rectangle feet = new Rectangle(this.player.collisionBox.Rectangle.X,
                this.player.collisionBox.Rectangle.Y + this.player.collisionBox.Rectangle.Height,
                this.player.collisionBox.Rectangle.Width,
                10);

            onFloor = collisionManager.CheckForCollision(feet);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle feet = new Rectangle(this.player.collisionBox.Rectangle.X,
                this.player.collisionBox.Rectangle.Y + this.player.collisionBox.Rectangle.Height - 10,
                this.player.collisionBox.Rectangle.Width,
                10);

            spriteBatch.Draw(Game1.redsquareDebug, feet, Color.Red);
        }
        /*
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
        */


        /*private Vector2 JumpPlayer(GameTime gameTime,Vector2 direction)
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
        */
        /*
        private void MovePlayer(Vector2 direction, GameTime gameTime)
        {
            
            Rectangle updatedRectangle = player.collisionBox.Rectangle;

            updatedRectangle.X += (int)(direction.X * speed.X * gameTime.ElapsedGameTime.Milliseconds);
            if (!collisionManager.CheckForCollision(updatedRectangle))
            {
                player.collisionBox.Rectangle = updatedRectangle;
            }
            updatedRectangle.Y += (int)(direction.Y * speed.Y * gameTime.ElapsedGameTime.Milliseconds);

            if (!collisionManager.CheckForCollision(updatedRectangle))
            {
                player.collisionBox.Rectangle = updatedRectangle;
                onFloor = false;
            }
            else
            {
                onFloor = true;
                IsJumping = false;
            }

            //player.drawingBox.DrawingRectangle = updatedRectangle;

        }
        */
        /*
        private Vector2 ApplyGravity(Vector2 direction)
        {
            //Debug.WriteLine("before: " + onFloor);
            Rectangle feetRectangle = new Rectangle(player.collisionBox.Rectangle.X,
                player.collisionBox.Rectangle.Y + player.collisionBox.Rectangle.Height,
                player.collisionBox.Rectangle.Width, 5);
            
            onFloor = collisionManager.CheckForCollision(feetRectangle);
           // Debug.WriteLine(onFloor);
            if (!onFloor && !IsJumping)
            {
                //Debug.WriteLine("hit if");
                direction.Y += 1;
            }

            return direction;
        }
        */
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
            if (!onFloor)
            {
                currentMovingState = MovementState.FALLING;
            }
            if (direction.X != 0)
            {
                currentMovingState = MovementState.MOVING;
            }
            else if (direction.X == 0 && onFloor)
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
