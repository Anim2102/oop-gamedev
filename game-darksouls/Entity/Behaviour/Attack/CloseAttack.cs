using game_darksouls;
using game_darksouls.Animation;
using game_darksouls.Component;
using game_darksouls.Entity;
using game_darksouls.Entity.Behaviour.Attack;
using game_darksouls.Enum;
using game_darksouls.Sound;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System.Diagnostics;

namespace Entity.Behaviour.Attack
{
    internal class CloseAttack : IAttack
    {
        private readonly IAnimationManager animationManager;
        private readonly Box collisionBox;
        private readonly ActionAnimation attackAnimation;
        private readonly CollisionManager collisionManager;
        private readonly AnimatedObject initiator;


        public AttackSquare AttackFrame { get; set; }

        public int AttackStartFrame { get; set; }
        public int AttackEndFrame { get; set; }
        private Rectangle collisionBoxRec => collisionBox.Rectangle;
        private int indexAnimationFrame => attackAnimation.Counter;

        private bool attackFinished = false;

        public bool IsAttackFinished
        {
            get
            {
                return attackFinished;
            }
        }

        public CloseAttack(AnimatedObject initiator, IAnimationManager animationManager, Box collisionBox, CollisionManager collisionManager, AttackSquare attackFrame)
        {
            this.collisionBox = collisionBox;
            this.collisionManager = collisionManager;
            this.animationManager = animationManager;
            attackAnimation = animationManager.ReturnAnimationOnState(MovementState.ATTACK);
            this.initiator = initiator;
            AttackFrame = attackFrame;
        }


        public bool PerformAttack()
        {
            AttackAnimation();
            bool hit = false;

            if (indexAnimationFrame >= AttackFrame.AttackStartFrame && indexAnimationFrame <= AttackFrame.AttackEndFrame)
            {
                SpawnAttackFrame();
                IEntity hittedObject = CheckHit();

                if (hittedObject != null)
                {
                    hit = true;
                    hittedObject.TakeDamage();
                }

                attackFinished = false;
            }

            if (indexAnimationFrame >= AttackFrame.AttackEndFrame)
            {
                attackAnimation.ResetAnimation();
                attackFinished = true;
            }

            return hit;
        }

        public void ResetAttack()
        {
            ResetAttackAnimation();
            RemoveAttackFrame();
            attackFinished = false;

        }
        private void SpawnAttackFrame()
        {
            if (animationManager.FacingLeft)
            {
                AttackFrame.ChangeFramePosition(new Vector2(collisionBox.CenterOfBox().X - AttackFrame.FrameWidth, collisionBoxRec.Y));
                //attackFrame = new Rectangle((int)collisionBox.CenterOfBox().X - attackFrame.Width, collisionBoxRec.Y, WidthAttackFrame, HeightAttackFrame);
            }
            else
            {
                AttackFrame.ChangeFramePosition(new Vector2(collisionBox.CenterOfBox().X,collisionBoxRec.Y));

                //attackFrame = new Rectangle((int)collisionBox.CenterOfBox().X, collisionBoxRec.Y, WidthAttackFrame, HeightAttackFrame);

            }
        }
        private IEntity CheckHit()
        {
            return collisionManager.CheckForHit(initiator, AttackFrame.ReturnAttackFrame());
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
            AttackFrame.RemovePosition();
        }
       
    }
}
