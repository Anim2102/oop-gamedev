using game_darksouls.Animation;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    public class AnimationManager : IAnimationManager, IDeathAnimation
    {
        public ActionAnimation CurrentAnimation { get; private set; }
        public ActionAnimation DeathAnimation { get; set; }
        public bool LockAnimation { get; set; }
        public Dictionary<MovementState, ActionAnimation> Animations { get; set; } = new();
        public bool FacingLeft { get; set; }
       
        public SpriteEffects SpriteFlip
        {
            get
            {
                return FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }
        }
        
        public bool DeathAnimationComplete
        {
            get
            {
                return DeathAnimation.Complete;
            }
        }

        public AnimationManager(Dictionary<MovementState, ActionAnimation> animations)
        {
            this.Animations = animations;
            CurrentAnimation = animations[MovementState.IDLE];
            DeathAnimation = ReturnAnimationOnState(MovementState.DEATH);

        }

        public void PlayAnimation(MovementState movementState)
        {
            if (LockAnimation)
                return;

            if (Animations.ContainsKey(movementState))
            {
                //currentAnimation.ResetAnimation();
                CurrentAnimation = Animations[movementState];
            }
        }

        public void AddAnimation(MovementState state, ActionAnimation actionAnimation)
        {
            Animations.Add(state, actionAnimation);
        }

        public void UpdateAnimationOnState(MovementState state)
        {
            if (LockAnimation)
                return;

            if (Animations.ContainsKey(state) && !CurrentAnimation.IsRunning)
            {
                //currentAnimation.ResetAnimation();
                CurrentAnimation = Animations[state];
            }

        }
        public ActionAnimation ReturnAnimationOnState(MovementState state)
        {
            if (Animations.ContainsKey(state))
                return Animations.GetValueOrDefault(state);

            return null;
        }
        public void ResetAnimationOnState(MovementState state)
        {
            if (Animations.ContainsKey(state))
            {
                ActionAnimation animation = Animations.GetValueOrDefault(state);
                animation.ResetAnimation();
            }
        }

        public void PlayDeathAnimation()
        {
            PlayAnimation(MovementState.DEATH);
            LockAnimation = true;
        }
        public void Update(GameTime gameTime)
        {
            CurrentAnimation.Update(gameTime);
        }
    }
}
