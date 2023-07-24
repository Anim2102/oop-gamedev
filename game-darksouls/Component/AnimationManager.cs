using game_darksouls.Animation;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace game_darksouls.Component
{
    internal class AnimationManager : IComponent
    {
        //private Dictionary<MovementState, ActionAnimation> animations = new();

        public ActionAnimation currentAnimation { get; set; }
        public Dictionary<MovementState,ActionAnimation> animations { get; set; } = new();

        public AnimationManager(Dictionary<MovementState, ActionAnimation> animations)
        {

            this.animations = animations;
            //temp default animation
            currentAnimation = animations[MovementState.IDLE];
        }

    

        public ActionAnimation LoadAnimations(int fps = 15, int amountFrames = 1, int yas = 40, int width = 32, int height = 27)
        {
            ActionAnimation animation = new ActionAnimation();
            animation.fps = fps;
            int xAs = 0;
            int yAs = yas;


            for (int i = 0; i < amountFrames; i++)
            {
                animation.AddFrame(new AnimationFrame(new Rectangle(xAs, yAs, width, height)));
                xAs += width;
            }

            return animation;
        }

        public void AddAnimation(MovementState state, ActionAnimation actionAnimation)
        {
            animations.Add(state, actionAnimation);
        }

        public void UpdateAnimationOnState(MovementState state)
        {
            currentAnimation = animations[state];
        }

        public void Update(GameTime gameTime)
        {
            currentAnimation.Update(gameTime);
        }
    }
}
