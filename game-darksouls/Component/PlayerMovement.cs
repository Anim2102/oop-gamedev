using game_darksouls.Entity;
using game_darksouls.Enum;
using game_darksouls.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
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

        private Vector2 direction;
        private Vector2 speed;
        private Vector2 velocity;

        private bool jumping;
        private bool onFloor;
        private const float MAXJUMP = 200f;
        private float currentJumpTime;

        public PlayerMovement(Player player, CollisionManager collisionManager, AnimationManager playerAnimation, InputManager inputManager)
        {
            this.player = player;
            this.collisionManager = collisionManager;
            this.playerAnimation = playerAnimation;
            this.inputManager = inputManager;

            this.direction = Vector2.Zero;
            this.onFloor = false;

            speed = new Vector2(0.3f, 0.4f);
            velocity = new Vector2(1, 1);
            jumping = false;
        }

        public void Update(GameTime gameTime)
        {

           
            CheckFloor();
            JumpPlayer(gameTime);
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
                futurePosition = currentPosition + direction * velocity * speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }

            MoveWithCollision(futurePosition);

        }
        private void MoveWithCollision(Vector2 futurePosition)
        {
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

                MoveWithCollision(futurePosition);
            }

        }

        private void JumpPlayer(GameTime gameTime)
        {
            //Debug.WriteLine(jumping);
            if (!jumping && inputManager.IsJumpButtonPress() && onFloor)
            {
                jumping = true;
                currentJumpTime = 0;
            }
            if (jumping)
            {
                currentJumpTime += (float)gameTime.ElapsedGameTime.Milliseconds;
                if (currentJumpTime < MAXJUMP)
                {
                    Vector2 futurePosition = this.player.collisionBox.Position;
                    futurePosition.Y -= 15f;
                    MoveWithCollision(futurePosition);
                }
                else
                {
                    jumping = false;
                }

            }
        }
        private void CheckFloor()
        {
            Rectangle feet = new Rectangle(this.player.collisionBox.Rectangle.X,
                this.player.collisionBox.Rectangle.Y + this.player.collisionBox.Rectangle.Height,
                this.player.collisionBox.Rectangle.Width,
                8);

            onFloor = collisionManager.CheckForCollision(feet);
        }
        /*
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle feet = new Rectangle(this.player.collisionBox.Rectangle.X,
                this.player.collisionBox.Rectangle.Y + this.player.collisionBox.Rectangle.Height - 10,
                this.player.collisionBox.Rectangle.Width,
                10);

            spriteBatch.Draw(Game1.redsquareDebug, feet, Color.Red);
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
            if (!onFloor || jumping)
            {
                currentMovingState = MovementState.FALLING;
            }
            if (direction.X != 0 && !jumping && onFloor)
            {
                currentMovingState = MovementState.MOVING;
            }
            else if (direction.X == 0 && onFloor)
            {
                currentMovingState = MovementState.IDLE;
            }

            playerAnimation.UpdateAnimationOnState(currentMovingState);
        }

    }
}
