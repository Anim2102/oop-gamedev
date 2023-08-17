using game_darksouls.Animation;
using game_darksouls.Entity;
using game_darksouls.Enum;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class CloseAttack
    {
        private readonly IAnimationManager animationManager;
        private readonly Box collisionBox;
        private readonly ActionAnimation attackAnimation;
        private readonly CollisionManager collisionManager;
        private readonly AnimatedObject initiator;

        public Rectangle attackFrame;
        public int WidthAttackFrame { get; set; }
        public int HeightAttackFrame { get; set; }

        public int AttackStartFrame { get; set; }
        public int AttackEndFrame { get; set; }
        private Rectangle collisionBoxRec => collisionBox.Rectangle;
        private int indexAnimationFrame => attackAnimation.Counter;

        public bool AttackFinished { get; private set; } = false;


        public CloseAttack(AnimatedObject initiator,IAnimationManager animationManager, Box collisionBox, CollisionManager collisionManager)
        {
            this.collisionBox = collisionBox;
            this.collisionManager = collisionManager;
            this.animationManager = animationManager;
            this.attackAnimation = animationManager.ReturnAnimationOnState(MovementState.ATTACK);
            this.initiator = initiator;
        }


        public bool AttackWithFrame()
        {
            AttackAnimation();
            Debug.WriteLine(AttackFinished);
            bool hit = false;

            if (indexAnimationFrame >= AttackStartFrame && indexAnimationFrame <= AttackEndFrame)
            {
                SpawnAttackFrame();
                IEntity hittedObject = CheckHit();

                if (hittedObject != null)
                {
                    hit = true;
                    hittedObject.HealthManager.TakeDamage();
                }

                AttackFinished = false;
            }
            
            if (indexAnimationFrame >= AttackEndFrame)
            {
                this.attackAnimation.ResetAnimation();
                AttackFinished = true;
            }
           
            return hit;
        }

        public void ResetAttack()
        {
            ResetAttackAnimation();
            RemoveAttackFrame();
            AttackFinished = false;
            
        }
        private void SpawnAttackFrame()
        {
            if (animationManager.FacingLeft)
            {
                attackFrame = new Rectangle((int)collisionBox.CenterOfBox().X - attackFrame.Width, collisionBoxRec.Y, WidthAttackFrame, HeightAttackFrame);
            }
            else
            {
                attackFrame = new Rectangle((int)collisionBox.CenterOfBox().X, collisionBoxRec.Y, WidthAttackFrame, HeightAttackFrame);

            }
        }
        private IEntity CheckHit()
        {
            return collisionManager.CheckForHit(initiator, attackFrame);
        }

        public void ResetAttackAnimation()
        {
            animationManager.ResetAnimationOnState(MovementState.ATTACK);
        }

        private void AttackAnimation()
        {
            animationManager.UpdateAnimationOnState(MovementState.ATTACK);
        }

        public void RemoveAttackFrame()
        {
            attackFrame = Rectangle.Empty;
        }
        

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.redsquareDebug, attackFrame, Color.White);
        }
    }
}
