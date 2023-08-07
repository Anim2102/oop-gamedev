using game_darksouls.Animation;
using game_darksouls.Entity;
using game_darksouls.Entity.EntityMovement;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class Attack
    {
        private readonly AnimationManager animationManager;
        private readonly AnimatedObject animatedObject;
        private readonly IMovementBehaviour npcMovementManager;
        private readonly ActionAnimation attackAnimation;
        private readonly Player player;
        
        private Rectangle attackFrame;
        private int attackStartFrame;
        private int attackEndFrame; 

        private Rectangle collisionBox => animatedObject.collisionBox.Rectangle;
        private int indexAnimationFrame => attackAnimation.Counter;

        public bool attackFinished { get; private set; } = false;

        public Attack(Rectangle attackFrame, 
            int attackStartFrame, 
            int attackEndFrame, 
            AnimationManager animationManager,
            AnimatedObject animatedObject,
            IMovementBehaviour npcMovementManager,
            Player player

            )
        {
            this.npcMovementManager = npcMovementManager;

            this.attackFrame = attackFrame;
            this.attackStartFrame = attackStartFrame;
            this.attackEndFrame = attackEndFrame;
            this.animationManager = animationManager;
            this.animatedObject = animatedObject;
            this.player = player;
            this.attackAnimation = animationManager.ReturnAnimationOnState(MovementState.ATTACK);

            this.attackStartFrame = 5;
            this.attackEndFrame = 10;

        }
       

        public void AttackWithFrame()
        {
            AttackAnimation();

            if (indexAnimationFrame >= attackStartFrame && indexAnimationFrame <= attackEndFrame)
            {
                this.attackFrame = new Rectangle(collisionBox.X, collisionBox.Y, 100, 100);
                CheckHit();
                attackFinished = false;
            }
            
            if (indexAnimationFrame > attackEndFrame)
            {
                attackFinished = true;
            }
        }
       
        private void CheckHit()
        {
            if (attackFrame.Intersects(player.collisionBox.Rectangle))
            {
                Debug.WriteLine("hit");
            }
        }
        public void RemoveAttackFrame()
        {
            this.attackFrame = Rectangle.Empty;
        }
        private void AttackAnimation()
        {
            animationManager.PlayAnimation(MovementState.ATTACK);
        }
     


    }
}
