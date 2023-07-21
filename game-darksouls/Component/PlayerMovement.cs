using game_darksouls.Entity;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game_darksouls.Component
{
    internal class PlayerMovement : IComponent
    {
        private readonly Player player;
        private readonly PlayerAnimation playerAnimation;
        private MovementState currentMovingState;

        public PlayerMovement(Player player, PlayerAnimation playerAnimation)
        {
            this.player = player;
            this.playerAnimation = playerAnimation;
            currentMovingState = MovementState.IDLE;
        }

        private void ChangeMovingState(Vector2 direction)
        {
            if (direction.X != 0)
            {
                currentMovingState = MovementState.MOVING;
                playerAnimation.currentAnimation = playerAnimation.animations[MovementState.MOVING];
            }
            else
            {
                currentMovingState = MovementState.IDLE;
                playerAnimation.currentAnimation = playerAnimation.animations[MovementState.IDLE];

            }
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

            ChangeMovingState(direction);

            Rectangle updatedRectangle = player.drawingBox.DrawingRectangle;
            updatedRectangle.X += (int)direction.X;
            updatedRectangle.Y += (int)direction.Y;
            player.drawingBox.DrawingRectangle = updatedRectangle;
        }
    }
}
