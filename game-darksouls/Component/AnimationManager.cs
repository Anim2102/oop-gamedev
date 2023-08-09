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

        public ActionAnimation currentAnimation { get; set; }
        public Dictionary<MovementState,ActionAnimation> animations { get; set; } = new();
        public bool FacingLeft { get; set; }
        public SpriteEffects SpriteFLip => FacingLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        

        public AnimationManager(Dictionary<MovementState, ActionAnimation> animations)
        {
            this.animations = animations;
            //temp default animation
            currentAnimation = animations[MovementState.ATTACK];
        }

        public void PlayAnimation(MovementState movementState)
        {
            if (animations.ContainsKey(movementState))
            {
                currentAnimation.ResetAnimation();
                currentAnimation = animations[movementState];                
            }
        }

        public void AddAnimation(MovementState state, ActionAnimation actionAnimation)
        {
            animations.Add(state, actionAnimation);
        }

        public void UpdateAnimationOnState(MovementState state)
        {
            if (animations.ContainsKey(state) && !currentAnimation.IsRunning)
            {
                //currentAnimation.ResetAnimation();
                currentAnimation = animations[state];
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
        public void Update(GameTime gameTime)
        {
            //Debug.WriteLine(currentAnimation.name);
            currentAnimation.Update(gameTime);
        }
    }
}
