using game_darksouls.Component;
using game_darksouls.Enum;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Animation
{
    public class AnimationFactory
{
        public static Dictionary<MovementState,ActionAnimation> LoadPlayerAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle",amountFrames: 4, fps: 8, yas: 0, width: 32, height: 32);
            ActionAnimation runningAnimation = LoadAnimations("running",amountFrames: 8, fps: 16, yas: 32, width: 32, height: 32);
            ActionAnimation fallingAnimation = LoadAnimations("falling",amountFrames: 3, fps: 4, yas: 192, width: 32, height: 32);
            ActionAnimation attackAnimation = LoadAnimations("attack",amountFrames: 5, fps: 8,yas: 64, width: 32, height: 32, loop: false);

            animations.Add(MovementState.FALLING, fallingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.MOVING, runningAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            return animations;
        }

        public static Dictionary<MovementState, ActionAnimation> LoadSkeletonAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations(amountFrames: 4, fps: 4, yas: 206, width: 69, height: 64);
            ActionAnimation runningAnimation = LoadAnimations(amountFrames: 12, fps: 12, yas: 143, width: 69, height: 64);
            ActionAnimation attackAnimation = LoadAnimations(amountFrames: 13, fps: 9, yas: 0, width: 64, height: 64);
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.MOVING, runningAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            

            return animations;
        }

        private static ActionAnimation LoadAnimations(string name = "none",int fps = 15, int amountFrames = 1, int yas = 40, int width = 32, int height = 27, bool loop = true)
        {
            ActionAnimation animation = new ActionAnimation(name);
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
    }
}
