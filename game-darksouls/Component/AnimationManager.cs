using game_darksouls.Animation;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Diagnostics;

namespace game_darksouls.Component
{
    internal class AnimationManager : IComponent
    {
        //private Dictionary<MovementState, ActionAnimation> animations = new();

        public ActionAnimation currentAnimation { get; set; }
        public Dictionary<MovementState,ActionAnimation> animations { get; set; } = new();
        private bool currentAnimationIsRunning = false;

        public AnimationManager(Dictionary<MovementState, ActionAnimation> animations)
        {
            this.animations = animations;
            //temp default animation
            currentAnimation = animations[MovementState.IDLE];
        }

        public void PlayAnimation(MovementState movementState)
        {
            if (animations.ContainsKey(movementState))
            {
                currentAnimation = animations[movementState];
                currentAnimation.Play();
            }
        }

        public void AddAnimation(MovementState state, ActionAnimation actionAnimation)
        {
            animations.Add(state, actionAnimation);
        }

        public void UpdateAnimationOnState(MovementState state)
        {
            if (animations.ContainsKey(state) && !currentAnimation.IsRunning)
                currentAnimation = animations[state];
        }

        public void Update(GameTime gameTime)
        {
            Debug.WriteLine(currentAnimation.name);
            currentAnimation.Update(gameTime);
        }
    }
}
