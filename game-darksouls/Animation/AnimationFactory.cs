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
    public static class AnimationFactory
{
        public static Dictionary<MovementState,ActionAnimation> LoadPlayerAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle",amountFrames: 4, fps: 8, yas: 0, width: 32, height: 32);
            ActionAnimation runningAnimation = LoadAnimations("running",amountFrames: 8, fps: 16, yas: 32, width: 32, height: 32);
            ActionAnimation fallingAnimation = LoadAnimations("falling",amountFrames: 3, fps: 2, yas: 192, width: 32, height: 32);
            ActionAnimation attackAnimation = LoadAnimations("attack", amountFrames: 5, fps: 8, yas: 64, width: 32, height: 32);
            ActionAnimation dyingAnimation = LoadAnimations("dying", amountFrames: 7, fps: 8, yas: 224, width: 32, height: 32);

            animations.Add(MovementState.DEATH, dyingAnimation);
            animations.Add(MovementState.FALLING, fallingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.MOVING, runningAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            return animations;
        }

        public static Dictionary<MovementState, ActionAnimation> LoadSkeletonAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle",amountFrames: 4, fps: 4, yas: 192, width: 64, height: 64);
            ActionAnimation runningAnimation = LoadAnimations("running",amountFrames: 12, fps: 12, yas: 128, width: 64, height: 64);
            ActionAnimation attackAnimation = LoadAnimations("attack",amountFrames: 13, fps: 9, yas: 0, width: 64, height: 64);
            ActionAnimation dyingAnimation = LoadAnimations("attacl",amountFrames: 13,fps:9,yas:64,width: 64, height: 64);
            dyingAnimation.Loop = false;

            animations.Add(MovementState.DEATH, dyingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.MOVING, runningAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            return animations;
        }
        public static Dictionary<MovementState, ActionAnimation> LoadBrainMobAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle", amountFrames: 4, fps: 4, yas: 64, width: 32, height: 32);
            ActionAnimation movingAnimation = LoadAnimations("moving", amountFrames: 4, fps: 4, yas: 0, width: 32, height: 32);
            ActionAnimation attackAnimation = LoadAnimations("attack", amountFrames: 4, fps: 4, yas: 32, width: 32, height: 32);
            ActionAnimation dyingAnimation = LoadAnimations("dying", amountFrames: 8, fps: 7, yas: 96,height:32,width: 32);
            dyingAnimation.Loop = false;

            animations.Add(MovementState.DEATH, dyingAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);
            animations.Add(MovementState.MOVING, movingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);

            return animations;
        }

        public static Dictionary<MovementState, ActionAnimation> LoadWizardAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle", amountFrames: 8, fps: 8, yas: 0, width: 160, height: 128);
            ActionAnimation attackAnimation = LoadAnimations("attack", amountFrames: 13, fps: 6, yas: 256, width: 160, height: 128);
            ActionAnimation dyingAnimation = LoadAnimations("dying", amountFrames: 10, fps: 9, yas: 768, width:160,height: 128) ;
            dyingAnimation.Loop = false;

            animations.Add(MovementState.DEATH, dyingAnimation) ;
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            return animations;
        }

        public static Dictionary<MovementState, ActionAnimation> LoadCrystalAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation staticAnimation = LoadAnimations("static",amountFrames: 24,fps: 12,yas: 0, width: 64,height: 64);

            animations.Add(MovementState.IDLE,staticAnimation);

            return animations;
        }
        public static ActionAnimation LoadAnimations(string name = "none",bool loop = true,int fps = 15, int amountFrames = 1, int yas = 40, int width = 32, int height = 27)
        {
            ActionAnimation animation = new ActionAnimation(name);
            animation.Loop = loop;
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
