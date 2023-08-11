using game_darksouls.Animation;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace game_darksouls.Component
{
    public class AnimationManager : IComponent
    {
        //private Dictionary<MovementState, ActionAnimation> animations = new();

        public ActionAnimation CurrentAnimation { get; set; }
        public ActionAnimation DeathAnimation { get; set; }
        public bool LockAnimation { get; set; }
        public Dictionary<MovementState,ActionAnimation> animations { get; set; } = new();
        public bool FacingLeft { get; set; }
        public SpriteEffects SpriteFLip => FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        

        public AnimationManager(Dictionary<MovementState, ActionAnimation> animations)
        {
            this.animations = animations;
            //temp default animation
            CurrentAnimation = animations[MovementState.IDLE];
            DeathAnimation = ReturnAnimationOnState(MovementState.DEATH);
           
        }

        public void PlayAnimation(MovementState movementState)
        {
            if (LockAnimation)
                return;

            if (animations.ContainsKey(movementState))
            {
                //currentAnimation.ResetAnimation();
                CurrentAnimation = animations[movementState];                
            }
        }

        public void AddAnimation(MovementState state, ActionAnimation actionAnimation)
        {
            animations.Add(state, actionAnimation);
        }

        public void UpdateAnimationOnState(MovementState state)
        {
            if (LockAnimation)
                return;

            if (animations.ContainsKey(state) && !CurrentAnimation.IsRunning)
            {
                //currentAnimation.ResetAnimation();
                CurrentAnimation = animations[state];
            }

        }
        public ActionAnimation ReturnAnimationOnState(MovementState state)
        {
            if (animations.ContainsKey(state))
                return animations.GetValueOrDefault(state);

            return null;
        }
        public void ResetAnimationOnState(MovementState state)
        {
            if (animations.ContainsKey(state))
            {
                ActionAnimation animation = animations.GetValueOrDefault(state);
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
            
            //Debug.WriteLine(currentAnimation.name);
            CurrentAnimation.Update(gameTime);
        }
    }
}
