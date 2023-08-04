using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Entity.Behaviour
{
    internal class RangeAttack : IBehave
{
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly AnimationManager animationManager;
        private readonly IMovementBehaviour movementBehaviour;
        private readonly CollisionManager collisionManager;

        private Vector2 currentPosition => animatedObject.collisionBox.Position;
        private Vector2 playerPosition => player.drawingBox.Position;

        private const double AIMRANGE = 200;

        private Box spelBlock;
        private bool shootSpell = false;


        public RangeAttack(Player player, 
            AnimatedObject animatedObject, 
            AnimationManager animationManager, 
            IMovementBehaviour movementBehaviour,
            CollisionManager collisionManager)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.animationManager = animationManager;
            this.movementBehaviour = movementBehaviour;
            this.collisionManager = collisionManager;

            spelBlock = new Box((int)currentPosition.X, (int)currentPosition.Y, 10, 10);
        }

        public void Behave(GameTime gameTime)
        {
            if (ReturnDistanceBetweenPlayer() <= AIMRANGE)
            {
                animationManager.FacingLeft = PlayerOnLeft();
                animationManager.PlayAnimation(MovementState.ATTACK);

                if (animationManager.currentAnimation.Complete)
                    shootSpell = true;
            }
            else
                animationManager.PlayAnimation(MovementState.IDLE);
        }

        public void UpdateSpell(GameTime gameTime)
        {
            if (!shootSpell)
                return;

            if (collisionManager.CheckForCollision(spelBlock))
            {
                shootSpell = false;
                spelBlock= null;
                ResetSpellBlock();
                return;
            }
                

            Vector2 direction = playerPosition - currentPosition;

            
            Vector2 normalizedDirection = Vector2.Normalize(direction);
            Rectangle updatedRectangle = spelBlock.Rectangle;

            updatedRectangle.X += (int)(normalizedDirection.X * 0.5f * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(normalizedDirection.Y * 0.5f * gameTime.ElapsedGameTime.Milliseconds);
            spelBlock.Rectangle = updatedRectangle;
            spelBlock.Rectangle = updatedRectangle;

        }
        
        private void ResetSpellBlock()
        {
            spelBlock = new Box((int)currentPosition.X, (int)currentPosition.Y, 10, 10);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(Game1.redsquareDebug, spelBlock.Rectangle, Color.Green);
        }

        private bool PlayerOnLeft()
        {
            if (playerPosition.X > currentPosition.X)
                return false;
            else 
                return true;
        }


        private float ReturnDistanceBetweenPlayer()
        {
            Vector2 currentPosition = new Vector2(animatedObject.collisionBox.Rectangle.X,
                animatedObject.collisionBox.Rectangle.Y);

            Vector2 playerPosition = player.drawingBox.CenterOfBox();

            return CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);
        }


        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }
    }
}
