using game_darksouls.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_darksouls.Animation.EntityAnimations
{
    public class SkeletonAnimationFactory : AnimationFactory
{
        public override Dictionary<MovementState, ActionAnimation> LoadAnimations()
        {
            Dictionary<MovementState, ActionAnimation> animations = new();
            ActionAnimation idleAnimation = LoadAnimations("idle", amountFrames: 4, fps: 4, yas: 192, width: 64, height: 64);
            ActionAnimation runningAnimation = LoadAnimations("running", amountFrames: 12, fps: 12, yas: 128, width: 64, height: 64);
            ActionAnimation attackAnimation = LoadAnimations("attack", amountFrames: 13, fps: 9, yas: 0, width: 64, height: 64);
            ActionAnimation dyingAnimation = LoadAnimations("attacl", amountFrames: 13, fps: 9, yas: 64, width: 64, height: 64);
            dyingAnimation.Loop = false;

            animations.Add(MovementState.DEATH, dyingAnimation);
            animations.Add(MovementState.IDLE, idleAnimation);
            animations.Add(MovementState.MOVING, runningAnimation);
            animations.Add(MovementState.ATTACK, attackAnimation);

            return animations;

        }
    }
}
