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
        private readonly PlayerAnimation playerAnimation;
        private MovementState currentMovingState;
        private InputManager inputManager;


        private bool IsFalling;
        private Vector2 speed;

        public PlayerMovement(Player player, PlayerAnimation playerAnimation)
        {

            //rework dependency injection either player or playeranimation or more specific
            this.player = player;
            this.playerAnimation = playerAnimation;
            this.collisionManager = new(player);
            this.inputManager = new();
            currentMovingState = MovementState.IDLE;
            speed = new Vector2(120, 200);
            IsFalling = false;
        }

        public void Update(GameTime gameTime)
        {
            var direction = inputManager.GetInput();


            /*collisionManager.CheckForGravity();

            if (!collisionManager.IsOnFloor)
            {
                direction = ApplyGravity(direction);
            }
            

            
            */
            direction = ApplyGravity(direction);
            MovePlayer(direction,gameTime);
            ChangeMovingState(direction);
        }


        private void MovePlayer(Vector2 direction, GameTime gameTime)
        {
            Rectangle updatedRectangle = player.drawingBox.DrawingRectangle;
            updatedRectangle.X += (int)(direction.X * speed.X * gameTime.ElapsedGameTime.TotalSeconds);
            updatedRectangle.Y += (int)(direction.Y * speed.Y * gameTime.ElapsedGameTime.TotalSeconds);
            player.drawingBox.DrawingRectangle = updatedRectangle;

        }
        private Vector2 ApplyGravity(Vector2 direction)
        {
            
            IsFalling = collisionManager.CheckForGravity(TempLevel.GetInstance().rectangles, player.drawingBox);

            if (!IsFalling)
            {
                direction.Y += 1;
            }

            return direction;
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
    }
}
