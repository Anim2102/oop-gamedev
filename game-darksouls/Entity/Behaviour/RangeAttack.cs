using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Enum;
using game_darksouls.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_darksouls.Entity.Behaviour
{
    internal class RangeAttack : IBehave
    {
        private readonly Player player;
        private readonly AnimatedObject animatedObject;
        private readonly IAnimationManager animationManager;
        private readonly CollisionManager collisionManager;
        private FlyingObject flyingObject;

        private Vector2 currentPosition => animatedObject.CollisionBox.Position;
        private Vector2 playerPosition => player.CollisionBox.Position;

        public double RangeOfAttack { get; set; }

        private bool shootSpell = false;
        private bool projectFlying = false;


        public RangeAttack(Player player, AnimatedObject animatedObject, IAnimationManager animationManager, CollisionManager collisionManager, FlyingObject flyingObject)
        {
            this.player = player;
            this.animatedObject = animatedObject;
            this.animationManager = animationManager;
            this.collisionManager = collisionManager;
            this.flyingObject = flyingObject;
        }

        public void Behave(GameTime gameTime)
        {
            float distance = VectorHelpingClass.CalculateDistanceBetweenTwoVectorsOnX(playerPosition, currentPosition);

            if (distance <= RangeOfAttack)
            {
                animationManager.FacingLeft = PlayerOnLeft();
                animationManager.PlayAnimation(MovementState.ATTACK);

                if (animationManager.CurrentAnimation.Complete)
                {
                    shootSpell = true;
                    flyingObject.ResetPosition(currentPosition);
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
                if (collisionManager.CheckForCollision(flyingObject.Object))
                {
                    shootSpell = false;
                    flyingObject.RemoveObjectScreen();
                    flyingObject.ResetPosition(currentPosition);
                    return;
                }

                flyingObject.UpdatePositionObject(gameTime, this.player.CollisionBox.CenterOfBox() - currentPosition);
            }
            else
            {
                float distance = VectorHelpingClass.CalculateDistanceBetweenTwoVectorsOnX(playerPosition, currentPosition);
                if (animationManager.CurrentAnimation.Complete && distance <= RangeOfAttack)
                {
                    projectFlying = true;
                }
            }

            CheckHit();

        }

        private void CheckHit()
        {
            if (projectFlying)
            {
                IEntity entity = collisionManager.CheckForHit(animatedObject, flyingObject.Object);
                if (entity != null)
                {
                    entity.HealthManager.TakeDamage();
                }
            }
        }
        private bool PlayerOnLeft()
        {
            if (playerPosition.X > currentPosition.X)
                return false;
            else
                return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (shootSpell)
                flyingObject.Draw(spriteBatch);
        }





    }
}
