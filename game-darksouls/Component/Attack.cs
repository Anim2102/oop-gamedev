using game_darksouls.Animation;
using game_darksouls.Entity;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.MediaFoundation;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class Attack
    {
        private readonly AnimationManager animationManager;
        private readonly Box collisionBox;
        private readonly ActionAnimation attackAnimation;
        private readonly CollisionManager collisionManager;

        public Rectangle attackFrame;
        public int WidthAttackFrame { get; set; }
        public int HeightAttackFrame { get; set; }
        public Vector2 Offset { get; set; }

        public int AttackStartFrame { get; set; }
        public int AttackEndFrame { get; set; }
        private Rectangle collisionBoxRec => collisionBox.Rectangle;


        private int indexAnimationFrame => attackAnimation.Counter;

        public bool AttackFinished { get; private set; } = false;


        public Attack(AnimationManager animationManager, Box collisionBox, Vector2 offsetAttackFrame, CollisionManager collisionManager)
        {
            this.collisionBox = collisionBox;
            this.collisionManager = collisionManager;
            this.animationManager = animationManager;
            this.attackAnimation = animationManager.ReturnAnimationOnState(MovementState.ATTACK);
            Offset = offsetAttackFrame;
        }


        public bool AttackWithFrame()
        {
            AttackAnimation();
            bool hit = false;

            if (indexAnimationFrame >= AttackStartFrame && indexAnimationFrame <= AttackEndFrame)
            {
                SpawnAttackFrame();
                AnimatedObject hittedObject = CheckHit();

                if (hittedObject != null)
                {
                    hittedObject.HealthManager.TakeDamage();
                }
                AttackFinished = false;
            }
            
            if (indexAnimationFrame >= AttackEndFrame)
            {
                this.attackAnimation.ResetAnimation();
                
                AttackFinished = true;
            }

            if (AttackFinished)
            {
                ResetAttackAnimation();
                RemoveAttackFrame();
                AttackFinished = false;
            }            
            return hit;
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
        private AnimatedObject CheckHit()
        {
            return collisionManager.CheckForHit(attackFrame);
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
