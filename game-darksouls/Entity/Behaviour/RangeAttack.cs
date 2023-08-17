using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace game_darksouls.Entity.Behaviour
{
    internal class RangeAttack : IBehave
    {
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly IAnimationManager animationManager;
        private readonly IMovementBehaviour movementBehaviour;
        private readonly CollisionManager collisionManager;
        private Texture2D fireBallTexure;

        private Vector2 currentPosition => animatedObject.CollisionBox.Position;
        private Vector2 playerPosition => player.CollisionBox.Position;

        public double RangeOfAttack { get; set; }

        private Box spelBlock;
        private bool shootSpell = false;

        private bool projectFlying = false;


        public RangeAttack(Player player,
        AnimatedObject animatedObject,
        IAnimationManager animationManager,
        IMovementBehaviour movementBehaviour,
        CollisionManager collisionManager,
        Texture2D fireball)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.animationManager = animationManager;
            this.movementBehaviour = movementBehaviour;
            this.collisionManager = collisionManager;

            this.spelBlock = new();
            this.fireBallTexure = fireball;
        }

        public void Behave(GameTime gameTime)
        {

            if (ReturnDistanceBetweenPlayer() <= RangeOfAttack)
            {
                animationManager.FacingLeft = PlayerOnLeft();
                animationManager.PlayAnimation(MovementState.ATTACK);

                if (animationManager.CurrentAnimation.Complete)
                {
                    shootSpell = true;
                    ResetSpellBlock();
                }
            }
            else
            {
                animationManager.ResetAnimationOnState(MovementState.ATTACK);
                animationManager.PlayAnimation(MovementState.IDLE);
            }
        }

        public void UpdateSpell(GameTime gameTime)
        {

            if (!shootSpell)
                return;

            if (projectFlying)
            {
                if (collisionManager.CheckForCollision(spelBlock))
                {
                    shootSpell = false;
                    spelBlock = null;
                    ResetSpellBlock();
                    return;
                }
                MoveSpell(gameTime);
            }
            else
            {
                if (animationManager.CurrentAnimation.Complete && ReturnDistanceBetweenPlayer() <= RangeOfAttack)
                {
                    projectFlying = true;
                }
            }

        }
        private void MoveSpell(GameTime gameTime)
        {
            Vector2 direction = this.player.CollisionBox.CenterOfBox() - currentPosition;


            Vector2 normalizedDirection = Vector2.Normalize(direction);
            Rectangle updatedRectangle = spelBlock.Rectangle;

            updatedRectangle.X += (int)(normalizedDirection.X * 0.5f * gameTime.ElapsedGameTime.Milliseconds);
            updatedRectangle.Y += (int)(normalizedDirection.Y * 0.5f * gameTime.ElapsedGameTime.Milliseconds);
            spelBlock.Rectangle = updatedRectangle;
        }
        private void ResetSpellBlock()
        {

            spelBlock = new Box((int)currentPosition.X, (int)currentPosition.Y, 10, 10);

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (shootSpell)
                spriteBatch.Draw(fireBallTexure, spelBlock.Rectangle, Color.White);
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
            Vector2 currentPosition = new Vector2(animatedObject.CollisionBox.Rectangle.X,
                animatedObject.CollisionBox.Rectangle.Y);

            Vector2 playerPosition = player.CollisionBox.CenterOfBox();

            return CalculateDistanceBetweenTwoVectorsOnX(currentPosition, playerPosition);
        }


        private float CalculateDistanceBetweenTwoVectorsOnX(Vector2 a, Vector2 b)
        {
            return Math.Abs(a.X - b.X);
        }
    }
}
