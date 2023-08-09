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

        public Rectangle attackFrame;
        public int WidthAttackFrame { get; set; }
        public int HeightAttackFrame { get; set; }
        public Vector2 Offset { get; set; }

        public int AttackStartFrame { get; set; }
        public int AttackEndFrame { get; set; }
        private Rectangle collisionBoxRec => collisionBox.Rectangle;


        private int indexAnimationFrame => attackAnimation.Counter;

        public bool AttackFinished { get; private set; } = false;


        public Attack(AnimationManager animationManager, Box collisionBox, Vector2 offsetAttackFrame)
        {
            this.collisionBox = collisionBox;
            this.animationManager = animationManager;
            this.attackAnimation = animationManager.ReturnAnimationOnState(MovementState.ATTACK);
            Offset = offsetAttackFrame;
        }


        public bool AttackWithFrame(AnimatedObject targetObject)
        {
            AttackAnimation();
            bool hit = false;

            if (indexAnimationFrame >= AttackStartFrame && indexAnimationFrame <= AttackEndFrame)
            {
                SpawnAttackFrame();
                hit = CheckHit(targetObject.collisionBox);

                if (hit)
                {
                    targetObject.HealthManager.TakeDamage();
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
        private bool CheckHit(Box collisionBoxTarget)
        {
            return attackFrame.Intersects(collisionBoxTarget.Rectangle);
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
