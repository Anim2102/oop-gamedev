using game_darksouls.Animation;
using game_darksouls.Entity;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class Attack
    {
        private readonly AnimationManager animationManager;
        private readonly Box collisionBox;
        private readonly ActionAnimation attackAnimation;

        public Rectangle attackFrame;
        public int WidthAttackFrame { get; set; }
        public int HeightAttackFrame { get; set; }

        public int AttackStartFrame { get; set; }
        public int AttackEndFrame { get; set; }
        private Rectangle collisionBoxRec => collisionBox.Rectangle;

        private int indexAnimationFrame => attackAnimation.Counter;

        public bool attackFinished { get; private set; } = false;

        public Attack(AnimationManager animationManager, Box collisionBox)
        {
            this.collisionBox = collisionBox;
            this.animationManager = animationManager;
            this.attackAnimation = animationManager.ReturnAnimationOnState(MovementState.ATTACK);
        }


        public bool AttackWithFrame(AnimatedObject animatedObject)
        {
            AttackAnimation();
            bool hit = false;
            if (indexAnimationFrame >= AttackStartFrame && indexAnimationFrame <= AttackEndFrame)
            {
                attackFrame = new Rectangle(collisionBoxRec.X, collisionBoxRec.Y, WidthAttackFrame, HeightAttackFrame);
                hit = CheckHit(animatedObject.collisionBox);
                attackFinished = false;
            }
            

            if (indexAnimationFrame >= AttackEndFrame)
            {
                this.attackAnimation.ResetAnimation();
                attackFinished = true;
            }
            if (attackFinished)
            {
                ResetAttackAnimation();
                RemoveAttackFrame();
                attackFinished = false;
            }

            return hit;
        }

        private bool CheckHit(Box collisionBoxTarget)
        {
            return attackFrame.Intersects(collisionBoxTarget.Rectangle);
        }

        public void ResetAttackAnimation()
        {
            animationManager.ResetAnimationOnState(MovementState.ATTACK);
        }
        public void RemoveAttackFrame()
        {
            attackFrame = Rectangle.Empty;
        }
        private void AttackAnimation()
        {
            animationManager.UpdateAnimationOnState(MovementState.ATTACK);
        }
    }
}
