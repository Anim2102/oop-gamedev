using game_darksouls.Animation;
using game_darksouls.Entity;
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
        private readonly NpcMovementManager npcMovementManager;
        private readonly ActionAnimation attackAnimation;
        
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
            NpcMovementManager npcMovementManager

            )
        {
            this.npcMovementManager = npcMovementManager;

            this.attackFrame = attackFrame;
            this.attackStartFrame = attackStartFrame;
            this.attackEndFrame = attackEndFrame;
            this.animationManager = animationManager;
            this.animatedObject = animatedObject;
            this.attackAnimation = animationManager.ReturnAnimationOnState(MovementState.ATTACK);

            this.attackStartFrame = 5;
            this.attackEndFrame = 10;

        }
       

        public void AttackWithFrame()
        {
            AttackAnimation();

            if (indexAnimationFrame >= attackStartFrame && indexAnimationFrame <= attackEndFrame)
            {
                this.attackFrame = new Rectangle(collisionBox.X, collisionBox.Y, 50, 50);
                attackFinished = false;
            }
            
            if (indexAnimationFrame > attackEndFrame)
            {
                attackFinished = true;
            }
            

        }

        private void AttackAnimation()
        {
            animationManager.PlayAnimation(MovementState.ATTACK);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Game1.redsquareDebug, attackFrame, Color.Black);
        }


    }
}
