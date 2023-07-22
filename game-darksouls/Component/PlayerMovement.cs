using game_darksouls.Entity;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class PlayerMovement : IComponent
    {
        private readonly Player player;
        private CollisionManager collisionManager;
        private readonly PlayerAnimation playerAnimation;
        private MovementState currentMovingState;

        public PlayerMovement(Player player, PlayerAnimation playerAnimation)
        {
            this.player = player;
            this.playerAnimation = playerAnimation;
            this.collisionManager = new(player);
            currentMovingState = MovementState.IDLE;
        }

        public void Update(GameTime gameTime)
        {
            var state = Keyboard.GetState();
            Vector2 direction = Vector2.Zero;

            if (state.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }
            if (state.IsKeyDown(Keys.Right))
            {
                direction.X = +1;
            }
            if (state.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            if (state.IsKeyDown(Keys.Down))
            {
                direction.Y = +1;
            }

            
            collisionManager.CheckForGravity();

            if (!collisionManager.IsOnFloor)
            {
                direction = ApplyGravity(direction);
            }


            ChangeMovingState(direction);
            Rectangle updatedRectangle = player.drawingBox.DrawingRectangle;
            updatedRectangle.X += (int)direction.X;
            updatedRectangle.Y += (int)direction.Y;
            player.drawingBox.DrawingRectangle = updatedRectangle;
        }

        private Vector2 ApplyGravity(Vector2 direction)
        {
            //change to gravity value
            direction.Y += 1f;
            return direction;
        }
        private void ChangeMovingState(Vector2 direction)
        {
            if (direction.Y > 0)
            {
                currentMovingState = MovementState.FALLING;
                playerAnimation.currentAnimation = playerAnimation.animations[MovementState.FALLING];
            }
            if (direction.X != 0)
            {
                currentMovingState = MovementState.MOVING;
                playerAnimation.currentAnimation = playerAnimation.animations[MovementState.MOVING];
            }
            else if (direction.X == 0 && direction.Y == 0)
            {
                currentMovingState = MovementState.IDLE;
                playerAnimation.currentAnimation = playerAnimation.animations[MovementState.IDLE];

            }
        }
    }
}
